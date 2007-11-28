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
    public partial class FeatsEditor : UserControl
    {
        private Feats feats;
        public Feats Feats
        {
            get { return feats; }
            set 
            { 
                feats = value;
                if( feats != null )
                {
                    featListBox.DataSource = feats.AllFeats;
                }
            }
        }

        public FeatsEditor()
        {
            stupidDateEditor1 = new LionEditor.Editors.Chronicle.StupidDateEditor( StupidDate.DateDictionary[0] );
            InitializeComponent();

            stateComboBox.Items.Add( State.Active );
            stateComboBox.Items.Add( State.Complete );
            stateComboBox.Items.Add( State.None );
            stupidDateEditor1.DateChangedEvent += new EventHandler( stupidDateEditor1_DateChangedEvent );
            featListBox.SelectedIndexChanged += new EventHandler( featListBox_SelectedIndexChanged );
            stateComboBox.SelectedIndexChanged += new EventHandler( stateComboBox_SelectedIndexChanged );
        }

        void featListBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            Feat f = featListBox.SelectedItem as Feat;
            stateComboBox.SelectedItem = f.State;
            stupidDateEditor1.CurrentDate = f.Date;
        }

        void stateComboBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            Feat f = featListBox.SelectedItem as Feat;
            f.State = (State)stateComboBox.SelectedItem;
        }

        void stupidDateEditor1_DateChangedEvent( object sender, EventArgs e )
        {
            Feat f = featListBox.SelectedItem as Feat;
            f.Date = stupidDateEditor1.CurrentDate;
            FireDataChangedEvent();
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
