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

using System.Runtime.CompilerServices;
using AirTravel.Application.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("Tests.AirTravel.API")]

namespace AirTravel.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public  abstract class BaseApiController : ControllerBase
{
    #region Fields

    private IMediator _mediator;

    #endregion


    #region Props

    public IMediator Mediator =>
        _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    #endregion


    #region Methods

    public IActionResult HandleResult<T>(Result<T> result)
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
