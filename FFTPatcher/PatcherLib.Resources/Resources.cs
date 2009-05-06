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
using System.IO;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Tar;
using ICSharpCode.SharpZipLib.Zip;

namespace PatcherLib
{
    using PatcherLib.Utilities;
    public static class Resources
    {
		#region Instance Variables (1) 

        private static Dictionary<byte, string> abilityFormulas;

		#endregion Instance Variables 

		#region Public Properties (2) 

        public static Dictionary<byte, string> AbilityFormulas
        {
            get
            {
                if( abilityFormulas == null )
                {
                    abilityFormulas = new Dictionary<byte, string>();
                    string[] formulaNames = PatcherLib.Utilities.Utilities.GetStringsFromNumberedXmlNodes(
                        ZipFileContents[Paths.AbilityFormulasXML].ToUTF8String(),
                        "/AbilityFormulas/Ability[@value='{0:X2}']",
                        256 );
                    for( int i = 0; i < 256; i++ )
                    {
                        abilityFormulas.Add( (byte)i, formulaNames[i] );
                    }
                }

                return abilityFormulas;
            }
        }

        public static Dictionary<string, byte[]> ZipFileContents
        {
            get; private set;
        }

		#endregion Public Properties 

		#region Private Properties (1) 

        private static Dictionary<string, byte[]> DefaultZipFileContents
        {
            get; set;
        }

		#endregion Private Properties 

		#region Constructors (1) 

        static Resources()
        {
            using( MemoryStream memStream = new MemoryStream( PatcherLib.Properties.Resources.ZippedResources, false ) )
            using( GZipInputStream gzStream = new GZipInputStream( memStream ) )
            using( TarInputStream tarStream = new TarInputStream( gzStream ) )
            {
                DefaultZipFileContents = new Dictionary<string, byte[]>();
                TarEntry entry;
                entry = tarStream.GetNextEntry();
                while( entry != null )
                {
                    if( entry.Size != 0 )
                    {
                        byte[] bytes = new byte[entry.Size];
                        StreamUtils.ReadFully( tarStream, bytes );
                        DefaultZipFileContents[entry.Name] = bytes;
                    }
                    entry = tarStream.GetNextEntry();
                }
            }

            string defaultsFile = Path.Combine( Path.GetDirectoryName( System.Windows.Forms.Application.ExecutablePath ), "Resources.zip" );
            if( File.Exists( defaultsFile ) )
            {
                try
                {
                    using( FileStream file = File.Open( defaultsFile, FileMode.Open, FileAccess.Read ) )
                    using( ZipInputStream zipStream = new ZipInputStream( file ) )
                    {
                        ZipFileContents = new Dictionary<string, byte[]>();
                        ZipEntry entry = zipStream.GetNextEntry();
                        while( entry != null )
                        {
                            if( entry.Size != 0 )
                            {
                                byte[] bytes = new byte[entry.Size];
                                StreamUtils.ReadFully( zipStream, bytes );
                                ZipFileContents[entry.Name] = bytes;
                            }
                            entry = zipStream.GetNextEntry();
                        }

                        foreach( KeyValuePair<string, byte[]> kvp in DefaultZipFileContents )
                        {
                            if( !ZipFileContents.ContainsKey( kvp.Key ) )
                            {
                                ZipFileContents[kvp.Key] = kvp.Value;
                            }
                        }
                    }
                }
                catch( Exception )
                {
                    ZipFileContents = DefaultZipFileContents;
                }
            }
            else
            {
                ZipFileContents = DefaultZipFileContents;
            }

        }

		#endregion Constructors 


        public static class Paths
        {
            public const string AbilityFormulasXML = "AbilityFormulas.xml";
            public const string DigestTransform = "digestTransform.xsl";
            private const string ENTD1 = "ENTD1.ENT";
            private const string ENTD2 = "ENTD2.ENT";
            private const string ENTD3 = "ENTD3.ENT";
            private const string ENTD4 = "ENTD4.ENT";
            private const string MoveFindBin = "MoveFind.bin";

