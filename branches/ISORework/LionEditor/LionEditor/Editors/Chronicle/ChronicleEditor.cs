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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LionEditor
{
    public partial class ChronicleEditor : UserControl
    {
        #region Fields

        private UserControl currentControl = null;
        private ArtefactsEditor artefacts = new ArtefactsEditor();
        private EventsEditor events = new EventsEditor();
        private FeatsEditor feats = new FeatsEditor();
        private PersonaeEditor personae = new PersonaeEditor();
        private WondersEditor wonders = new WondersEditor();
        private UserControl[] controls;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the number of kills in the kills spinbox
        /// </summary>
        public uint Kills
        {
            get { return (uint)killsSpinner.Value; }
            set { killsSpinner.Value = value; }
        }

        /// <summary>
        /// Gets or sets the number of casualties in the casualties spinbox
        /// </summary>
        public uint Casualties
        {
            get { return (uint)casualtiesSpinner.Value; }
            set { casualtiesSpinner.Value = value; }
        }

        /// <summary>
        /// Gets or set the gametime in the timer editor
        /// </summary>
        public uint Timer
        {
            get { return timerEditor.Value; }
            set { timerEditor.Value = value; }
        }

        /// <summary>
        /// Gets or sets the feats in the feats editor
        /// </summary>
        public Feats Feats
        {
            get { return feats.Feats; }
            set { feats.Feats = value; }
        }

        /// <summary>
        /// Gets or sets the wonders in the wonders editor
        /// </summary>
        public Wonders Wonders
        {
            get { return wonders.Wonders; }
            set { wonders.Wonders = value; }
        }

        /// <summary>
        /// Gets or sets the artefacts in the artfacts editor
        /// </summary>
        public Artefacts Artefacts
        {
            get { return artefacts.Artefacts; }
            set { artefacts.Artefacts = value; }
        }

        /// <summary>
        /// Gets or sets the date in the date editor
        /// </summary>
        [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden)]
        public StupidDate Date
        {
            get { return date.CurrentDate; }
            set { date.CurrentDate = value; }
        }

        /// <summary>
        /// Gets or sets the value in the war funds spinbox
        /// </summary>
        public uint WarFunds
        {
            get { return (uint)warFunds.Value; }
            set { warFunds.Value = value; }
        }

        #endregion

        public ChronicleEditor()
        {
            date = new StupidDateEditor();
            InitializeComponent();
            pageSelector.SelectedIndexChanged += selectedPageChanged;
            controls = new UserControl[] { events, personae, feats, wonders, artefacts };
            events.DataChangedEvent += controlDataChangedEvent;
            personae.DataChangedEvent += controlDataChangedEvent;
            feats.DataChangedEvent += controlDataChangedEvent;
            wonders.DataChangedEvent += controlDataChangedEvent;
            artefacts.DataChangedEvent += controlDataChangedEvent;
            casualtiesSpinner.ValueChanged += controlDataChangedEvent;
            killsSpinner.ValueChanged += controlDataChangedEvent;
            timerEditor.DataChangedEvent += controlDataChangedEvent;
            warFunds.Validated += controlDataChangedEvent;
            date.DateChangedEvent += controlDataChangedEvent;

            pageSelector.SelectedIndex = 0;
        }

        #region Events

        void controlDataChangedEvent( object sender, EventArgs e )
        {
            FireDataChangedEvent();
        }

        void selectedPageChanged( object sender, EventArgs e )
        {
            if( currentControl != null )
            {
                entireTable.Controls.RemoveAt( entireTable.Controls.IndexOf( currentControl ) );
            }
            currentControl = controls[pageSelector.SelectedIndex];
            currentControl.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            currentControl.Location = new Point( 0, 0 );
            entireTable.Controls.Add( controls[pageSelector.SelectedIndex], 1, 1 );
        }

        public event EventHandler DataChangedEvent;

        private void FireDataChangedEvent()
        {
            if( DataChangedEvent != null )
            {
                DataChangedEvent( this, EventArgs.Empty );
            }
        }

        #endregion
    }
}
