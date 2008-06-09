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
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using FFTPatcher.TextEditor.Files;
using System.Diagnostics;

namespace FFTPatcher.TextEditor
{
    public partial class MainForm : Form
    {

        #region Fields (3)

        private readonly MenuItem[] defaultPspMenuItems;
        private readonly MenuItem[] defaultPsxMenuItems;

        private FFTText file;
        private MenuItem[] menuItems;

        #endregion Fields

        #region Properties (1)


        /// <summary>
        /// Gets or sets the file being edited.
        /// </summary>
        public FFTText File
        {
            get { return file; }
            set
            {
                if( value == null )
                {
                    stringSectionedEditor.Visible = false;
                    compressedStringSectionedEditor.Visible = false;
                    partitionEditor.Visible = false;
                }
                else if( file != value )
                {
                    file = value;
                    if( menuItems != null )
                    {
                        foreach( MenuItem item in menuItems )
                        {
                            item.Click -= menuItem_Click;
                        }
                    }
                    menuItems = BuildMenuItems().ToArray();

                    foreach( MenuItem item in menuItems )
                    {
                        item.Click += menuItem_Click;
                    }

                    if( File.Filetype == Filetype.PSP )
                    {
                        pspMenuItem.MenuItems.AddRange( defaultPspMenuItems );
                    }
                    else if( File.Filetype == Filetype.PSX )
                    {
                        psxMenuItem.MenuItems.AddRange( defaultPsxMenuItems );
                    }

                    menuItem_Click( menuItems[0], EventArgs.Empty );
                    saveMenuItem.Enabled = true;
                    allowedSymbolsMenuItem.Enabled = true;
                }
            }
        }


        #endregion Properties

        #region Constructors (1)

        public MainForm()
        {
#if DEBUG
            FillPSPFiles();
            FillPSXFiles();
#endif
            InitializeComponent();

            stringSectionedEditor.Visible = false;
            compressedStringSectionedEditor.Visible = false;
            partitionEditor.Visible = false;
            stringSectionedEditor.SavingFile += editor_SavingFile;
            compressedStringSectionedEditor.SavingFile += editor_SavingFile;
            partitionEditor.SavingFile += partitionEditor_SavingFile;

            newPspMenuItem.Click += newPspMenuItem_Click;
            newPsxMenuItem.Click += newPsxMenuItem_Click;
            saveMenuItem.Click += saveMenuItem_Click;
            openMenuItem.Click += openMenuItem_Click;
            exitMenuItem.Click += exitMenuItem_Click;
            aboutMenuItem.Click += aboutMenuItem_Click;
            allowedSymbolsMenuItem.Click += allowedSymbolsMenuItem_Click;
            defaultPspMenuItems = new MenuItem[2] { 
                new MenuItem( "-" ), 
                new MenuItem( "Patch ISO...", patchMenuItem_Click ) };
            defaultPsxMenuItems = new MenuItem[2] {
                new MenuItem("-"),
                new MenuItem("Patch ISO...", patchMenuItem_Click ) };
        }

        #endregion Constructors

        #region Methods (22)


        private void aboutMenuItem_Click( object sender, EventArgs e )
        {
            new About().ShowDialog( this );
        }

        private MenuItem AddMenuItem( MenuItem owner, string text, object tag )
        {
            MenuItem result = new MenuItem( text );
            owner.MenuItems.Add( result );
            result.Tag = tag;
            return result;
        }

        private void allowedSymbolsMenuItem_Click( object sender, EventArgs e )
        {
            CharmapForm.Show( File.CharMap );
        }

        private List<MenuItem> BuildMenuItems()
        {
            List<MenuItem> result = new List<MenuItem>();

            IList<MenuItem> items = File.GetMenuItems();
            MenuItem parent = File.Filetype == Filetype.PSP ? pspMenuItem : psxMenuItem;
            pspMenuItem.Visible = File.Filetype == Filetype.PSP;
            psxMenuItem.Visible = File.Filetype == Filetype.PSX;

            RemoveAllDescendants( parent );

            foreach( MenuItem item in items )
            {
                if( item.Parent == null )
                {
                    parent.MenuItems.Add( item );
                }
            }
            result.AddRange( items );

            return result;
        }

