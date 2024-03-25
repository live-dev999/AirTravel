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
using AirTravel.Domain;

namespace AirTravel.API.Services
{
    internal static class Mappings
    {
        public static Flight ToFlight(this ExternalFlightData source)
        {
            return source != null
                ? new Flight
                {
                    Id = new Random().Next(10000),
                    ExternalId = source.FlightId,
                    FlightNumber = source.FlightNumber,
                    From = source.DepartureAirport,
                    To = source.ArrivalAirport,
                    DepartureTime = source.DepartureTime,
                    ArrivalTime = source.ArrivalTime,
                    Status = source.Status
                }
                : null;
        }

        public static ExternalFlightData ToFlightData(this Flight source)
        {
            return source != null
                ? new ExternalFlightData
                {
                    FlightId = source.ExternalId,
                    FlightNumber = source.FlightNumber,
                    DepartureAirport = source.From,
                    ArrivalAirport = source.To,
                    DepartureTime = source.DepartureTime,
                    ArrivalTime = source.ArrivalTime,
                    Status = source.Status
                }
                : null;
        }

        public static IReadOnlyCollection<Flight>
            ToFlight(this IReadOnlyCollection<ExternalFlightData> entities) =>
            entities.MapCollection(ToFlight);
    }
}
