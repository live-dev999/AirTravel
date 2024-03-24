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
using System.Threading;
using System.Threading.Tasks;
using AirTravel.Aggregator.Core;
using AirTravel.Aggregator.Services;
using AirTravel.Aggregator.Services.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AirTravel.Aggregator.Controllers
{
    public class TicketController : BaseApiController
    {
        private readonly IFlightAggregator _aggregator;

        public TicketController(IFlightAggregator aggregator, ILogger<TicketController> logger):base(logger)
        {
            this._aggregator = aggregator;
        }

        [HttpPost("Search")]
        public async Task<IActionResult> SearchTicketsAsync(CancellationToken ct) =>
            HandleResult(
                Result<List<IFlightInfo>>.Success(
                    await _aggregator.SearchFlightsAsync("", "", DateTime.Now)
                ),
                ct
            );

        [HttpPost("SetReservation")]
        public async Task<IActionResult> SetReservationAsync(
            [FromBody] FlightInfo tiket,
            CancellationToken ct
        ) => HandleResult(
                Result<IFlightInfo>.Success(
                    await _aggregator.SetReservationAsync(tiket, DateTime.Now)
                ),
                ct
            );
    }
}
