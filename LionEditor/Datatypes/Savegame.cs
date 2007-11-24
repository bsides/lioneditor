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
using System.Text;

namespace LionEditor
{
    public class Savegame
    {
        public const uint saveFileSize = 0x2A3C;

        byte[] offset0x00 = new byte[257];
        byte[] saveNameRaw = new byte[15];

        public string SaveName
        {
            get { return Character.DecodeName( saveNameRaw, 0 ); }
            set { Character.EncodeName( value, saveNameRaw, 0 ); }
        }

        byte[] offset0x110 = new byte[4];
        private SaveScreenMonths m_saveScreenMonth;

        public SaveScreenMonths SaveScreenMonth
        {
        	get { return m_saveScreenMonth; }
        	set { m_saveScreenMonth = value; }
        }

        private byte m_saveScreenDay;
        public byte SaveScreenDay
        {
        	get { return m_saveScreenDay; }
        	set { m_saveScreenDay = value; }
        }

        private byte m_saveScreenMapPosition;
        public byte SaveScreenMapPosition
        {
        	get { return m_saveScreenMapPosition; }
        	set { m_saveScreenMapPosition = value; }
        }

        byte[] offset0x117 = new byte[17];
        private uint m_timer;
        public uint Timer
        {
        	get { return m_timer; }
        	set { m_timer = value; }
        }

        byte[] artifactDates = new byte[53];
        byte[] wondersDates = new byte[17];
        byte[] featsDates = new byte[108];
        byte[] offset0x1DE = new byte[610];
        byte numPropositions;
        byte[] prop1 = new byte[9];
        byte[] prop2 = new byte[9];
        byte[] prop3 = new byte[9];
        byte[] prop4 = new byte[9];
        byte[] prop5 = new byte[9];
        byte[] prop6 = new byte[9];
        byte[] prop7 = new byte[9];
        byte[] prop8 = new byte[9];
        byte[] offset0x489 = new byte[3];
        private Character[] m_characters = new Character[24];
        public Character[] Characters
        {
        	get { return m_characters; }
        	set { m_characters = value; }
        }

        private Character[] m_guests = new Character[4];
        public Character[] Guests
        {
        	get { return m_guests; }
        	set { m_guests = value; }
        }

        byte[] inventory = new byte[316];
        byte[] furshop = new byte[316];
        byte[] offset0x2304 = new byte[304];
        private uint m_warFunds;
        public uint WarFunds
        {
        	get { return m_warFunds; }
        	set { m_warFunds = value; }
        }

        byte[] offset0x2438 = new byte[4];
        private uint m_month;
        public uint Month
        {
        	get { return m_month; }
        	set { m_month = value; }
        }

        private uint m_day;
        public uint Day
        {
        	get { return m_day; }
        	set { m_day = value; }
        }

        byte[] offset0x2444 = new byte[4];
        private byte m_mapPosition;
        public byte MapPosition
        {
        	get { return m_mapPosition; }
        	set { m_mapPosition = value; }
        }

        byte[] offset0x2449 = new byte[191];
        private uint m_kills;
        public uint Kills
        {
        	get { return m_kills; }
        	set { m_kills = value; }
        }

        private uint m_casualties;
        public uint Casualties
        {
        	get { return m_casualties; }
        	set { m_casualties = value; }
        }

        byte[] offset0x2510 = new byte[200];
        byte[] artefacts = new byte[6];
        byte[] wonders = new byte[2];
        byte[] feats = new byte[48];
        byte[] offset0x2610 = new byte[372];
        byte[] options = new byte[4];
        byte[] offset0x2788 = new byte[692];

        public enum SaveScreenMonths
        {
            Aries = 0x01,
            Taurus = 0x02,
            Gemini = 0x03,
            Cancer = 0x04,
            Leo = 0x05,
            Virgo = 0x06,
            Libra = 0x07,
            Scorpio = 0x08,
            Sagittarius = 0x09,
            Capricorn = 0x0A,
            Aquarius = 0x0B,
            Pisces = 0x0C,
            Level = 0x0D,
            Empty = 0x0E,
            Unusable = 0x0F,
        }
        
