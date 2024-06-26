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
using AirTravel.Aggregator.Services.Models;

namespace AirTravel.Aggregator.Services;

public interface IFlightInfo
{
    string FlightId { get; set; }
    string FlightNumber { get; set; }
    string DepartureAirport { get; set; }
    string ArrivalAirport { get; set; }
    DateTime DepartureTime { get; set; }
    DateTime ArrivalTime { get; set; }
    FlightInfoSource FlightInfoSource { get; set; }
    Status Status { get; set; }
}
