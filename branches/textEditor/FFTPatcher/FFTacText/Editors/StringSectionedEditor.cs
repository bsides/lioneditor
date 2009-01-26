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
using FFTPatcher.TextEditor.Files;

namespace FFTPatcher.TextEditor
{
    /// <summary>
    /// An editor for <see cref="IStringSectioned"/> objects.
    /// </summary>
    public partial class StringSectionedEditor : UserControl
    {

        /// <summary>
        /// Whether or not there is an error with the current input.
        /// </summary>
        protected bool error = false;
        private bool ignoreChanges = false;
        private IStringSectioned strings;

        /// <summary>
        /// Gets the length label format string.
        /// </summary>
        /// <value>The length label format string.</value>
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
                    UpdateFilenames();
                    UpdateLengthLabels();
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringSectionedEditor"/> class.
        /// </summary>
        public StringSectionedEditor()
        {
            InitializeComponent();
            sectionComboBox.SelectedIndexChanged += sectionComboBox_SelectedIndexChanged;
            stringListEditor1.TextBoxTextChanged += currentString_TextChanged;
            stringListEditor1.CellValidating += currentString_Validating;
            //filesListBox.SelectedIndexChanged += filesListBox_SelectedIndexChanged;
            //saveButton.Click += saveButton_Click;
            errorLabel.VisibleChanged += errorLabel_VisibleChanged;
            errorLabel.Visible = false;
        }

        /// <summary>
        /// Occurs when the user has requested that a file be saved.
        /// </summary>
        public event EventHandler<SavingFileEventArgs> SavingFile;

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
            // if( !ignoreChanges && (sectionComboBox.SelectedIndex > -1) && stringListEditor1.CurrentRow > -1 )
            // {
                // try
                // {
                    // strings[sectionComboBox.SelectedIndex, stringListEditor1.CurrentRow] = (sender as Control).Text;
                    // UpdateLengthLabels();
                // }
                // catch( Exception )
                // {
                    // error = true;
                    // errorLabel.Visible = true;
                // }
            // }
        }

        private void currentString_Validating( object sender, DataGridViewCellValidatingEventArgs e )
        {
            if ( e.ColumnIndex == StringListEditor.TextColumnIndex )
            {
                string s = e.FormattedValue as string;

                if ( Strings.CharMap.ValidateString( s ) )
                {
                    errorLabel.Visible = false;
                }
                else
                {
                    e.Cancel = true;
                    errorLabel.Visible = true;
                    errorLabel.Text = string.Format( "Error near \"{0}\"", Strings.CharMap.LastError );
                }
            }
        }

        private void errorLabel_VisibleChanged( object sender, EventArgs e )
        {
            //saveButton.Enabled = filesListBox.SelectedIndex > -1 && !error;
        }

        private void filesListBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            //saveButton.Enabled = filesListBox.SelectedIndex > -1 && !error;
        }

        private void FireSavingFileEvent( string suggested )
        {
            //if( SavingFile != null )
            //{
            //    SavingFile( this, new SavingFileEventArgs( strings, suggested ) );
            //}
        }

        private void saveButton_Click( object sender, EventArgs e )
        {
            //if( filesListBox.SelectedIndex > -1 )
            //{
            //    FireSavingFileEvent( filesListBox.SelectedItem.ToString() );
            //}
        }

        private void sectionComboBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            Cursor = Cursors.WaitCursor;
            UpdateCurrentStringListBox();
            Cursor = Cursors.Default;
        }

        private void UpdateCurrentStringListBox()
        {
            stringListEditor1.BindTo( strings.EntryNames[Math.Max( 0, sectionComboBox.SelectedIndex )], strings.Sections[Math.Max( 0, sectionComboBox.SelectedIndex )] );
        }

        private void UpdateFilenames()
        {
            // filesListBox.SuspendLayout();
            // filesListBox.BeginUpdate();
            // filesListBox.ClearSelected();
            // filesListBox.Items.Clear();
            // foreach( Enum s in strings.Locations.Keys )
            // {
            //     filesListBox.Items.Add( s.ToString() );
            // }
            // filesListBox.EndUpdate();
            // filesListBox.ResumeLayout();
        }

        private void UpdateLengthLabels()
        {
            // try
            // {
                // lengthLabel.Text = string.Format( LengthLabelFormatString, strings.EstimatedLength );
                // maxLengthLabel.Text = string.Format( "Max: {0} bytes", strings.MaxLength );

                // error = false;
                // errorLabel.Visible = false;
            // }
            // catch( Exception )
            // {
                // error = true;
                // errorLabel.Visible = true;
            // }

            // bool quickedit = Strings is IQuickEdit;
            // maxLengthLabel.Visible = !quickedit;
            // lengthLabel.Visible = !quickedit;
            // filesListBox.Visible = !quickedit;
            // saveButton.Visible = !quickedit;
        }


 

    }
}
