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

namespace FFTPatcher.TextEditor.Editors
{
    /// <summary>
    /// An editor for <see cref="IPartition"/> objects.
    /// </summary>
    public partial class PartitionEditor : UserControl
    {

		#region Fields (3) 

        private bool error = false;
        private bool ignoreChanges = false;
        private IPartition strings;

		#endregion Fields 

		#region Properties (2) 


        /// <summary>
        /// Gets or sets the <see cref="IPartition"/> object being edited.
        /// </summary>
        public IPartition Strings
        {
            get { return strings; }
            set
            {
                if( strings != value )
                {
                    strings = value;
                    UpdateCurrentStringListBox();
                    currentStringListBox.SelectedIndex = 0;
                    UpdateFilenames();
                    UpdateCurrentString();
                    UpdateLengthLabels();
                }
            }
        }



        /// <summary>
        /// Gets the length label format string.
        /// </summary>
        /// <value>The length label format string.</value>
        protected virtual string LengthLabelFormatString
        {
            get { return "Length: {0} bytes"; }
        }


		#endregion Properties 

		#region Constructors (1) 

        /// <summary>
        /// Initializes a new instance of the <see cref="PartitionEditor"/> class.
        /// </summary>
        public PartitionEditor()
        {
            InitializeComponent();
            currentStringListBox.SelectedIndexChanged += currentStringListBox_SelectedIndexChanged;
            currentString.TextChanged += currentString_TextChanged;
            currentString.Validating += currentString_Validating;
            currentString.Font = new Font( "Arial Unicode MS", 10 );
            filesListBox.SelectedIndexChanged += filesListBox_SelectedIndexChanged;
            saveAllButton.Click += saveAllButton_Click;
            saveThisButton.Click += saveThisButton_Click;
        }

		#endregion Constructors 

		#region Events (1) 

        /// <summary>
        /// Occurs when the user has requested that a file be saved.
        /// </summary>
        public event EventHandler<SavingFileEventArgs> SavingFile;

		#endregion Events 

		#region Methods (12) 


        private void currentString_TextChanged( object sender, EventArgs e )
        {
            if( !ignoreChanges && (currentStringListBox.SelectedIndex > -1) )
            {
                strings.Entries[currentStringListBox.SelectedIndex] = currentString.Text;
                UpdateLengthLabels();
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

        private void filesListBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            saveThisButton.Enabled = filesListBox.SelectedIndex > -1;
            saveAllButton.Enabled = filesListBox.SelectedIndex > -1;
        }

        private void FireSavingFileEvent( string suggested, int partition )
        {
            if( SavingFile != null )
            {
                if( partition != -1 )
                {
                    SavingFile( this, new SavingFileEventArgs( strings.Owner, suggested, partition ) );
                }
                else
                {
                    SavingFile( this, new SavingFileEventArgs( strings.Owner as IFile, suggested ) );
                }
            }
        }

        private void saveAllButton_Click( object sender, EventArgs e )
        {
            if( filesListBox.SelectedIndex > -1 )
            {
                FireSavingFileEvent( filesListBox.SelectedItem.ToString(), -1 );
            }
        }

        private void saveThisButton_Click( object sender, EventArgs e )
        {
            if( filesListBox.SelectedIndex > -1 )
            {
                FireSavingFileEvent( filesListBox.SelectedItem.ToString(), strings.Owner.Sections.IndexOf( strings ) );
            }
        }

        private void sectionComboBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            UpdateCurrentStringListBox();
        }

        private void UpdateCurrentString()
        {
            ignoreChanges = true;
            currentString.Text = strings.Entries[Math.Max( 0, currentStringListBox.SelectedIndex )];
            ignoreChanges = false;
        }

        private void UpdateCurrentStringListBox()
        {
            currentStringListBox.SuspendLayout();
            currentStringListBox.BeginUpdate();
            currentStringListBox.Items.Clear();
            for( int i = 0; i < strings.Entries.Count; i++ )
            {
                currentStringListBox.Items.Add( string.Format( "{0} {1}", i + 1, strings.EntryNames[i] ) );
            }
            currentStringListBox.SelectedIndex = 0;
            UpdateCurrentString();
            currentStringListBox.EndUpdate();
            currentStringListBox.ResumeLayout();
        }

        private void UpdateFilenames()
        {
            filesListBox.SuspendLayout();
            filesListBox.ClearSelected();
            filesListBox.Items.Clear();

            foreach( Enum s in strings.Owner.Locations.Keys )
            {
                filesListBox.Items.Add( s.ToString() );
            }

            filesListBox.ResumeLayout();
        }

        private void UpdateLengthLabels()
        {
            try
            {
                lengthLabel.Text = string.Format( LengthLabelFormatString, strings.Length );
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
