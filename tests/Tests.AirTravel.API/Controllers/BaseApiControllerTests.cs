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

using AirTravel.API.Controllers;
using AirTravel.Application.Core;
using FakeItEasy;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Tests.AirTravel.API.Controllers
{
    public class BaseApiControllerStub : BaseApiController { }

    public class BaseApiControllerTests
    {
        #region Fields

        private readonly IMediator _mediator;
        private readonly HttpContext _httpContext;
        
        #endregion


        #region Ctors

        public BaseApiControllerTests()
        {
            _mediator = A.Fake<IMediator>();
            _httpContext = A.Fake<HttpContext>();
            A.CallTo(() => _httpContext.RequestServices.GetService(typeof(IMediator)))
                .Returns(_mediator);
        }

        #endregion
        
        
        #region  Methods

        [Fact]
        public void Mediator_Property_Returns_Valid_Instance()
        {
            // Arrange
            var controller = new BaseApiControllerStub();
            controller.ControllerContext.HttpContext = _httpContext;

            // Act
            var mediator = controller.Mediator;

            // Assert
            Assert.NotNull(mediator);
            Assert.IsAssignableFrom<IMediator>(mediator);
        }

        [Fact]
        public void HandleResult_Returns_OkObjectResult_With_Valid_Result()
        {
            // Arrange
            var controller = new BaseApiControllerStub();
            var validResult = Result<int>.Success(42);

            // Act
            var result = controller.HandleResult(validResult);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.Equal(42, okResult.Value);
        }

        [Fact]
        public void HandleResult_Returns_NotFoundResult_When_Result_Is_Null()
        {
            // Arrange
            var controller = new BaseApiControllerStub();
            controller.ControllerContext.HttpContext = _httpContext;

            // Act
            var result = controller.HandleResult<int>(null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void HandleResult_Returns_NotFoundResult_When_Result_Is_Success_But_Value_Is_Null()
        {
            // Arrange
            var controller = new BaseApiControllerStub();
            var nullValueResult = Result<int?>.Success(null);

            // Act
            var result = controller.HandleResult(nullValueResult);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void HandleResult_Returns_BadRequestResult_When_Result_Is_Not_Success()
        {
            // Arrange
            var controller = new BaseApiControllerStub();
            var errorResult = Result<int>.Failure("Test error message");

            // Act
            var result = controller.HandleResult(errorResult);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.Equal("Test error message", badRequestResult.Value);
        }

        #endregion
    }
}
