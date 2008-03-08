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
using System.Windows.Forms;
using FFTPatcher.TextEditor.Files;
using FFTPatcher.TextEditor.Files.PSX;
using System.IO;

namespace FFTPatcher.TextEditor
{
    public partial class MainForm : Form
    {

		#region Fields (1) 

        private MenuItem[] menuItems;

		#endregion Fields 

		#region Constructors (1) 

        public MainForm()
        {
            InitializeComponent();

            FillFiles();
            menuItems = BuildMenuItems().ToArray();
            menuItems[0].Checked = true;
            compressedStringSectionedEditor1.Visible = false;

            stringSectionedEditor1.Visible = true;
            stringSectionedEditor1.Strings = menuItems[0].Tag as IStringSectioned;

            partitionEditor1.Visible = false;

            foreach( MenuItem menuItem in menuItems )
            {
                menuItem.Click += menuItem_Click;
            }
        }

		#endregion Constructors 

		#region Methods (5) 


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
            result.Add( AddMenuItem( psxMenuItem, "ATCHELP.LZW", new ATCHELPLZW( PSXResources.ATCHELP_LZW ) ) );
            result.Add( AddMenuItem( psxMenuItem, "ATTACK.OUT", new ATTACKOUT( PSXResources.ATTACK_OUT_partial ) ) );
            result.Add( AddMenuItem( psxMenuItem, "HELPMENU.OUT", new HELPMENU( PSXResources.HELPMENU_OUT ) ) );
            result.Add( AddMenuItem( psxMenuItem, "JOIN.LZW", new JOINLZW( PSXResources.JOIN_LZW ) ) );
            result.Add( AddMenuItem( psxMenuItem, "OPEN.LZW", new OPENLZW( PSXResources.OPEN_LZW ) ) );
            result.Add( AddMenuItem( psxMenuItem, "SAMPLE.LZW", new SAMPLELZW( PSXResources.SAMPLE_LZW ) ) );
            MenuItem item = AddMenuItem( psxMenuItem, "SNPLMES.BIN", null );
            IPartitionedFile file = new SNPLMESBIN( PSXResources.SNPLMES_BIN );
            for( int i = 0; i < 6; i++ )
            {
                result.Add( AddMenuItem( item, string.Format( "Section {0}", i + 1 ), file.Sections[i] ) );
            }
            result.Add( item );
            result.Add( AddMenuItem( psxMenuItem, "WLDHELP.LZW", new WLDHELPLZW( PSXResources.WLDHELP_LZW ) ) );
            item = AddMenuItem( psxMenuItem, "WLDMES.BIN", null );
            file = new WLDMES( PSXResources.WLDMES_BIN );
            for( int i = 0; i < file.NumberOfSections; i++ )
            {
                MenuItem newItem = AddMenuItem( item, string.Format( "Section {0}", i + 1 ), file.Sections[i] );
                if( (i != 0) && (i % 25 == 0) )
                {
                    newItem.Break = true;
                }
                result.Add( newItem );
            }
            result.Add( item );
            result.Add( AddMenuItem( psxMenuItem, "WORLD.LZW", new WORLDLZW( PSXResources.WORLD_LZW ) ) );

            result.Add( AddMenuItem( pspMenuItem, "ATCHELP.LZW", new FFTPatcher.TextEditor.Files.PSP.ATCHELPLZW( PSPResources.ATCHELP_LZW ) ) );
            result.Add( AddMenuItem( pspMenuItem, "OPEN.LZW", new FFTPatcher.TextEditor.Files.PSP.OPENLZW( PSPResources.OPEN_LZW ) ) );

