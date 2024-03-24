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

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AirTravel.API.Controllers;
using AirTravel.Application.Bookings;
using AirTravel.Application.Core;
using FakeItEasy;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Tests.AirTravel.API.Controllers
{
    public class BookingControllerTests
    {
        #region Fields

        private readonly IMediator _mediator;
        private readonly HttpContext _httpContext;
        private readonly List<BookingDto> _bookings;

        #endregion

        #region Ctors

        public BookingControllerTests()
        {
            _mediator = A.Fake<IMediator>();

            _httpContext = A.Fake<HttpContext>();
            A.CallTo(() => _httpContext.RequestServices.GetService(typeof(IMediator)))
                .Returns(_mediator);

            _bookings = new List<BookingDto>
            {
                new BookingDto { BookingId = 1 },
                new BookingDto { BookingId = 2 }
            };
        }

        #endregion

        #region Methods

        [Fact]
        public async Task GetBookings_WhenCalled_ReturnsOkResultWithBookings()
        {
            // Arrange
            var controller = new BookingController();
            A.CallTo(() => _mediator.Send(A<List.Query>.Ignored, A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(Result<List<BookingDto>>.Success(_bookings)));

            controller.ControllerContext.HttpContext = _httpContext;

            // Act
            var result = await controller.GetBookingsAsync(CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedBookings = Assert.IsAssignableFrom<IEnumerable<BookingDto>>(okResult.Value);
            Assert.Equal(_bookings, returnedBookings);
        }

        [Fact]
        public async Task GetBookings_ReturnsValidModel()
        {
            // Arrange
            var controller = new BookingController();
            controller.ControllerContext.HttpContext = _httpContext;
            A.CallTo(() => _mediator.Send(A<List.Query>.Ignored, A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(Result<List<BookingDto>>.Success(_bookings)));
            controller.ControllerContext.HttpContext = _httpContext;

            // Act
            var result = await controller.GetBookingsAsync(CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<List<BookingDto>>(okResult.Value);
            // Add additional assertions for model validation
        }

        #endregion
    }
}
