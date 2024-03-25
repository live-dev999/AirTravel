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
    internal class PassengerConfiguration : IEntityTypeConfiguration<Passenger>
    {
        public void Configure(EntityTypeBuilder<Passenger> builder)
        {
            // Setup Passenger
            builder.HasKey(p => p.PassengerId);

            builder.Property(p => p.PassengerId).UseSerialColumn();

            builder.Property(p => p.FirstName).IsRequired();

            builder.Property(p => p.LastName).IsRequired();

            builder.Property(p => p.Email).IsRequired();
        }
    }
}
