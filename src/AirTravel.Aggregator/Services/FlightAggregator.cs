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

using AirTravel.Aggregator.Services;

namespace AirTravel.Aggregator;

public class FlightAggregator : IFlightAggregator
{
    private readonly List<IFlightDataAdapter> adapters;

    public FlightAggregator(List<IFlightDataAdapter> adapters)
    {
        this.adapters = adapters;
    }

    public List<IFlightInfo> SearchFlights(string from, string to, DateTime date)
    {
        List<IFlightInfo> allFlights = new List<IFlightInfo>();

        // Getting flight data from each adapter and aggregating them
        foreach (var adapter in adapters)
        {
            allFlights.AddRange(adapter.GetFlights(from, to, date));
        }

        return allFlights;
    }
}
