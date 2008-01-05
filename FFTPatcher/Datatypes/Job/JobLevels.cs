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

using System.Collections.Generic;
using FFTPatcher.Properties;

namespace FFTPatcher.Datatypes
{
    public class JobLevels
    {
        private string[] reqs = new string[] {
            "Chemist", "Knight", "Archer", "Monk", "WhiteMage", "BlackMage", "TimeMage", "Summoner", "Thief", "Orator", 
            "Mystic", "Geomancer", "Dragoon", "Samurai", "Ninja", "Arithmetician", "Bard", "Dancer", "Mime", "DarkKnight", "OnionKnight", "Unknown" };
        public Requirements Chemist { get; private set; }
        public Requirements Knight { get; private set; }
        public Requirements Archer { get; private set; }
        public Requirements Monk { get; private set; }
        public Requirements WhiteMage { get; private set; }
        public Requirements BlackMage { get; private set; }
        public Requirements TimeMage { get; private set; }
        public Requirements Summoner { get; private set; }
        public Requirements Thief { get; private set; }
        public Requirements Orator { get; private set; }
        public Requirements Mystic { get; private set; }
        public Requirements Geomancer { get; private set; }
        public Requirements Dragoon { get; private set; }
        public Requirements Samurai { get; private set; }
        public Requirements Ninja { get; private set; }
        public Requirements Arithmetician { get; private set; }
        public Requirements Bard { get; private set; }
        public Requirements Dancer { get; private set; }
        public Requirements Mime { get; private set; }
        public Requirements DarkKnight { get; private set; }
        public Requirements OnionKnight { get; private set; }
        public Requirements Unknown { get; private set; }
        public ushort Level1 { get; set; }
        public ushort Level2 { get; set; }
        public ushort Level3 { get; set; }
        public ushort Level4 { get; set; }
        public ushort Level5 { get; set; }
        public ushort Level6 { get; set; }
        public ushort Level7 { get; set; }
        public ushort Level8 { get; set; }

        public JobLevels Default { get; private set; }

        public JobLevels( SubArray<byte> bytes )
            : this( Context.US_PSP, bytes )
        {
        }

        public JobLevels( Context context, SubArray<byte> bytes )
            : this( context, bytes, null )
        {
        }

        public JobLevels( Context context, SubArray<byte> bytes, JobLevels defaults )
        {
            Default = defaults;
            int jobCount = context == Context.US_PSP ? 22 : 19;
            int requirementsLength = context == Context.US_PSP ? 12 : 10;

            for( int i = 0; i < jobCount; i++ )
            {
                Utilities.SetFieldOrProperty( this, reqs[i],
                    new Requirements( context,
                        new SubArray<byte>( bytes, i * requirementsLength, (i + 1) * requirementsLength - 1 ) ) );
            }

            int start = requirementsLength * jobCount;
            if( context == Context.US_PSX )
                start += 2;
            Level1 = Utilities.BytesToUShort( bytes[start + 0], bytes[start + 1] );
            Level2 = Utilities.BytesToUShort( bytes[start + 2], bytes[start + 3] );
            Level3 = Utilities.BytesToUShort( bytes[start + 4], bytes[start + 5] );
            Level4 = Utilities.BytesToUShort( bytes[start + 6], bytes[start + 7] );
            Level5 = Utilities.BytesToUShort( bytes[start + 8], bytes[start + 9] );
            Level6 = Utilities.BytesToUShort( bytes[start + 10], bytes[start + 11] );
            Level7 = Utilities.BytesToUShort( bytes[start + 12], bytes[start + 13] );
            Level8 = Utilities.BytesToUShort( bytes[start + 14], bytes[start + 15] );
        }

        public byte[] ToByteArray()
        {
            return ToByteArray( Context.US_PSP );
        }

        public byte[] ToByteArray( Context context )
        {
            int jobCount = context == Context.US_PSP ? 22 : 19;
            List<byte> result = new List<byte>( 0x118 );
            for( int i = 0; i < jobCount; i++ )
            {
                result.AddRange( Utilities.GetFieldOrProperty<Requirements>( this, reqs[i] ).ToByteArray( context ) );
            }
            if( context == Context.US_PSX )
            {
                result.Add( 0x00 );
                result.Add( 0x00 );
            }
            result.AddRange( Utilities.UShortToBytes( Level1 ) );
            result.AddRange( Utilities.UShortToBytes( Level2 ) );
            result.AddRange( Utilities.UShortToBytes( Level3 ) );
            result.AddRange( Utilities.UShortToBytes( Level4 ) );
            result.AddRange( Utilities.UShortToBytes( Level5 ) );
            result.AddRange( Utilities.UShortToBytes( Level6 ) );
            result.AddRange( Utilities.UShortToBytes( Level7 ) );
            result.AddRange( Utilities.UShortToBytes( Level8 ) );

            return result.ToArray();
        }

