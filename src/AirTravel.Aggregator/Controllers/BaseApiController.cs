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

using AirTravel.Aggregator.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AirTravel.Aggregator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
        private readonly ILogger<BaseApiController> _logger;

        #region Ctors
        public BaseApiController(ILogger<BaseApiController> logger)
        {
            this._logger = logger;
        }
        #endregion


        #region Methods

        public IActionResult HandleResult<T>(
            Result<T> result,
            System.Threading.CancellationToken ct
        )
        {
            if (result == null)
                return NotFound();

            if (result.IsSeccess && result.Value != null)
                return Ok(result.Value);

            if (result.IsSeccess && result.Value == null)
                return NotFound();

            return BadRequest(result.Error);
        }

        #endregion
    }
}
