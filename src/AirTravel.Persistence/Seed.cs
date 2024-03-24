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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirTravel.Domain;

namespace AirTravel.Persistence
{
    public class Seed
    {
        // public static async Task SeedData(DataContext context)
        // {
            // if (context.Bookings.Any())
            //     return;

            // var flights = new List<Flight>{
            //     new Flight(){
            //         FlightId = 1,
            //         From = "Minsk",
            //         To = "Moscow"
            //     },
            //     new Flight(){
            //         FlightId = 2,
            //         From = "Minsk",
            //         To = "Moscow"
            //     },
            //     new Flight(){
            //         FlightId = 3,
            //         From = "Minsk",
            //         To = "Moscow"
            //     },
            //     new Flight(){
            //         FlightId = 4,
            //         From = "Minsk",
            //         To = "Moscow"
            //     },
            //     new Flight(){
            //         FlightId = 5,
            //         From = "Minsk",
            //         To = "Moscow"
            //     }
            // };
            // await context.Flights.AddRangeAsync(flights);

            // var passengers = new List<Passenger>{
            //  new Passenger{
            //     PassengerId = 1,
            //     FirstName = "DZIANIS",
            //     LastName = "PROKHARHCYK",
            //     Email = "test@test.com"
            //  },
            //  new Passenger{
            //     PassengerId = 2,
            //     FirstName = "VASYA",
            //     LastName = "PUPKIN",
            //     Email = "test@test.com"
            //  }
            // };
            // await context.Passengers.AddRangeAsync(passengers);

            // var bookings = new List<Booking> { 
            //     new Booking{
            //         BookingId = 1,
            //         FlightId = 1,
            //         PassengerId = 1,
            //         BookingTime = DateTime.UtcNow.AddDays(4),
            //         Status = BookingStatus.Paid

            //     },
            //     new Booking{
            //         BookingId = 2,
            //         FlightId = 2,
            //         PassengerId = 1,
            //         BookingTime = DateTime.UtcNow.AddDays(-3),
            //         Status = BookingStatus.Paid
            //     },
            //     new Booking{
            //         BookingId = 3,
            //         FlightId = 3,
            //         PassengerId = 2,
            //         BookingTime = DateTime.UtcNow.AddDays(-2),
            //         Status = BookingStatus.Paid
            //     },
            //     new Booking{
            //         BookingId = 4,
            //         FlightId = 4,
            //         PassengerId = 2,
            //         BookingTime = DateTime.UtcNow.AddDays(-1),
            //         Status = BookingStatus.Paid
            //     },
            //      new Booking{
            //         BookingId = 5,
            //         FlightId = 5,
            //         PassengerId = 2,
            //         BookingTime = DateTime.UtcNow,
            //         Status = BookingStatus.Paid
            //     }
            // };

            // await context.Bookings.AddRangeAsync(bookings);
            // await context.SaveChangesAsync();
        // }
    }
}
