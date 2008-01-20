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
using System.IO;
using System.Text;
using System.Xml;

namespace FFTPatcher.Datatypes
{
    public static class FFTPatch
    {
        public static Context Context { get; private set; }
        public static AllAbilities Abilities { get; private set; }
        public static AllItems Items { get; private set; }
        public static AllItemAttributes ItemAttributes { get; private set; }
        public static AllJobs Jobs { get; private set; }
        public static JobLevels JobLevels { get; private set; }
        public static AllSkillSets SkillSets { get; private set; }
        public static AllMonsterSkills MonsterSkills { get; private set; }
        public static AllActionMenus ActionMenus { get; private set; }
        public static AllStatusAttributes StatusAttributes { get; private set; }
        public static AllInflictStatuses InflictStatuses { get; private set; }
        public static AllPoachProbabilities PoachProbabilities { get; private set; }
        public static AllENTDs ENTDs { get; private set; }
        public static FFTFont Font { get; private set; }

        public static event EventHandler DataChanged;

        private static void BuildFromContext()
        {
            switch( Context )
            {
                case Context.US_PSP:
                    Abilities = new AllAbilities( new SubArray<byte>( Resources.AbilitiesBin ) );
                    Items = new AllItems(
                        new SubArray<byte>( Resources.OldItemsBin ),
                        new SubArray<byte>( Resources.NewItemsBin ) );
                    ItemAttributes = new AllItemAttributes(
                        new SubArray<byte>( Resources.OldItemAttributesBin ),
                        new SubArray<byte>( Resources.NewItemAttributesBin ) );
                    Jobs = new AllJobs( Context, new SubArray<byte>( Resources.JobsBin ) );
                    JobLevels = new JobLevels( Context, new SubArray<byte>( Resources.JobLevelsBin ),
                        new JobLevels( Context, new SubArray<byte>( Resources.JobLevelsBin ) ) );
                    SkillSets = new AllSkillSets( Context, new SubArray<byte>( Resources.SkillSetsBin ),
                        new SubArray<byte>( Resources.SkillSetsBin ) );
                    MonsterSkills = new AllMonsterSkills( new SubArray<byte>( Resources.MonsterSkillsBin ) );
                    ActionMenus = new AllActionMenus( new SubArray<byte>( Resources.ActionEventsBin ) );
                    StatusAttributes = new AllStatusAttributes( new SubArray<byte>( Resources.StatusAttributesBin ) );
                    InflictStatuses = new AllInflictStatuses( new SubArray<byte>( Resources.InflictStatusesBin ) );
                    PoachProbabilities = new AllPoachProbabilities( new SubArray<byte>( Resources.PoachProbabilitiesBin ) );
                    Font = new FFTFont( new SubArray<byte>( Resources.FontBin ), new SubArray<byte>( Resources.FontWidthsBin ) );
                    break;
                case Context.US_PSX:
                    Abilities = new AllAbilities( new SubArray<byte>( PSXResources.AbilitiesBin ) );
                    Items = new AllItems( new SubArray<byte>( PSXResources.OldItemsBin ), null );
                    ItemAttributes = new AllItemAttributes( new SubArray<byte>( PSXResources.OldItemAttributesBin ), null );
                    Jobs = new AllJobs( Context, new SubArray<byte>( PSXResources.JobsBin ) );
                    JobLevels = new JobLevels( Context, new SubArray<byte>( PSXResources.JobLevelsBin ),
                        new JobLevels( Context, new SubArray<byte>( PSXResources.JobLevelsBin ) ) );
                    SkillSets = new AllSkillSets( Context, new SubArray<byte>( PSXResources.SkillSetsBin ),
                        new SubArray<byte>( PSXResources.SkillSetsBin ) );
                    MonsterSkills = new AllMonsterSkills( new SubArray<byte>( PSXResources.MonsterSkillsBin ) );
                    ActionMenus = new AllActionMenus( new SubArray<byte>( PSXResources.ActionEventsBin ) );
                    StatusAttributes = new AllStatusAttributes( new SubArray<byte>( PSXResources.StatusAttributesBin ) );
                    InflictStatuses = new AllInflictStatuses( new SubArray<byte>( PSXResources.InflictStatusesBin ) );
                    PoachProbabilities = new AllPoachProbabilities( new SubArray<byte>( PSXResources.PoachProbabilitiesBin ) );
                    Font = new FFTFont( new SubArray<byte>( PSXResources.FontBin ), new SubArray<byte>( PSXResources.FontWidthsBin ) );
                    break;
                default:
                    throw new ArgumentException();
            }
            ENTDs = new AllENTDs( Resources.ENTD1, Resources.ENTD2, Resources.ENTD3, Resources.ENTD4 );
        }

