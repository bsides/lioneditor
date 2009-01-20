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
using System.Text;
using System.Xml;
using System.ComponentModel;
using System.Windows.Forms;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Core;

namespace FFTPatcher.Datatypes
{
    public static class FFTPatch
    {
		#region Instance Variables (2) 

        private static IDictionary<ElementName, string> elementNames = Utilities.BuildDictionary<ElementName, string>( new object[] {
            ElementName.Abilities, "abilities",
            ElementName.AbilityEffects, "abilityEffects", 
            ElementName.Items, "items", 
            ElementName.ItemAttributes, "itemAttributes", 
            ElementName.PSPItems, "pspItems", 
            ElementName.PSPItemAttributes, "pspItemAttributes", 
            ElementName.Jobs, "jobs", 
            ElementName.JobLevels, "jobLevels",
            ElementName.SkillSets, "skillSets", 
            ElementName.MonsterSkills, "monsterSkills", 
            ElementName.ActionMenus, "actionMenus", 
            ElementName.InflictStatuses, "inflictStatuses", 
            ElementName.StatusAttributes, "statusAttributes", 
            ElementName.Poaching, "poaching",
            ElementName.ENTD1, "entd1", 
            ElementName.ENTD2, "entd2", 
            ElementName.ENTD3, "entd3", 
            ElementName.ENTD4, "entd4", 
            ElementName.ENTD5, "entd5", 
            ElementName.Font, "font", 
            ElementName.FontWidths, "fontWidths",
            ElementName.MoveFindItems, "moveFindItems",
            ElementName.StoreInventories, "storeInventories" } );
        private static string[] elementNameStrings = new string[] {
            "abilities", "abilityEffects", "items", "itemAttributes", "pspItems", "pspItemAttributes", "jobs", "jobLevels",
            "skillSets", "monsterSkills", "actionMenus", "inflictStatuses", "statusAttributes", "poaching",
            "entd1", "entd2", "entd3", "entd4", "entd5", "font", "fontWidths", "moveFindItems", "storeInventories" };

		#endregion Instance Variables 

		#region Public Properties (15) 

        public static AllAbilities Abilities { get; private set; }

        public static AllActionMenus ActionMenus { get; private set; }

        public static Context Context { get; private set; }

        public static AllENTDs ENTDs { get; private set; }

        public static FFTFont Font { get; private set; }

        public static AllInflictStatuses InflictStatuses { get; private set; }

        public static AllItemAttributes ItemAttributes { get; private set; }

        public static AllItems Items { get; private set; }

        public static JobLevels JobLevels { get; private set; }

        public static AllJobs Jobs { get; private set; }

        public static AllMonsterSkills MonsterSkills { get; private set; }

        public static AllMoveFindItems MoveFind { get; private set; }

        public static AllPoachProbabilities PoachProbabilities { get; private set; }

        public static AllSkillSets SkillSets { get; private set; }

        public static AllStatusAttributes StatusAttributes { get; private set; }

        public static AllStoreInventories StoreInventories { get; private set; }

		#endregion Public Properties 

		#region Public Methods (7) 

