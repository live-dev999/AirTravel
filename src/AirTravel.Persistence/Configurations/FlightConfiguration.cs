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

using AirTravel.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirTravel.Persistence
{
    internal class FlightConfiguration : IEntityTypeConfiguration<Flight>
    {
        public void Configure(EntityTypeBuilder<Flight> builder)
        {
            // Setup Flight
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Id).UseSerialColumn();

            builder.Property(f => f.From).IsRequired();

            builder.Property(f => f.To).IsRequired();

            builder.Property(f => f.DepartureTime).IsRequired();

            builder.Property(f => f.ArrivalTime).IsRequired();

            // builder.Property(f => f.Price).IsRequired();
        }
    }
}
