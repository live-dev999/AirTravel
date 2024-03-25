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
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace AirTravel.API.Extensions
{
    public static class HttpExtensions
    {
        public static void AddPaginationHeader(
            this HttpResponse response,
            int currentPage,
            int itemsPerPage,
            int totalItems,
            int totalPages
        )
        {
            var paginationHeader = new
            {
                currentPage,
                itemsPerPage,
                totalItems,
                totalPages
            };
            // Response.Headers.Add("X-XSS-Protection", new StringValues("1; mode=block"));
            response.Headers.Append("Pagination", new StringValues(JsonSerializer.Serialize(paginationHeader)));
            response.Headers.Append("Access-Control-Expose-Headers",  new StringValues("Pagination"));
        }
    }
}
