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
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using AirTravel.API.Middleware;
using AirTravel.Application.Core;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;

namespace Tests.AirTravel.API.Middleware;

public class ExceptionMiddlewareTests
{
    #region Fields

    private readonly DefaultHttpContext _context;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _environment;

    #endregion


    #region Ctors

    public ExceptionMiddlewareTests()
    {
        _context = new DefaultHttpContext();
        _logger = A.Fake<ILogger<ExceptionMiddleware>>();
        _environment = new HostingEnvironment { EnvironmentName = Environments.Development };
    }

    #endregion


    #region Methods

    [Fact]
    public async Task InvokeAsync_NoException_ResponseIsPassedToNextDelegate()
    {
        // Arrange
        var nextDelegate = new RequestDelegate(context => Task.CompletedTask);
        var middleware = new ExceptionMiddleware(nextDelegate, _logger, _environment);

        // Act
        await middleware.InvokeAsync(_context);

        // Assert
        Assert.Null(_context.Response.ContentType); // No content type set
        Assert.Equal(StatusCodes.Status200OK, _context.Response.StatusCode); // Status code is OK
    }

    #endregion
}
