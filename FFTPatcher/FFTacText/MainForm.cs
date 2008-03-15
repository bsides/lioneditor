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

namespace FFTPatcher.TextEditor
{
    public partial class MainForm : Form
    {

		#region Fields (2) 

        private FFTText file;
        private MenuItem[] menuItems;

		#endregion Fields 

		#region Properties (1) 


        public FFTText File
        {
            get { return file; }
            set
            {
                if( value == null )
                {
                    stringSectionedEditor1.Visible = false;
                    compressedStringSectionedEditor1.Visible = false;
                    partitionEditor1.Visible = false;
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

                    menuItem_Click( menuItems[0], EventArgs.Empty );
                    saveMenuItem.Enabled = true;
                }
            }
        }


		#endregion Properties 

		#region Constructors (1) 

        public MainForm()
        {
            InitializeComponent();

            stringSectionedEditor1.Visible = false;
            compressedStringSectionedEditor1.Visible = false;
            partitionEditor1.Visible = false;
            stringSectionedEditor1.SavingFile += editor_SavingFile;
            compressedStringSectionedEditor1.SavingFile += editor_SavingFile;

            newPspMenuItem.Click += newPspMenuItem_Click;
            newPsxMenuItem.Click += newPsxMenuItem_Click;
            saveMenuItem.Click += saveMenuItem_Click;
            openMenuItem.Click += openMenuItem_Click;
            exitMenuItem.Click += exitMenuItem_Click;

            //FillFiles();
        }

		#endregion Constructors 

		#region Methods (13) 


        private MenuItem AddMenuItem( MenuItem owner, string text, object tag )
        {
            MenuItem result = new MenuItem( text );
            owner.MenuItems.Add( result );
            result.Tag = tag;
            return result;
        }

        private List<MenuItem> BuildMenuItems()
        {
            List<MenuItem> result = new List<MenuItem>();

            IList<MenuItem> items = File.GetMenuItems();
            MenuItem parent = File.Filetype == Filetype.PSP ? pspMenuItem : psxMenuItem;
            pspMenuItem.Visible = File.Filetype == Filetype.PSP;
            psxMenuItem.Visible = File.Filetype == Filetype.PSX;

            RemoveAllDescendants( parent );

            foreach( MenuItem psxItem in items )
            {
                if( psxItem.Parent == null )
                {
                    parent.MenuItems.Add( psxItem );
                }
            }
            result.AddRange( items );

            return result;
        }

        private void editor_SavingFile( object sender, SavingFileEventArgs e )
        {
            string name = Path.GetFileName( e.SuggestedFilename );
            saveFileDialog.Filter = string.Format( "{0}|{0}", name );
            if( saveFileDialog.ShowDialog( this ) == DialogResult.OK )
            {
                try
                {
                    using( FileStream stream = new FileStream( saveFileDialog.FileName, FileMode.Open ) )
                    {
                        stream.WriteArrayToPosition( e.File.ToByteArray(), e.File.Locations[e.SuggestedFilename] );
                    }
                }
                catch( Exception )
                {
                    MessageBox.Show( this, "Error writing to file", "Error", MessageBoxButtons.OK );
                }
            }
        }

        private void exitMenuItem_Click( object sender, EventArgs e )
        {
            Application.Exit();
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
                compressedStringSectionedEditor1.Strings = file as IStringSectioned;
                compressedStringSectionedEditor1.Visible = true;
                stringSectionedEditor1.Visible = false;
                partitionEditor1.Visible = false;
            }
            else if( file is IStringSectioned )
            {
                stringSectionedEditor1.Strings = file as IStringSectioned;
                stringSectionedEditor1.Visible = true;
                compressedStringSectionedEditor1.Visible = false;
                partitionEditor1.Visible = false;
            }
            else if( file is IPartition )
            {
                partitionEditor1.Visible = true;
                partitionEditor1.Strings = file as IPartition;
                compressedStringSectionedEditor1.Visible = false;
                stringSectionedEditor1.Visible = false;
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


		#endregion Methods 

    }
}
