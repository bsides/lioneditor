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
using System.Windows.Forms;

namespace LionEditor
{
    public partial class ArtefactsEditor : UserControl
    {
        private Artefacts artefacts;

        /// <summary>
        /// Gets or sets the <see cref="Artefacts"/> currently being edited.
        /// </summary>
        public Artefacts Artefacts
        {
            get { return artefacts; }
            set
            {
                artefacts = value;
                artefactsListBox.Items.Clear();
                if( artefacts != null )
                {
                    foreach( Artefact a in artefacts.AllArtefacts )
                    {
                        artefactsListBox.Items.Add( a, a.Discovered );
                    }
                    artefactsListBox.SelectedIndex = 0;
                }
            }
        }

        public ArtefactsEditor()
        {
            stupidDateEditor = new StupidDateEditor( StupidDate.DateDictionary[0] );
            InitializeComponent();

            artefactsListBox.SelectedIndexChanged += artefactsListBox_SelectedIndexChanged;
            stupidDateEditor.DateChangedEvent += stupidDateEditor_DateChangedEvent;
            artefactsListBox.ItemCheck += artefactsListBox_ItemCheck;
        }

        #region Events

        private void artefactsListBox_ItemCheck( object sender, ItemCheckEventArgs e )
        {
            Artefact a = artefactsListBox.Items[e.Index] as Artefact;
            a.Discovered = (e.NewValue == CheckState.Checked);
            FireDataChangedEvent();
        }

        private void stupidDateEditor_DateChangedEvent( object sender, EventArgs e )
        {
            Artefact a = artefactsListBox.SelectedItem as Artefact;
            a.Date = stupidDateEditor.CurrentDate;
            FireDataChangedEvent();
        }

        private void artefactsListBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            Artefact a = artefactsListBox.SelectedItem as Artefact;
            stupidDateEditor.CurrentDate = a.Date;
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
