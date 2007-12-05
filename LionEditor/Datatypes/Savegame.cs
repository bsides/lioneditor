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
    /// <summary>
    /// Represents a saved game
    /// </summary>
    public class Savegame
    {
        #region Fields

        public const uint saveFileSize = 0x2A3C;
        private byte[] offset0x00 = new byte[257];
        private byte[] saveNameRaw = new byte[17];
        private byte saveScreenMapPosition;
        private byte[] offset0x117 = new byte[17];
        private uint timer;
        private byte[] offset0x1DF = new byte[609];
        private byte numPropositions;
        private byte[] prop1 = new byte[9];
        private byte[] prop2 = new byte[9];
        private byte[] prop3 = new byte[9];
        private byte[] prop4 = new byte[9];
        private byte[] prop5 = new byte[9];
        private byte[] prop6 = new byte[9];
        private byte[] prop7 = new byte[9];
        private byte[] prop8 = new byte[9];
        private byte[] offset0x489 = new byte[3];
        private Character[] characters = new Character[28];
        private Inventory inventory;
        private Inventory poachersDen;
        private byte[] offset0x2304 = new byte[304];
        private uint warFunds;
        private byte[] offset0x2438 = new byte[4];
        private byte[] offset0x2444 = new byte[4];
        private byte mapPosition;
        private byte[] offset0x2449 = new byte[191];
        private uint kills;
        private uint casualties;
        private byte[] offset0x2510 = new byte[200];
        private byte[] offset0x2610 = new byte[372];
        private byte[] offset0x2788 = new byte[692];
        private Options options;
        private Feats feats;
        private Wonders wonders;
        private Artefacts artefacts;
        private StupidDate date;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of this save
        /// </summary>
        public string SaveName
        {
            get { return Character.DecodeName( saveNameRaw, 0 ); }
        }

        /// <summary>
        /// Gets or sets the map position displayed on the save screen
        /// </summary>
        public byte SaveScreenMapPosition
        {
            get { return saveScreenMapPosition; }
            set { saveScreenMapPosition = value; }
        }

        /// <summary>
        /// Gets or sets the game timer
        /// </summary>
        public uint Timer
        {
            get { return timer; }
            set { timer = value; }
        }

        /// <summary>
        /// Gets the collection of <see cref="Character"/>s.
        /// </summary>
        public Character[] Characters
        {
            get { return characters; }
        }

        /// <summary>
        /// Gets the <see cref="Inventory"/>
        /// </summary>
        public Inventory Inventory
        {
            get { return inventory; }
        }

        /// <summary>
        /// Gets the Poacher's Den <see cref="Inventory"/>
        /// </summary>
        public Inventory PoachersDen
        {
            get { return poachersDen; }
        }

        /// <summary>
        /// Gets or sets the war funds
        /// </summary>
        public uint WarFunds
        {
            get { return warFunds; }
            set { warFunds = value; }
        }

        /// <summary>
        /// Gets or sets the map position
        /// </summary>
        public byte MapPosition
        {
            get { return mapPosition; }
            set { mapPosition = value; }
        }

        /// <summary>
        /// Gets or sets the number of kills
        /// </summary>
        public uint Kills
        {
            get { return kills; }
            set { kills = value; }
        }

        /// <summary>
        /// Gets or sets the number of casualties
        /// </summary>
        public uint Casualties
        {
            get { return casualties; }
            set { casualties = value; }
        }

        /// <summary>
        /// Gets the game options
        /// </summary>
        public Options Options
        {
            get { return options; }
        }

        /// <summary>
        /// Gets the feats
        /// </summary>
        public Feats Feats
        {
            get { return feats; }
        }

        /// <summary>
        /// Gets the wonders
        /// </summary>
        public Wonders Wonders
        {
            get { return wonders; }
        }

        /// <summary>
        /// Gets the artefacts
        /// </summary>
        public Artefacts Artefacts
        {
            get { return artefacts; }
        }

        /// <summary>
        /// Gets the current date
        /// </summary>
        public StupidDate Date
        {
            get { return date; }
            set { date = value; }
        }

        #endregion

        #region Constructor

        public Savegame( byte[] file )
        {
            CopyArray( file, offset0x00, 0, 257 );
            CopyArray( file, saveNameRaw, 0x101, 17 );

            Date = new StupidDate( file[0x115], (Zodiac)((file[0x114] - 1) << 4) );

            SaveScreenMapPosition = file[0x116];
            CopyArray( file, offset0x117, 0x117, 17 );
            Timer = (uint)((uint)file[0x128] + ((uint)file[0x129] << 8) + ((uint)file[0x12A] << 16) + ((uint)file[0x12B] << 24));

            byte[] artefactsDates = new byte[53];
            byte[] artefactsStates = new byte[6];
            CopyArray( file, artefactsDates, 0x12C, 53 );
            CopyArray( file, artefactsStates, 0x25D8, 6 );
            artefacts = new Artefacts( artefactsDates, artefactsStates );

            byte[] wondersDates = new byte[18];
            byte[] wondersStates = new byte[2];
            CopyArray( file, wondersDates, 0x161, 18 );
            CopyArray( file, wondersStates, 0x25DE, 2 );
            wonders = new Wonders( wondersDates, wondersStates );

            byte[] featsDates = new byte[108];
            byte[] featsStates = new byte[48];
            CopyArray( file, featsDates, 0x173, 108 );
            CopyArray( file, featsStates, 0x25E0, 48 );
            feats = new Feats( featsDates, featsStates );

            CopyArray( file, offset0x1DF, 0x1DF, 609 );
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
            for( int i = 0; i < 28; i++ )
            {
                byte[] charBytes = new byte[256];
                CopyArray( file, charBytes, 0x48C + i * 0x100, 0x100 );
                Characters[i] = new Character(charBytes, i);
            }

            byte[] inventoryBytes = new byte[316];
            CopyArray( file, inventoryBytes, 0x208C, 316 );

            inventory = new Inventory( inventoryBytes );

            CopyArray( file, inventoryBytes, 0x21C8, 316 );
            poachersDen = new Inventory( inventoryBytes, 255 );

            CopyArray( file, offset0x2304, 0x2304, 304 );
            WarFunds = (uint)((uint)file[0x2434] + ((uint)file[0x2435] << 8) + ((uint)file[0x2436] << 16) + ((uint)file[0x2437] << 24));
            CopyArray( file, offset0x2438, 0x2438, 4 );
            CopyArray( file, offset0x2444, 0x2444, 4 );
            MapPosition = file[0x2448];
            CopyArray( file, offset0x2449, 0x2449, 191 );
            Kills = (uint)((uint)file[0x2508] + ((uint)file[0x2509] << 8) + ((uint)file[0x250A] << 16) + ((uint)file[0x250B] << 25));
            Casualties = (uint)((uint)file[0x250C] + ((uint)file[0x250D] << 8) + ((uint)file[0x250E] << 16) + ((uint)file[0x250F] << 24));
            CopyArray( file, offset0x2510, 0x2510, 200 );
            CopyArray( file, offset0x2610, 0x2610, 372 );

            byte[] optionsBytes = new byte[4];
            CopyArray( file, optionsBytes, 0x2784, 4 );
            options = new Options( optionsBytes );

            CopyArray( file, offset0x2788, 0x2788, 692 );
        }

        #endregion

        #region Utilities

        public byte[] ToByteArray()
        {
            byte[] result = new byte[saveFileSize];
            CopyArray( offset0x00, result, 0, 0, 257 );
            Character.EncodeName( characters[0].Name, result, 0x101 );
            result[0x112] = Characters[0].Job.Byte;
            result[0x113] = Characters[0].Level;
            result[0x114] = (byte)((((byte)Date.Month) >> 4) + 1);
            result[0x115] = (byte)Date.Day;
            result[0x116] = SaveScreenMapPosition;
            CopyArray( offset0x117, result, 0, 0x117, 17 );
            result[0x128] = (byte)(Timer & 0xFF);
            result[0x129] = (byte)((Timer >> 8) & 0xFF);
            result[0x12A] = (byte)((Timer >> 16) & 0xFF);
            result[0x12B] = (byte)((Timer >> 24) & 0xFF);
            CopyArray( artefacts.DatesToByteArray(), result, 0, 0x12C, 53 );
            CopyArray( wonders.DatesToByteArray(), result, 0, 0x161, 18 );
            CopyArray( feats.DatesToByteArray(), result, 0, 0x173, 108 );
            CopyArray( offset0x1DF, result, 0, 0x1DF, 609 );
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

            for( int i = 0; i < 28; i++ )
            {
                if( Characters[i] != null )
                {
                    byte[] charBytes = Characters[i].ToByteArray();
                    CopyArray( charBytes, result, 0, 0x48C + i * 0x100, 0x100 );
                }
            }

            inventory.UpdateEquippedQuantities( new Character[][] { Characters } );

            CopyArray( inventory.ToByteArray(), result, 0, 0x208C, 316 );
            CopyArray( poachersDen.ToByteArray(), result, 0, 0x21C8, 316 );
            CopyArray( offset0x2304, result, 0, 0x2304, 304 );
            result[0x2434] = (byte)(WarFunds & 0xFF);
            result[0x2435] = (byte)((WarFunds >> 8) & 0xFF);
            result[0x2436] = (byte)((WarFunds >> 16) & 0xFF);
            result[0x2437] = (byte)((WarFunds >> 24) & 0xFF);
            CopyArray( offset0x2438, result, 0, 0x2438, 4 );
            DateTime realDate = Date.ToNormalDate();
            result[0x243C] = (byte)realDate.Month;
            result[0x2440] = (byte)realDate.Day;
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
            CopyArray( artefacts.StatesToByteArray(), result, 0, 0x25D8, 6 );
            CopyArray( wonders.StatesToByteArray(), result, 0, 0x25DE, 2 );
            CopyArray( feats.StatesToByteArray(), result, 0, 0x25E0, 48 );
            CopyArray( offset0x2610, result, 0, 0x2610, 372 );
            CopyArray( options.ToByteArray(), result, 0, 0x2784, 4 );
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

        public override string ToString()
        {
            TimeSpan span = new TimeSpan( (long)((long)Timer * 10000000) );
            string time = string.Format( "{0:000}:{1:00}:{2:00}", (int)span.TotalHours, span.Minutes, span.Seconds );

            string date = string.Format( "{0} {1}", Date.Day, Date.Month );

            string loc = LionEditor.Location.AllLocations[SaveScreenMapPosition].ToString();

            return string.Format( "{0} ({1}) [{2}] ~{3}~", SaveName, time, date, loc );
        }

        #endregion
    }
}
