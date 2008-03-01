/*
    Copyright 2007, Joe Davidson <joedavidson@gmail.com>

    This file is part of FFTPatcher.

    FFTPatcher is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    FFTPatcher is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with FFTPatcher.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using FFTPatcher.Datatypes;

namespace FFTPatcher.TextEditor.Files.PSX
{
    public class WLDHELPLZW : BasePSXFile, ICompressed
    {
        private static Dictionary<string, long> locations;
        public override IDictionary<string, long> Locations
        {
            get
            {
                if( locations == null )
                {
                    locations = new Dictionary<string, long>();
                    locations.Add( "EVENT/WLDHELP.LZW", 0x00 );
                    locations.Add( "WORLD/WORLD.BIN", 0x8EE68 );
                }

                return locations;
            }
        }

        private const int numberOfSections = 21;
        protected override int NumberOfSections { get { return numberOfSections; } }
        public override int EstimatedLength
        {
            get { return (int)(base.EstimatedLength * 0.65346430772862594919277); }
        }
        private static WLDHELPLZW Instance { get; set; }
        private static string[] sectionNames;

        public static string[][] entryNames;

        public override IList<string> SectionNames { get { return sectionNames; } }
        public override IList<IList<string>> EntryNames { get { return entryNames; } }

        public override int MaxLength { get { return 0x01ADE4; } }

        public event EventHandler<CompressionEventArgs> ProgressChanged;
        public event EventHandler<CompressionEventArgs> CompressionFinished;

        static WLDHELPLZW()
        {
            Instance = new WLDHELPLZW( Properties.Resources.WLDHELP_LZW );

            sectionNames = new string[21] {
                "","Help", "Menu/Options Help", "", "Error/Confirmation messages", "", "",
                "","","","","","Job descriptions", "Item descriptions",
                "","Ability descriptions","Location descriptions","Location descriptions","","Skillset descriptions","Unit quotes"};
            entryNames = new string[numberOfSections][];
            for( int i = 0; i < entryNames.Length; i++ )
            {
                entryNames[i] = new string[Instance.Sections[i].Count];
            }

            entryNames[1] = new string[40] {
                "Unit #", "Level", "HP", "MP", "CT", "AT", "Exp", "Name",
                "Brave", "Faith", "", "Move", "Jump", "", "", "",
                "Speed", "ATK", "Weapon ATK", "", "Eva%", "SEv%", "AEv%", "Phys land effect",
                "Magic land effect", "Estimated", "Hit rate", "Aries", "Taurus", "Gemini", "Cancer", "Leo", 
                "Virgo", "Libra", "Scorpio", "Sagittarius", "Capricorn", "Aquarius", "Pisces", "Serpentarius" };
            entryNames[4] = new string[31] {
                "No items", "Item out of stock", "No more shields", "No unequipped items", "Set number to dispose", "Cancel equip?", "No ability learned", "Learned all abilities",
                "Is __ OK?", "Insufficient JP", "Remove __?", "Team not ready", "Team full", "Can't delete leader", "No memory card", "No saves",
                "No room on memory card", "Save?", "Can't remove unit", "Format memory card?", "Input birthday", "Input name", "Kanji search method", "First Kanji", 
                "Kanji strokes", "Input name", "Is __ OK?", "Destroy egg?", "Save error", "Load error", "Save?" };
            entryNames[12] = AllJobs.PSXNames;
            entryNames[13] = Item.PSXNames.ToArray();
            entryNames[15] = AllAbilities.PSXNames;
            List<string> temp = new List<string>( 188 );
            temp.AddRange( new SubArray<string>( SkillSet.PSXNames, 0, 175 ) );
            temp.AddRange( new string[12] );

            entryNames[19] = temp.ToArray();
        }

        private WLDHELPLZW( IList<byte> bytes )
        {
            Sections = new List<IList<string>>( NumberOfSections );
            for( int i = 0; i < NumberOfSections; i++ )
            {
                uint start = Utilities.BytesToUInt32( new SubArray<byte>( bytes, i * 4, i * 4 + 3 ) );
                uint stop = Utilities.BytesToUInt32( new SubArray<byte>( bytes, (i + 1) * 4, (i + 1) * 4 + 3 ) ) - 1;
                if( i == NumberOfSections - 1 )
                {
                    stop = (uint)bytes.Count - 1 - dataStart;
                }

                IList<byte> thisSection = TextUtilities.Decompress(
                    bytes,
                    new SubArray<byte>( bytes, (int)(start + dataStart), (int)(stop + dataStart) ),
                    (int)(start + dataStart) );
                Sections.Add( TextUtilities.ProcessList( thisSection, CharMap ) );
            }
        }

        public static WLDHELPLZW GetInstance()
        {
            return Instance;
        }

        protected override IList<byte> ToFinalBytes()
        {
            return Compress();
        }

        private void FireProgressChangedEvent( int progress )
        {
            if( ProgressChanged != null )
            {
                ProgressChanged( this, new CompressionEventArgs( progress ) );
            }
        }

        private void FireCompressionFinishedEvent( IList<byte> result )
        {
            if( CompressionFinished != null )
            {
                CompressionFinished( this, new CompressionEventArgs( 100, result ) );
            }
        }

        public IList<byte> Compress()
        {
            TextUtilities.ProgressCallback p = new TextUtilities.ProgressCallback(
                delegate( int progress )
                {
                    FireProgressChangedEvent( progress );
                } );

            IList<byte> bytes = TextUtilities.Recompress( new SubArray<byte>( ToUncompressedBytes(), 0x80 ), p );

            List<UInt32> sectionOffsets = new List<UInt32>();
            sectionOffsets.Add( 0 );

            int i = 0;
            for( int s = 0; s < Sections.Count - 1; s++ )
            {
                IList<string> section = Sections[s];
                int feFound = 0;
                for( int j = i; j < bytes.Count && feFound != section.Count; j++, i++ )
                {
                    if( bytes[j] == 0xFE )
                        feFound++;
                }
                sectionOffsets.Add( (UInt32)i );
            }

            List<byte> result = new List<byte>( 0x80 + bytes.Count );
            result.AddRange( BuildHeaderFromSectionOffsets( sectionOffsets ) );
            result.AddRange( bytes );

            FireCompressionFinishedEvent( result );

            return result;
        }

        private IList<byte> BuildHeaderFromSectionOffsets( IList<UInt32> offsets )
        {
            List<byte> result = new List<byte>( 0x80 );
            foreach( UInt32 offset in offsets )
            {
                result.AddRange( offset.ToBytes() );
            }

            while( result.Count < 0x80 )
            {
                result.Add( 0x00 );
            }

            return result;
        }
    }
}