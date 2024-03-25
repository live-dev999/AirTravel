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
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AirTravel.Persistence
{
    internal class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            // Setup Booking
            builder.HasKey(b => b.BookingId);

            builder.Property(b => b.BookingId).UseSerialColumn();

            // builder.HasOne(b => b.Flight).WithMany().HasForeignKey(b => b.FlightId).IsRequired();

            builder
                .HasOne(b => b.Passenger)
                .WithMany()
                .HasForeignKey(b => b.PassengerId)
                .IsRequired();

            builder.Property(b => b.BookingTime).IsRequired();

            var converter = new ValueConverter<BookingStatus, string>(
                v => v.ToString(),
                v => (BookingStatus)Enum.Parse(typeof(BookingStatus), v)
            );

            builder.Property(s => s.Status).HasConversion(converter);
        }
    }
}
