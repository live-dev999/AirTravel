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
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Tests.AirTravel.API.Controllers
{
    public class ReservationControllerTests
    {
        #region Fields

        private readonly IMediator _mediator;
        private readonly HttpContext _httpContext;

        #endregion

        #region Ctors

        public ReservationControllerTests()
        {
            _mediator = A.Fake<IMediator>();

            _httpContext = A.Fake<HttpContext>();
            A.CallTo(() => _httpContext.RequestServices.GetService(typeof(IMediator)))
                .Returns(_mediator);
        }

        #endregion
    }
}
