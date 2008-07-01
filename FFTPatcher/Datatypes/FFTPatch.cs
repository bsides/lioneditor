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

namespace FFTPatcher.Datatypes
{
    public static class FFTPatch
    {

        #region Properties (14)


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


        #endregion Properties

        #region Events (1)

        public static event EventHandler DataChanged;

        #endregion Events

        #region Methods (15)


        private static void BuildFromContext()
        {
            switch( Context )
            {
                case Context.US_PSP:
                    Abilities = new AllAbilities( Resources.AbilitiesBin, Resources.AbilityEffectsBin );
                    Items = new AllItems(
                        Resources.OldItemsBin,
                        Resources.NewItemsBin );
                    ItemAttributes = new AllItemAttributes(
                        Resources.OldItemAttributesBin,
                        Resources.NewItemAttributesBin );
                    Jobs = new AllJobs( Context, Resources.JobsBin );
                    JobLevels = new JobLevels( Context, Resources.JobLevelsBin,
                        new JobLevels( Context, Resources.JobLevelsBin ) );
                    SkillSets = new AllSkillSets( Context, Resources.SkillSetsBin,
                        Resources.SkillSetsBin );
                    MonsterSkills = new AllMonsterSkills( Resources.MonsterSkillsBin );
                    ActionMenus = new AllActionMenus( Resources.ActionEventsBin, Context );
                    StatusAttributes = new AllStatusAttributes( Resources.StatusAttributesBin );
                    InflictStatuses = new AllInflictStatuses( Resources.InflictStatusesBin );
                    PoachProbabilities = new AllPoachProbabilities( Resources.PoachProbabilitiesBin );
                    Font = new FFTFont( Resources.FontBin, Resources.FontWidthsBin );
                    ENTDs = new AllENTDs( Resources.ENTD1, Resources.ENTD2, Resources.ENTD3, Resources.ENTD4, Resources.ENTD5 );
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
                    ENTDs = new AllENTDs( Resources.ENTD1, Resources.ENTD2, Resources.ENTD3, Resources.ENTD4 );
                    break;
                default:
                    throw new ArgumentException();
            }
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

                stream.WriteArrayToPosition( Abilities.ToByteArray( Context ), abilities );
                stream.WriteArrayToPosition( Items.ToFirstByteArray(), oldItems );
                stream.WriteArrayToPosition( ItemAttributes.ToFirstByteArray(), oldItemAttributes );
                stream.WriteArrayToPosition( Jobs.ToByteArray( Context ), jobs );
                stream.WriteArrayToPosition( JobLevels.ToByteArray( Context ), jobLevels );
                stream.WriteArrayToPosition( SkillSets.ToByteArray( Context ), skillSets );
                stream.WriteArrayToPosition( MonsterSkills.ToByteArray( Context ), monsterSkills );
                stream.WriteArrayToPosition( ActionMenus.ToByteArray( Context ), actionEvents );
                stream.WriteArrayToPosition( StatusAttributes.ToByteArray( Context ), statusAttributes );
                stream.WriteArrayToPosition( InflictStatuses.ToByteArray(), inflictStatuses );
                stream.WriteArrayToPosition( PoachProbabilities.ToByteArray( Context ), poach );
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
            byte[] abilityEffects = GetFromNodeOrReturnDefault( rootNode, "abilityEffects", psp ? Resources.AbilityEffectsBin : PSXResources.AbilityEffectsBin );

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
            byte[] entd5 = GetFromNodeOrReturnDefault( rootNode, "entd5", Resources.ENTD5 );
            byte[] font = GetFromNodeOrReturnDefault( rootNode, "font", psp ? Resources.FontBin : PSXResources.FontBin );
            byte[] fontWidths = GetFromNodeOrReturnDefault( rootNode, "fontWidths", psp ? Resources.FontWidthsBin : PSXResources.FontWidthsBin );

            Abilities = new AllAbilities( abilities, abilityEffects );
            Items = new AllItems( oldItems, newItems != null ? newItems : null );
            ItemAttributes = new AllItemAttributes( oldItemAttributes, newItemAttributes != null ? newItemAttributes : null );
            Jobs = new AllJobs( Context, jobs );
            JobLevels = new JobLevels( Context, jobLevels,
                new JobLevels( Context, Context == Context.US_PSP ? Resources.JobLevelsBin : PSXResources.JobLevelsBin ) );
            SkillSets = new AllSkillSets( Context, skillSets,
                Context == Context.US_PSP ? Resources.SkillSetsBin : PSXResources.SkillSetsBin );
            MonsterSkills = new AllMonsterSkills( monsterSkills );
            ActionMenus = new AllActionMenus( actionMenus, Context );
            StatusAttributes = new AllStatusAttributes( statusAttributes );
            InflictStatuses = new AllInflictStatuses( inflictStatuses );
            PoachProbabilities = new AllPoachProbabilities( poach );
            ENTDs = psp ? new AllENTDs( entd1, entd2, entd3, entd4, entd5 ) : new AllENTDs( entd1, entd2, entd3, entd4 );
            Font = new FFTFont( font, fontWidths );
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

                Abilities = new AllAbilities( abilities, PSXResources.AbilityEffectsBin );
                Items = new AllItems( oldItems, null );
                ItemAttributes = new AllItemAttributes( oldItemAttributes, null );
                Jobs = new AllJobs( Context, jobs );
                JobLevels = new JobLevels( Context, jobLevels,
                    new JobLevels( Context.US_PSX, PSXResources.JobLevelsBin ) );
                SkillSets = new AllSkillSets( Context, skillSets, PSXResources.SkillSetsBin );
                MonsterSkills = new AllMonsterSkills( monsterSkills );
                ActionMenus = new AllActionMenus( actionMenus, Context.US_PSX );
                StatusAttributes = new AllStatusAttributes( statusAttributes );
                InflictStatuses = new AllInflictStatuses( inflictStatuses );
                PoachProbabilities = new AllPoachProbabilities( poach );
                ENTDs = new AllENTDs( Resources.ENTD1, Resources.ENTD2, Resources.ENTD3, Resources.ENTD4 );
                Font = new FFTFont( PSXResources.FontBin, PSXResources.FontWidthsBin );
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
        /// Applies font widths and ability effects patches to BATTLE.BIN
        /// </summary>
        public static void PatchBattleBin( string fileName )
        {
            FileStream stream = null;
            try
            {
                stream = new FileStream( fileName, FileMode.Open );
                stream.WriteArrayToPosition( FFTPatch.Font.ToWidthsByteArray(), 0xFF0FC );
                stream.WriteArrayToPosition( FFTPatch.Abilities.ToEffectsByteArray(), 0x14F3F0 );
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
        /// Saves this patch to an XML document.
        /// </summary>
        public static void SavePatchToFile( string path )
        {
            SavePatchToFile( path, FFTPatch.Context, true );
        }

        public static void PatchPsxIso( BackgroundWorker backgroundWorker, DoWorkEventArgs e )
        {
            string filename = e.Argument as string;
            const int defaultNumberOfTasks = 19;
            const bool patchSCEAP = true;
            int numberOfTasks = defaultNumberOfTasks;
            int tasksComplete = 1;
            List<PatchedByteArray> patches = new List<PatchedByteArray>();

            MethodInvoker progress =
                delegate()
                {
                    numberOfTasks = defaultNumberOfTasks + patches.Count * 2;
                    backgroundWorker.ReportProgress( tasksComplete++ * 100 / numberOfTasks );
                };

            if ( Abilities.HasChanged )
            {
                patches.Add( new PatchedByteArray( PsxIso.Sectors.SCUS_942_21, 0x4F3F0, Abilities.ToByteArray() ) );
                patches.Add( new PatchedByteArray( PsxIso.Sectors.BATTLE_BIN, 0x14F3F0, Abilities.ToEffectsByteArray() ) );
            }
            progress();
            progress();

            if ( Items.HasChanged )
            {
                patches.Add( new PatchedByteArray( PsxIso.Sectors.SCUS_942_21, 0x536B8, Items.ToFirstByteArray() ) );
            }
            progress();
            if ( ItemAttributes.HasChanged )
            {
                patches.Add( new PatchedByteArray( PsxIso.Sectors.SCUS_942_21, 0x54AC4, ItemAttributes.ToFirstByteArray() ) );
            }
            progress();
            if ( Jobs.HasChanged )
            {
                patches.Add( new PatchedByteArray( PsxIso.Sectors.SCUS_942_21, 0x518B8, Jobs.ToByteArray( Context.US_PSX ) ) );
            }
            progress();
            if ( JobLevels.HasChanged )
            {
                patches.Add( new PatchedByteArray( PsxIso.Sectors.SCUS_942_21, 0x568C4, JobLevels.ToByteArray( Context.US_PSX ) ) );
            }
            progress();
            if ( SkillSets.HasChanged )
            {
                patches.Add( new PatchedByteArray( PsxIso.Sectors.SCUS_942_21, 0x55294, SkillSets.ToByteArray( Context.US_PSX ) ) );
            }
            progress();
            if ( MonsterSkills.HasChanged )
            {
                patches.Add( new PatchedByteArray( PsxIso.Sectors.SCUS_942_21, 0x563C4, MonsterSkills.ToByteArray( Context.US_PSX ) ) );
            }
            progress();
            if ( ActionMenus.HasChanged )
            {
                patches.Add( new PatchedByteArray( PsxIso.Sectors.SCUS_942_21, 0x564B4, ActionMenus.ToByteArray( Context.US_PSX ) ) );
            }
            progress();
            if ( StatusAttributes.HasChanged )
            {
                patches.Add( new PatchedByteArray( PsxIso.Sectors.SCUS_942_21, 0x565E4, StatusAttributes.ToByteArray( Context.US_PSX ) ) );
            }
            progress();
            if ( InflictStatuses.HasChanged )
            {
                patches.Add( new PatchedByteArray( PsxIso.Sectors.SCUS_942_21, 0x547C4, InflictStatuses.ToByteArray() ) );
            }
            progress();
            if ( PoachProbabilities.HasChanged )
            {
                patches.Add( new PatchedByteArray( PsxIso.Sectors.SCUS_942_21, 0x56864, PoachProbabilities.ToByteArray( Context.US_PSX ) ) );
            }
            progress();

            for ( int i = 0; i < 4; i++ )
            {
                if ( ENTDs.ENTDs[i].HasChanged )
                {
                    patches.Add( new PatchedByteArray( (PsxIso.Sectors)Enum.Parse( typeof( PsxIso.Sectors ), string.Format( "BATTLE_ENTD{0}_ENT", i + 1 ), false ), 0, ENTDs.ENTDs[i].ToByteArray() ) );
                }
                progress();
            }

            patches.Add( new PatchedByteArray( PsxIso.Sectors.EVENT_FONT_BIN, 0, Font.ToByteArray() ) );
            progress();
            patches.Add( new PatchedByteArray( PsxIso.Sectors.BATTLE_BIN, 0xFF0FC, Font.ToWidthsByteArray() ) );
            progress();

            if ( patchSCEAP )
            {
                patches.Add( new PatchedByteArray( PsxIso.Sectors.SCEAP_DAT, 0, PSXResources.SCEAPDAT ) );
            }
            progress();

            string fullpath = Path.GetFullPath( filename );
            string ppfFilename = 
                fullpath.Remove( fullpath.LastIndexOf( Path.GetExtension( fullpath ) ) ) + ".fftpatcher.ppf";
            using ( FileStream stream = new FileStream( filename, FileMode.Open ) )
            using ( FileStream ppfStream = new FileStream( ppfFilename, FileMode.Create ) )
            {
                IList<byte> ppf = IsoPatch.InitializePpf();
                foreach ( PatchedByteArray patch in patches )
                {
                    IsoPatch.PatchFileAtSector( IsoPatch.IsoType.Mode2Form1, stream, true, patch.Sector, patch.Offset, patch.Bytes, true, true, ppf );
                    progress();
                }
                ppfStream.Write( ppf.ToArray(), 0, ppf.Count );
            }
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
                    amBytes.AddRange( Resources.ActionEventsBin.Sub( 0xE0, 0xE2 ) );
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
                    AllJobs defaultPspJobs = new AllJobs( Context.US_PSP, Resources.JobsBin );
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
                    JobLevels pspJobLevels = new JobLevels( Context.US_PSP, Resources.JobLevelsBin );

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
                    ssBytes.AddRange( Resources.SkillSetsBin.Sub( ssBytes.Count ) );
                    StringBuilder ssBytesString = new StringBuilder( Convert.ToBase64String( ssBytes.ToArray(), Base64FormattingOptions.InsertLineBreaks ) );
                    sbPrettifier( ssBytesString );
                    skillSetsNode.InnerText = ssBytesString.ToString();
                }
            }
        }

        private static string[] elementNameStrings = new string[] {
            "abilities", "abilityEffects", "items", "itemAttributes", "pspItems", "pspItemAttributes", "jobs", "jobLevels",
            "skillSets", "monsterSkills", "actionMenus", "inflictStatuses", "statusAttributes", "poaching",
            "entd1", "entd2", "entd3", "entd4", "entd5", "font", "fontWidths" };

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
            ElementName.FontWidths, "fontWidths" } );

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
            FontWidths
        }

        /// <summary>
        /// Saves this patch to an XML document.
        /// </summary>
        public static void SavePatchToFile( string path, Context destinationContext, bool saveDigest )
        {
            bool psp = destinationContext == Context.US_PSP;

            StringBuilder abilities = GetBase64StringIfNonDefault( Abilities.ToByteArray( destinationContext ), psp ? Resources.AbilitiesBin : PSXResources.AbilitiesBin );
            StringBuilder abilityEffects = GetBase64StringIfNonDefault( Abilities.ToEffectsByteArray(), psp ? Resources.AbilityEffectsBin : PSXResources.AbilityEffectsBin );
            StringBuilder oldItems = GetBase64StringIfNonDefault( Items.ToFirstByteArray(), psp ? Resources.OldItemsBin : PSXResources.OldItemsBin );
            StringBuilder oldItemAttributes = GetBase64StringIfNonDefault( ItemAttributes.ToFirstByteArray(), psp ? Resources.OldItemAttributesBin : PSXResources.OldItemAttributesBin );
            StringBuilder newItems = null;
            StringBuilder newItemAttributes = null;
            if( psp && Context == Context.US_PSP )
            {
                newItems = GetBase64StringIfNonDefault( Items.ToSecondByteArray(), Resources.NewItemsBin );
                newItemAttributes = GetBase64StringIfNonDefault( ItemAttributes.ToSecondByteArray(), Resources.NewItemAttributesBin );
            }
            StringBuilder jobs = GetBase64StringIfNonDefault( Jobs.ToByteArray( destinationContext ), psp ? Resources.JobsBin : PSXResources.JobsBin );
            StringBuilder jobLevels = GetBase64StringIfNonDefault( JobLevels.ToByteArray( destinationContext ), psp ? Resources.JobLevelsBin : PSXResources.JobLevelsBin );
            StringBuilder monsterSkills = GetBase64StringIfNonDefault( MonsterSkills.ToByteArray( destinationContext ), psp ? Resources.MonsterSkillsBin : PSXResources.MonsterSkillsBin );
            StringBuilder skillSets = GetBase64StringIfNonDefault( SkillSets.ToByteArray( destinationContext ), psp ? Resources.SkillSetsBin : PSXResources.SkillSetsBin );
            StringBuilder actionMenus = GetBase64StringIfNonDefault( ActionMenus.ToByteArray( destinationContext ), psp ? Resources.ActionEventsBin : PSXResources.ActionEventsBin );
            StringBuilder statusAttributes = GetBase64StringIfNonDefault( StatusAttributes.ToByteArray( destinationContext ), psp ? Resources.StatusAttributesBin : PSXResources.StatusAttributesBin );
            StringBuilder inflictStatuses = GetBase64StringIfNonDefault( InflictStatuses.ToByteArray(), psp ? Resources.InflictStatusesBin : PSXResources.InflictStatusesBin );
            StringBuilder poach = GetBase64StringIfNonDefault( PoachProbabilities.ToByteArray( destinationContext ), psp ? Resources.PoachProbabilitiesBin : PSXResources.PoachProbabilitiesBin );
            StringBuilder entd1 = GetBase64StringIfNonDefault( ENTDs.ENTDs[0].ToByteArray(), Resources.ENTD1 );
            StringBuilder entd2 = GetBase64StringIfNonDefault( ENTDs.ENTDs[1].ToByteArray(), Resources.ENTD2 );
            StringBuilder entd3 = GetBase64StringIfNonDefault( ENTDs.ENTDs[2].ToByteArray(), Resources.ENTD3 );
            StringBuilder entd4 = GetBase64StringIfNonDefault( ENTDs.ENTDs[3].ToByteArray(), Resources.ENTD4 );
            StringBuilder entd5 = Context == Context.US_PSP ? GetBase64StringIfNonDefault( ENTDs.PSPEventsToByteArray(), Resources.ENTD5 ) : null;
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
                writer.WriteAttributeString( "type", destinationContext.ToString() );

                StringBuilder[] builders = new StringBuilder[] { 
                    abilities, abilityEffects, oldItems, oldItemAttributes, newItems, newItemAttributes, jobs, jobLevels, 
                    skillSets, monsterSkills, actionMenus, inflictStatuses, statusAttributes, poach,
                    entd1, entd2, entd3, entd4, entd5, font, fontWidths };
                foreach( StringBuilder s in builders )
                {
                    if( s != null )
                    {
                        s.Insert( 0, "\r\n" );
                        s.Replace( "\r\n", "\r\n    " );
                        s.Append( "\r\n  " );
                    }
                }

                for( int i = 0; i < builders.Length; i++ )
                {
                    if( builders[i] != null )
                    {
                        writer.WriteElementString( elementNameStrings[i], builders[i].ToString() );
                    }
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();

                if( saveDigest )
                {
                    GenerateDigest( Path.Combine( Path.GetDirectoryName( path ), Path.GetFileNameWithoutExtension( path ) + ".digest.html" ) );
                }
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
                    InflictStatuses, PoachProbabilities, ENTDs };
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
            using( StringReader transformStringReader = new StringReader( FFTPatcher.Properties.Resources.digestTransform ) )
            using( XmlReader transformXmlReader = XmlReader.Create( transformStringReader ) )
            using( StringReader inputReader = new StringReader( sb.ToString() ) )
            using( XmlReader inputXmlReader = XmlReader.Create( inputReader ) )
            using( XmlWriter outputWriter = XmlWriter.Create( filename, settings ) )
            {
                System.Xml.Xsl.XslCompiledTransform t = new System.Xml.Xsl.XslCompiledTransform();
                t.Load( transformXmlReader );
                t.Transform( inputXmlReader, outputWriter );
            }
        }

        #endregion Methods
    }
}