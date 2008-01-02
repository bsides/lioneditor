/*
    Copyright 2007, Joe Davidson <joedavidson@gmail.com>

    This file is part of FFTPatcher.

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
using System.Xml;
using FFTPatcher.Properties;

namespace FFTPatcher.Datatypes
{
    public class AllAbilities
    {
        public  static string[] PSXNames { get; private set; }
        public static string[] PSPNames { get; private set; }
        public static string[] Names
        {
            get
            {
                return FFTPatch.Context == Context.US_PSP ? PSPNames : PSXNames;
            }
        }

        public static Ability[] PSPAbilities { get; private set; }
        public static Ability[] PSXAbilities { get; private set; }
        public static Ability[] DummyAbilities 
        {
            get
            {
                return FFTPatch.Context == Context.US_PSP ? PSPAbilities : PSXAbilities;
            }
        }

        static AllAbilities()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml( Resources.Abilities );
            XmlDocument psxDoc = new XmlDocument();
            psxDoc.LoadXml( PSXResources.Abilities );

            PSPNames = new string[512];
            PSXNames = new string[512];
            PSPAbilities = new Ability[512];
            PSXAbilities = new Ability[512];
            for( int i = 0; i < 512; i++ )
            {
                PSPNames[i] = doc.SelectSingleNode( string.Format( "/Abilities/Ability[@value='{0}']/@name", i ) ).InnerText;
                PSXNames[i] = psxDoc.SelectSingleNode( string.Format( "/Abilities/Ability[@value='{0}']/@name", i ) ).InnerText;
                PSPAbilities[i] = new Ability( PSPNames[i], (UInt16)i );
                PSXAbilities[i] = new Ability( PSXNames[i], (UInt16)i );
            }
        }

        public Ability[] Abilities { get; private set; }

        public AllAbilities( SubArray<byte> bytes )
        {
            Abilities = new Ability[512];
            for( UInt16 i = 0; i < 512; i++ )
            {
                SubArray<byte> first = new SubArray<byte>( bytes, i * 8, i * 8 + 7 );
                SubArray<byte> second;
                if( i <= 0x16F )
                {
                    second = new SubArray<byte>( bytes, 0x1000 + 14 * i, 0x1000 + 14 * i + 13 );
                }
                else if( i <= 0x17D )
                {
                    second = new SubArray<byte>( bytes, 0x2420 + i - 0x170, 0x2420 + i - 0x170 );
                }
                else if( i <= 0x189 )
                {
                    second = new SubArray<byte>( bytes, 0x2430 + i - 0x17E, 0x2430 + i - 0x17E );
                }
                else if( i <= 0x195 )
                {
                    second = new SubArray<byte>( bytes, 0x243C + (i - 0x18A) * 2, 0x243C + (i - 0x18A) * 2 + 1 );
                }
                else if( i <= 0x19D )
                {
                    second = new SubArray<byte>( bytes, 0x2454 + (i - 0x196) * 2, 0x2454 + (i - 0x196) * 2 + 1 );
                }
                else if( i <= 0x1A5 )
                {
                    second = new SubArray<byte>( bytes, 0x2464 + i - 0x19E, 0x2464 + i - 0x19E );
                }
                else
                {
                    second = new SubArray<byte>( bytes, 0x246C + i - 0x1A6, 0x246C + i - 0x1A6 );
                }

                Abilities[i] = new Ability( Names[i], i, first, second );
            }
        }

        public byte[] ToByteArray()
        {
            List<byte> bytes = new List<byte>();
            for( UInt16 i = 0; i < 512; i++ )
            {
                bytes.AddRange( Abilities[i].ToByteArray() );
            }
            for( UInt16 i = 0; i < 512; i++ )
            {
                bytes.AddRange( Abilities[i].ToSecondByteArray() );
            }

            bytes.Insert( 0x242E, 0x00 );
            bytes.Insert( 0x242E, 0x00 );
            return bytes.ToArray();
        }

        public byte[] ToByteArray( Context context )
        {
            return ToByteArray();
        }

        public List<string> GenerateCodes()
        {
            if( FFTPatch.Context == Context.US_PSP )
            {
                return Utilities.GenerateCodes( Context.US_PSP, Resources.AbilitiesBin, this.ToByteArray(), 0x2754C0 );
            }
            else
            {
                return Utilities.GenerateCodes( Context.US_PSX, PSXResources.AbilitiesBin, this.ToByteArray(), 0x05EBF0 );
            }
        }
    }
}
