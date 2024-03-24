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

using System.Threading;
using System.Threading.Tasks;
using AirTravel.Application.Core;
using AirTravel.Domain;
using AirTravel.Persistence;
using FluentValidation;
using MediatR;

namespace AirTravel.Application.Reservations
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Reservation Reservation { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Reservation).SetValidator(new ReservationValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;

            //private readonly IUserAccessor _userAccessor;

            public Handler(
                DataContext context
            //, IUserAccessor userAccessor
            )
            {
                //_userAccessor = userAccessor;
                _context = context;
            }

            public async Task<Result<Unit>> Handle(
                Command request,
                CancellationToken cancellationToken
            )
            {
                //var user = _context.Users.FirstOrDefault(x => x.UserName == _userAccessor.GetUsername());


                // var attendee = new ReservationAttendee
                // {
                //     AppUser = user,
                //     Reservation = request.Reservation,
                //     IsHost = true
                // };

                // request.Reservation.Attendees.Add(attendee);

                _context.Add(request.Reservation);

                var result = await _context.SaveChangesAsync() > 0;
                if (!result)
                    return Result<Unit>.Failure("Failed to create Reservation");
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
