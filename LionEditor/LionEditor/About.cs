/*
    Copyright 2007, Joe Davidson <joedavidson@gmail.com>

    This file is part of LionEditor.

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
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace LionEditor
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            textBoxNoCaret.Text =
@"Credits:
    Alan Morris: For his FFTEdit source code, especially the checksum algorithm and text encoding information
    Avaj: FFTastic was used to determine the offsets of some values
    FreePlay: Kernel plugin function hooking method
    Fritz Fraundorf: Final Fantasy Tactics: War of the Lions FAQ
    aerostar: Final Fantasy Tactics Battle Mechanics Guide

Copyright 2007, Joe Davidson <joedavidson@gmail.com>

LionEditor is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.

LionEditor is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License along with LionEditor.  If not, see <http://www.gnu.org/licenses/>.";
            versionLabel.Text = string.Format( "v0.{0}", Assembly.GetExecutingAssembly().GetName().Version.Revision.ToString() );
        }
    }

    public class TextBoxNoCaret : TextBox
    {
        [DllImport( "user32.dll", EntryPoint = "HideCaret" )]
        public static extern bool HideCaret( IntPtr hwnd );

        protected override void WndProc( ref Message m )
        {
            base.WndProc( ref m );
            HideCaret( this.Handle );
        }
    }
}

