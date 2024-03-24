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

namespace AirTravel.Persistence
{
    public class DataContext : DbContext
    {
        #region Props

        public DbSet<Reservation> Reseravations { get; set; }

        #endregion


        #region Ctors

        public DataContext(DbContextOptions options)
            : base(options) { }

        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        //     var interceptor = new DbConnectionInterceptorDerivative();

        //     optionsBuilder.AddInterceptors(interceptor);

            optionsBuilder.UseNpgsql("Host=192.168.0.97;Port=5432;Database=airtravel;Username=postgres;Password=NewPassw0rd");
        }
    }
}