        private static StringBuilder GetBase64StringIfNonDefault( byte[] bytes, byte[] def )
        {
            if( !Utilities.CompareArrays( bytes, def ) )
            {
                return new StringBuilder( Convert.ToBase64String( bytes, Base64FormattingOptions.InsertLineBreaks ) );
            }
            return null;
        }

        /// <summary>
        /// Saves the fonts to the path.
        /// </summary>
        public static void SaveFontsAs( string path )
        {
            FileStream writer = null;
            try
            {
                writer = new FileStream( path, FileMode.Create );
                byte[] bytes = FFTPatch.Font.ToByteArray();
                writer.Write( bytes, 0, bytes.Length );
                writer.Flush();
                writer.Close();
                writer = null;
            }
            catch( Exception )
            {
                throw;
            }
            finally
            {
                if( writer != null )
                {
                    writer.Flush();
                    writer.Close();
                }
            }
        }

        /// <summary>
        /// Saves the ENTD files to a path.
        /// </summary>
        public static void ExportENTDFiles( string path )
        {
            FileStream writer = null;
            try
            {
                byte[] entd1 = ENTDs.ENTDs[0].ToByteArray();
                byte[] entd2 = ENTDs.ENTDs[1].ToByteArray();
                byte[] entd3 = ENTDs.ENTDs[2].ToByteArray();
                byte[] entd4 = ENTDs.ENTDs[3].ToByteArray();

                writer = new FileStream( Path.Combine( path, "ENTD1.ENT" ), FileMode.Create );
                writer.Write( entd1, 0, entd1.Length );
                writer.Flush();
                writer.Close();
                writer = new FileStream( Path.Combine( path, "ENTD2.ENT" ), FileMode.Create );
                writer.Write( entd2, 0, entd2.Length );
                writer.Flush();
                writer.Close();
                writer = new FileStream( Path.Combine( path, "ENTD3.ENT" ), FileMode.Create );
                writer.Write( entd3, 0, entd3.Length );
                writer.Flush();
                writer.Close();
                writer = new FileStream( Path.Combine( path, "ENTD4.ENT" ), FileMode.Create );
                writer.Write( entd4, 0, entd4.Length );
                writer.Flush();
                writer.Close();

                if( Context == Context.US_PSP )
                {
                    writer = new FileStream( Path.Combine( path, "ENTD5.ENT" ), FileMode.Create );
                    foreach( Event e in ENTDs.PSPEvent )
                    {
                        byte[] bytes = e.ToByteArray();
                        writer.Write( bytes, 0, bytes.Length );
                    }
                    writer.Write( new byte[0x780], 0, 0x780 );
                    writer.Flush();
                    writer.Close();
                }

                writer = null;
            }
            catch( Exception )
            {
                throw;
            }
            finally
            {
                if( writer != null )
                {
                    writer.Flush();
                    writer.Close();
                }
            }            
        }

