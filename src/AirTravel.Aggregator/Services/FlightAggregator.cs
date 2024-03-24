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
using AirTravel.Aggregator.Services;
using AirTravel.Aggregator.Services.Sources.FirstSource;
using AirTravel.Aggregator.Services.Sources.SecondSource;

namespace AirTravel.Aggregator;

public class FlightAggregator : IFlightAggregator
{
    private readonly Dictionary<FlightInfoSource, IFlightDataAdapter> adapters;

    public FlightAggregator()
    {
        this.adapters = new Dictionary<FlightInfoSource, IFlightDataAdapter>()
        {
            {
                FlightInfoSource.FakeFirstFlightSource,
                new FakeFirstFlightSourceAdapter(new FakeFirstFlightSource())
            },
            {
                FlightInfoSource.FakeSecondFlightSource,
                new FakeSecondFlightSourceAdapter(new FakeSecondFlightSource())
            },
        };
    }

    public async Task<List<IFlightInfo>> SearchFlightsAsync(string from, string to, DateTime date)
    {
        await Task.Delay(300);

        List<IFlightInfo> allFlights = new List<IFlightInfo>();

        List<Task> tasks = new List<Task>();

        // Getting flight data from each adapter and aggregating them
        foreach (var adapter in adapters)
        {
            tasks.Add(
                Task.Run(async () =>
                {
                    allFlights.AddRange(await adapter.Value.GetFlightsAsync(from, to, date));
                })
            );
        }

        await Task.WhenAll(tasks);

        return allFlights;
    }

    public async Task<IFlightInfo> SetReservationAsync(IFlightInfo tiket, DateTime reservationDate)
    {
        var source = SelectAdapter(tiket);
        return await source.SetReservationAsync(tiket);
        //throw new NotImplementedException();
    }

    private IFlightDataAdapter SelectAdapter(IFlightInfo tiket)
    {
        return adapters.First(_=>_.Key==tiket.FlightInfoSource).Value;
    }
}
