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
    public partial class WondersEditor : UserControl
    {
        #region Fields

        private Wonders wonders;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the wonders currently being edited
        /// </summary>
        public Wonders Wonders
        {
            get { return wonders; }
            set
            {
                wonders = value;
                wondersListBox.Items.Clear();
                if( wonders != null )
                {
                    foreach( Wonder w in wonders.AllWonders )
                    {
                        wondersListBox.Items.Add( w, w.Discovered );
                    }
                    wondersListBox.SelectedIndex = 0;
                }
            }
        }

        #endregion

        #region Events

        private void wondersListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            Wonder w = wondersListBox.Items[e.Index] as Wonder;
            w.Discovered = (e.NewValue == CheckState.Checked);
            FireDataChangedEvent();
        }

        private void stupidDateEditor_DateChangedEvent(object sender, EventArgs e)
        {
            Wonder w = wondersListBox.SelectedItem as Wonder;
            w.Date = stupidDateEditor.CurrentDate;
            FireDataChangedEvent();
        }

        private void wondersListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Wonder w = wondersListBox.SelectedItem as Wonder;
            stupidDateEditor.CurrentDate = w.Date;
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

        public WondersEditor()
        {
            stupidDateEditor = new StupidDateEditor( StupidDate.DateDictionary[0] );
            InitializeComponent();

            wondersListBox.SelectedIndexChanged += wondersListBox_SelectedIndexChanged;
            stupidDateEditor.DateChangedEvent += stupidDateEditor_DateChangedEvent;
            wondersListBox.ItemCheck += wondersListBox_ItemCheck;
        }
    }
}