        public Savegame( byte[] file )
        {
            CopyArray( file, offset0x00, 0, 257 );
            CopyArray( file, saveNameRaw, 0x101, 15 );

            CopyArray( file, offset0x110, 0x110, 4 );
            SaveScreenMonth = (SaveScreenMonths)file[0x114];
            SaveScreenDay = file[0x115];
            SaveScreenMapPosition = file[0x116];
            CopyArray( file, offset0x117, 0x117, 17 );
            Timer = (uint)((uint)file[0x128] + ((uint)file[0x129] << 8) + ((uint)file[0x12A] << 16) + ((uint)file[0x12B] << 24));
            CopyArray( file, artifactDates, 0x12C, 53 );
            CopyArray( file, wondersDates, 0x161, 17 );
            CopyArray( file, featsDates, 0x172, 108 );
            CopyArray( file, offset0x1DE, 0x1DE, 610 );
            numPropositions = file[0x440];
            CopyArray( file, prop1, 0x441, 9 );
            CopyArray( file, prop2, 0x44A, 9 );
            CopyArray( file, prop3, 0x453, 9 );
            CopyArray( file, prop4, 0x45C, 9 );
            CopyArray( file, prop5, 0x465, 9 );
            CopyArray( file, prop6, 0x46E, 9 );
            CopyArray( file, prop7, 0x477, 9 );
            CopyArray( file, prop8, 0x480, 9 );
            CopyArray( file, offset0x489, 0x489, 3 );
            for( int i = 0; i < 24; i++ )
            {
                byte[] charBytes = new byte[256];
                CopyArray( file, charBytes, 0x48C + i * 0x100, 0x100 );
                try
                {
                    if( charBytes[1] == 0xFF )
                    {
                        throw new BadCharacterDataException();
                    }
                    Characters[i] = new Character( charBytes );
                }
                catch( Exception e )
                {
                    if( e is BadCharacterDataException )
                    {
                        Characters[i] = new Character( i );
                        Characters[i].Index = 0xFF;
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            for( int i = 0; i < 4; i++ )
            {
                byte[] guestBytes = new byte[256];
                CopyArray( file, guestBytes, 0x1C8C + i * 0x100, 0x100 );
                try
                {
                    if( guestBytes[1] == 0xFF )
                    {
                        throw new BadCharacterDataException();
                    }
                    Guests[i] = new Character( guestBytes );
                }
                catch( Exception e )
                {
                    if( e is BadCharacterDataException )
                    {
                        Guests[i] = new Character( i );
                        Guests[i].Index = 0xFF;
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            CopyArray( file, inventory, 0x208C, 316 );
            CopyArray( file, furshop, 0x21C8, 316 );
            CopyArray( file, offset0x2304, 0x2304, 304 );
            WarFunds = (uint)((uint)file[0x2434] + ((uint)file[0x2435] << 8) + ((uint)file[0x2436] << 16) + ((uint)file[0x2437] << 24));
            CopyArray( file, offset0x2438, 0x2438, 4 );
            Month = file[0x243C];
            Day = file[0x2440];
            CopyArray( file, offset0x2444, 0x2444, 4 );
            MapPosition = file[0x2448];
            CopyArray( file, offset0x2449, 0x2449, 191 );
            Kills = (uint)((uint)file[0x2508] + ((uint)file[0x2509] << 8) + ((uint)file[0x250A] << 16) + ((uint)file[0x250B] << 25));
            Casualties = (uint)((uint)file[0x250C] + ((uint)file[0x250D] << 8) + ((uint)file[0x250E] << 16) + ((uint)file[0x250F] << 24));
            CopyArray( file, offset0x2510, 0x2510, 200 );
            CopyArray( file, artefacts, 0x25D8, 6 );
            CopyArray( file, wonders, 0x25DE, 2 );
            CopyArray( file, feats, 0x25E0, 48 );
            CopyArray( file, offset0x2610, 0x2610, 372 );
            CopyArray( file, options, 0x2784, 4 );
            CopyArray( file, offset0x2788, 0x2788, 692 );
        }

        public byte[] ToByteArray()
        {
            byte[] result = new byte[saveFileSize];
            CopyArray( offset0x00, result, 0, 0, 257 );
            CopyArray( saveNameRaw, result, 0, 0x101, 15 );
            CopyArray( offset0x110, result, 0, 0x110, 4 );
            result[0x114] = (byte)SaveScreenMonth;
            result[0x115] = SaveScreenDay;
            result[0x116] = SaveScreenMapPosition;
            CopyArray( offset0x117, result, 0, 0x117, 17 );
            result[0x128] = (byte)(Timer & 0xFF);
            result[0x129] = (byte)((Timer >> 8) & 0xFF);
            result[0x12A] = (byte)((Timer >> 16) & 0xFF);
            result[0x12B] = (byte)((Timer >> 24) & 0xFF);
            CopyArray( artifactDates, result, 0, 0x12C, 53 );
            CopyArray( wondersDates, result, 0, 0x161, 17 );
            CopyArray( featsDates, result, 0, 0x172, 108 );
            CopyArray( offset0x1DE, result, 0, 0x1DE, 610 );
            result[0x440] = numPropositions;
            CopyArray( prop1, result, 0, 0x441, 9 );
            CopyArray( prop2, result, 0, 0x44A, 9 );
            CopyArray( prop3, result, 0, 0x453, 9 );
            CopyArray( prop4, result, 0, 0x45C, 9 );
            CopyArray( prop5, result, 0, 0x465, 9 );
            CopyArray( prop6, result, 0, 0x46E, 9 );
            CopyArray( prop7, result, 0, 0x477, 9 );
            CopyArray( prop8, result, 0, 0x480, 9 );
            CopyArray( offset0x489, result, 0, 0x489, 3 );
            for( int i = 0; i < 24; i++ )
            {
                if( Characters[i] != null )
                {
                    byte[] charBytes = Characters[i].ToByteArray();
                    CopyArray( charBytes, result, 0, 0x48C + i * 0x100, 0x100 );
                }
            }
            for( int i = 0; i < 4; i++ )
            {
                if( Guests[i] != null )
                {
                    byte[] guestBytes = Guests[i].ToByteArray();
                    CopyArray( guestBytes, result, 0, 0x1C8C + i * 0x100, 0x100 );
                }
            }
            CopyArray( inventory, result, 0, 0x208C, 316 );
            CopyArray( furshop, result, 0, 0x21C8, 316 );
            CopyArray( offset0x2304, result, 0, 0x2304, 304 );
            result[0x2434] = (byte)(WarFunds & 0xFF);
            result[0x2435] = (byte)((WarFunds >> 8) & 0xFF);
            result[0x2436] = (byte)((WarFunds >> 16) & 0xFF);
            result[0x2437] = (byte)((WarFunds >> 24) & 0xFF);
            CopyArray( offset0x2438, result, 0, 0x2438, 4 );
            result[0x243C] = (byte)Month;
            result[0x2440] = (byte)Day;
            CopyArray( offset0x2444, result, 0, 0x2444, 4 );
            result[0x2448] = MapPosition;
            CopyArray( offset0x2449, result, 0, 0x2449, 191 );
            result[0x2508] = (byte)(Kills & 0xFF);
            result[0x2509] = (byte)((Kills >> 8) & 0xFF);
            result[0x250A] = (byte)((Kills >> 16) & 0xFF);
            result[0x250B] = (byte)((Kills >> 24) & 0xFF);
            result[0x250C] = (byte)(Casualties & 0xFF);
            result[0x250D] = (byte)((Casualties >> 8) & 0xFF);
            result[0x250E] = (byte)((Casualties >> 16) & 0xFF);
            result[0x250F] = (byte)((Casualties >> 24) & 0xFF);
            CopyArray( offset0x2510, result, 0, 0x2510, 200 );
            CopyArray( artefacts, result, 0, 0x25D8, 6 );
            CopyArray( wonders, result, 0, 0x25DE, 2 );
            CopyArray( feats, result, 0, 0x25E0, 48 );
            CopyArray( offset0x2610, result, 0, 0x2610, 372 );
            CopyArray( options, result, 0, 0x2784, 4 );
            CopyArray( offset0x2788, result, 0, 0x2788, 692 );

            return result;
        }

        public static void CopyArray( byte[] source, byte[] destination, int sourceStart, int destStart, int length )
        {
            for( int i = 0; i < length; i++ )
            {
                destination[i + destStart] = source[i + sourceStart];
            }
        }

        public static void CopyArray( byte[] source, byte[] destination, int sourceStart, int length )
        {
            CopyArray( source, destination, sourceStart, 0, length );
        }

        private string Time
        {
            get
            {
                TimeSpan span = new TimeSpan( (long)((long)Timer * 10000000) );
                //TimeSpan span = new TimeSpan(
                //uint seconds = Timer % 60;
                //uint minutes = ((Timer - seconds) / 60) % 60;
                //uint hours = (Timer - seconds - minutes * 60) / 60 / 60;
                return string.Format( "{0:000}:{1:00}:{2:00}", (int)span.TotalHours, span.Minutes, span.Seconds );
            }
        }

        private string Date
        {
            get 
            {
                return string.Format( "{0} {1}", SaveScreenDay, SaveScreenMonth );
            }
        }
        
        private string Location
        {
            get
            {
                return LionEditor.Location.AllLocations[SaveScreenMapPosition].ToString();
            }
        }
        
        public override string ToString()
        {
            return string.Format("{0} ({1}) [{2}] ~{3}~", SaveName, Time, Date, Location);
        }
    }


}
