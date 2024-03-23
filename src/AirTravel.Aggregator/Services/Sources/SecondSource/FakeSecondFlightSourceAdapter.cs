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

namespace AirTravel.Aggregator.Services.Sources.SecondSource;

public class FakeSecondFlightSourceAdapter : IFlightDataAdapter
{
    private readonly IFakeSecondFlightSource source;

    public FakeSecondFlightSourceAdapter(IFakeSecondFlightSource source)
    {
        this.source = source;
    }

    public List<IFlightInfo> GetFlights(string from, string to, DateTime date)
    {
        // We retrieve data from data source 2 and transform it into a unified format.
        List<IFlightInfo> flightsFromSource = source.SearchFlights(from, to, date);
        return flightsFromSource.Cast<IFlightInfo>().ToList();
    }
}
