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
using System.Xml;
using FFTPatcher.Properties;
using System.IO;
using System.Text;

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

            XmlTextWriter writer = new XmlTextWriter( path, System.Text.Encoding.UTF8 );
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
            writer.Close();
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

        public static void ApplyPatchesToFile( string filename )
        {
            FileStream stream = null;
            try
            {
                bool psp = Context == Context.US_PSP;
                stream = new FileStream( filename, FileMode.Open );
                long newItems = -1;
                long newItemAttributes = -1;
                if( Context == Context.US_PSP )
                {
                    newItems = FindArrayInStream( Resources.NewItemsBin, stream );
                    newItemAttributes = FindArrayInStream( Resources.NewItemAttributesBin, stream );
                }

                long abilities = FindArrayInStream( psp ? Resources.AbilitiesBin : PSXResources.AbilitiesBin, stream );
                long jobs = FindArrayInStream( psp ? Resources.JobsBin : PSXResources.JobsBin, stream );
                long skillSets = FindArrayInStream( psp ? Resources.SkillSetsBin : PSXResources.SkillSetsBin, stream );
                long monsterSkills = FindArrayInStream( psp ? Resources.MonsterSkillsBin : PSXResources.MonsterSkillsBin, stream );
                long actionEvents = FindArrayInStream( psp ? Resources.ActionEventsBin : PSXResources.ActionEventsBin, stream );
                long statusAttributes = FindArrayInStream( psp ? Resources.StatusAttributesBin : PSXResources.StatusAttributesBin, stream );
                long poach = FindArrayInStream( psp ? Resources.PoachProbabilitiesBin : PSXResources.PoachProbabilitiesBin, stream );
                long jobLevels = FindArrayInStream( psp ? Resources.JobLevelsBin : PSXResources.JobLevelsBin, stream );
                long oldItems = FindArrayInStream( psp ? Resources.OldItemsBin : PSXResources.OldItemsBin, stream );
                long inflictStatuses = FindArrayInStream( psp ? Resources.InflictStatusesBin : PSXResources.InflictStatusesBin, stream );
                long oldItemAttributes = FindArrayInStream( psp ? Resources.OldItemAttributesBin : PSXResources.OldItemAttributesBin, stream );

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
                }
            }
        }

        private static void WriteArrayToPosition( byte[] array, FileStream stream, long position )
        {
            stream.Seek( position, SeekOrigin.Begin );
            stream.Write( array, 0, array.Length );
        }
        
        private static long FindArrayInStream( byte[] array, FileStream stream )
        {
            byte[] read = new byte[array.Length];
            long startPosition = stream.Position;

            while( stream.Position + array.Length < stream.Length )
            {
                stream.Read( read, 0, array.Length );
                if( Utilities.CompareArrays( array, read ) )
                {
                    return stream.Position - array.Length;
                }
                stream.Seek( 1 - (array.Length), SeekOrigin.Current );
            }
            stream.Seek( 0, SeekOrigin.Begin );
            while( stream.Position < startPosition )
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
