using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using PatcherLib.Utilities;

namespace FFTorgASM
{
    public partial class MainForm : Form
    {
        AsmPatch[] patches;
        private bool ignoreChanges = true;
        public MainForm()
        {
            InitializeComponent();
            versionLabel.Text = string.Format( "v0.{0}", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Revision.ToString() );
            XmlDocument doc = new XmlDocument();
            reloadButton_Click( reloadButton, EventArgs.Empty );
            if ( patches == null || patches.Length == 0 )
            {
                IList<AsmPatch> temp;
                if ( PatchXmlReader.TryGetPatches( FFTorgASM.Properties.Resources.DefaultHacks, out temp ) )
                {
                    LoadPatches( temp );
                }
            }
            patchButton.Click += new EventHandler( patchButton_Click );
            reloadButton.Click += new EventHandler( reloadButton_Click );
            checkedListBox1.ItemCheck += new ItemCheckEventHandler( checkedListBox1_ItemCheck );
            patchButton.Enabled = false;
            checkedListBox1.SelectedIndexChanged += new EventHandler( checkedListBox1_SelectedIndexChanged );
            variableSpinner.ValueChanged += new EventHandler( variableSpinner_ValueChanged );
            variableComboBox.SelectedIndexChanged += new EventHandler( variableComboBox_SelectedIndexChanged );
        }

        void variableComboBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            if ( !ignoreChanges )
            {
                variableSpinner.Value = ( checkedListBox1.SelectedItem as AsmPatch ).Variables[variableComboBox.SelectedIndex].Value.GetBytes()[0];
            }
        }

        void variableSpinner_ValueChanged( object sender, EventArgs e )
        {
            if ( !ignoreChanges )
            {
                (checkedListBox1.SelectedItem as AsmPatch).Variables[variableComboBox.SelectedIndex].Value.GetBytes()[0] = (byte)variableSpinner.Value;
            }
        }

        void checkedListBox1_SelectedIndexChanged( object sender, EventArgs e )
        {
            AsmPatch p = checkedListBox1.SelectedItem as AsmPatch;
            textBox1.Text = p.Description;
            if ( p.Variables.Count > 0 )
            {
                ignoreChanges = true;
                variableComboBox.Items.Clear();
                p.Variables.ForEach( kvp => variableComboBox.Items.Add( kvp.Key ) );
                variableComboBox.SelectedIndex = 0;
                variableSpinner.Value = p.Variables[0].Value.GetBytes()[0];
                ignoreChanges = false;
                variableSpinner.Visible = true;
                variableComboBox.Visible = true;
            }
            else
            {
                ignoreChanges = true;
                variableComboBox.Visible = false;
                variableSpinner.Visible = false;
            }
        }

        private void LoadPatches( IList<AsmPatch> patches )
        {
            this.patches = patches.ToArray();
            checkedListBox1.Items.Clear();
            checkedListBox1.Items.AddRange( this.patches );
            patchButton.Enabled = false;
        }

        void reloadButton_Click( object sender, EventArgs e )
        {
            List<AsmPatch> result = new List<AsmPatch>();
            string[] files = Directory.GetFiles( Application.StartupPath, "*.xml", SearchOption.TopDirectoryOnly );
            foreach ( string file in files )
            {
                IList<AsmPatch> tryPatches;
                if ( PatchXmlReader.TryGetPatches( File.ReadAllText( file, Encoding.UTF8 ), out tryPatches ) )
                {
                    result.AddRange( tryPatches );
                }
            }
            LoadPatches( result );
        }

        void checkedListBox1_ItemCheck( object sender, ItemCheckEventArgs e )
        {
            patchButton.Enabled = ( checkedListBox1.CheckedItems.Count > 0 || e.NewValue == CheckState.Checked ) &&
                                  !( checkedListBox1.CheckedItems.Count == 1 && e.NewValue == CheckState.Unchecked );
        }

        void patchButton_Click( object sender, EventArgs e )
        {
            if ( saveFileDialog1.ShowDialog( this ) == DialogResult.OK )
            {
                using ( Stream file = File.Open( saveFileDialog1.FileName, FileMode.Open, FileAccess.ReadWrite ) )
                {
                    foreach ( AsmPatch patch in checkedListBox1.CheckedItems )
                    {
                        PatcherLib.Iso.PsxIso.PatchPsxIso( file, patch );
                    }
                }
            }
        }
    }
}
