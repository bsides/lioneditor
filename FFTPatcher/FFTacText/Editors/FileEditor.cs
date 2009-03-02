using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PatcherLib.Utilities;

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
            restoreButton.Visible = boundFile is ISerializableFile;
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

        private void restoreButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "ISO files (*.bin, *.iso, *.img)|*.bin;*.iso;*.img";
            openFileDialog1.FileName = string.Empty;
            if (openFileDialog1.ShowDialog(this.TopLevelControl as Form) == DialogResult.OK)
            {
                using (System.IO.Stream stream = System.IO.File.OpenRead(openFileDialog1.FileName))
                {
                    (boundFile as AbstractFile).RestoreFile(stream);
                }
                BindTo(boundFile);
                //AbstractFile.ConstructFile(f.Layout.FileType, f.CharMap, f.Layout, 
            }
        }
    }
}
