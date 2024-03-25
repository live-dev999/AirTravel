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
using System.Linq;
using System.Threading.Tasks;
using AirTravel.Domain;
using Microsoft.Extensions.Logging;

namespace AirTravel.API.Services
{
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
                _logger.LogError($"Error in FlightAggregator: {ex.Message}");
                throw;
            }
        }

        public async Task<Flight> SetupReservation(Flight flight)
        {
            var exData = flight.ToFlightData();
            var res = await _externalFlightApi.SetupReservation(exData);
            return res.ToFlight();
        }

        private List<Flight> ConvertToUnifiedFormat(List<ExternalFlightData> flightsData)
        {
            try
            {
                // We convert data from the external API format into a unified format
                return flightsData.ToFlight().ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in ConvertToUnifiedFormat: {ex.Message}");
                throw;
            }
        }
    }
}
