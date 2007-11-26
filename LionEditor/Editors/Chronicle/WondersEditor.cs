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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace LionEditor
{
    public partial class WondersEditor : UserControl
    {
        private Wonders wonders;
        public Wonders Wonders
        {
            get { return wonders; }
            set
            {
                wonders = value;
                checkedListBox1.Items.Clear();
                if( wonders != null )
                {
                    foreach( Wonder w in wonders.AllWonders )
                    {
                        checkedListBox1.Items.Add( w, w.Discovered );
                    }
                    checkedListBox1.SelectedIndex = 0;
                }
            }
        }

        public WondersEditor()
        {
            stupidDateEditor1 = new LionEditor.Editors.Chronicle.StupidDateEditor( StupidDate.DateDictionary[0] );
            InitializeComponent();

            checkedListBox1.SelectedIndexChanged += new EventHandler( checkedListBox1_SelectedIndexChanged );
            stupidDateEditor1.DateChangedEvent += new EventHandler( stupidDateEditor1_DateChangedEvent );
            checkedListBox1.ItemCheck += new ItemCheckEventHandler( checkedListBox1_ItemCheck );
        }

        void checkedListBox1_ItemCheck( object sender, ItemCheckEventArgs e )
        {
            Wonder w = checkedListBox1.Items[e.Index] as Wonder;
            w.Discovered = (e.NewValue == CheckState.Checked);
            FireDataChangedEvent();
        }

        void stupidDateEditor1_DateChangedEvent( object sender, EventArgs e )
        {
            Wonder w = checkedListBox1.SelectedItem as Wonder;
            w.Date = stupidDateEditor1.CurrentDate;
            FireDataChangedEvent();
        }

        void checkedListBox1_SelectedIndexChanged( object sender, EventArgs e )
        {
            Wonder w = checkedListBox1.SelectedItem as Wonder;
            stupidDateEditor1.CurrentDate = w.Date;
        }

        public event EventHandler DataChangedEvent;

        private void FireDataChangedEvent()
        {
            if( DataChangedEvent != null )
            {
                DataChangedEvent( this, EventArgs.Empty );
            }
        }

    }
}
