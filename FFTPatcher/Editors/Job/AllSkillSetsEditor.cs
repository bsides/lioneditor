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
using System.Windows.Forms;
using FFTPatcher.Datatypes;
using PatcherLib.Datatypes;

namespace FFTPatcher.Editors
{
    public partial class AllSkillSetsEditor : UserControl
    {
		#region Instance Variables (2) 

        private SkillSet cbSkillSet = null;
        private Context ourContext = Context.Default;

		#endregion Instance Variables 

		#region Public Properties (1) 

        public int SelectedIndex { get { return skillSetListBox.SelectedIndex; } set { skillSetListBox.SelectedIndex = value; } }

		#endregion Public Properties 

		#region Constructors (1) 

        public AllSkillSetsEditor()
        {
            InitializeComponent();
            skillSetEditor.DataChanged += new EventHandler( skillSetEditor_DataChanged );
            skillSetListBox.ContextMenu = new ContextMenu( new MenuItem[] {
                new MenuItem("Clone", CloneClick),
                new MenuItem("Paste", PasteClick) } );
            skillSetListBox.ContextMenu.Popup += new EventHandler( ContextMenu_Popup );
            skillSetListBox.MouseDown += new MouseEventHandler( skillSetListBox_MouseDown );
        }

		#endregion Constructors 

		#region Public Methods (1) 

        public void UpdateView( AllSkillSets skills )
        {
            if( ourContext != FFTPatch.Context )
            {
                ourContext = FFTPatch.Context;
                cbSkillSet = null;
            }
            skillSetListBox.SelectedIndexChanged -= skillSetListBox_SelectedIndexChanged;
            skillSetListBox.DataSource = skills.SkillSets;
            skillSetListBox.SelectedIndexChanged += skillSetListBox_SelectedIndexChanged;
            skillSetListBox.SelectedIndex = 0;
            skillSetEditor.SkillSet = skillSetListBox.SelectedItem as SkillSet;
        }

		#endregion Public Methods 

		#region Private Methods (6) 

        private void CloneClick( object sender, EventArgs args )
        {
            cbSkillSet = skillSetListBox.SelectedItem as SkillSet;
        }

        void ContextMenu_Popup( object sender, EventArgs e )
        {
            skillSetListBox.ContextMenu.MenuItems[1].Enabled = cbSkillSet != null;
        }

        private void PasteClick( object sender, EventArgs args )
        {
            if( cbSkillSet != null )
            {
                cbSkillSet.CopyTo( skillSetListBox.SelectedItem as SkillSet );
                skillSetEditor.UpdateView();
                skillSetEditor_DataChanged( skillSetEditor, EventArgs.Empty );
            }
        }

        private void skillSetEditor_DataChanged( object sender, EventArgs e )
        {
            CurrencyManager cm = (CurrencyManager)BindingContext[skillSetListBox.DataSource];
            cm.Refresh();
        }

        void skillSetListBox_MouseDown( object sender, MouseEventArgs e )
        {
            if( e.Button == MouseButtons.Right )
            {
                skillSetListBox.SelectedIndex = skillSetListBox.IndexFromPoint( e.Location );
            }
        }

        private void skillSetListBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            SkillSet s = skillSetListBox.SelectedItem as SkillSet;
            skillSetEditor.SkillSet = s;
        }

		#endregion Private Methods 
    }
}
