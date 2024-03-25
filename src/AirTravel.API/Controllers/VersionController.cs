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
using System.Reflection;
using System.Threading.Tasks;
using AirTravel.Application.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AirTravel.API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    public class VersionController : BaseApiController
    {
        #region Fields

        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<VersionController> _logger;

        #endregion


        #region Ctors

        public VersionController(IWebHostEnvironment environment, ILogger<VersionController> logger)
        {
            _environment = environment;
            _logger = logger;
        }

        #endregion


        #region Methods

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            return HandleResult(
                await Task.Run(() =>
                {
                    try
                    {
                        var execVersion = GetVersion();
                        _logger.LogInformation($"get version - {execVersion}");
                        var versionResult = new VersionResult
                        {
                            Environment = _environment.EnvironmentName,
                            Version = execVersion?.ToString()
                        };

                        return Result<VersionResult>.Success(versionResult);
                    }
                    catch
                    {
                        return Result<VersionResult>.Failure("Can't get version");
                    }
                })
            );
        }

        protected virtual Version GetVersion()
        {
            var execVersion = Assembly.GetExecutingAssembly().GetName().Version;
            return execVersion ?? throw new InvalidOperationException();
        }

        #endregion
    }
}
