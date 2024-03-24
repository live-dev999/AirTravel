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
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AirTravel.Application.Core;
using AirTravel.Config;
using AirTravel.Domain;
using Microsoft.Extensions.Logging;

namespace AirTravel.API.Services
{
    public class ExternalFlightData
    {
        public string FlightNumber { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
    }

    public interface IExternalFlightApi
    {
        Task<List<ExternalFlightData>> GetFlights(string from, string to, DateTime date);
    }

    public static class HttpContentExtensions
    {
        public static async Task<T> ReadAsAsync<T>(
            this HttpContent content,
            CancellationToken ct
        ) => await JsonSerializer.DeserializeAsync<T>(await content.ReadAsStreamAsync(ct));
    }

    public class ExternalFlightApi : IExternalFlightApi
    {
        #region Fields

        private readonly ILogger<ExternalFlightApi> _logger;
        private readonly UrlsConfig _config;
        private HttpClient _httpClient;

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
        }

        #endregion
        public async Task<List<ExternalFlightData>> GetFlights(
            string from,
            string to,
            DateTime date
        )
        {
            _logger.LogInformation("Called is: ExternalFlightApi - GetFlights");
            var ct = new CancellationToken();

            var response = await _httpClient.PostAsync($"api/Ticket/Search", null);
            _logger.LogInformation(
                $"Called is: ExternalFlightApi - GetFlights  = {response.StatusCode}"
            );
            var result = await response.Content.ReadAsAsync<List<ExternalFlightData>>(ct);

            return result;
        }
    }

    public interface IFlightAggregator
    {
        Task<List<Flight>> GetFlights(string from, string to, DateTime date);
    }

    public class FlightAggregator : IFlightAggregator
    {
        private readonly IExternalFlightApi _externalFlightApi;
        private readonly ILogger<FlightAggregator> _logger;

        public FlightAggregator(
            IExternalFlightApi externalFlightApi,
            ILogger<FlightAggregator> logger
        )
        {
            this._externalFlightApi = externalFlightApi;
            this._logger = logger;
        }

        public async Task<List<Flight>> GetFlights(string from, string to, DateTime date)
        {
            try
            {
                _logger.LogInformation("Called is: GetFlights");
                var flightsData = await _externalFlightApi.GetFlights(from, to, date);
                if (flightsData == null)
                {
                    throw new Exception("External API returned null data.");
                }

                List<Flight> flights = ConvertToUnifiedFormat(flightsData);
                return flights;
            }
            catch (Exception ex)
            {
                // Логируем ошибку
                Console.WriteLine($"Error in FlightAggregator: {ex.Message}");
                throw; // Пробрасываем исключение дальше
            }
        }

        private List<Flight> ConvertToUnifiedFormat(List<ExternalFlightData> flightsData)
        {
            try
            {
                // Преобразовываем данные из формата внешнего API в унифицированный формат
                List<Flight> flights = new List<Flight>();
                foreach (var flightData in flightsData)
                {
                    // Реализация маппинга данных
                    Flight flight = new Flight
                    {
                        // Преобразование данных
                    };
                    flights.Add(flight);
                }
                return flights;
            }
            catch (Exception ex)
            {
                // Логируем ошибку
                Console.WriteLine($"Error in ConvertToUnifiedFormat: {ex.Message}");
                throw; // Пробрасываем исключение дальше
            }
        }
    }
}
