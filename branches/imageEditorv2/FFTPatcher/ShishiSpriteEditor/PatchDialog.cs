using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using PatcherLib.Datatypes;

namespace FFTPatcher.SpriteEditor
{
    public partial class PatchDialog : Form
    {
        public PatchDialog()
        {
            InitializeComponent();
            listView1.Columns.Add( "Filename" );
            listView1.Columns.Add( "Original Size" );
            listView1.Columns.Add( "Max Size" );
            listView1.Columns.Add( "New Size" );
            listView1.ItemChecked += new ItemCheckedEventHandler( listView1_ItemChecked );
        }

        private void listView1_ItemChecked( object sender, ItemCheckedEventArgs e )
        {
            if ( e.Item.Checked )
            {
                set.Add( (KeyValuePair<AbstractSprite, int>)e.Item.Tag );
            }
            else
            {
                set.Remove( (KeyValuePair<AbstractSprite, int>)e.Item.Tag );
            }
        }

        private bool ValidateISO( string filename )
        {
            return
                filename != string.Empty &&
                File.Exists( filename );
        }

        private void isoBrowseButton_Click( object sender, EventArgs e )
        {
            fileSaveDialog.FileName = isoPathTextBox.Text;
            while (
                fileSaveDialog.ShowDialog( this ) == DialogResult.OK &&
                !ValidateISO( fileSaveDialog.FileName ) )
                ;
            isoPathTextBox.Text = fileSaveDialog.FileName;
            UpdateNextEnabled();
        }

        public string Filename { get { return isoPathTextBox.Text; } }

        private void UpdateNextEnabled()
        {
            okButton.Enabled = ValidateISO( isoPathTextBox.Text );
        }

        private Set<KeyValuePair<AbstractSprite, int>> set;

        private ListViewItem GenerateItem( string filename, int originalSize, int maximumSize, int currentSize )
        {
            ListViewItem result = new ListViewItem( new string[] {
                filename,
                originalSize.ToString( System.Globalization.CultureInfo.InvariantCulture ),
                maximumSize.ToString( System.Globalization.CultureInfo.InvariantCulture ),
                currentSize.ToString( System.Globalization.CultureInfo.InvariantCulture ) } );

            if ( currentSize > maximumSize )
            {
                const int currentSizeIndex = 3;
                result.UseItemStyleForSubItems = false;
                Font f = result.SubItems[currentSizeIndex].Font;
                result.SubItems[currentSizeIndex].Font = new Font( f, FontStyle.Bold );
            }

            return result;
        }

        public void LoadFullSpriteSet( FullSpriteSet set )
        {
            this.set = new Set<KeyValuePair<AbstractSprite, int>>();
            isoPathTextBox.Text = string.Empty;

            listView1.BeginUpdate();
            listView1.Items.Clear();
            foreach ( AbstractSprite sprite in set.Sprites )
            {
                ListViewItem item = GenerateItem(
                    sprite.Filenames[0],
                    sprite.OriginalSize,
                    sprite.MaximumSize,
                    sprite.CurrentSize );
                item.Tag = new KeyValuePair<AbstractSprite, int>( sprite, 0 );
                listView1.Items.Add( item );

                if ( sprite is MonsterSprite )
                {
                    MonsterSprite m = sprite as MonsterSprite;
                    if ( m.Filenames.Count > 1 )
                    {
                        for ( int i = 1; i < m.Filenames.Count; i++ )
                        {
                            const int sp2Length = 32768;
                            string lengthString = sp2Length.ToString();
                            ListViewItem item2 = GenerateItem(
                                sprite.Filenames[i],
                                sp2Length,
                                sp2Length,
                                sp2Length );
                            item2.Tag = new KeyValuePair<AbstractSprite, int>( sprite, i );
                            listView1.Items.Add( item2 );
                        }
                    }
                }
            }

            listView1.AutoResizeColumns( ColumnHeaderAutoResizeStyle.HeaderSize );
            listView1.EndUpdate();
        }

        private void SetAllItems( bool checkState )
        {
            listView1.BeginUpdate();
            int count = listView1.Items.Count;
            for ( int i = 0; i < count; i++ )
            {
                listView1.Items[i].Checked = checkState;
            }
            listView1.EndUpdate();
        }

        private void checkAllButton_Click( object sender, EventArgs e )
        {
            SetAllItems( true );
        }

        private void uncheckButton_Click( object sender, EventArgs e )
        {
            SetAllItems( false );
        }

        private void toggleButton_Click( object sender, EventArgs e )
        {
            listView1.BeginUpdate();
            int count = listView1.Items.Count;
            for ( int i = 0; i < count; i++ )
            {
                listView1.Items[i].Checked = !listView1.Items[i].Checked;
            }
            listView1.EndUpdate();
        }
    }
}