        /// <summary>
        /// Saves this patch to an XML document.
        /// </summary>
        public static void SavePatchToFile( string path )
        {
            bool psp = Context == Context.US_PSP;

            StringBuilder abilities = GetBase64StringIfNonDefault( Abilities.ToByteArray( Context ), psp ? Resources.AbilitiesBin : PSXResources.AbilitiesBin );
            StringBuilder oldItems = GetBase64StringIfNonDefault( Items.ToFirstByteArray(), psp ? Resources.OldItemsBin : PSXResources.OldItemsBin );
            StringBuilder oldItemAttributes = GetBase64StringIfNonDefault( ItemAttributes.ToFirstByteArray(), psp ? Resources.OldItemAttributesBin : PSXResources.OldItemAttributesBin );
            StringBuilder newItems = null;
            StringBuilder newItemAttributes = null;
            if( psp )
            {
                newItems = GetBase64StringIfNonDefault( Items.ToSecondByteArray(), Resources.NewItemsBin );
                newItemAttributes = GetBase64StringIfNonDefault( ItemAttributes.ToSecondByteArray(), Resources.NewItemAttributesBin );
            }
            StringBuilder jobs = GetBase64StringIfNonDefault( Jobs.ToByteArray( Context ), psp ? Resources.JobsBin : PSXResources.JobsBin );
            StringBuilder jobLevels = GetBase64StringIfNonDefault( JobLevels.ToByteArray( Context ), psp ? Resources.JobLevelsBin : PSXResources.JobLevelsBin );
            StringBuilder monsterSkills = GetBase64StringIfNonDefault( MonsterSkills.ToByteArray( Context ), psp ? Resources.MonsterSkillsBin : PSXResources.MonsterSkillsBin );
            StringBuilder skillSets = GetBase64StringIfNonDefault( SkillSets.ToByteArray( Context ), psp ? Resources.SkillSetsBin : PSXResources.SkillSetsBin );
            StringBuilder actionMenus = GetBase64StringIfNonDefault( ActionMenus.ToByteArray( Context ), psp ? Resources.ActionEventsBin : PSXResources.ActionEventsBin );
            StringBuilder statusAttributes = GetBase64StringIfNonDefault( StatusAttributes.ToByteArray( Context ), psp ? Resources.StatusAttributesBin : PSXResources.StatusAttributesBin );
            StringBuilder inflictStatuses = GetBase64StringIfNonDefault( InflictStatuses.ToByteArray(), psp ? Resources.InflictStatusesBin : PSXResources.InflictStatusesBin );
            StringBuilder poach = GetBase64StringIfNonDefault( PoachProbabilities.ToByteArray( Context ), psp ? Resources.PoachProbabilitiesBin : PSXResources.PoachProbabilitiesBin );
            StringBuilder entd1 = GetBase64StringIfNonDefault( ENTDs.ENTDs[0].ToByteArray(), Resources.ENTD1 );
            StringBuilder entd2 = GetBase64StringIfNonDefault( ENTDs.ENTDs[1].ToByteArray(), Resources.ENTD2 );
            StringBuilder entd3 = GetBase64StringIfNonDefault( ENTDs.ENTDs[2].ToByteArray(), Resources.ENTD3 );
            StringBuilder entd4 = GetBase64StringIfNonDefault( ENTDs.ENTDs[3].ToByteArray(), Resources.ENTD4 );
            StringBuilder font = GetBase64StringIfNonDefault( Font.ToByteArray(), psp ? Resources.FontBin : PSXResources.FontBin );
            StringBuilder fontWidths = GetBase64StringIfNonDefault( Font.ToWidthsByteArray(), psp ? Resources.FontWidthsBin : PSXResources.FontWidthsBin );
            XmlTextWriter writer = null;
            try
            {
                writer = new XmlTextWriter( path, System.Text.Encoding.UTF8 );
                writer.Formatting = Formatting.Indented;
                writer.IndentChar = ' ';
                writer.Indentation = 2;
                writer.WriteStartDocument();
                writer.WriteStartElement( "patch" );
                writer.WriteAttributeString( "type", Context.ToString() );

                StringBuilder[] builders = new StringBuilder[] { 
                    abilities, oldItems, oldItemAttributes, newItems, newItemAttributes, jobs, jobLevels, 
                    skillSets, monsterSkills, actionMenus, inflictStatuses, statusAttributes, poach,
                    entd1, entd2, entd3, entd4, font, fontWidths };
                foreach( StringBuilder s in builders )
                {
                    if( s != null )
                    {
                        s.Insert( 0, "\r\n" );
                        s.Replace( "\r\n", "\r\n    " );
                        s.Append( "\r\n  " );
                    }
                }

                string[] elementNames = new string[] {
                    "abilities", "items", "itemAttributes", "pspItems", "pspItemAttributes", "jobs", "jobLevels",
                    "skillSets", "monsterSkills", "actionMenus", "inflictStatuses", "statusAttributes", "poaching",
                    "entd1", "entd2", "entd3", "entd4", "font", "fontWidths" };
                for( int i = 0; i < builders.Length; i++ )
                {
                    if( builders[i] != null )
                    {
                        writer.WriteElementString( elementNames[i], builders[i].ToString() );
                    }
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
            catch( Exception )
            {
                throw;
            }
            finally
            {
                if( writer != null )
                {
                    writer.Flush();
                    writer.Close();
                }
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

        private static void FireDataChangedEvent()
        {
            if( DataChanged != null )
            {
                DataChanged( null, EventArgs.Empty );
            }
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

        /// <summary>
        /// Reads an XML fftpatch file.
        /// </summary>
        public static void LoadPatch( string filename )
        {
            XmlDocument doc = new XmlDocument();
            doc.Load( filename );
            XmlNode rootNode = doc.SelectSingleNode( "/patch" );
            string type = rootNode.Attributes["type"].InnerText;
            Context = (Context)Enum.Parse( typeof( Context ), type );
            bool psp = Context == Context.US_PSP;

            byte[] abilities = GetFromNodeOrReturnDefault( rootNode, "abilities", psp ? Resources.AbilitiesBin : PSXResources.AbilitiesBin );

            byte[] oldItems = GetFromNodeOrReturnDefault( rootNode, "items", psp ? Resources.OldItemsBin : PSXResources.OldItemsBin );
            byte[] oldItemAttributes = GetFromNodeOrReturnDefault( rootNode, "itemAttributes", psp ? Resources.OldItemAttributesBin : PSXResources.OldItemAttributesBin );
            byte[] newItems = psp ? GetFromNodeOrReturnDefault( rootNode, "pspItems", Resources.NewItemsBin ) : null;
            byte[] newItemAttributes = psp ? GetFromNodeOrReturnDefault( rootNode, "pspItemAttributes", Resources.NewItemAttributesBin ) : null;
            byte[] jobs = GetFromNodeOrReturnDefault( rootNode, "jobs", psp ? Resources.JobsBin : PSXResources.JobsBin );
            byte[] jobLevels = GetFromNodeOrReturnDefault( rootNode, "jobLevels", psp ? Resources.JobLevelsBin : PSXResources.JobLevelsBin );
            byte[] skillSets = GetFromNodeOrReturnDefault( rootNode, "skillSets", psp ? Resources.SkillSetsBin : PSXResources.SkillSetsBin );
            byte[] monsterSkills = GetFromNodeOrReturnDefault( rootNode, "monsterSkills", psp ? Resources.MonsterSkillsBin : PSXResources.MonsterSkillsBin );
            byte[] actionMenus = GetFromNodeOrReturnDefault( rootNode, "actionMenus", psp ? Resources.ActionEventsBin : PSXResources.ActionEventsBin );
            byte[] statusAttributes = GetFromNodeOrReturnDefault( rootNode, "statusAttributes", psp ? Resources.StatusAttributesBin : PSXResources.StatusAttributesBin );
            byte[] inflictStatuses = GetFromNodeOrReturnDefault( rootNode, "inflictStatuses", psp ? Resources.InflictStatusesBin : PSXResources.InflictStatusesBin );
            byte[] poach = GetFromNodeOrReturnDefault( rootNode, "poaching", psp ? Resources.PoachProbabilitiesBin : PSXResources.PoachProbabilitiesBin );
            byte[] entd1 = GetFromNodeOrReturnDefault( rootNode, "entd1", Resources.ENTD1 );
            byte[] entd2 = GetFromNodeOrReturnDefault( rootNode, "entd2", Resources.ENTD2 );
            byte[] entd3 = GetFromNodeOrReturnDefault( rootNode, "entd3", Resources.ENTD3 );
            byte[] entd4 = GetFromNodeOrReturnDefault( rootNode, "entd4", Resources.ENTD4 );
            byte[] font = GetFromNodeOrReturnDefault( rootNode, "font", psp ? Resources.FontBin : PSXResources.FontBin );
            byte[] fontWidths = GetFromNodeOrReturnDefault( rootNode, "fontWidths", psp ? Resources.FontWidthsBin : PSXResources.FontWidthsBin );

            Abilities = new AllAbilities( new SubArray<byte>( abilities ) );
            Items = new AllItems( new SubArray<byte>( oldItems ), newItems != null ? new SubArray<byte>( newItems ) : null );
            ItemAttributes = new AllItemAttributes( new SubArray<byte>( oldItemAttributes ), newItemAttributes != null ? new SubArray<byte>( newItemAttributes ) : null );
            Jobs = new AllJobs( Context, new SubArray<byte>( jobs ) );
            JobLevels = new JobLevels( Context, new SubArray<byte>( jobLevels ),
                new JobLevels( Context, new SubArray<byte>( Context == Context.US_PSP ? Resources.JobLevelsBin : PSXResources.JobLevelsBin ) ) );
            SkillSets = new AllSkillSets( Context, new SubArray<byte>( skillSets ),
                new SubArray<byte>( Context == Context.US_PSP ? Resources.SkillSetsBin : PSXResources.SkillSetsBin ) );
            MonsterSkills = new AllMonsterSkills( new SubArray<byte>( monsterSkills ) );
            ActionMenus = new AllActionMenus( new SubArray<byte>( actionMenus ) );
            StatusAttributes = new AllStatusAttributes( new SubArray<byte>( statusAttributes ) );
            InflictStatuses = new AllInflictStatuses( new SubArray<byte>( inflictStatuses ) );
            PoachProbabilities = new AllPoachProbabilities( new SubArray<byte>( poach ) );
            ENTDs = new AllENTDs( entd1, entd2, entd3, entd4 );
            Font = new FFTFont( new SubArray<byte>( font ), new SubArray<byte>( fontWidths ) );
            FireDataChangedEvent();
        }

        /// <summary>
        /// Opens a modified SCUS_942.21 file.
        /// </summary>
        public static void OpenModifiedPSXFile( string filename )
        {
            FileStream stream = null;
            try
            {
                stream = new FileStream( filename, FileMode.Open );
                VerifyFileIsSCUS94221( stream );
                Context = Context.US_PSX;

                byte[] abilities = new byte[0x24C6];
                byte[] oldItems = new byte[0x110A];
                byte[] oldItemAttributes = new byte[0x7D0];
                byte[] jobs = new byte[0x1E00];
                byte[] jobLevels = new byte[0xD0];
                byte[] skillSets = new byte[0x1130];
                byte[] monsterSkills = new byte[0xF0];
                byte[] actionMenus = new byte[0x100];
                byte[] statusAttributes = new byte[0x280];
                byte[] inflictStatuses = new byte[0x300];
                byte[] poach = new byte[0x60];
                byte[][] buffers = new byte[][] { 
                    abilities, oldItems, oldItemAttributes, jobs, 
                    jobLevels, skillSets, monsterSkills, actionMenus, 
                    statusAttributes, inflictStatuses, poach };

                UInt32[] offsets = new UInt32[] { 
                    0x4F3F0, 0x536B8, 0x54AC4, 0x518B8,
                    0x568C4, 0x55294, 0x563C4, 0x564B4,
                    0x565E4, 0x547C4, 0x56864 };
                for( int i = 0; i < buffers.Length; i++ )
                {
                    stream.Seek( offsets[i], SeekOrigin.Begin );
                    stream.Read( buffers[i], 0, buffers[i].Length );
                }

                Abilities = new AllAbilities( new SubArray<byte>( abilities ) );
                Items = new AllItems( new SubArray<byte>( oldItems ), null );
                ItemAttributes = new AllItemAttributes( new SubArray<byte>( oldItemAttributes ), null );
                Jobs = new AllJobs( Context, new SubArray<byte>( jobs ) );
                JobLevels = new JobLevels( Context, new SubArray<byte>( jobLevels ),
                    new JobLevels( Context.US_PSX, new SubArray<byte>( PSXResources.JobLevelsBin ) ) );
                SkillSets = new AllSkillSets( Context, new SubArray<byte>( skillSets ), new SubArray<byte>( PSXResources.SkillSetsBin ) );
                MonsterSkills = new AllMonsterSkills( new SubArray<byte>( monsterSkills ) );
                ActionMenus = new AllActionMenus( new SubArray<byte>( actionMenus ) );
                StatusAttributes = new AllStatusAttributes( new SubArray<byte>( statusAttributes ) );
                InflictStatuses = new AllInflictStatuses( new SubArray<byte>( inflictStatuses ) );
                PoachProbabilities = new AllPoachProbabilities( new SubArray<byte>( poach ) );
                ENTDs = new AllENTDs( Resources.ENTD1, Resources.ENTD2, Resources.ENTD3, Resources.ENTD4 );
                Font = new FFTFont( new SubArray<byte>( PSXResources.FontBin ), new SubArray<byte>( PSXResources.FontWidthsBin ) );
                FireDataChangedEvent();
            }
            catch( InvalidDataException )
            {
                throw;
            }
            catch( FileNotFoundException )
            {
                throw;
            }
            finally
            {
                if( stream != null )
                {
                    stream.Close();
                    stream.Dispose();
                }
            }
        }

        /// <summary>
        /// Applies font widths patches to BATTLE.BIN
        /// </summary>
        public static void PatchBattleBin( string fileName )
        {
            FileStream stream = null;
            try
            {
                stream = new FileStream( fileName, FileMode.Open );
                WriteArrayToPosition( FFTPatch.Font.ToWidthsByteArray(), stream, 0xFF0FC );
            }
            catch( InvalidDataException )
            {
                throw;
            }
            catch( Exception )
            {
                throw;
            }
            finally
            {
                if( stream != null )
                {
                    stream.Flush();
                    stream.Close();
                    stream.Dispose();
                }
            }
        }

        /// <summary>
        /// Applies patches to a SCUS_942.21 file.
        /// </summary>
        public static void ApplyPatchesToExecutable( string filename )
        {
            FileStream stream = null;
            try
            {
                bool psp = Context == Context.US_PSP;
                stream = new FileStream( filename, FileMode.Open );
                long newItems = -1;
                long newItemAttributes = -1;
                long abilities = -1;
                long jobs = -1;
                long skillSets = -1;
                long monsterSkills = -1;
                long actionEvents = -1;
                long statusAttributes = -1;
                long poach = -1;
                long jobLevels = -1;
                long oldItems = -1;
                long inflictStatuses = -1;
                long oldItemAttributes = -1;

                if( Context == Context.US_PSP )
                {
                    byte[] elf = new byte[] { 0x7F, 0x45, 0x4C, 0x46 };
                    long bootBinLocation = FindArrayInStream( elf, stream );
                    abilities = bootBinLocation + 0x271514;
                    statusAttributes = bootBinLocation + 0x276DA4;
                    skillSets = bootBinLocation + 0x275A38;
                    oldItemAttributes = bootBinLocation + 0x3266E8;
                    actionEvents = bootBinLocation + 0x276CA4;
                    inflictStatuses = bootBinLocation + 0x3263E8;
                    jobLevels = bootBinLocation + 0x277084;
                    jobs = bootBinLocation + 0x2739DC;
                    monsterSkills = bootBinLocation + 0x276BB4;
                    newItemAttributes = bootBinLocation + 0x25720C;
                    newItems = bootBinLocation + 0x256E00;
                    oldItems = bootBinLocation + 0x3252DC;
                    poach = bootBinLocation + 0x277024;

                    // Write to BOOT.BIN
                    WriteArrayToPosition( Abilities.ToByteArray( Context ), stream, abilities );
                    WriteArrayToPosition( Items.ToFirstByteArray(), stream, oldItems );
                    WriteArrayToPosition( ItemAttributes.ToFirstByteArray(), stream, oldItemAttributes );
                    WriteArrayToPosition( ItemAttributes.ToSecondByteArray(), stream, newItemAttributes );
                    WriteArrayToPosition( Items.ToSecondByteArray(), stream, newItems );
                    WriteArrayToPosition( Jobs.ToByteArray( Context ), stream, jobs );
                    WriteArrayToPosition( JobLevels.ToByteArray( Context ), stream, jobLevels );
                    WriteArrayToPosition( SkillSets.ToByteArray( Context ), stream, skillSets );
                    WriteArrayToPosition( MonsterSkills.ToByteArray( Context ), stream, monsterSkills );
                    WriteArrayToPosition( ActionMenus.ToByteArray( Context ), stream, actionEvents );
                    WriteArrayToPosition( StatusAttributes.ToByteArray( Context ), stream, statusAttributes );
                    WriteArrayToPosition( InflictStatuses.ToByteArray(), stream, inflictStatuses );
                    WriteArrayToPosition( PoachProbabilities.ToByteArray( Context ), stream, poach );

                    byte[] psx = new byte[] { 0x50, 0x53, 0x2D, 0x58 };
                    long slps00770Location = FindArrayInStream( psx, stream );
                    oldItemAttributes = slps00770Location + 0x51B04;
                    oldItems = slps00770Location + 0x506F8;
                    poach = slps00770Location + 0x538A4;
                    skillSets = slps00770Location + 0x522D4;
                    statusAttributes = slps00770Location + 0x53624;
                    abilities = slps00770Location + 0x4C430;
                    actionEvents = slps00770Location + 0x534F4;
                    inflictStatuses = slps00770Location + 0x51804;
                    jobLevels = slps00770Location + 0x53904;
                    jobs = slps00770Location + 0x4E8F8;
                    monsterSkills = slps00770Location + 0x53404;

                    byte[] jobsBytes = Jobs.ToByteArray( Context.US_PSX );
                    byte[] realJobsBytes = new byte[0x1E00];
                    Array.Copy( jobsBytes, 0, realJobsBytes, 0, 0x1E00 );
                    WriteArrayToPosition( realJobsBytes, stream, jobs );
                    WriteArrayToPosition( ActionMenus.ToByteArray( Context ), stream, actionEvents );
                    WriteArrayToPosition( StatusAttributes.ToByteArray( Context ), stream, statusAttributes );
                    WriteArrayToPosition( ItemAttributes.ToFirstByteArray(), stream, oldItemAttributes );
                    WriteArrayToPosition( MonsterSkills.ToByteArray( Context ), stream, monsterSkills );
                    WriteArrayToPosition( InflictStatuses.ToByteArray(), stream, inflictStatuses );
                    byte[] skillSetsBytes = SkillSets.ToByteArray( Context );
                    byte[] realSkillSetsBytes = new byte[0x1130];
                    Array.Copy( skillSetsBytes, 0, realSkillSetsBytes, 0, 0x1130 );
                    WriteArrayToPosition( realSkillSetsBytes, stream, skillSets );
                    WriteArrayToPosition( Items.ToFirstByteArray(), stream, oldItems );
                    WriteArrayToPosition( Abilities.ToByteArray( Context ), stream, abilities );
                    WriteArrayToPosition( JobLevels.ToByteArray( Context.US_PSX ), stream, jobLevels );
                    WriteArrayToPosition( PoachProbabilities.ToByteArray( Context ), stream, poach );
                }
                else
                {
                    VerifyFileIsSCUS94221( stream );

                    oldItemAttributes = 0x54AC4;
                    oldItems = 0x536B8;
                    poach = 0x56864;
                    skillSets = 0x55294;
                    statusAttributes = 0x565E4;
                    abilities = 0x4F3F0;
                    actionEvents = 0x564B4;
                    inflictStatuses = 0x547C4;
                    jobLevels = 0x568C4;
                    jobs = 0x518B8;
                    monsterSkills = 0x563C4;
                    WriteArrayToPosition( Abilities.ToByteArray( Context ), stream, abilities );
                    WriteArrayToPosition( Items.ToFirstByteArray(), stream, oldItems );
                    WriteArrayToPosition( ItemAttributes.ToFirstByteArray(), stream, oldItemAttributes );
                    WriteArrayToPosition( Jobs.ToByteArray( Context ), stream, jobs );
                    WriteArrayToPosition( JobLevels.ToByteArray( Context ), stream, jobLevels );
                    WriteArrayToPosition( SkillSets.ToByteArray( Context ), stream, skillSets );
                    WriteArrayToPosition( MonsterSkills.ToByteArray( Context ), stream, monsterSkills );
                    WriteArrayToPosition( ActionMenus.ToByteArray( Context ), stream, actionEvents );
                    WriteArrayToPosition( StatusAttributes.ToByteArray( Context ), stream, statusAttributes );
                    WriteArrayToPosition( InflictStatuses.ToByteArray(), stream, inflictStatuses );
                    WriteArrayToPosition( PoachProbabilities.ToByteArray( Context ), stream, poach );
                }
            }
            catch( InvalidDataException )
            {
                throw;
            }
            catch( FileNotFoundException )
            {
                throw;
            }
            finally
            {
                if( stream != null )
                {
                    stream.Flush();
                    stream.Close();
                    stream.Dispose();
                }
            }
        }

        private static void VerifyFileIsSCUS94221( FileStream stream )
        {
            stream.Seek( 0, SeekOrigin.Begin );
            string header = ReadString( stream, 8 );

            stream.Seek( 0x4C, SeekOrigin.Begin );
            string scea = ReadString( stream, 0x37 );

            stream.Seek( 0x1C, SeekOrigin.Begin );
            byte[] length = new byte[4];
            stream.Read( length, 0, 4 );
            UInt32 l = Utilities.BytesToUInt32( length ) + 0x800;

            if( (header != "PS-X EXE") ||
                (scea != "Sony Computer Entertainment Inc. for North America area") ||
                (l != stream.Length) )
            {
                throw new InvalidDataException();
            }
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

        private static void WriteArrayToPosition( byte[] array, FileStream stream, long position )
        {
            stream.Seek( position, SeekOrigin.Begin );
            stream.Write( array, 0, array.Length );
        }

        private static long FindArrayInStream( byte[] array, FileStream stream )
        {
            byte[] read = new byte[4];

            stream.Seek( 0, SeekOrigin.Begin );
            while( stream.Position + read.Length <= stream.Length )
            {
                stream.Read( read, 0, array.Length );
                if( Utilities.CompareArrays( array, read ) )
                {
                    return stream.Position - array.Length;
                }
                stream.Seek( 2044, SeekOrigin.Current );
            }

            throw new InvalidDataException();
        }

    }
}