            result.Add( AddMenuItem( pspMenuItem, "BOOT.BIN[0x299024]", new FFTPatcher.TextEditor.Files.PSP.BOOT299024( PSPResources.BOOT_299024 ) ) );
            result.Add( AddMenuItem( pspMenuItem, "BOOT.BIN[0x29E334]", new FFTPatcher.TextEditor.Files.PSP.BOOT29E334( PSPResources.BOOT_29E334 ) ) );
            result.Add( AddMenuItem( pspMenuItem, "BOOT.BIN[0x2A1630]", new FFTPatcher.TextEditor.Files.PSP.BOOT2A1630( PSPResources.BOOT_2A1630 ) ) );
            result.Add( AddMenuItem( pspMenuItem, "BOOT.BIN[0x2EB4C0]", new FFTPatcher.TextEditor.Files.PSP.BOOT2EB4C0( PSPResources.BOOT_2EB4C0 ) ) );
            result.Add( AddMenuItem( pspMenuItem, "BOOT.BIN[0x32D368]", new FFTPatcher.TextEditor.Files.PSP.BOOT32D368( PSPResources.BOOT_32D368 ) ) );

            item = AddMenuItem( pspMenuItem, "WLDMES.BIN", null );
            file = new FFTPatcher.TextEditor.Files.PSP.WLDMES( PSPResources.WLDMES_BIN );
            for( int i = 0; i < file.NumberOfSections; i++ )
            {
                MenuItem newItem = AddMenuItem( item, string.Format( "Section {0}", i + 1 ), file.Sections[i] );
                if( (i != 0) && (i % 25 == 0) )
                {
                    newItem.Break = true;
                }
                result.Add( newItem );
            }
            result.Add( item );

            return result;
        }

        private void FillFiles()
        {
            BasePSXSectionedFile[] psxFiles1 = new BasePSXSectionedFile[] {
                new ATCHELPLZW(PSXResources.ATCHELP_LZW),
                new ATTACKOUT(PSXResources.ATTACK_OUT_partial),
                new JOINLZW(PSXResources.JOIN_LZW),
                new OPENLZW(PSXResources.OPEN_LZW),
                new SAMPLELZW(PSXResources.SAMPLE_LZW),
                new WORLDLZW(PSXResources.WORLD_LZW)};
            BasePSXCompressedFile[] psxFiles2 = new BasePSXCompressedFile[] {
                new WLDHELPLZW(PSXResources.WLDHELP_LZW),
                new HELPMENU(PSXResources.HELPMENU_OUT)};
            //BasePSXPartitionedFile[] psxFiles3 = new BasePSXPartitionedFile[] {
            //    new SNPLMESBIN(PSXResources.SNPLMES_BIN),
            //    new WLDMES(PSXResources.WLDMES_BIN) };

            foreach( BasePSXSectionedFile sectionFile in psxFiles1 )
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

                    byte[] bytes = sectionFile.ToByteArray();
                    if( bytes.Length <= sectionFile.MaxLength )
                    {
                        FileStream stream = new FileStream( Path.Combine( @"M:\dev\LionEditor\fftpack\US\changed", filename.Substring( filename.LastIndexOf( "/" ) + 1 ) ), FileMode.Open );
                        stream.Seek( kvp.Value, SeekOrigin.Begin );
                        stream.Write( bytes, 0, bytes.Length );
                        stream.Flush();
                        stream.Close();
                        stream.Dispose();
                    }
                    else
                    {
                    }
                }
            }

            foreach( BasePSXCompressedFile sectionFile in psxFiles2 )
            {
                foreach( var kvp in sectionFile.Locations )
                {
                    var filename = kvp.Key;
                    var realFilename = filename.Substring( filename.LastIndexOf( "/" ) + 1 );
                    int dotIndex = realFilename.LastIndexOf( '.' );
                    if( dotIndex < 0 )
                        dotIndex = realFilename.Length - 1;
                    realFilename = realFilename.Substring( 0, dotIndex );
                    string format = "{0}/{1}/{2:X}";

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

                    byte[] bytes = sectionFile.ToByteArray();
                    if( bytes.Length <= sectionFile.MaxLength )
                    {
                        FileStream stream = new FileStream( Path.Combine( @"M:\dev\LionEditor\fftpack\US\changed", filename.Substring( filename.LastIndexOf( "/" ) + 1 ) ), FileMode.Open );
                        stream.Seek( kvp.Value, SeekOrigin.Begin );
                        stream.Write( bytes, 0, bytes.Length );
                        stream.Flush();
                        stream.Close();
                        stream.Dispose();
                    }
                    else
                    {
                    }
                }
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
