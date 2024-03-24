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

namespace AirTravel.Aggregator.Utils
{
    public static class KeyGenerator
    {
        // private const string ValidChars = "abcdefghjkmnprstwxz2345789"; // Letters and numbers that are not easily mixed with others when reading
    private const string ValidChars = "ABCDEFHJKLMNPRSTUWXYZ012345789";
    private static readonly Dictionary<long, bool> ValidCharLookup = new Dictionary<long, bool>();
    private static readonly Random Rnd = new Random();

    static KeyGenerator()
    {
        // Set up a quick lookup dictionary for all valid characters
        foreach (var c in ValidChars.ToUpperInvariant())
            ValidCharLookup.Add(c, true);
    }

    public static string Generate(int length)
    {
        var ret = new char[length];
        for (var i = 0; i < length; i++)
        {
            int c;
            lock (Rnd)
            {
                c = Rnd.Next(0, ValidChars.Length);
            }

            ret[i] = ValidChars[c];
        }

        return new string(ret);
    }

    public static bool Validate(int maxLength, string key)
    {
        if (key.Length > maxLength)
            return false;

        foreach (var c in key.ToUpperInvariant())
            if (!ValidCharLookup.ContainsKey(c))
                return false;
        return true;
    }
    }
}