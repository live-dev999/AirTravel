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

using System.Text.Json;
using AirTravel.Application.Core;

namespace Tests.AirTravel.Application.Core
{
    public class AppExceptionTests
    {
        [Fact]
        public void AppException_IsSerializable()
        {
            // Arrange
            var statusCode = 500;
            var message = "Test message";
            var details = "Test details";
            var originalException = new AppException(statusCode, message, details);
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            // Act
            var jsonString = JsonSerializer.Serialize(originalException, jsonOptions);
            AppException deserializedException = null;
            try
            {
                deserializedException = JsonSerializer.Deserialize<AppException>(jsonString, jsonOptions);
            }
            catch (JsonException)
            {
                // Serialization or deserialization failed
            }

            // Assert
            Assert.NotNull(deserializedException); // Deserialized object is not null
            Assert.Equal(statusCode, deserializedException.StatusCode); // Status code matches
            Assert.Equal(message, deserializedException.Message); // Message matches
            Assert.Equal(details, deserializedException.Details); // Details match
        }
    }
}