using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FFTPatcher.TextEditor.Editors
{
    partial class FileEditor : UserControl
    {
        private IFile boundFile;

        bool ignoreChanges;
        public void BindTo( IFile file )
        {
            ignoreChanges = true;
            IList<string> sectionNames = file.SectionNames;
            sectionComboBox.Items.Clear();
            sectionNames.ForEach( n => sectionComboBox.Items.Add( n ) );
            sectionComboBox.SelectedIndex = 0;
            stringListEditor1.BindTo( file.EntryNames[0], file, 0 );
            boundFile = file;
            ignoreChanges = false;
        }

        public FileEditor()
        {
            InitializeComponent();
            sectionComboBox.SelectedIndexChanged += new EventHandler( sectionComboBox_SelectedIndexChanged );
            stringListEditor1.CellValidating += new DataGridViewCellValidatingEventHandler( stringListEditor1_CellValidating );
        }

        private void stringListEditor1_CellValidating( object sender, DataGridViewCellValidatingEventArgs e )
        {
            e.Cancel = !boundFile.CharMap.ValidateString( e.FormattedValue.ToString() );
            if ( e.Cancel )
            {
                errorLabel.Text = boundFile.CharMap.LastError;
                errorLabel.Visible = true;
            }
            else
            {
                errorLabel.Visible = false;
            }
        }

        private void sectionComboBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            if ( !ignoreChanges && boundFile != null )
            {
                stringListEditor1.BindTo( boundFile.EntryNames[sectionComboBox.SelectedIndex], boundFile, sectionComboBox.SelectedIndex );
            }
        }
    }
}