        private void editor_SavingFile( object sender, SavingFileEventArgs e )
        {
            string name = Path.GetFileName( e.SuggestedFilename );
            openFileDialog.Filter = string.Format( "{0}|{0}", name );
            if( openFileDialog.ShowDialog( this ) == DialogResult.OK )
            {
                WriteBytesToFile( e.File.ToByteArray(), openFileDialog.FileName, e.File.Locations[e.SuggestedFilename] );
            }
        }

        private void exitMenuItem_Click( object sender, EventArgs e )
        {
            Application.Exit();
        }

#if DEBUG
        private void FillFile( IPartitionedFile file, string filename )
        {
            string format = "{0}/{1}/{2:X}";
            for( int section = 0; section < file.Sections.Count; section++ )
            {
                for( int i = 0; i < file.Sections[section].Entries.Count; i++ )
                {
                    string[] layout = GetLayoutOfCloseAndNewLines( file.Sections[section].Entries[i] );

                    string newString = string.Format( format, filename, section + 1, i + 1 );
                    int sub = 2;
                    foreach( string divider in layout )
                    {
                        newString += divider;
                        newString += sub.ToString();
                        sub++;
                    }

                    if( file.Sections[section].Entries[i].IndexOf( @"{END}" ) != -1 )
                    {
                        newString += @"{END}";
                    }
                    file.Sections[section].Entries[i] = newString;
                }
            }
        }

        private void FillFileExcept( IStringSectioned file, string filename, IList<int> badSections )
        {
            string format = "{0}/{1}/{2:X}";
            for( int section = 0; section < file.Sections.Count; section++ )
            {
                if( !badSections.Contains( section ) )
                {
                    for( int i = 0; i < file.Sections[section].Count; i++ )
                    {
                        string[] layout = GetLayoutOfCloseAndNewLines( file.Sections[section][i] );

                        string newString = string.Format( format, filename, section + 1, i + 1 );
                        int sub = 2;
                        foreach( string divider in layout )
                        {
                            newString += divider;
                            newString += sub.ToString();
                            sub++;
                        }

                        if( file.Sections[section][i].IndexOf( @"{END}" ) != -1 )
                        {
                            newString += @"{END}";
                        }
                        file.Sections[section][i] = newString;
                    }
                }
            }
        }