        public List<string> GenerateCodes()
        {
            if( FFTPatch.Context == Context.US_PSP )
            {
                return Utilities.GenerateCodes( Context.US_PSP, Resources.JobLevelsBin, this.ToByteArray(), 0x27B030 );
            }
            else
            {
                return Utilities.GenerateCodes( Context.US_PSX, PSXResources.JobLevelsBin, this.ToByteArray( Context.US_PSX ), 0x0660C4 );
            }
        }
    }

    public class Requirements
    {
        public int Squire { get; set; }
        public int Chemist { get; set; }
        public int Knight { get; set; }
        public int Archer { get; set; }
        public int Monk { get; set; }
        public int WhiteMage { get; set; }
        public int BlackMage { get; set; }
        public int TimeMage { get; set; }
        public int Summoner { get; set; }
        public int Thief { get; set; }
        public int Orator { get; set; }
        public int Mystic { get; set; }
        public int Geomancer { get; set; }
        public int Dragoon { get; set; }
        public int Samurai { get; set; }
        public int Ninja { get; set; }
        public int Arithmetician { get; set; }
        public int Bard { get; set; }
        public int Dancer { get; set; }
        public int Mime { get; set; }
        public int DarkKnight { get; set; }
        public int OnionKnight { get; set; }
        public int Unknown1 { get; set; }
        public int Unknown2 { get; set; }

        public Requirements( SubArray<byte> bytes )
            : this( Context.US_PSP, bytes )
        {
        }

        public Requirements( Context context, SubArray<byte> bytes )
        {
            Squire = Utilities.UpperNibble( bytes[0] );
            Chemist = Utilities.LowerNibble( bytes[0] );
            Knight = Utilities.UpperNibble( bytes[1] );
            Archer = Utilities.LowerNibble( bytes[1] );
            Monk = Utilities.UpperNibble( bytes[2] );
            WhiteMage = Utilities.LowerNibble( bytes[2] );
            BlackMage = Utilities.UpperNibble( bytes[3] );
            TimeMage = Utilities.LowerNibble( bytes[3] );
            Summoner = Utilities.UpperNibble( bytes[4] );
            Thief = Utilities.LowerNibble( bytes[4] );
            Orator = Utilities.UpperNibble( bytes[5] );
            Mystic = Utilities.LowerNibble( bytes[5] );
            Geomancer = Utilities.UpperNibble( bytes[6] );
            Dragoon = Utilities.LowerNibble( bytes[6] );
            Samurai = Utilities.UpperNibble( bytes[7] );
            Ninja = Utilities.LowerNibble( bytes[7] );
            Arithmetician = Utilities.UpperNibble( bytes[8] );
            Bard = Utilities.LowerNibble( bytes[8] );
            Dancer = Utilities.UpperNibble( bytes[9] );
            Mime = Utilities.LowerNibble( bytes[9] );
            if( context == Context.US_PSP )
            {
                DarkKnight = Utilities.UpperNibble( bytes[10] );
                OnionKnight = Utilities.LowerNibble( bytes[10] );
                Unknown1 = Utilities.UpperNibble( bytes[11] );
                Unknown2 = Utilities.LowerNibble( bytes[11] );
            }
        }

        public byte[] ToByteArray( Context context )
        {
            List<byte> result = new List<byte>( 12 );
            result.Add( Utilities.MoveToUpperAndLowerNibbles( Squire, Chemist ) );
            result.Add( Utilities.MoveToUpperAndLowerNibbles( Knight, Archer ) );
            result.Add( Utilities.MoveToUpperAndLowerNibbles( Monk, WhiteMage ) );
            result.Add( Utilities.MoveToUpperAndLowerNibbles( BlackMage, TimeMage ) );
            result.Add( Utilities.MoveToUpperAndLowerNibbles( Summoner, Thief ) );
            result.Add( Utilities.MoveToUpperAndLowerNibbles( Orator, Mystic ) );
            result.Add( Utilities.MoveToUpperAndLowerNibbles( Geomancer, Dragoon ) );
            result.Add( Utilities.MoveToUpperAndLowerNibbles( Samurai, Ninja ) );
            result.Add( Utilities.MoveToUpperAndLowerNibbles( Arithmetician, Bard ) );
            result.Add( Utilities.MoveToUpperAndLowerNibbles( Dancer, Mime ) );
            if( context == Context.US_PSP )
            {
                result.Add( Utilities.MoveToUpperAndLowerNibbles( DarkKnight, OnionKnight ) );
                result.Add( Utilities.MoveToUpperAndLowerNibbles( Unknown1, Unknown2 ) );
            }

            return result.ToArray();
        }
        
        public byte[] ToByteArray()
        {
            return ToByteArray( Context.US_PSP );
        }
    }
}
