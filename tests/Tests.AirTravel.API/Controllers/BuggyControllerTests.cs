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
using AirTravel.API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Tests.AirTravel.API.Controllers
{
    public class BuggyControllerTests
    {
        [Fact]
        public void GetNotFound_ReturnsNotFoundResult()
        {
            // Arrange
            var controller = new BuggyController();

            // Act
            var result = controller.GetNotFound();

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetBadRequest_ReturnsBadRequestResultWithErrorMessage()
        {
            // Arrange
            var controller = new BuggyController();
            var expectedErrorMessage = "This is a bad request";

            // Act
            var result = controller.GetBadRequest();

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = Assert.IsAssignableFrom<BadRequestObjectResult>(result);
            Assert.Equal(expectedErrorMessage, badRequestResult.Value);
        }

        [Fact]
        public void GetServerError_ThrowsException()
        {
            // Arrange
            var controller = new BuggyController();

            // Act & Assert
            Assert.Throws<Exception>(() => controller.GetServerError());
        }

        [Fact]
        public void GetUnauthorised_ReturnsUnauthorisedResult()
        {
            // Arrange
            var controller = new BuggyController();

            // Act
            var result = controller.GetUnauthorised();

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }
    }
}
