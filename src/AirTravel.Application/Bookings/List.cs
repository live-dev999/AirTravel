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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AirTravel.Application.Core;
using AirTravel.Persistence;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AirTravel.Application.Bookings
{
    public class List
    {
        public class Query : IRequest<Result<PagedList<BookingDto>>>
        {
            public PagingParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<PagedList<BookingDto>>>
        {
            private readonly DataContext _context;
            private readonly ILogger _logger;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper, ILogger<Handler> logger)
            {
                _mapper = mapper;
                _logger = logger;
                _context = context;
            }

            public async Task<Result<PagedList<BookingDto>>> Handle(
                Query request,
                CancellationToken cancellationToken
            )
            {
                var query = _context
                    .Bookings.AsNoTracking()
                    .OrderByDescending(_ => _.BookingTime)
                    .ProjectTo<BookingDto>(_mapper.ConfigurationProvider)
                    .AsQueryable();

                return Result<PagedList<BookingDto>>.Success(
                    await PagedList<BookingDto>.CreateAsync(
                        query,
                        request.Params.PageNumber,
                        request.Params.PageSize
                    )
                );
            }
        }
    }
}
