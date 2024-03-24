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
using AirTravel.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AirTravel.Persistence
{
    public class DataContext : DbContext
    {
        #region Props

        public DbSet<Booking> Tickets { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Flight> Flights { get; set; }
        #endregion


        #region Ctors

        public DataContext(DbContextOptions options)
            : base(options) { }

        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                "Host=192.168.0.97;Port=5432;Database=airtravel;Username=postgres;Password=NewPassw0rd"
            );

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);

            // Setup Flight
            modelBuilder.Entity<Flight>().HasKey(f => f.FlightId);

            modelBuilder.Entity<Flight>().Property(f => f.From).IsRequired();

            modelBuilder.Entity<Flight>().Property(f => f.To).IsRequired();

            modelBuilder.Entity<Flight>().Property(f => f.DepartureTime).IsRequired();

            modelBuilder.Entity<Flight>().Property(f => f.ArrivalTime).IsRequired();

            modelBuilder.Entity<Flight>().Property(f => f.Price).IsRequired();

            // Setup Passenger
            modelBuilder.Entity<Passenger>().HasKey(p => p.PassengerId);

            modelBuilder.Entity<Passenger>().Property(p => p.FirstName).IsRequired();

            modelBuilder.Entity<Passenger>().Property(p => p.LastName).IsRequired();

            modelBuilder.Entity<Passenger>().Property(p => p.Email).IsRequired();

            // Setup Booking
            modelBuilder.Entity<Booking>().HasKey(b => b.BookingId);

            modelBuilder
                .Entity<Booking>()
                .HasOne(b => b.Flight)
                .WithMany()
                .HasForeignKey(b => b.FlightId)
                .IsRequired();

            modelBuilder
                .Entity<Booking>()
                .HasOne(b => b.Passenger)
                .WithMany()
                .HasForeignKey(b => b.PassengerId)
                .IsRequired();

            modelBuilder.Entity<Booking>().Property(b => b.BookingTime).IsRequired();

            var converter = new ValueConverter<BookingStatus, string>(
                v => v.ToString(),
                v => (BookingStatus)Enum.Parse(typeof(BookingStatus), v)
            );

            modelBuilder.Entity<Booking>().Property(s => s.Status).HasConversion(converter);
        }
    }
}