        public static void ConvertPsxPatchToPsp( string filename )
        {
            Dictionary<string, byte[]> fileList = new Dictionary<string, byte[]>();
            using( ZipFile zipFile = new ZipFile( filename ) )
            {
                foreach( ZipEntry entry in zipFile )
                {
                    byte[] bytes = new byte[entry.Size];
                    StreamUtils.ReadFully( zipFile.GetInputStream( entry ), bytes );
                    fileList[entry.Name] = bytes;
                }
            }

            File.Delete( filename );

            if( fileList["type"].ToUTF8String() == Context.US_PSX.ToString() )
            {
                List<byte> amBytes = new List<byte>( fileList["actionMenus"] );
                amBytes.AddRange( PSPResources.ActionEventsBin.Sub( 0xE0, 0xE2 ) );
                fileList["actionMenus"] = amBytes.ToArray();

                AllJobs aj = new AllJobs( Context.US_PSX, fileList["jobs"] );
                List<Job> jobs = new List<Job>( aj.Jobs );
                AllJobs defaultPspJobs = new AllJobs( Context.US_PSP, PSPResources.JobsBin );
                for( int i = 0; i < jobs.Count; i++ )
                {
                    jobs[i].Equipment.Unknown1 = defaultPspJobs.Jobs[i].Equipment.Unknown1;
                    jobs[i].Equipment.Unknown2 = defaultPspJobs.Jobs[i].Equipment.Unknown2;
                    jobs[i].Equipment.Unknown3 = defaultPspJobs.Jobs[i].Equipment.Unknown3;
                    jobs[i].Equipment.FellSword = defaultPspJobs.Jobs[i].Equipment.FellSword;
                    jobs[i].Equipment.LipRouge = defaultPspJobs.Jobs[i].Equipment.LipRouge;
                    jobs[i].Equipment.Unknown6 = defaultPspJobs.Jobs[i].Equipment.Unknown6;
                    jobs[i].Equipment.Unknown7 = defaultPspJobs.Jobs[i].Equipment.Unknown7;
                    jobs[i].Equipment.Unknown8 = defaultPspJobs.Jobs[i].Equipment.Unknown8;
                }
                for( int i = 160; i < 169; i++ )
                {
                    jobs.Add( defaultPspJobs.Jobs[i] );
                }
                ReflectionHelpers.SetFieldOrProperty( aj, "Jobs", jobs.ToArray() );
                fileList["jobs"] = aj.ToByteArray( Context.US_PSP );

                JobLevels jl = new JobLevels( Context.US_PSX, fileList["jobLevels"] );
                JobLevels pspJobLevels = new JobLevels( Context.US_PSP, PSPResources.JobLevelsBin );
                foreach( string jobName in new string[19] { "Archer", "Arithmetician", "Bard", "BlackMage", "Chemist", "Dancer", "Dragoon", "Geomancer",
                            "Knight", "Mime", "Monk", "Mystic", "Ninja", "Orator", "Samurai", "Summoner", "Thief", "TimeMage", "WhiteMage" } )
                {
                    Requirements psxR = ReflectionHelpers.GetFieldOrProperty<Requirements>( jl, jobName );
                    Requirements pspR = ReflectionHelpers.GetFieldOrProperty<Requirements>( pspJobLevels, jobName );
                    psxR.Unknown1 = pspR.Unknown1;
                    psxR.Unknown2 = pspR.Unknown2;
                    psxR.DarkKnight = pspR.DarkKnight;
                    psxR.OnionKnight = pspR.OnionKnight;
                }
                ReflectionHelpers.SetFieldOrProperty( jl, "OnionKnight", pspJobLevels.OnionKnight );
                ReflectionHelpers.SetFieldOrProperty( jl, "DarkKnight", pspJobLevels.DarkKnight );
                ReflectionHelpers.SetFieldOrProperty( jl, "Unknown", pspJobLevels.Unknown );
                fileList["jobLevels"] = jl.ToByteArray( Context.US_PSP );

                List<byte> ssBytes = new List<byte>( fileList["skillSets"] );
                ssBytes.AddRange( PSPResources.SkillSetsBin.Sub( ssBytes.Count ) );
                fileList["skillSets"] = ssBytes.ToArray();

                fileList["entd5"] = PSPResources.ENTD5;


                if( Utilities.CompareArrays( fileList["font"], PSXResources.FontBin ) )
                {
                    fileList["font"] = PSPResources.FontBin;
                }
                if( Utilities.CompareArrays( fileList["fontWidths"], PSXResources.FontWidthsBin ) )
                {
                    fileList["fontWidths"] = PSPResources.FontWidthsBin;
                }

                fileList["type"] = Encoding.UTF8.GetBytes( Context.US_PSP.ToString() );

                fileList["pspItemAttributes"] = PSPResources.NewItemAttributesBin;
                fileList["pspItems"] = PSPResources.NewItemsBin;
            }

            using( FileStream outFile = new FileStream( filename, FileMode.Create, FileAccess.ReadWrite ) )
            using( ZipOutputStream output = new ZipOutputStream( outFile ) )
            {
                foreach( KeyValuePair<string, byte[]> entry in fileList )
                {
                    WriteFileToZip( output, entry.Key, entry.Value );
                }
            }
        }

