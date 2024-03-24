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
using System.Threading.Tasks;
using AirTravel.Aggregator.Services.Models;

namespace AirTravel.Aggregator.Services.Sources.SecondSource;

public class FakeSecondFlightSource : IFakeSecondFlightSource
{
    #region Fields
    private readonly List<IFlightInfo> _flightInfos = new List<IFlightInfo>
    {
        new FlightInfo()
        {
            FlightId = Guid.NewGuid().ToString(),
            FlightNumber = Utils.KeyGenerator.Generate(9),
            ArrivalAirport = "Minsk",
            DepartureAirport = "Gomel",
            DepartureTime = DateTime.UtcNow.AddMonths(1),
            ArrivalTime = DateTime.UtcNow.AddMonths(1).AddDays(1)
        },
        new FlightInfo()
        {
            FlightId = Guid.NewGuid().ToString(),
            FlightNumber = Utils.KeyGenerator.Generate(5),
            ArrivalAirport = "New Orleans",
            DepartureAirport = "Washington",
            DepartureTime = DateTime.UtcNow.AddMonths(2),
            ArrivalTime = DateTime.UtcNow.AddMonths(1).AddDays(2)
        },
    };
    #endregion
    
    
    #region Ctors
    public FakeSecondFlightSource() { }

    #endregion


    /*
    Note: To handle unpredictably long responses from sources in the system, you should implement
    timeout mechanisms and exception handling.
    Here are a few steps you can take:

    Provide the ability to correctly process unpredictably long responses from sources.
    1) Setting timeouts for requests: Set a maximum waiting time for responses from each data source.
    If the waiting time expires, the request should be considered unsuccessful, and appropriate handling
    should be performed.
    2) Asynchronous requests: Execute requests to data sources asynchronously to avoid blocking the main
    execution thread. This allows parallel access to multiple data sources and improves overall system
    performance.
    3) Handling timeouts: When a timeout occurs for a request to a data source, the system should take
    appropriate actions, such as retrying the request or returning partially received information if
    available.
    4) Logging: It's important to log all requests and exceptions related to unpredictably long responses
    from data sources. This helps in performance analysis and identifying potential issues.
    5) Monitoring and notifications: Implement performance and availability monitoring for data sources.
    If any data source frequently produces unpredictably long responses, the system should send notifications
    for timely response.
    */
    public async Task<List<IFlightInfo>> SearchFlightsAsync(string from, string to, DateTime date)
    {
        return await Task.FromResult(
            _flightInfos
        );
        // throw new NotImplementedException();
    }
}
