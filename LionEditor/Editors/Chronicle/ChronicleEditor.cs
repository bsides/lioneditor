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
    public partial class ChronicleEditor : UserControl
    {
        UserControl currentControl = null;
        ArtefactsEditor artefacts = new ArtefactsEditor();
        EventsEditor events = new EventsEditor();
        FeatsEditor feats = new FeatsEditor();
        PersonaeEditor personae = new PersonaeEditor();
        WondersEditor wonders = new WondersEditor();
        UserControl[] controls;

        public uint Kills
        {
            get { return (uint)killsSpinner.Value; }
            set { killsSpinner.Value = value; }
        }

        public uint Casualties
        {
            get { return (uint)casualtiesSpinner.Value; }
            set { casualtiesSpinner.Value = value; }
        }

        public uint Timer
        {
            get { return timerEditor1.Value; }
            set { timerEditor1.Value = value; }
        }

        public Feats Feats
        {
            get { return feats.Feats; }
            set { feats.Feats = value; }
        }

        public Wonders Wonders
        {
            get { return wonders.Wonders; }
            set { wonders.Wonders = value; }
        }

        public Artefacts Artefacts
        {
            get { return artefacts.Artefacts; }
            set { artefacts.Artefacts = value; }
        }

        public StupidDate Date
        {
            get { return date.CurrentDate; }
            set { date.CurrentDate = value; }
        }

        public uint WarFunds
        {
            get { return (uint)warFunds.Value; }
            set { warFunds.Value = value; }
        }

        public ChronicleEditor()
        {
            date = new LionEditor.Editors.Chronicle.StupidDateEditor();
            InitializeComponent();
            listBox1.SelectedIndexChanged += selectedPageChanged;
            controls = new UserControl[] { events, personae, feats, wonders, artefacts };
            events.DataChangedEvent += controlDataChangedEvent;
            personae.DataChangedEvent += controlDataChangedEvent;
            feats.DataChangedEvent += controlDataChangedEvent;
            wonders.DataChangedEvent += controlDataChangedEvent;
            artefacts.DataChangedEvent += controlDataChangedEvent;
            casualtiesSpinner.ValueChanged += controlDataChangedEvent;
            killsSpinner.ValueChanged += controlDataChangedEvent;
            timerEditor1.DataChangedEvent += controlDataChangedEvent;
            warFunds.Validated += controlDataChangedEvent;
            date.DateChangedEvent += controlDataChangedEvent;

            listBox1.SelectedIndex = 0;
        }

        void controlDataChangedEvent( object sender, EventArgs e )
        {
            FireDataChangedEvent();
        }

        void selectedPageChanged( object sender, EventArgs e )
        {
            if( currentControl != null )
            {
                tableLayoutPanel1.Controls.RemoveAt( tableLayoutPanel1.Controls.IndexOf( currentControl ) );
            }
            currentControl = controls[listBox1.SelectedIndex];
            //currentControl.Dock = DockStyle.Fill;
            currentControl.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            currentControl.Location = new Point( 0, 0 );
            tableLayoutPanel1.Controls.Add( controls[listBox1.SelectedIndex], 1, 1 );
            //tableLayoutPanel1.SetRowSpan( controls[listBox1.SelectedIndex], 2 );
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
