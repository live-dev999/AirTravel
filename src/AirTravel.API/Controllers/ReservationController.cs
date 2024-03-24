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
using AirTravel.Domain;
using Microsoft.AspNetCore.Mvc;

namespace AirTravel.API.Controllers;

public class ReservationController : BaseApiController
{
    [HttpPost] //reservation param -  ticket_id & user_id
    public IActionResult Reservation()
    {
        throw new NotImplementedException();
    }

    [HttpPut("{id}")] //api/reservation/{id}
    public Task<IActionResult> EditReservation(Guid id, [FromBody] Reseravation reservation)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id}")] //api/reservation/{id}
    public IActionResult Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}
