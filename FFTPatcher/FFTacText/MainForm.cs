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

using System.Collections.Generic;
using System.Windows.Forms;
using FFTPatcher.TextEditor.Files.PSX;
using FFTPatcher.TextEditor.Files;
using System;

namespace FFTPatcher.TextEditor
{
    public partial class MainForm : Form
    {
        private Dictionary<string, AbstractStringSectioned> stringSections = new Dictionary<string, AbstractStringSectioned>();
        private MenuItem[] menuItems;

        public MainForm()
        {
            BuildStringSections();
            InitializeComponent();
            menuItems = new MenuItem[] { 
                atchelpMenuItem, attackoutMenuItem, joinlzwMenuItem, 
                openlzwMenuItem, samplelzwMenuItem, wldhelplzwMenuItem, worldlzwMenuItem,
                pspatchelpMenuItem, pspopenlzwMenuItem, psp299024MenuItem,psp29E334MenuItem,psp2A1630MenuItem,psp2EB4C0MenuItem,psp32D368MenuItem };
            compressedStringSectionedEditor1.Visible = false;
            compressedStringSectionedEditor1.Strings = stringSections["WLDHELP.LZW"];

            stringSectionedEditor1.Visible = true;
            stringSectionedEditor1.Strings = stringSections["ATCHELP.LZW"];

            foreach( MenuItem menuItem in menuItems )
            {
                menuItem.Click +=  menuItem_Click;
            }
        }

        private void menuItem_Click( object sender, EventArgs e )
        {
            UncheckAllMenuItems( menuItems );
            MenuItem thisItem = sender as MenuItem;
            thisItem.Checked = true;

            AbstractStringSectioned file = stringSections[thisItem.Tag.ToString()];

            if( file is ICompressed )
            {
                compressedStringSectionedEditor1.Strings = file;
                compressedStringSectionedEditor1.Visible = true;
                stringSectionedEditor1.Visible = false;
            }
            else
            {
                stringSectionedEditor1.Strings = file;
                stringSectionedEditor1.Visible = true;
                compressedStringSectionedEditor1.Visible = false;
            }
        }

        private void UncheckAllMenuItems( MenuItem[] menuItems )
        {
            foreach (MenuItem item in menuItems)
            {
                item.Checked = false;
            }
        }

        private void BuildStringSections()
        {
            stringSections.Add( "ATCHELP.LZW", FFTPatcher.TextEditor.Files.PSX.ATCHELPLZW.GetInstance() );
            stringSections.Add( "ATTACK.OUT", FFTPatcher.TextEditor.Files.PSX.ATTACKOUT.GetInstance() );
            stringSections.Add( "JOIN.LZW", FFTPatcher.TextEditor.Files.PSX.JOINLZW.GetInstance() );
            stringSections.Add( "OPEN.LZW", FFTPatcher.TextEditor.Files.PSX.OPENLZW.GetInstance() );
            stringSections.Add( "SAMPLE.LZW", FFTPatcher.TextEditor.Files.PSX.SAMPLELZW.GetInstance() );
            stringSections.Add( "WLDHELP.LZW", FFTPatcher.TextEditor.Files.PSX.WLDHELPLZW.GetInstance() );
            stringSections.Add( "WORLD.LZW", FFTPatcher.TextEditor.Files.PSX.WORLDLZW.GetInstance() );
            stringSections.Add( "pspATCHELP.LZW", FFTPatcher.TextEditor.Files.PSP.ATCHELPLZW.GetInstance() );
            stringSections.Add( "pspOPEN.LZW", FFTPatcher.TextEditor.Files.PSP.OPENLZW.GetInstance() );
            stringSections.Add( "psp299024", FFTPatcher.TextEditor.Files.PSP.BOOT299024.GetInstance() );
            stringSections.Add( "psp29E334", FFTPatcher.TextEditor.Files.PSP.BOOT29E334.GetInstance() );
            stringSections.Add( "psp2A1630", FFTPatcher.TextEditor.Files.PSP.BOOT2A1630.GetInstance() );
            stringSections.Add( "psp2EB4C0", FFTPatcher.TextEditor.Files.PSP.BOOT2EB4C0.GetInstance() );
            stringSections.Add( "psp32D368", FFTPatcher.TextEditor.Files.PSP.BOOT32D368.GetInstance() );
        }
    }
}
