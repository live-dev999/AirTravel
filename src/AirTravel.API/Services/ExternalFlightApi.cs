/*
 *   Copyright (c) 2024 Dzianis Prokharchyk

 *   This program is free software: you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation, either version 3 of the License, or
 *   (at your option) any later version.

 *   This program is distributed in the hope that it will be useful,
 *   but WITHOUT ANY WARRANTY; without even the implied warranty of
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *   GNU General Public License for more details.

 *   You should have received a copy of the GNU General Public License
 *   along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AirTravel.Config;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AirTravel.API.Services
{
    public class ExternalFlightApi : IExternalFlightApi
    {
        #region Fields

        private readonly ILogger<ExternalFlightApi> _logger;
        private readonly UrlsConfig _config;
        private HttpClient _httpClient;
        private readonly Dictionary<string, (List<ExternalFlightData>, DateTime)> _cache;

        #endregion

        #region Ctors

        public ExternalFlightApi(
            HttpClient httpClient,
            ILogger<ExternalFlightApi> logger,
            UrlsConfig config
        )
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this._config = config ?? throw new ArgumentNullException(nameof(config));

            _cache = new Dictionary<string, (List<ExternalFlightData>, DateTime)>();
        }

        #endregion
        public async Task<List<ExternalFlightData>> GetFlights(
            string from,
            string to,
            DateTime date
        )
        {
            string cacheKey = $"{from}-{to}-{date:yyyyMMdd}";

            if (_cache.TryGetValue(cacheKey, out (List<ExternalFlightData>, DateTime) cachedEntry))
            {
                var (cachedFlights, expiryTime) = cachedEntry;
                if (expiryTime >= DateTime.Now)
                {
                    return cachedFlights;
                }
                else
                {
                    _cache.Remove(cacheKey);
                }
            }

            _logger.LogInformation("Called is: ExternalFlightApi - GetFlights");
            var ct = new CancellationToken();

            var response = await _httpClient.PostAsync($"api/Ticket/Search", null);
            _logger.LogInformation(
                $"Called is: ExternalFlightApi - GetFlights  = {response.StatusCode}"
            );

            // _logger.LogInformation(await response.Content.ReadAsStringAsync());

            var result = await response.Content.ReadFromJsonAsync<List<ExternalFlightData>>(ct);

            _cache[cacheKey] = (result, DateTime.Now.AddMonths(10));

            return result;
        }

        public async Task<ExternalFlightData> SetupReservation(
            ExternalFlightData externalFlightData
        )
        {
            // TODO: update this line
            var ct = new CancellationToken();
            var json = JsonConvert.SerializeObject(externalFlightData
             ,
              new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });


            var response = await _httpClient.PostAsync(
                $"api/Ticket/SetReservation",
                new StringContent(json, Encoding.UTF8, "application/json")
            );

            _logger.LogInformation(
                $"Called is: ExternalFlightApi - SetupReservation  = {response.StatusCode}"
            );

            var result = await response.Content.ReadFromJsonAsync<ExternalFlightData>(ct);

            // TODO: reset cache
            _cache.Clear();

            return result;
        }
    }
}
