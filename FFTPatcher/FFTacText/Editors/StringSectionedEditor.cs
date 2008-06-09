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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using FFTPatcher.TextEditor.Files;

namespace FFTPatcher.TextEditor
{
    /// <summary>
    /// An editor for <see cref="IStringSectioned"/> objects.
    /// </summary>
    public partial class StringSectionedEditor : UserControl
    {

        #region Fields (3)

        protected bool error = false;
        private bool ignoreChanges = false;
        private IStringSectioned strings;

        #endregion Fields

        #region Properties (2)


        protected virtual string LengthLabelFormatString
        {
            get { return "Length: {0} bytes"; }
        }

        /// <summary>
        /// Gets or sets the <see cref="IStringSectioned"/> object being edited.
        /// </summary>
        public virtual IStringSectioned Strings
        {
            get { return strings; }
            set
            {
                if( strings != value )
                {
                    strings = value;
                    AddSections();
                    sectionComboBox.SelectedIndex = 0;
                    UpdateCurrentStringListBox();
                    //currentStringListBox.SelectedIndex = 0;
                    UpdateCurrentString();
                    UpdateFilenames();
                    UpdateLengthLabels();
                }
            }
        }


        #endregion Properties

        #region Constructors (1)

        public StringSectionedEditor()
        {
            InitializeComponent();
            sectionComboBox.SelectedIndexChanged += sectionComboBox_SelectedIndexChanged;
            currentStringListBox.SelectedIndexChanged += currentStringListBox_SelectedIndexChanged;
            stringListEditor1.CellTextChanged += stringListEditor1_CellTextChanged;
            //currentString.TextChanged += currentString_TextChanged;
            //currentString.Validating += currentString_Validating;
            //currentString.Font = new Font( "Arial Unicode MS", 10 );
            filesListBox.SelectedIndexChanged += filesListBox_SelectedIndexChanged;
            saveButton.Click += saveButton_Click;
            errorLabel.VisibleChanged += errorLabel_VisibleChanged;
        }

        #endregion Constructors

        #region Events (1)

        public event EventHandler<SavingFileEventArgs> SavingFile;

        #endregion Events

        #region Methods (13)


        private void AddSections()
        {
            sectionComboBox.BeginUpdate();
            sectionComboBox.Items.Clear();
            for( int i = 0; i < strings.Sections.Count; i++ )
            {
                sectionComboBox.Items.Add( string.Format( "{0} {1}", i + 1, strings.SectionNames[i] ) );
            }
            sectionComboBox.EndUpdate();
        }

        private void currentString_TextChanged( object sender, EventArgs e )
        {
            if( !ignoreChanges && (sectionComboBox.SelectedIndex > -1) && (currentStringListBox.SelectedIndex > -1) )
            {
                //strings.Sections[sectionComboBox.SelectedIndex][currentStringListBox.SelectedIndex] = currentString.Text;
                UpdateLengthLabels();
            }
        }

        private void stringListEditor1_CellTextChanged( object sender, StringListEditor.CellValidatingEventArgs e )
        {
            if( !ignoreChanges && (sectionComboBox.SelectedIndex > -1) )
            {
                string s = e.FormattedValue.ToString();
                strings.Sections[sectionComboBox.SelectedIndex][e.RowIndex] = s;
                UpdateLengthLabels();
                e.Cancel = error;
            }
        }

        private void currentString_Validating( object sender, CancelEventArgs e )
        {
            e.Cancel = error;
        }

        private void currentStringListBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            UpdateCurrentString();
        }

        private void errorLabel_VisibleChanged( object sender, EventArgs e )
        {
            saveButton.Enabled = filesListBox.SelectedIndex > -1 && !error;
        }

        private void filesListBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            saveButton.Enabled = filesListBox.SelectedIndex > -1 && !error;
        }

        private void FireSavingFileEvent( string suggested )
        {
            if( SavingFile != null )
            {
                SavingFile( this, new SavingFileEventArgs( strings, suggested ) );
            }
        }

        private void saveButton_Click( object sender, EventArgs e )
        {
            if( filesListBox.SelectedIndex > -1 )
            {
                FireSavingFileEvent( filesListBox.SelectedItem.ToString() );
            }
        }

        private void sectionComboBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            UpdateCurrentStringListBox();
        }

        private void UpdateCurrentString()
        {
            ignoreChanges = true;
            //currentString.Text = strings.Sections[Math.Max( 0, sectionComboBox.SelectedIndex )][Math.Max( 0, currentStringListBox.SelectedIndex )];
            ignoreChanges = false;
        }

        private void UpdateCurrentStringListBox()
        {
            stringListEditor1.BindTo( strings.EntryNames[Math.Max( 0, sectionComboBox.SelectedIndex )], strings.Sections[Math.Max( 0, sectionComboBox.SelectedIndex )] );
            //currentStringListBox.SuspendLayout();
            //currentStringListBox.BeginUpdate();
            //currentStringListBox.Items.Clear();
            //for( int i = 0; i < strings.Sections[Math.Max( 0, sectionComboBox.SelectedIndex )].Count; i++ )
            //{
            //    currentStringListBox.Items.Add( string.Format( "{0} {1}", i + 1, strings.EntryNames[sectionComboBox.SelectedIndex][i] ) );
            //}
            //currentStringListBox.SelectedIndex = 0;
            //UpdateCurrentString();
            //currentStringListBox.EndUpdate();
            //currentStringListBox.ResumeLayout();
        }

        private void UpdateFilenames()
        {
            filesListBox.SuspendLayout();
            filesListBox.BeginUpdate();
            filesListBox.ClearSelected();
            filesListBox.Items.Clear();
            foreach( string s in strings.Locations.Keys )
            {
                filesListBox.Items.Add( s );
            }
            filesListBox.EndUpdate();
            filesListBox.ResumeLayout();
        }

        private void UpdateLengthLabels()
        {
            try
            {
                lengthLabel.Text = string.Format( LengthLabelFormatString, strings.EstimatedLength );
                maxLengthLabel.Text = string.Format( "Max: {0} bytes", strings.MaxLength );

                error = false;
                errorLabel.Visible = false;
            }
            catch( Exception )
            {
                error = true;
                errorLabel.Visible = true;
            }
        }

        #endregion Methods

    }
}
