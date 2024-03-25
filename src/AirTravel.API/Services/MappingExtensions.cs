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
using System.Collections.ObjectModel;

namespace AirTravel.API.Services
{
    internal static class MappingExtensions
    {
        public static IReadOnlyCollection<TOut> MapCollection<TIn, TOut>(
            this IReadOnlyCollection<TIn> input,
            Func<TIn, TOut> mapper
        )
        {
            if (mapper is null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            if (input is null || input.Count == 0)
            {
                return Array.Empty<TOut>();
            }

            var output = new TOut[input.Count];
            var i = 0;
            foreach (var entity in input)
            {
                output[i] = mapper(entity);
                i++;
            }

            return new ReadOnlyCollection<TOut>(output);
        }
    }
}
