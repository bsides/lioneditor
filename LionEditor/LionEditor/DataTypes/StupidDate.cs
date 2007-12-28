/*
    Copyright 2007, Joe Davidson <joedavidson@gmail.com>

    This file is part of LionEditor.

    LionEditor is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    LionEditor is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with LionEditor.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;

namespace LionEditor
{
    /// <summary>
    /// Represents FFT's moronic 9-bit date structures
    /// </summary>
    public class StupidDate : IEquatable<StupidDate>
    {
        #region Fields

        public Zodiac Month;
        public int Day;
        private static List<StupidDate> stupidDateList;
        private static StupidDate[] dateDictionary;

        #endregion

        #region Properties

        /// <summary>
        /// Gets a list of all possible dates
        /// </summary>
        private static List<StupidDate> StupidDateList
        {
            get
            {
                if( stupidDateList == null )
                {
                    stupidDateList = new List<StupidDate>( DateDictionary );
                }

                return stupidDateList;
            }
        }

        /// <summary>
        /// Gets an array of all possible dates, indexed by a 9-bit value
        /// </summary>
        public static StupidDate[] DateDictionary
        {
            get
            {
                if( dateDictionary == null )
                {
                    SetupDateDictionary();
                }

                return dateDictionary;
            }
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Given an array of 9-bit dates squashed together, finds the date at offset
        /// </summary>
        public static StupidDate GetDateFromOffset( int offset, byte[] dates )
        {
            int index = 0;

            int start = (offset / 8) * 9;
            switch( offset % 8 )
            {
                case 0:
                    index = dates[start] + ((dates[start + 1] & 0x01) << 8);
                    break;
                case 1:
                    index = ((dates[start + 1] & 0xFE) >> 1) + ((dates[start + 2] & 0x03) << 7);
                    break;
                case 2:
                    index = ((dates[start + 2] & 0xFC) >> 2) + ((dates[start + 3] & 0x07) << 6);
                    break;
                case 3:
                    index = ((dates[start + 3] & 0xF8) >> 3) + ((dates[start + 4] & 0x0F) << 5);
                    break;
                case 4:
                    index = ((dates[start + 4] & 0xF0) >> 4) + ((dates[start + 5] & 0x1F) << 4);
                    break;
                case 5:
                    index = ((dates[start + 5] & 0xE0) >> 5) + ((dates[start + 6] & 0x3F) << 3);
                    break;
                case 6:
                    index = ((dates[start + 6] & 0xC0) >> 6) + ((dates[start + 7] & 0x7F) << 2);
                    break;
                case 7:
                    index = ((dates[start + 7] & 0x80) >> 7) + ((dates[start + 8] << 1));
                    break;
            }

            return DateDictionary[index];
        }

        /// <summary>
        /// Sets the appropriate 9 bits in dates for this date at offset
        /// </summary>
        public void SetDateAtOffset( int offset, byte[] dates )
        {
            int start = (offset / 8) * 9;
            int date = StupidDateList.IndexOf( this );
            switch( offset % 8 )
            {
                case 0:
                    dates[start] = (byte)(date & 0xFF);
                    dates[start + 1] = (byte)((dates[start + 1] & 0xFE) | (byte)((date & 0x100) >> 8));
                    break;
                case 1:
                    dates[start + 1] = (byte)((dates[start + 1] & 0x01) | (byte)((date & 0x7F) << 1));
                    dates[start + 2] = (byte)((dates[start + 2] & 0xFC) | (byte)((date & 0x180) >> 7));
                    break;
                case 2:
                    dates[start + 2] = (byte)((dates[start + 2] & 0x03) | (byte)((date & 0x3F) << 2));
                    dates[start + 3] = (byte)((dates[start + 3] & 0xF8) | (byte)((date & 0x1C0) >> 6));
                    break;
                case 3:
                    dates[start + 3] = (byte)((dates[start + 3] & 0x07) | (byte)((date & 0x1F) << 3));
                    dates[start + 4] = (byte)((dates[start + 4] & 0xF0) | (byte)((date & 0x1E0) >> 5));
                    break;
                case 4:
                    dates[start + 4] = (byte)((dates[start + 4] & 0x0F) | (byte)((date & 0x0F) << 4));
                    dates[start + 5] = (byte)((dates[start + 5] & 0xE0) | (byte)((date & 0x1F0) >> 4));
                    break;
                case 5:
                    dates[start + 5] = (byte)((dates[start + 5] & 0x1F) | (byte)((date & 0x07) << 5));
                    dates[start + 6] = (byte)((dates[start + 6] & 0xC0) | (byte)((date & 0x1F8) >> 3));
                    break;
                case 6:
                    dates[start + 6] = (byte)((dates[start + 6] & 0x3F) | (byte)((date & 0x03) << 6));
                    dates[start + 7] = (byte)((dates[start + 7] & 0x80) | (byte)((date & 0x1FC) >> 2));
                    break;
                case 7:
                    dates[start + 7] = (byte)((dates[start + 7] & 0x7F) | (byte)((date & 0x01) << 7));
                    dates[start + 8] = (byte)((date & 0x1FE) >> 1);
                    break;
            }
        }

        public override string ToString()
        {
            return string.Format( "{0} {1}", Day, Month );
        }

        public int ToInt()
        {
            return StupidDateList.IndexOf( this );
        }

        /// <summary>
        /// Converts a War of the Lions date into a Gregorian date
        /// </summary>
        /// <returns></returns>
        public DateTime ToNormalDate()
        {
            switch( Month )
            {
                case Zodiac.Aries:
                    return new DateTime( 1990, 3, 21 ).AddDays( Day - 1 );
                case Zodiac.Taurus:
                    return new DateTime( 1990, 4, 20 ).AddDays( Day - 1 );
                case Zodiac.Gemini:
                    return new DateTime( 1990, 5, 21 ).AddDays( Day - 1 );
                case Zodiac.Cancer:
                    return new DateTime( 1990, 6, 22 ).AddDays( Day - 1 );
                case Zodiac.Leo:
                    return new DateTime( 1990, 7, 23 ).AddDays( Day - 1 );
                case Zodiac.Virgo:
                    return new DateTime( 1990, 8, 23 ).AddDays( Day - 1 );
                case Zodiac.Libra:
                    return new DateTime( 1990, 9, 23 ).AddDays( Day - 1 );
                case Zodiac.Scorpio:
                    return new DateTime( 1990, 10, 24 ).AddDays( Day - 1 );
                case Zodiac.Sagittarius:
                    return new DateTime( 1990, 11, 23 ).AddDays( Day - 1 );
                case Zodiac.Capricorn:
                    return new DateTime( 1990, 12, 23 ).AddDays( Day - 1 );
                case Zodiac.Aquarius:
                    return new DateTime( 1990, 1, 20 ).AddDays( Day - 1 );
                case Zodiac.Pisces:
                    return new DateTime( 1990, 2, 19 ).AddDays( Day - 1 );
                default:
                    // Something terrible happened
                    return DateTime.Now;
            }
        }

        /// <summary>
        /// Creates a StupidDate from a Gregorian date
        /// </summary>
        public static StupidDate FromNormalDate(int month, int day)
        {
            int dayOfYear = new DateTime(1990, month, day).DayOfYear;
            if (dayOfYear <= 19)
            {
                return new StupidDate(dayOfYear+9, Zodiac.Capricorn);
            }
            else if ((dayOfYear >= 20) && (dayOfYear <= 49))
            {
                return new StupidDate(dayOfYear - 19, Zodiac.Aquarius);
            }
            else if ((dayOfYear >= 50) && (dayOfYear <= 79))
            {
                return new StupidDate(dayOfYear - 49, Zodiac.Pisces);
            }
            else if ((dayOfYear >= 80) && (dayOfYear <= 109))
            {
                return new StupidDate(dayOfYear - 79, Zodiac.Aries);
            }
            else if ((dayOfYear >= 110) && (dayOfYear <= 140))
            {
                return new StupidDate(dayOfYear - 109, Zodiac.Taurus);
            }
            else if ((dayOfYear >= 140) && (dayOfYear <= 171))
            {
                return new StupidDate(dayOfYear - 139, Zodiac.Gemini);
            }
            else if ((dayOfYear >= 172) && (dayOfYear <= 202))
            {
                return new StupidDate(dayOfYear - 171, Zodiac.Cancer);
            }
            else if ((dayOfYear >= 203) && (dayOfYear <= 233))
            {
                return new StupidDate(dayOfYear - 202, Zodiac.Leo);
            }
            else if ((dayOfYear >= 234) && (dayOfYear <= 264))
            {
                return new StupidDate(dayOfYear - 233, Zodiac.Virgo);
            }
            else if ((dayOfYear >= 265) && (dayOfYear <= 295))
            {
                return new StupidDate(dayOfYear - 264, Zodiac.Libra);
            }
            else if ((dayOfYear >= 296) && (dayOfYear <= 325))
            {
                return new StupidDate(dayOfYear - 295, Zodiac.Scorpio);
            }
            else if ((dayOfYear >= 326) && (dayOfYear <= 355))
            {
                return new StupidDate(dayOfYear - 325, Zodiac.Sagittarius);
            }
            else if (dayOfYear >= 356)
            {
                return new StupidDate(dayOfYear - 355, Zodiac.Capricorn);
            }
            else
            {
                return FromNormalDate(1, 1);
            }
        }

        /// <summary>
        /// Builds the date dictionary
        /// </summary>
        private static void SetupDateDictionary()
        {
            dateDictionary = new StupidDate[512];

            dateDictionary[0] = new StupidDate( 10, Zodiac.Capricorn );

            int i;
            for( i = 2; i <= 19; i++ )
            {
                dateDictionary[i] = new StupidDate( i + 9, Zodiac.Capricorn );
            }

            for( i = 20; i <= 31; i++ )
            {
                dateDictionary[i] = new StupidDate( i - 19, Zodiac.Aquarius );
            }

            dateDictionary[64] = new StupidDate( 13, Zodiac.Aquarius );

            for( i = 66; i <= 82; i++ )
            {
                dateDictionary[i] = new StupidDate( i - 52, Zodiac.Aquarius );
            }

            for( i = 83; i <= 95; i++ )
            {
                dateDictionary[i] = new StupidDate( i - 82, Zodiac.Pisces );
            }

            for( i = 100; i <= 116; i++ )
            {
                dateDictionary[i] = new StupidDate( i - 86, Zodiac.Pisces );
            }

            for( i = 117; i <= 128; i++ )
            {
                dateDictionary[i] = new StupidDate( i - 116, Zodiac.Aries );
            }

            for( i = 130; i <= 147; i++ )
            {
                dateDictionary[i] = new StupidDate( i - 117, Zodiac.Aries );
            }

            for( i = 148; i <= 159; i++ )
            {
                dateDictionary[i] = new StupidDate( i - 147, Zodiac.Taurus );
            }

            for( i = 162; i <= 180; i++ )
            {
                dateDictionary[i] = new StupidDate( i - 149, Zodiac.Taurus );
            }

            for( i = 181; i <= 192; i++ )
            {
                dateDictionary[i] = new StupidDate( i - 180, Zodiac.Gemini );
            }

            for( i = 194; i <= 213; i++ )
            {
                dateDictionary[i] = new StupidDate( i - 181, Zodiac.Gemini );
            }

            for( i = 214; i <= 223; i++ )
            {
                dateDictionary[i] = new StupidDate( i - 213, Zodiac.Cancer );
            }

            for( i = 226; i <= 246; i++ )
            {
                dateDictionary[i] = new StupidDate( i - 215, Zodiac.Cancer );
            }

            for( i = 247; i <= 256; i++ )
            {
                dateDictionary[i] = new StupidDate( i - 246, Zodiac.Leo );
            }

            for( i = 258; i <= 278; i++ )
            {
                dateDictionary[i] = new StupidDate( i - 247, Zodiac.Leo );
            }

            for( i = 279; i <= 288; i++ )
            {
                dateDictionary[i] = new StupidDate( i - 278, Zodiac.Virgo );
            }

            for( i = 290; i <= 310; i++ )
            {
                dateDictionary[i] = new StupidDate( i - 279, Zodiac.Virgo );
            }

            for( i = 311; i <= 319; i++ )
            {
                dateDictionary[i] = new StupidDate( i - 310, Zodiac.Libra );
            }

            for( i = 322; i <= 343; i++ )
            {
                dateDictionary[i] = new StupidDate( i - 312, Zodiac.Libra );
            }

            for( i = 344; i <= 352; i++ )
            {
                dateDictionary[i] = new StupidDate( i - 343, Zodiac.Scorpio );
            }

            for( i = 354; i <= 374; i++ )
            {
                dateDictionary[i] = new StupidDate( i - 344, Zodiac.Scorpio );
            }

            for( i = 375; i <= 383; i++ )
            {
                dateDictionary[i] = new StupidDate( i - 374, Zodiac.Sagittarius );
            }

            for( i = 386; i <= 406; i++ )
            {
                dateDictionary[i] = new StupidDate( i - 376, Zodiac.Sagittarius );
            }

            for( i = 407; i <= 415; i++ )
            {
                dateDictionary[i] = new StupidDate( i - 406, Zodiac.Capricorn );
            }

            dateDictionary[1] = dateDictionary[0];
            dateDictionary[32] = dateDictionary[0];
            dateDictionary[65] = dateDictionary[64];
            dateDictionary[96] = dateDictionary[93];
            dateDictionary[97] = dateDictionary[93];
            dateDictionary[98] = dateDictionary[94];
            dateDictionary[99] = dateDictionary[95];
            dateDictionary[129] = dateDictionary[128];
            dateDictionary[160] = dateDictionary[159];
            dateDictionary[161] = dateDictionary[159];
            dateDictionary[193] = dateDictionary[192];
            dateDictionary[224] = dateDictionary[223];
            dateDictionary[225] = dateDictionary[223];
            dateDictionary[257] = dateDictionary[256];
            dateDictionary[289] = dateDictionary[288];
            dateDictionary[320] = dateDictionary[319];
            dateDictionary[321] = dateDictionary[319];
            dateDictionary[353] = dateDictionary[352];
            dateDictionary[384] = dateDictionary[383];
            dateDictionary[385] = dateDictionary[383];
            dateDictionary[416] = dateDictionary[0];
            dateDictionary[448] = dateDictionary[0];
            dateDictionary[480] = dateDictionary[0];

            for( i = 0; i < 31; i++ )
            {
                dateDictionary[i + 33] = dateDictionary[i + 1];
                dateDictionary[i + 417] = dateDictionary[i + 1];
                dateDictionary[i + 449] = dateDictionary[i + 1];
                dateDictionary[i + 481] = dateDictionary[i + 1];
            }
        }

        public bool Equals( StupidDate other )
        {
            return ((this.Day == other.Day) && (this.Month == other.Month));
        }

        #endregion

        public StupidDate( int day, Zodiac month )
        {
            Month = month;
            Day = day;
        }
    }
}