        private void FillFiles()
        {
            //BasePSXSectionedFile[] psxFiles1 = new BasePSXSectionedFile[] {
            //    new ATCHELPLZW(PSXResources.ATCHELP_LZW),
            //    new ATTACKOUT(PSXResources.ATTACK_OUT_partial),
            //    new JOINLZW(PSXResources.JOIN_LZW),
            //    new OPENLZW(PSXResources.OPEN_LZW),
            //    new SAMPLELZW(PSXResources.SAMPLE_LZW),
            //    new WORLDLZW(PSXResources.WORLD_LZW)};
            //BasePSXCompressedFile[] psxFiles2 = new BasePSXCompressedFile[] {
            //    new WLDHELPLZW(PSXResources.WLDHELP_LZW),
            //    new HELPMENU(PSXResources.HELPMENU_OUT)};
            ////BasePSXPartitionedFile[] psxFiles3 = new BasePSXPartitionedFile[] {
            ////    new SNPLMESBIN(PSXResources.SNPLMES_BIN),
            ////    new WLDMES(PSXResources.WLDMES_BIN) };
            XmlSerializer xs = new XmlSerializer( typeof( FFTText ) );

            FFTText mine = null;
            using( MemoryStream ms = new MemoryStream( PSXResources.DefaultDocument ) )
            {
                mine = xs.Deserialize( ms ) as FFTText;
            }


            foreach( IStringSectioned sectionFile in mine.SectionedFiles )
            {
                foreach( KeyValuePair<string, long> kvp in sectionFile.Locations )
                {
                    var filename = kvp.Key;
                    var realFilename = filename.Substring( filename.LastIndexOf( "/" ) + 1 );
                    int dotIndex = realFilename.LastIndexOf( '.' );
                    if( dotIndex < 0 )
                        dotIndex = realFilename.Length - 1;
                    realFilename = realFilename.Substring( 0, dotIndex );
                    string format = "{0}/{1}/{2:X}";
                    if( realFilename == "ATTACK" )
                    {
                        realFilename = "A";
                        format = "{0}{1}{2:X}";
                    }
                    else if( realFilename == "SMALL" )
                    {
                        realFilename = "S";
                        format = "{0}{1}{2:X}";
                    }
                    else if( filename.Contains( "WORLD.LZW" ) )
                    {
                        realFilename = "WLD";
                    }
                    for( int section = 0; section < sectionFile.Sections.Count; section++ )
                    {
                        for( int i = 0; i < sectionFile.Sections[section].Count; i++ )
                        {
                            string newString = string.Format( format, realFilename, section, i );
                            if( sectionFile.Sections[section][i].IndexOf( @"{END}" ) != -1 )
                            {
                                newString += @"{END}";
                            }
                            sectionFile.Sections[section][i] = newString;
                        }
                    }
                }
            }

            foreach( IPartitionedFile partitionedFile in mine.PartitionedFiles )
            {
                foreach( var kvp in partitionedFile.Locations )
                {
                    var filename = kvp.Key;
                    var realFilename = filename.Substring( filename.LastIndexOf( "/" ) + 1 );
                    int dotIndex = realFilename.LastIndexOf( '.' );
                    if( dotIndex < 0 )
                        dotIndex = realFilename.Length - 1;
                    realFilename = realFilename.Substring( 0, dotIndex );
                    string format = "{0}/{1}/{2:X}";

                    for( int section = 0; section < partitionedFile.Sections.Count; section++ )
                    {
                        for( int i = 0; i < partitionedFile.Sections[section].Entries.Count; i++ )
                        {
                            string newString = string.Format( format, realFilename, section, i );
                            if( partitionedFile.Sections[section].Entries[i].IndexOf( @"{END}" ) != -1 )
                            {
                                newString += @"{END}";
                            }
                            partitionedFile.Sections[section].Entries[i] = newString;
                        }
                    }
                }
            }

            File = mine;
        }

        private void FillPSPFiles()
        {
            XmlSerializer xs = new XmlSerializer( typeof( FFTText ) );

            FFTText mine = null;
            using( MemoryStream ms = new MemoryStream( PSPResources.DefaultDocument ) )
            {
                mine = xs.Deserialize( ms ) as FFTText;
            }

            FillFile( mine.PartitionedFiles.Find( delegate( IPartitionedFile file ) { return file.GetType().ToString().Contains( "SNPLMESBIN" ); } ), "SNPLMES" );
            FillFile( mine.PartitionedFiles.Find( delegate( IPartitionedFile file ) { return file.GetType().ToString().Contains( "WLDMESBIN" ); } ), "WLDMES" );

            foreach( IStringSectioned sectioned in mine.SectionedFiles )
            {
                FillFileExcept(
                    sectioned,
                    sectioned.GetType().ToString().Substring( sectioned.GetType().ToString().LastIndexOf( "." ) + 1 ),
                    new int[0] );
            }

            using( FileStream fs = new FileStream( "Filled.xml", FileMode.Create ) )
            {
                xs.Serialize( fs, mine );
            }
        }

