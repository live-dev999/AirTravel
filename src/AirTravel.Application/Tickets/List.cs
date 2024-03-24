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
using AirTravel.Application.Core;
using AirTravel.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AirTravel.Application.Tickets
{
    public class List
    {
        public class Query : IRequest<Result<List<TicketDto>>> { }

        public class Handler : IRequestHandler<Query, Result<List<TicketDto>>>
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

            public async Task<Result<List<TicketDto>>> Handle(
                Query request,
                CancellationToken cancellationToken
            )
            {
                var tickets = await _context.Tickets
                .ProjectTo<TicketDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
                
                return Result<List<TicketDto>>.Success(tickets);
            }
        }
    }
}