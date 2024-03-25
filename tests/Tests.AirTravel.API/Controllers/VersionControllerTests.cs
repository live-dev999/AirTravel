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

using FakeItEasy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Tests.AirTravel.API.Stub;

namespace Tests.AirTravel.API.Controllers;

public class VersionControllerTests
{
    #region Fields

    private readonly ILogger<StubVersionController> _logger;
    private readonly IWebHostEnvironment _webHostEnvironment;
    
    #endregion


    #region

    public VersionControllerTests()
    {
        _logger = A.Fake<ILogger<StubVersionController>>();
        _webHostEnvironment = A.Fake<IWebHostEnvironment>();
    }

    #endregion


    #region Methods

    [Fact]
    public async void VersionController_GetVersion_Test()
    {
        var stub = new StubVersionController(_webHostEnvironment, _logger)
        {
            VersionString = "1.0.0.0"
        };

        var result = await stub.IndexAsync();

        // Assert
        Assert.NotNull(result);
    }

    #endregion
}
