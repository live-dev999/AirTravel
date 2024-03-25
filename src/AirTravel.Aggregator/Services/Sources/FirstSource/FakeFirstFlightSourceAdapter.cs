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

namespace AirTravel.Aggregator.Services.Sources.FirstSource;

public class FakeFirstFlightSourceAdapter : IFlightDataAdapter
{
    private readonly IFakeFirstFlightSource source;

    public FakeFirstFlightSourceAdapter(IFakeFirstFlightSource source)
    {
        this.source = source;
    }

    public async Task<List<IFlightInfo>> GetFlightsAsync(string from, string to, DateTime date)
    {
        // We retrieve data from data source 1 and transform it into a unified format.
        List<IFlightInfo> flightsFromSource = await source.SearchFlightsAsync(from, to, date);
        return flightsFromSource.Cast<IFlightInfo>().ToList();
    }

    public async Task<IFlightInfo> SetReservationAsync(IFlightInfo tiket)
    {
        return await source.SetReservationAsync(tiket);
    }
}