            public static class PSP
            {
                public static class Binaries
                {
                    public const string ENTD1 = Paths.ENTD1;
                    public const string ENTD2 = Paths.ENTD2;
                    public const string ENTD3 = Paths.ENTD3;
                    public const string ENTD4 = Paths.ENTD4;
                    public const string ENTD5 = "PSP/bin/ENTD5.bin";
                    public const string MoveFind = Paths.MoveFindBin;
                    public const string Abilities = "PSP/bin/Abilities.bin";
                    public const string AbilityAnimations = "PSP/bin/AbilityAnimations.bin";
                    public const string AbilityEffects = "PSP/bin/AbilityEffects.bin";
                    public const string ActionEvents = "PSP/bin/ActionEvents.bin";
                    public const string Font = "PSP/bin/font.bin";
                    public const string FontWidths = "PSP/bin/FontWidths.bin";
                    public const string ICON0 = "PSP/bin/ICON0";
                    public const string InflictStatuses = "PSP/bin/InflictStatuses.bin";
                    public const string JobLevels = "PSP/bin/JobLevels.bin";
                    public const string Jobs = "PSP/bin/Jobs.bin";
                    public const string MonsterSkills = "PSP/bin/MonsterSkills.bin";
                    public const string NewItemAttributes = "PSP/bin/NewItemAttributes.bin";
                    public const string NewItems = "PSP/bin/NewItems.bin";
                    public const string OldItemAttributes = "PSP/bin/OldItemAttributes.bin";
                    public const string OldItems = "PSP/bin/OldItems.bin";
                    public const string PoachProbabilities = "PSP/bin/PoachProbabilities.bin";
                    public const string SkillSets = "PSP/bin/SkillSetsBin.bin";
                    public const string StatusAttributes = "PSP/bin/StatusAttributes.bin";
                    public const string StoreInventories = "StoreInventories.bin";
                }
                public const string EventNamesXML = "PSP/EventNames.xml";
                public const string FFTPackFilesXML = "PSP/FFTPackFiles.xml";
                public const string JobsXML = "PSP/Jobs.xml";
                public const string SkillSetsXML = "PSP/SkillSets.xml";
                public const string MapNamesXML = "PSP/MapNames.xml";
                public const string SpecialNamesXML = "PSP/SpecialNames.xml";
                public const string SpriteSetsXML = "PSP/SpriteSets.xml";
                public const string StatusNamesXML = "PSP/StatusNames.xml";
                public const string AbilitiesNamesXML = "PSP/Abilities/Abilities.xml";
                public const string AbilitiesStringsXML = "PSP/Abilities/Strings.xml";
                public const string AbilityEffectsXML = "PSP/Abilities/Effects.xml";
                public const string ItemAttributesXML = "PSP/Items/ItemAttributes.xml";
                public const string ItemsXML = "PSP/Items/Items.xml";
                public const string ItemsStringsXML = "PSP/Items/Strings.xml";
                public const string ShopNamesXML = "PSP/ShopNames.xml";
            }

            public static class PSX
            {


                public static class Binaries
                {
                    public const string ENTD1 = Paths.ENTD1;
                    public const string ENTD2 = Paths.ENTD2;
                    public const string ENTD3 = Paths.ENTD3;
                    public const string ENTD4 = Paths.ENTD4;
                    public const string MoveFind = Paths.MoveFindBin;
                    public const string Abilities = "PSX-US/bin/Abilities.bin";
                    public const string AbilityAnimations = "PSX-US/bin/AbilityAnimations.bin";
                    public const string AbilityEffects = "PSX-US/bin/AbilityEffects.bin";
                    public const string ActionEvents = "PSX-US/bin/ActionEvents.bin";
                    public const string Font = "PSX-US/bin/font.bin";
                    public const string FontWidths = "PSX-US/bin/FontWidths.bin";
                    public const string SCEAP = "PSX-US/bin/SCEAP.DAT.patched.bin";
                    public const string InflictStatuses = "PSX-US/bin/InflictStatuses.bin";
                    public const string JobLevels = "PSX-US/bin/JobLevels.bin";
                    public const string Jobs = "PSX-US/bin/Jobs.bin";
                    public const string MonsterSkills = "PSX-US/bin/MonsterSkills.bin";
                    public const string OldItemAttributes = "PSX-US/bin/OldItemAttributes.bin";
                    public const string OldItems = "PSX-US/bin/OldItems.bin";
                    public const string PoachProbabilities = "PSX-US/bin/PoachProbabilities.bin";
                    public const string SkillSets = "PSX-US/bin/SkillSetsBin.bin";
                    public const string StatusAttributes = "PSX-US/bin/StatusAttributes.bin";
                    public const string StoreInventories = "StoreInventories.bin";
                }
                public const string EventNamesXML = "PSX-US/EventNames.xml";
                public const string FileList = "PSX-US/FileList.txt";
                public const string JobsXML = "PSX-US/Jobs.xml";
                public const string SkillSetsXML = "PSX-US/SkillSets.xml";
                public const string SpecialNamesXML = "PSX-US/SpecialNames.xml";
                public const string SpriteSetsXML = "PSX-US/SpriteSets.xml";
                public const string StatusNamesXML = "PSX-US/StatusNames.xml";
                public const string AbilitiesNamesXML = "PSX-US/Abilities/Abilities.xml";
                public const string AbilitiesStringsXML = "PSX-US/Abilities/Strings.xml";
                public const string AbilityEffectsXML = "PSX-US/Abilities/Effects.xml";
                public const string ItemAttributesXML = "PSX-US/Items/ItemAttributes.xml";
                public const string ItemsXML = "PSX-US/Items/Items.xml";
                public const string ItemsStringsXML = "PSX-US/Items/Strings.xml";
                public const string ShopNamesXML = "PSX-US/ShopNames.xml";
                public const string MapNamesXML = "PSX-US/MapNames.xml";
            }
        }
    }
}