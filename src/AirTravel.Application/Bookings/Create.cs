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
using System.Threading;
using System.Threading.Tasks;
using AirTravel.Application.Core;
using AirTravel.Domain;
using AirTravel.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.VisualBasic;

namespace AirTravel.Application.Bookings
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Booking Booking { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Booking).SetValidator(new BookingValidator());
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


                // var attendee = new BookingAttendee
                // {
                //     AppUser = user,
                //     Booking = request.Booking,
                //     IsHost = true
                // };
                var newFlight = new Flight()
                {
                    From = "Start",
                    To = "End",
                    DepartureTime = DateTime.Now.AddMonths(1).SetKindUtc(),
                    ArrivalTime = DateTime.Now.AddMonths(1).AddHours(4).SetKindUtc(),
                };
                var flightEntity = _context.Add(newFlight);
                // var result1 = await _context.SaveChangesAsync() > 0;
                // if (!result1)
                //     return Result<Unit>.Failure("Failed to create Booking");
                // request.Booking.Attendees.Add(attendee);
                var newPassanger = request.Booking.Passenger;
                
                var passenger = new Passenger
                {
                    LastName = newPassanger.LastName,
                    FirstName = newPassanger.FirstName,
                    Email = newPassanger.Email
                };

                var passEntity = _context.Add(passenger);
                // var result2 = await _context.SaveChangesAsync() > 0;
                
                // if (!result2)
                //     return Result<Unit>.Failure("Failed to create Booking");
                
                request.Booking.Flight = flightEntity.Entity;
                request.Booking.Passenger = passEntity.Entity;
                request.Booking.BookingTime = DateTime.Now.SetKindUtc();

                _context.Add(request.Booking);

                var result = await _context.SaveChangesAsync() > 0;
                if (!result)
                    return Result<Unit>.Failure("Failed to create Booking");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
