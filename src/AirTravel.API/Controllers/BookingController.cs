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
using AirTravel.Application.Bookings;
using AirTravel.Application.Core;
using AirTravel.Domain;
using Microsoft.AspNetCore.Mvc;

namespace AirTravel.API.Controllers
{
    public class BookingController : BaseApiController
    {
        [HttpGet] //api/booking
        public async Task<IActionResult> GetBookingsAsync([FromQuery] PagingParams pagingParams ,CancellationToken ct) =>
            HandlePagedResult(await Mediator.Send(new List.Query(){Params = pagingParams}, ct));

        [HttpPost] //bookings param -  ticket_id & user_id
        public async Task<IActionResult> ReservationAsync([FromBody] Booking bookings)
        {
            return HandleResult(await Mediator.Send(new Create.Command { Booking = bookings }));
        }

        [HttpPut("{id}")] //api/bookings/{id}
        public Task<IActionResult> EditReservation(Guid id, [FromBody] Booking bookings)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")] //api/bookings/{id}
        public IActionResult Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
