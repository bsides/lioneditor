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
using System.IO;
using System.Text;
using System.Xml;
using FFTPatcher.Properties;

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
                    JobLevels = new JobLevels( Context, new SubArray<byte>( Resources.JobLevelsBin ) );
                    SkillSets = new AllSkillSets( Context, new SubArray<byte>( Resources.SkillSetsBin ) );
                    MonsterSkills = new AllMonsterSkills( new SubArray<byte>( Resources.MonsterSkillsBin ) );
                    ActionMenus = new AllActionMenus( new SubArray<byte>( Resources.ActionEventsBin ) );
                    StatusAttributes = new AllStatusAttributes( new SubArray<byte>( Resources.StatusAttributesBin ) );
                    InflictStatuses = new AllInflictStatuses( new SubArray<byte>( Resources.InflictStatusesBin ) );
                    PoachProbabilities = new AllPoachProbabilities( new SubArray<byte>( Resources.PoachProbabilitiesBin ) );
                    break;
                case Context.US_PSX:
                    Abilities = new AllAbilities( new SubArray<byte>( PSXResources.AbilitiesBin ) );
                    Items = new AllItems( new SubArray<byte>( PSXResources.OldItemsBin ), null );
                    ItemAttributes = new AllItemAttributes( new SubArray<byte>( PSXResources.OldItemAttributesBin ), null );
                    Jobs = new AllJobs( Context, new SubArray<byte>( PSXResources.JobsBin ) );
                    JobLevels = new JobLevels( Context, new SubArray<byte>( PSXResources.JobLevelsBin ) );
                    SkillSets = new AllSkillSets( Context, new SubArray<byte>( PSXResources.SkillSetsBin ) );
                    MonsterSkills = new AllMonsterSkills( new SubArray<byte>( PSXResources.MonsterSkillsBin ) );
                    ActionMenus = new AllActionMenus( new SubArray<byte>( PSXResources.ActionEventsBin ) );
                    StatusAttributes = new AllStatusAttributes( new SubArray<byte>( PSXResources.StatusAttributesBin ) );
                    InflictStatuses = new AllInflictStatuses( new SubArray<byte>( PSXResources.InflictStatusesBin ) );
                    PoachProbabilities = new AllPoachProbabilities( new SubArray<byte>( PSXResources.PoachProbabilitiesBin ) );
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        public static void SaveToFile( string path )
        {
            StringBuilder abilities = new StringBuilder( Convert.ToBase64String( Abilities.ToByteArray( Context ), Base64FormattingOptions.InsertLineBreaks ) );
            StringBuilder oldItems = new StringBuilder( Convert.ToBase64String( Items.ToFirstByteArray(), Base64FormattingOptions.InsertLineBreaks ) );
            StringBuilder oldItemAttributes = new StringBuilder( Convert.ToBase64String( ItemAttributes.ToFirstByteArray(), Base64FormattingOptions.InsertLineBreaks ) );
            StringBuilder newItems = new StringBuilder();
            StringBuilder newItemAttributes = new StringBuilder();
            if( Context == Context.US_PSP )
            {
                newItems.Append( Convert.ToBase64String( Items.ToSecondByteArray(), Base64FormattingOptions.InsertLineBreaks ) );
                newItemAttributes.Append( Convert.ToBase64String( ItemAttributes.ToSecondByteArray(), Base64FormattingOptions.InsertLineBreaks ) );
            }
            StringBuilder jobs = new StringBuilder( Convert.ToBase64String( Jobs.ToByteArray( Context ), Base64FormattingOptions.InsertLineBreaks ) );
            StringBuilder jobLevels = new StringBuilder( Convert.ToBase64String( JobLevels.ToByteArray( Context ), Base64FormattingOptions.InsertLineBreaks ) );
            StringBuilder skillSets = new StringBuilder( Convert.ToBase64String( SkillSets.ToByteArray( Context ), Base64FormattingOptions.InsertLineBreaks ) );
            StringBuilder monsterSkills = new StringBuilder( Convert.ToBase64String( MonsterSkills.ToByteArray( Context ), Base64FormattingOptions.InsertLineBreaks ) );
            StringBuilder actionMenus = new StringBuilder( Convert.ToBase64String( ActionMenus.ToByteArray( Context ), Base64FormattingOptions.InsertLineBreaks ) );
            StringBuilder statusAttributes = new StringBuilder( Convert.ToBase64String( StatusAttributes.ToByteArray( Context ), Base64FormattingOptions.InsertLineBreaks ) );
            StringBuilder inflictStatuses = new StringBuilder( Convert.ToBase64String( InflictStatuses.ToByteArray(), Base64FormattingOptions.InsertLineBreaks ) );
            StringBuilder poach = new StringBuilder( Convert.ToBase64String( PoachProbabilities.ToByteArray( Context ), Base64FormattingOptions.InsertLineBreaks ) );

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

                foreach( StringBuilder s in new StringBuilder[] { abilities, oldItems, oldItemAttributes, newItems, newItemAttributes, jobs, jobLevels, skillSets, monsterSkills, actionMenus, inflictStatuses, statusAttributes, poach } )
                {
                    s.Insert( 0, "\r\n" );
                    s.Replace( "\r\n", "\r\n    " );
                    s.Append( "\r\n  " );
                }

                writer.WriteElementString( "abilities", abilities.ToString() );
                writer.WriteElementString( "items", oldItems.ToString() );
                writer.WriteElementString( "itemAttributes", oldItemAttributes.ToString() );
                if( Context == Context.US_PSP )
                {
                    writer.WriteElementString( "pspItems", newItems.ToString() );
                    writer.WriteElementString( "pspItemAttributes", newItemAttributes.ToString() );
                }
                writer.WriteElementString( "jobs", jobs.ToString() );
                writer.WriteElementString( "jobLevels", jobLevels.ToString() );
                writer.WriteElementString( "skillSets", skillSets.ToString() );
                writer.WriteElementString( "monsterSkills", monsterSkills.ToString() );
                writer.WriteElementString( "actionMenus", actionMenus.ToString() );
                writer.WriteElementString( "statusAttributes", statusAttributes.ToString() );
                writer.WriteElementString( "inflictStatuses", inflictStatuses.ToString() );
                writer.WriteElementString( "poaching", poach.ToString() );
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

        public static void Load( string filename )
        {
            XmlDocument doc = new XmlDocument();
            doc.Load( filename );
            XmlNode rootNode = doc.SelectSingleNode( "/patch" );
            string type = rootNode.Attributes["type"].InnerText;
            Context = (Context)Enum.Parse( typeof( Context ), type );

            byte[] abilities = Convert.FromBase64String( rootNode.SelectSingleNode( "abilities" ).InnerText );
            byte[] oldItems = Convert.FromBase64String( rootNode.SelectSingleNode( "items" ).InnerText );
            byte[] oldItemAttributes = Convert.FromBase64String( rootNode.SelectSingleNode( "itemAttributes" ).InnerText );
            byte[] newItems = Context == Context.US_PSP ? Convert.FromBase64String( rootNode.SelectSingleNode( "pspItems" ).InnerText ) : null;
            byte[] newItemAttributes = Context == Context.US_PSP ? Convert.FromBase64String( rootNode.SelectSingleNode( "pspItemAttributes" ).InnerText ) : null;
            byte[] jobs = Convert.FromBase64String( rootNode.SelectSingleNode( "jobs" ).InnerText );
            byte[] jobLevels = Convert.FromBase64String( rootNode.SelectSingleNode( "jobLevels" ).InnerText );
            byte[] skillSets = Convert.FromBase64String( rootNode.SelectSingleNode( "skillSets" ).InnerText );
            byte[] monsterSkills = Convert.FromBase64String( rootNode.SelectSingleNode( "monsterSkills" ).InnerText );
            byte[] actionMenus = Convert.FromBase64String( rootNode.SelectSingleNode( "actionMenus" ).InnerText );
            byte[] statusAttributes = Convert.FromBase64String( rootNode.SelectSingleNode( "statusAttributes" ).InnerText );
            byte[] inflictStatuses = Convert.FromBase64String( rootNode.SelectSingleNode( "inflictStatuses" ).InnerText );
            byte[] poach = Convert.FromBase64String( rootNode.SelectSingleNode( "poaching" ).InnerText );

            Abilities = new AllAbilities( new SubArray<byte>( abilities ) );
            Items = new AllItems( new SubArray<byte>( oldItems ), newItems != null ? new SubArray<byte>( newItems ) : null );
            ItemAttributes = new AllItemAttributes( new SubArray<byte>( oldItemAttributes ), newItemAttributes != null ? new SubArray<byte>( newItemAttributes ) : null );
            Jobs = new AllJobs( Context, new SubArray<byte>( jobs ) );
            JobLevels = new JobLevels( Context, new SubArray<byte>( jobLevels ) );
            SkillSets = new AllSkillSets( Context, new SubArray<byte>( skillSets ) );
            MonsterSkills = new AllMonsterSkills( new SubArray<byte>( monsterSkills ) );
            ActionMenus = new AllActionMenus( new SubArray<byte>( actionMenus ) );
            StatusAttributes = new AllStatusAttributes( new SubArray<byte>( statusAttributes ) );
            InflictStatuses = new AllInflictStatuses( new SubArray<byte>( inflictStatuses ) );
            PoachProbabilities = new AllPoachProbabilities( new SubArray<byte>( poach ) );
            FireDataChangedEvent();
        }

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
                JobLevels = new JobLevels( Context, new SubArray<byte>( jobLevels ) );
                SkillSets = new AllSkillSets( Context, new SubArray<byte>( skillSets ) );
                MonsterSkills = new AllMonsterSkills( new SubArray<byte>( monsterSkills ) );
                ActionMenus = new AllActionMenus( new SubArray<byte>( actionMenus ) );
                StatusAttributes = new AllStatusAttributes( new SubArray<byte>( statusAttributes ) );
                InflictStatuses = new AllInflictStatuses( new SubArray<byte>( inflictStatuses ) );
                PoachProbabilities = new AllPoachProbabilities( new SubArray<byte>( poach ) );
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

        public static void ApplyPatchesToFile( string filename )
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
                }
                else
                {
                    VerifyFileIsSCUS94221( stream );
                    byte[] psx = new byte[stream.Length];

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
                }

                WriteArrayToPosition( Abilities.ToByteArray( Context ), stream, abilities );
                WriteArrayToPosition( Items.ToFirstByteArray(), stream, oldItems );
                WriteArrayToPosition( ItemAttributes.ToFirstByteArray(), stream, oldItemAttributes );
                if( Context == Context.US_PSP )
                {
                    WriteArrayToPosition( ItemAttributes.ToSecondByteArray(), stream, newItemAttributes );
                    WriteArrayToPosition( Items.ToSecondByteArray(), stream, newItems );
                }
                WriteArrayToPosition( Jobs.ToByteArray( Context ), stream, jobs );
                WriteArrayToPosition( JobLevels.ToByteArray( Context ), stream, jobLevels );
                WriteArrayToPosition( SkillSets.ToByteArray( Context ), stream, skillSets );
                WriteArrayToPosition( MonsterSkills.ToByteArray( Context ), stream, monsterSkills );
                WriteArrayToPosition( ActionMenus.ToByteArray( Context ), stream, actionEvents );
                WriteArrayToPosition( StatusAttributes.ToByteArray( Context ), stream, statusAttributes );
                WriteArrayToPosition( InflictStatuses.ToByteArray(), stream, inflictStatuses );
                WriteArrayToPosition( PoachProbabilities.ToByteArray( Context ), stream, poach );
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
            byte[] read = new byte[array.Length];

            stream.Seek( 0, SeekOrigin.Begin );
            while( stream.Position + array.Length < stream.Length )
            {
                stream.Read( read, 0, array.Length );
                if( Utilities.CompareArrays( array, read ) )
                {
                    return stream.Position - array.Length;
                }
                stream.Seek( 1 - (array.Length), SeekOrigin.Current );
            }

            throw new InvalidDataException();
        }

    }
}