        private void FillPSXFiles()
        {
            XmlSerializer xs = new XmlSerializer( typeof( FFTText ) );
            FFTText mine = null;
            using( MemoryStream ms = new MemoryStream( PSXResources.DefaultDocument ) )
            {
                mine = xs.Deserialize( ms ) as FFTText;
            }
            FillFile( mine.PartitionedFiles.Find( delegate( IPartitionedFile file ) { return file.GetType().ToString().Contains( "SNPLMESBIN" ); } ), "SNPLMES" );
            FillFile( mine.PartitionedFiles.Find( delegate( IPartitionedFile file ) { return file.GetType().ToString().Contains( "WLDMESBIN" ); } ), "WLDMES" );

            foreach( IStringSectioned sectioned in mine.SectionedFiles )
            {
                FillFileExcept(
                    sectioned,
                    sectioned.GetType().ToString().Substring( sectioned.GetType().ToString().LastIndexOf( "." ) + 1 ),
                    new int[0] );
            }

            using( FileStream fs = new FileStream( "FilledPSX.xml", FileMode.Create ) )
            {
                xs.Serialize( fs, mine );
            }
        }
#endif

        private void LoadFileFromByteArray( byte[] bytes )
        {
            XmlSerializer xs = new XmlSerializer( typeof( FFTText ) );
            using( MemoryStream ms = new MemoryStream( bytes ) )
            {
                File = xs.Deserialize( ms ) as FFTText;
            }
        }

        private void menuItem_Click( object sender, EventArgs e )
        {
            UncheckAllMenuItems( menuItems );
            MenuItem thisItem = sender as MenuItem;
            thisItem.Checked = true;

            object file = thisItem.Tag;

            if( file is ICompressed )
            {
                compressedStringSectionedEditor.Strings = file as IStringSectioned;
                compressedStringSectionedEditor.Visible = true;
                stringSectionedEditor.Visible = false;
                partitionEditor.Visible = false;
            }
            else if( file is IStringSectioned )
            {
                stringSectionedEditor.Strings = file as IStringSectioned;
                stringSectionedEditor.Visible = true;
                compressedStringSectionedEditor.Visible = false;
                partitionEditor.Visible = false;
            }
            else if( file is IPartition )
            {
                partitionEditor.Visible = true;
                partitionEditor.Strings = file as IPartition;
                compressedStringSectionedEditor.Visible = false;
                stringSectionedEditor.Visible = false;
            }
        }

        private void newPspMenuItem_Click( object sender, EventArgs e )
        {
            LoadFileFromByteArray( PSPResources.DefaultDocument );
        }

        private void newPsxMenuItem_Click( object sender, EventArgs e )
        {
            LoadFileFromByteArray( PSXResources.DefaultDocument );
        }

        private void openMenuItem_Click( object sender, EventArgs e )
        {
            openFileDialog.Filter = "FFTacText files (*.ffttext)|*.ffttext";
            if( openFileDialog.ShowDialog( this ) == DialogResult.OK )
            {
                try
                {
                    XmlSerializer xs = new XmlSerializer( typeof( FFTText ) );
                    using( FileStream stream = new FileStream( openFileDialog.FileName, FileMode.Open ) )
                    {
                        File = xs.Deserialize( stream ) as FFTText;
                    }
                }
                catch( Exception )
                {
                    MessageBox.Show( this, "Error opening file.", "Error", MessageBoxButtons.OK );
                }
            }
        }

        private void partitionEditor_SavingFile( object sender, SavingFileEventArgs e )
        {
            IPartitionedFile file = e.File as IPartitionedFile;
            if( file != null )
            {
                string name = Path.GetFileName( e.SuggestedFilename );
                openFileDialog.Filter = string.Format( "{0}|{0}", name );

                if( openFileDialog.ShowDialog( this ) == DialogResult.OK )
                {
                    if( e.PartitionNumber == -1 )
                    {
                        WriteBytesToFile( file.ToByteArray(), openFileDialog.FileName, e.File.Locations[e.SuggestedFilename] );
                    }
                    else
                    {
                        WriteBytesToFile( file.Sections[e.PartitionNumber].ToByteArray(), openFileDialog.FileName,
                            e.File.Locations[e.SuggestedFilename] + file.SectionLength * e.PartitionNumber );
                    }
                }
            }
        }