        public static void GenerateDigest( string filename )
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            StringBuilder sb = new StringBuilder();

            using( XmlWriter writer = XmlWriter.Create( sb, settings ) )
            {
                writer.WriteStartElement( "digest" );
                IXmlDigest[] digestable = new IXmlDigest[] {
                    Abilities, Items, ItemAttributes, Jobs, JobLevels, SkillSets, MonsterSkills, ActionMenus, StatusAttributes,
                    InflictStatuses, PoachProbabilities, ENTDs, MoveFind };
                foreach( IXmlDigest digest in digestable )
                {
                    digest.WriteXml( writer );
                }
                writer.WriteEndElement();
            }


#if DEBUG
            using( FileStream stream = new FileStream( filename + ".xml", FileMode.Create ) )
            {
                byte[] bytes = sb.ToString().ToByteArray();
                stream.Write( bytes, 0, bytes.Length );
            }
#endif

            settings.ConformanceLevel = ConformanceLevel.Fragment;
            using( MemoryStream memoryStream = new MemoryStream( Resources.ZipFileContents[Resources.Paths.DigestTransform] ) )
            using( XmlReader transformXmlReader = XmlReader.Create( memoryStream ) )
            using( StringReader inputReader = new StringReader( sb.ToString() ) )
            using( XmlReader inputXmlReader = XmlReader.Create( inputReader ) )
            using( XmlWriter outputWriter = XmlWriter.Create( filename, settings ) )
            {
                System.Xml.Xsl.XslCompiledTransform t = new System.Xml.Xsl.XslCompiledTransform();
                t.Load( transformXmlReader );
                t.Transform( inputXmlReader, outputWriter );
            }
        }

