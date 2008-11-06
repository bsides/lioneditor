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

		#region Static Fields (2) 

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
            ElementName.MoveFindItems, "moveFindItems" } );
        private static string[] elementNameStrings = new string[] {
            "abilities", "abilityEffects", "items", "itemAttributes", "pspItems", "pspItemAttributes", "jobs", "jobLevels",
            "skillSets", "monsterSkills", "actionMenus", "inflictStatuses", "statusAttributes", "poaching",
            "entd1", "entd2", "entd3", "entd4", "entd5", "font", "fontWidths", "moveFindItems" };

		#endregion Static Fields 

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
            MoveFindItems
        }


		#region Static Properties (14) 


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

        public static AllPoachProbabilities PoachProbabilities { get; private set; }

        public static AllSkillSets SkillSets { get; private set; }

        public static AllStatusAttributes StatusAttributes { get; private set; }


		#endregion Static Properties 

		#region Events (1) 

        public static event EventHandler DataChanged;

		#endregion Events 

		#region Methods (19) 


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
                    break;
                default:
                    throw new ArgumentException();
            }
        }
        public static AllMoveFindItems MoveFind { get; private set; }

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

        public static void ConvertPsxPatchToPsp( XmlNode document )
        {
            Action<StringBuilder> sbPrettifier = new Action<StringBuilder>(
                delegate( StringBuilder sb )
                {
                    if( sb != null )
                    {
                        sb.Insert( 0, "\r\n" );
                        sb.Replace( "\r\n", "\r\n    " );
                        sb.Append( "\r\n  " );
                    }
                } );

            XmlNode rootNode = document.SelectSingleNode( "patch" );
            XmlNode typeNode = rootNode.SelectSingleNode( "@type" );

            if( typeNode.InnerText == Context.US_PSX.ToString() )
            {
                typeNode.InnerText = Context.US_PSP.ToString();

                XmlNode actionMenusNode = rootNode.SelectSingleNode( elementNames[ElementName.ActionMenus] );
                if( actionMenusNode != null )
                {
                    // Action Menus 224->227
                    List<byte> amBytes = new List<byte>( Convert.FromBase64String( actionMenusNode.InnerText ) );
                    amBytes.AddRange( PSPResources.ActionEventsBin.Sub( 0xE0, 0xE2 ) );
                    StringBuilder amBytesString = new StringBuilder( Convert.ToBase64String( amBytes.ToArray(), Base64FormattingOptions.InsertLineBreaks ) );
                    sbPrettifier( amBytesString );
                    actionMenusNode.InnerText = amBytesString.ToString();
                }

                XmlNode jobsNode = rootNode.SelectSingleNode( elementNames[ElementName.Jobs] );
                if( jobsNode != null )
                {
                    // Jobs 160->169, 48 bytes->49 bytes
                    AllJobs aj = new AllJobs( Context.US_PSX, Convert.FromBase64String( jobsNode.InnerText ) );
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
                    StringBuilder jobsBytesString = new StringBuilder( Convert.ToBase64String( aj.ToByteArray( Context.US_PSP ), Base64FormattingOptions.InsertLineBreaks ) );
                    sbPrettifier( jobsBytesString );
                    jobsNode.InnerText = jobsBytesString.ToString();
                }

                XmlNode jobLevelsNode = rootNode.SelectSingleNode( elementNames[ElementName.JobLevels] );
                if( jobLevelsNode != null )
                {
                    // JobLevels, 208 bytes->280 bytes (Requirements 10 bytes->12 bytes)
                    JobLevels jl = new JobLevels( Context.US_PSX, Convert.FromBase64String( jobLevelsNode.InnerText ) );
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

                    StringBuilder levelsBytesString = new StringBuilder( Convert.ToBase64String( jl.ToByteArray( Context.US_PSP ), Base64FormattingOptions.InsertLineBreaks ) );
                    sbPrettifier( levelsBytesString );
                    jobLevelsNode.InnerText = levelsBytesString.ToString();
                }

                XmlNode skillSetsNode = rootNode.SelectSingleNode( elementNames[ElementName.SkillSets] );
                if( skillSetsNode != null )
                {
                    // Skillsets, 176->179
                    List<byte> ssBytes = new List<byte>( Convert.FromBase64String( skillSetsNode.InnerText ) );
                    ssBytes.AddRange( PSPResources.SkillSetsBin.Sub( ssBytes.Count ) );
                    StringBuilder ssBytesString = new StringBuilder( Convert.ToBase64String( ssBytes.ToArray(), Base64FormattingOptions.InsertLineBreaks ) );
                    sbPrettifier( ssBytesString );
                    skillSetsNode.InnerText = ssBytesString.ToString();
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

            LoadDataFromBytes( abilities, abilityEffects, oldItems, oldItemAttributes, newItems, newItemAttributes,
                jobs, jobLevels, skillSets, monsterSkills, actionMenus, statusAttributes,
                inflictStatuses, poach, entd1, entd2, entd3, entd4, entd5, font,
                fontWidths, moveFind );
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
            byte[] moveFind )
        {
            bool psp = Context == Context.US_PSP;
            Abilities = new AllAbilities( abilities, abilityEffects );
            Items = new AllItems( oldItems, newItems != null ? newItems : null );
            ItemAttributes = new AllItemAttributes( oldItemAttributes, newItemAttributes != null ? newItemAttributes : null );
            Jobs = new AllJobs( Context, jobs );
            JobLevels = new JobLevels( Context, jobLevels,
                new JobLevels( Context, Context == Context.US_PSP ? PSPResources.JobLevelsBin : PSXResources.JobLevelsBin ) );
            SkillSets = new AllSkillSets( Context, skillSets,
                Context == Context.US_PSP ? PSPResources.SkillSetsBin : PSXResources.SkillSetsBin );
            MonsterSkills = new AllMonsterSkills( monsterSkills );
            ActionMenus = new AllActionMenus( actionMenus, Context );
            StatusAttributes = new AllStatusAttributes( statusAttributes );
            InflictStatuses = new AllInflictStatuses( inflictStatuses );
            PoachProbabilities = new AllPoachProbabilities( poach );
            ENTDs = psp ? new AllENTDs( entd1, entd2, entd3, entd4, entd5 ) : new AllENTDs( entd1, entd2, entd3, entd4 );
            Font = new FFTFont( font, fontWidths );
            MoveFind = new AllMoveFindItems( Context, moveFind, new AllMoveFindItems( Context, psp ? PSPResources.MoveFind : PSXResources.MoveFind ) );
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
                    PsxIso.GetBlock( stream, PsxIso.MoveFindItems ) );
                FireDataChangedEvent();
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
                    GetZipEntry( file, elementNames[ElementName.MoveFindItems] ) );
            }
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

        /// <summary>
        /// Builds a new (unmodified) patch from a context.
        /// </summary>
        public static void New( Context context )
        {
            Context = context;
            BuildFromContext();
            FireDataChangedEvent();
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
            }
        }

        private static void WriteFileToZip( ZipOutputStream stream, string filename, byte[] bytes )
        {
            stream.PutNextEntry( new ZipEntry( filename ) );
            stream.Write( bytes, 0, bytes.Length );
        }

		#endregion Methods 

    }
}