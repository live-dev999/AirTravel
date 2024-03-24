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
using System.Threading.Tasks;
using AirTravel.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace AirTravel.API.Controllers
{
    public class FlightsController : BaseApiController
    {
        private readonly IFlightAggregator _flightAggregator;

        public FlightsController(IFlightAggregator flightAggregator)
        {
            _flightAggregator = flightAggregator;
        }

        [HttpGet]
        public async Task<IActionResult> GetFlights(string from, string to, DateTime date)
        {
            try
            {
                var flights = await _flightAggregator.GetFlights(from, to, date);
                return Ok(flights);
            }
            catch (Exception ex)
            {
                // Логируем ошибку
                Console.WriteLine($"Error in FlightsController: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