        /// <summary>
        /// Reads an XML fftpatch file.
        /// </summary>
        public static void LoadPatch( string filename )
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load( filename );
                LoadOldStylePatch( doc );
            }
            catch ( XmlException )
            {
                // Is new format file
                LoadNewStylePatch( filename );
            }
            FireDataChangedEvent();
        }

        /// <summary>
        /// Builds a new (unmodified) patch from a context.
        /// </summary>
        public static void New( Context context )
        {
            Context = context;
            BuildFromContext();
            FireDataChangedEvent();
        }

        public static void OpenPatchedISO( string filename )
        {
            using( FileStream stream = new FileStream( filename, FileMode.Open, FileAccess.Read ) )
            {
                Context = Context.US_PSX;
                LoadDataFromBytes(
                    PsxIso.GetBlock( stream, PsxIso.Abilities ),
                    PsxIso.GetBlock( stream, PsxIso.AbilityEffects ),
                    PsxIso.GetBlock( stream, PsxIso.OldItems ),
                    PsxIso.GetBlock( stream, PsxIso.OldItemAttributes ),
                    null,
                    null,
                    PsxIso.GetBlock( stream, PsxIso.Jobs ),
                    PsxIso.GetBlock( stream, PsxIso.JobLevels ),
                    PsxIso.GetBlock( stream, PsxIso.SkillSets ),
                    PsxIso.GetBlock( stream, PsxIso.MonsterSkills ),
                    PsxIso.GetBlock( stream, PsxIso.ActionEvents ),
                    PsxIso.GetBlock( stream, PsxIso.StatusAttributes ),
                    PsxIso.GetBlock( stream, PsxIso.InflictStatuses ),
                    PsxIso.GetBlock( stream, PsxIso.PoachProbabilities ),
                    PsxIso.GetBlock( stream, PsxIso.ENTD1 ),
                    PsxIso.GetBlock( stream, PsxIso.ENTD2 ),
                    PsxIso.GetBlock( stream, PsxIso.ENTD3 ),
                    PsxIso.GetBlock( stream, PsxIso.ENTD4 ),
                    null,
                    PsxIso.GetBlock( stream, PsxIso.Font ),
                    PsxIso.GetBlock( stream, PsxIso.FontWidths ),
                    PsxIso.GetBlock( stream, PsxIso.MoveFindItems ),
                    PsxIso.GetBlock( stream, PsxIso.StoreInventories ) );
                FireDataChangedEvent();
            }
        }

        /// <summary>
        /// Saves this patch to an XML document.
        /// </summary>
        public static void SavePatchToFile( string path )
        {
            SavePatchToFile( path, FFTPatch.Context, true );
        }

        /// <summary>
        /// Saves this patch to an XML document.
        /// </summary>
        public static void SavePatchToFile( string path, Context destinationContext, bool saveDigest )
        {
            SaveZippedPatch( path, destinationContext );
            if ( saveDigest )
            {
                GenerateDigest( Path.Combine( Path.GetDirectoryName( path ), Path.GetFileNameWithoutExtension( path ) + ".digest.html" ) );
            }
        }

		#endregion Public Methods 

		#region Private Methods (11) 

        private static void BuildFromContext()
        {
            switch( Context )
            {
                case Context.US_PSP:
                    Abilities = new AllAbilities( PSPResources.AbilitiesBin, PSPResources.AbilityEffectsBin );
                    Items = new AllItems(
                        PSPResources.OldItemsBin,
                        PSPResources.NewItemsBin );
                    ItemAttributes = new AllItemAttributes(
                        PSPResources.OldItemAttributesBin,
                        PSPResources.NewItemAttributesBin );
                    Jobs = new AllJobs( Context, PSPResources.JobsBin );
                    JobLevels = new JobLevels( Context, PSPResources.JobLevelsBin,
                        new JobLevels( Context, PSPResources.JobLevelsBin ) );
                    SkillSets = new AllSkillSets( Context, PSPResources.SkillSetsBin,
                        PSPResources.SkillSetsBin );
                    MonsterSkills = new AllMonsterSkills( PSPResources.MonsterSkillsBin );
                    ActionMenus = new AllActionMenus( PSPResources.ActionEventsBin, Context );
                    StatusAttributes = new AllStatusAttributes( PSPResources.StatusAttributesBin );
                    InflictStatuses = new AllInflictStatuses( PSPResources.InflictStatusesBin );
                    PoachProbabilities = new AllPoachProbabilities( PSPResources.PoachProbabilitiesBin );
                    Font = new FFTFont( PSPResources.FontBin, PSPResources.FontWidthsBin );
                    ENTDs = new AllENTDs( PSPResources.ENTD1, PSPResources.ENTD2, PSPResources.ENTD3, PSPResources.ENTD4, PSPResources.ENTD5 );
                    MoveFind = new AllMoveFindItems( Context, PSPResources.MoveFind, new AllMoveFindItems( Context, PSPResources.MoveFind ) );
                    StoreInventories = new AllStoreInventories( Context, PSPResources.StoreInventoriesBin, PSPResources.StoreInventoriesBin );
                    break;
                case Context.US_PSX:
                    Abilities = new AllAbilities( PSXResources.AbilitiesBin, PSXResources.AbilityEffectsBin );
                    Items = new AllItems( PSXResources.OldItemsBin, null );
                    ItemAttributes = new AllItemAttributes( PSXResources.OldItemAttributesBin, null );
                    Jobs = new AllJobs( Context, PSXResources.JobsBin );
                    JobLevels = new JobLevels( Context, PSXResources.JobLevelsBin,
                        new JobLevels( Context, PSXResources.JobLevelsBin ) );
                    SkillSets = new AllSkillSets( Context, PSXResources.SkillSetsBin,
                        PSXResources.SkillSetsBin );
                    MonsterSkills = new AllMonsterSkills( PSXResources.MonsterSkillsBin );
                    ActionMenus = new AllActionMenus( PSXResources.ActionEventsBin, Context );
                    StatusAttributes = new AllStatusAttributes( PSXResources.StatusAttributesBin );
                    InflictStatuses = new AllInflictStatuses( PSXResources.InflictStatusesBin );
                    PoachProbabilities = new AllPoachProbabilities( PSXResources.PoachProbabilitiesBin );
                    Font = new FFTFont( PSXResources.FontBin, PSXResources.FontWidthsBin );
                    ENTDs = new AllENTDs( PSXResources.ENTD1, PSXResources.ENTD2, PSXResources.ENTD3, PSXResources.ENTD4 );
                    MoveFind = new AllMoveFindItems( Context, PSXResources.MoveFind, new AllMoveFindItems( Context, PSXResources.MoveFind ) );
                    StoreInventories = new AllStoreInventories( Context, PSXResources.StoreInventoriesBin, PSXResources.StoreInventoriesBin );
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        private static void FireDataChangedEvent()
        {
            if( DataChanged != null )
            {
                DataChanged( null, EventArgs.Empty );
            }
        }

        private static StringBuilder GetBase64StringIfNonDefault( byte[] bytes, byte[] def )
        {
            if( !Utilities.CompareArrays( bytes, def ) )
            {
                return new StringBuilder( Convert.ToBase64String( bytes, Base64FormattingOptions.InsertLineBreaks ) );
            }
            return null;
        }

        private static byte[] GetFromNodeOrReturnDefault( XmlNode node, string name, byte[] def )
        {
            XmlNode n = node.SelectSingleNode( name );
            if( n != null )
            {
                try
                {
                    byte[] result = Convert.FromBase64String( n.InnerText );
                    return result;
                }
                catch( Exception )
                {
                }
            }

            return def;
        }

        private static byte[] GetZipEntry( ZipFile file, string entry )
        {
            if ( file.FindEntry( entry, false ) == -1 )
            {
                throw new FormatException( "entry not found" );
            }
            else
            {
                ZipEntry zEntry = file.GetEntry( entry );
                Stream s = file.GetInputStream( zEntry );
                byte[] result = new byte[zEntry.Size];
                StreamUtils.ReadFully( s, result );
                return result;
            }
        }

        private static void LoadDataFromBytes(
            byte[] abilities, byte[] abilityEffects,
            byte[] oldItems, byte[] oldItemAttributes,
            byte[] newItems, byte[] newItemAttributes,
            byte[] jobs, byte[] jobLevels,
            byte[] skillSets, byte[] monsterSkills,
            byte[] actionMenus,
            byte[] statusAttributes, byte[] inflictStatuses,
            byte[] poach,
            byte[] entd1, byte[] entd2, byte[] entd3, byte[] entd4, byte[] entd5,
            byte[] font, byte[] fontWidths,
            byte[] moveFind,
            byte[] inventories )
        {
            try
            {
                bool psp = Context == Context.US_PSP;
                var Abilities = new AllAbilities( abilities, abilityEffects );
                var Items = new AllItems( oldItems, newItems != null ? newItems : null );
                var ItemAttributes = new AllItemAttributes( oldItemAttributes, newItemAttributes != null ? newItemAttributes : null );
                var Jobs = new AllJobs( Context, jobs );
                var JobLevels = new JobLevels( Context, jobLevels,
                    new JobLevels( Context, Context == Context.US_PSP ? PSPResources.JobLevelsBin : PSXResources.JobLevelsBin ) );
                var SkillSets = new AllSkillSets( Context, skillSets,
                    Context == Context.US_PSP ? PSPResources.SkillSetsBin : PSXResources.SkillSetsBin );
                var MonsterSkills = new AllMonsterSkills( monsterSkills );
                var ActionMenus = new AllActionMenus( actionMenus, Context );
                var StatusAttributes = new AllStatusAttributes( statusAttributes );
                var InflictStatuses = new AllInflictStatuses( inflictStatuses );
                var PoachProbabilities = new AllPoachProbabilities( poach );
                var ENTDs = psp ? new AllENTDs( entd1, entd2, entd3, entd4, entd5 ) : new AllENTDs( entd1, entd2, entd3, entd4 );
                var Font = new FFTFont( font, fontWidths );
                var MoveFind = new AllMoveFindItems( Context, moveFind, new AllMoveFindItems( Context, psp ? PSPResources.MoveFind : PSXResources.MoveFind ) );
                var StoreInventories = new AllStoreInventories( Context, inventories, psp ? PSPResources.StoreInventoriesBin : PSXResources.StoreInventoriesBin );
                FFTPatch.Abilities = Abilities;
                FFTPatch.Items = Items;
                FFTPatch.ItemAttributes = ItemAttributes;
                FFTPatch.Jobs = Jobs;
                FFTPatch.JobLevels = JobLevels;
                FFTPatch.SkillSets = SkillSets;
                FFTPatch.MonsterSkills = MonsterSkills;
                FFTPatch.ActionMenus = ActionMenus;
                FFTPatch.StatusAttributes = StatusAttributes;
                FFTPatch.InflictStatuses = InflictStatuses;
                FFTPatch.PoachProbabilities = PoachProbabilities;
                FFTPatch.ENTDs = ENTDs;
                FFTPatch.Font = Font;
                FFTPatch.MoveFind = MoveFind;
                FFTPatch.StoreInventories = StoreInventories;
            }
            catch( Exception )
            {
                throw new LoadPatchException();
            }
        }

        private static void LoadNewStylePatch( string filename )
        {
            using ( ZipFile file = new ZipFile( filename ) )
            {
                string fileVersion = Encoding.UTF8.GetString( GetZipEntry( file, "version" ) );
                Context = (Context)Enum.Parse( typeof( Context ), Encoding.UTF8.GetString( GetZipEntry( file, "type" ) ) );
                bool psp = Context == Context.US_PSP;


                LoadDataFromBytes(
                    GetZipEntry( file, elementNames[ElementName.Abilities] ),
                    GetZipEntry( file, elementNames[ElementName.AbilityEffects] ),
                    GetZipEntry( file, elementNames[ElementName.Items] ),
                    GetZipEntry( file, elementNames[ElementName.ItemAttributes] ),
                    psp ? GetZipEntry( file, elementNames[ElementName.PSPItems] ) : null,
                    psp ? GetZipEntry( file, elementNames[ElementName.PSPItemAttributes] ) : null,
                    GetZipEntry( file, elementNames[ElementName.Jobs] ),
                    GetZipEntry( file, elementNames[ElementName.JobLevels] ),
                    GetZipEntry( file, elementNames[ElementName.SkillSets] ),
                    GetZipEntry( file, elementNames[ElementName.MonsterSkills] ),
                    GetZipEntry( file, elementNames[ElementName.ActionMenus] ),
                    GetZipEntry( file, elementNames[ElementName.StatusAttributes] ),
                    GetZipEntry( file, elementNames[ElementName.InflictStatuses] ),
                    GetZipEntry( file, elementNames[ElementName.Poaching] ),
                    GetZipEntry( file, elementNames[ElementName.ENTD1] ),
                    GetZipEntry( file, elementNames[ElementName.ENTD2] ),
                    GetZipEntry( file, elementNames[ElementName.ENTD3] ),
                    GetZipEntry( file, elementNames[ElementName.ENTD4] ),
                    psp ? GetZipEntry( file, elementNames[ElementName.ENTD5] ) : null,
                    GetZipEntry( file, elementNames[ElementName.Font] ),
                    GetZipEntry( file, elementNames[ElementName.FontWidths] ),
                    GetZipEntry( file, elementNames[ElementName.MoveFindItems] ),
                    GetZipEntry( file, elementNames[ElementName.StoreInventories] ) );
            }
        }

        private static void LoadOldStylePatch( XmlDocument doc )
        {
            XmlNode rootNode = doc.SelectSingleNode( "/patch" );
            string type = rootNode.Attributes["type"].InnerText;
            Context = (Context)Enum.Parse( typeof( Context ), type );
            bool psp = Context == Context.US_PSP;

            byte[] abilities = GetFromNodeOrReturnDefault( rootNode, "abilities", psp ? PSPResources.AbilitiesBin : PSXResources.AbilitiesBin );
            byte[] abilityEffects = GetFromNodeOrReturnDefault( rootNode, "abilityEffects", psp ? PSPResources.AbilityEffectsBin : PSXResources.AbilityEffectsBin );

            byte[] oldItems = GetFromNodeOrReturnDefault( rootNode, "items", psp ? PSPResources.OldItemsBin : PSXResources.OldItemsBin );
            byte[] oldItemAttributes = GetFromNodeOrReturnDefault( rootNode, "itemAttributes", psp ? PSPResources.OldItemAttributesBin : PSXResources.OldItemAttributesBin );
            byte[] newItems = psp ? GetFromNodeOrReturnDefault( rootNode, "pspItems", PSPResources.NewItemsBin ) : null;
            byte[] newItemAttributes = psp ? GetFromNodeOrReturnDefault( rootNode, "pspItemAttributes", PSPResources.NewItemAttributesBin ) : null;
            byte[] jobs = GetFromNodeOrReturnDefault( rootNode, "jobs", psp ? PSPResources.JobsBin : PSXResources.JobsBin );
            byte[] jobLevels = GetFromNodeOrReturnDefault( rootNode, "jobLevels", psp ? PSPResources.JobLevelsBin : PSXResources.JobLevelsBin );
            byte[] skillSets = GetFromNodeOrReturnDefault( rootNode, "skillSets", psp ? PSPResources.SkillSetsBin : PSXResources.SkillSetsBin );
            byte[] monsterSkills = GetFromNodeOrReturnDefault( rootNode, "monsterSkills", psp ? PSPResources.MonsterSkillsBin : PSXResources.MonsterSkillsBin );
            byte[] actionMenus = GetFromNodeOrReturnDefault( rootNode, "actionMenus", psp ? PSPResources.ActionEventsBin : PSXResources.ActionEventsBin );
            byte[] statusAttributes = GetFromNodeOrReturnDefault( rootNode, "statusAttributes", psp ? PSPResources.StatusAttributesBin : PSXResources.StatusAttributesBin );
            byte[] inflictStatuses = GetFromNodeOrReturnDefault( rootNode, "inflictStatuses", psp ? PSPResources.InflictStatusesBin : PSXResources.InflictStatusesBin );
            byte[] poach = GetFromNodeOrReturnDefault( rootNode, "poaching", psp ? PSPResources.PoachProbabilitiesBin : PSXResources.PoachProbabilitiesBin );
            byte[] entd1 = GetFromNodeOrReturnDefault( rootNode, "entd1", PSPResources.ENTD1 );
            byte[] entd2 = GetFromNodeOrReturnDefault( rootNode, "entd2", PSPResources.ENTD2 );
            byte[] entd3 = GetFromNodeOrReturnDefault( rootNode, "entd3", PSPResources.ENTD3 );
            byte[] entd4 = GetFromNodeOrReturnDefault( rootNode, "entd4", PSPResources.ENTD4 );
            byte[] entd5 = GetFromNodeOrReturnDefault( rootNode, "entd5", PSPResources.ENTD5 );
            byte[] font = GetFromNodeOrReturnDefault( rootNode, "font", psp ? PSPResources.FontBin : PSXResources.FontBin );
            byte[] fontWidths = GetFromNodeOrReturnDefault( rootNode, "fontWidths", psp ? PSPResources.FontWidthsBin : PSXResources.FontWidthsBin );
            byte[] moveFind = GetFromNodeOrReturnDefault( rootNode, "moveFindItems", psp ? PSPResources.MoveFind : PSXResources.MoveFind );
            byte[] inventories = GetFromNodeOrReturnDefault( rootNode, "storeInventories", psp ? PSPResources.StoreInventoriesBin : PSXResources.StoreInventoriesBin );

            LoadDataFromBytes( abilities, abilityEffects, oldItems, oldItemAttributes, newItems, newItemAttributes,
                jobs, jobLevels, skillSets, monsterSkills, actionMenus, statusAttributes,
                inflictStatuses, poach, entd1, entd2, entd3, entd4, entd5, font,
                fontWidths, moveFind, inventories );
        }

        private static string ReadString( FileStream stream, int length )
        {
            byte[] bytes = new byte[length];
            stream.Read( bytes, 0, length );
            StringBuilder result = new StringBuilder();
            foreach( byte b in bytes )
            {
                result.Append( Convert.ToChar( b ) );
            }

            return result.ToString();
        }

        private static void SaveZippedPatch( string path, Context destinationContext )
        {
            using ( ZipOutputStream stream = new ZipOutputStream( File.Open( path, FileMode.Create, FileAccess.ReadWrite ) ) )
            {
                const string fileVersion = "1.0";

                bool psp = destinationContext == Context.US_PSP;
                WriteFileToZip( stream, "version", Encoding.UTF8.GetBytes( fileVersion ) );
                WriteFileToZip( stream, "type", Encoding.UTF8.GetBytes( destinationContext.ToString() ) );

                WriteFileToZip( stream, elementNames[ElementName.Abilities], Abilities.ToByteArray( destinationContext ) );
                WriteFileToZip( stream, elementNames[ElementName.AbilityEffects], Abilities.ToEffectsByteArray() );
                WriteFileToZip( stream, elementNames[ElementName.Items], Items.ToFirstByteArray() );
                WriteFileToZip( stream, elementNames[ElementName.ItemAttributes], ItemAttributes.ToFirstByteArray() );
                if ( psp && Context == Context.US_PSP )
                {
                    WriteFileToZip( stream, elementNames[ElementName.PSPItems], Items.ToSecondByteArray() );
                    WriteFileToZip( stream, elementNames[ElementName.PSPItemAttributes], ItemAttributes.ToSecondByteArray() );
                    WriteFileToZip( stream, elementNames[ElementName.ENTD5], ENTDs.PSPEventsToByteArray() );
                }
                WriteFileToZip( stream, elementNames[ElementName.Jobs], Jobs.ToByteArray( destinationContext ) );
                WriteFileToZip( stream, elementNames[ElementName.JobLevels], JobLevels.ToByteArray( destinationContext ) );
                WriteFileToZip( stream, elementNames[ElementName.MonsterSkills], MonsterSkills.ToByteArray( destinationContext ) );
                WriteFileToZip( stream, elementNames[ElementName.SkillSets], SkillSets.ToByteArray( destinationContext ) );
                WriteFileToZip( stream, elementNames[ElementName.ActionMenus], ActionMenus.ToByteArray( destinationContext ) );
                WriteFileToZip( stream, elementNames[ElementName.StatusAttributes], StatusAttributes.ToByteArray( destinationContext ) );
                WriteFileToZip( stream, elementNames[ElementName.InflictStatuses], InflictStatuses.ToByteArray() );
                WriteFileToZip( stream, elementNames[ElementName.Poaching], PoachProbabilities.ToByteArray( destinationContext ) );
                WriteFileToZip( stream, elementNames[ElementName.ENTD1], ENTDs.ENTDs[0].ToByteArray() );
                WriteFileToZip( stream, elementNames[ElementName.ENTD2], ENTDs.ENTDs[1].ToByteArray() );
                WriteFileToZip( stream, elementNames[ElementName.ENTD3], ENTDs.ENTDs[2].ToByteArray() );
                WriteFileToZip( stream, elementNames[ElementName.ENTD4], ENTDs.ENTDs[3].ToByteArray() );
                WriteFileToZip( stream, elementNames[ElementName.Font], Font.ToByteArray() );
                WriteFileToZip( stream, elementNames[ElementName.FontWidths], Font.ToWidthsByteArray() );
                WriteFileToZip( stream, elementNames[ElementName.MoveFindItems], MoveFind.ToByteArray() );
                WriteFileToZip( stream, elementNames[ElementName.StoreInventories], StoreInventories.ToByteArray() );
            }
        }

        private static void WriteFileToZip( ZipOutputStream stream, string filename, byte[] bytes )
        {
            stream.PutNextEntry( new ZipEntry( filename ) );
            stream.Write( bytes, 0, bytes.Length );
        }

		#endregion Private Methods 

        private enum ElementName
        {
            Abilities,
            AbilityEffects,
            Items,
            ItemAttributes,
            PSPItems,
            PSPItemAttributes,
            Jobs,
            JobLevels,
            SkillSets,
            MonsterSkills,
            ActionMenus,
            InflictStatuses,
            StatusAttributes,
            Poaching,
            ENTD1,
            ENTD2,
            ENTD3,
            ENTD4,
            ENTD5,
            Font,
            FontWidths,
            MoveFindItems,
            StoreInventories
        }

        public static event EventHandler DataChanged;

        public class LoadPatchException : Exception
        {

}
    }
}