        private void patchMenuItem_Click( object sender, EventArgs e )
        {
            if( File.Filetype == Filetype.PSP )
            {
                openFileDialog.Filter = "ISO images (*.iso)|*.iso";
                if( openFileDialog.ShowDialog( this ) == DialogResult.OK )
                {
                    try
                    {
                        using( FileStream stream = new FileStream( openFileDialog.FileName, FileMode.Open ) )
                        {
                            File.UpdatePspIso( stream );
                        }
                    }
                    catch( Exception )
                    {
                        MessageBox.Show( this, "Error patching file.", "Error", MessageBoxButtons.OK );
                    }
                }
            }
            else if( File.Filetype == Filetype.PSX )
            {
                Enabled = false;
                DataReceivedEventHandler dataReceived = new DataReceivedEventHandler( delegate( object o, DataReceivedEventArgs drea ) { } );
                EventHandler finished = new EventHandler(
                    delegate( object o2, EventArgs ea )
                    {
                        MethodInvoker mi = new MethodInvoker( delegate() { Enabled = true; } );
                        if( InvokeRequired )
                        {
                            Invoke( mi );
                        }
                        else
                        {
                            mi();
                        }
                    } );
                File.UpdatePsxIso( dataReceived, finished );
            }
        }

        private void RemoveAllDescendants( MenuItem item )
        {
            foreach( MenuItem subitem in item.MenuItems )
            {
                RemoveAllDescendants( subitem );
            }

            item.MenuItems.Clear();
        }

        private void saveMenuItem_Click( object sender, EventArgs e )
        {
            saveFileDialog.Filter = "FFTacText files (*.ffttext)|*.ffttext";
            if( saveFileDialog.ShowDialog( this ) == DialogResult.OK )
            {
                try
                {
                    XmlSerializer xs = new XmlSerializer( typeof( FFTText ) );
                    using( FileStream stream = new FileStream( saveFileDialog.FileName, FileMode.Create ) )
                    using( XmlTextWriter writer = new XmlTextWriter( stream, System.Text.Encoding.UTF8 ) )
                    {
                        writer.Formatting = Formatting.Indented;
                        xs.Serialize( writer, File );
                    }
                }
                catch( Exception )
                {
                    MessageBox.Show( this, "Error saving file.", "Error", MessageBoxButtons.OK );
                }
            }
        }

        private void UncheckAllMenuItems( MenuItem[] menuItems )
        {
            foreach( MenuItem item in menuItems )
            {
                item.Checked = false;
            }
        }

        private void WriteBytesToFile( byte[] bytes, string filename, long position )
        {
            try
            {
                using( FileStream stream = new FileStream( filename, FileMode.Open ) )
                {
                    stream.WriteArrayToPosition( bytes, position );
                }
            }
            catch( Exception )
            {
                MessageBox.Show( this, "Error writing to file", "Error", MessageBoxButtons.OK );
            }
        }


        #endregion Methods
#if DEBUG
        private string[] GetLayoutOfCloseAndNewLines( string s )
        {
            List<string> result = new List<string>();
            int lastIndex = 0;

            while( lastIndex != -1 )
            {
                int lastIndexA = s.IndexOf( @"{Close}", lastIndex );
                int lastIndexB = s.IndexOf( "\r\n", lastIndex );
                if( lastIndexA == -1 && lastIndexB == -1 )
                {
                    lastIndex = -1;
                }
                else if( lastIndexA != -1 && lastIndexB != -1 )
                {
                    lastIndex = Math.Min( lastIndexA, lastIndexB );
                    if( lastIndex == lastIndexA )
                    {
                        lastIndex += @"{Close}".Length;
                        result.Add( @"{Close}" );
                    }
                    else
                    {
                        lastIndex += "\r\n".Length;
                        result.Add( "\r\n" );
                    }
                }
                else if( lastIndexA != -1 )
                {
                    lastIndex = lastIndexA;
                    lastIndex += @"{Close}".Length;
                    result.Add( @"{Close}" );
                }
                else
                {
                    lastIndex = lastIndexB;
                    lastIndex += "\r\n".Length;
                    result.Add( "\r\n" );
                }
            }

            return result.ToArray();
        }
#endif

    }
}