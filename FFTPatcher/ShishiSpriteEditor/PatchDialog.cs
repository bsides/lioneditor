using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

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

        }

        public void LoadFullSpriteSet( FullSpriteSet set )
        {
            listView1.BeginUpdate();
            listView1.Items.Clear();
            foreach( AbstractSprite sprite in set.Sprites )
            {
                listView1.Items.Add( new ListViewItem( new string[] { 
                    sprite.Filenames[0], 
                    sprite.OriginalSize.ToString(), 
                    sprite.MaximumSize.ToString(), 
                    sprite.CurrentSize.ToString() } ) );
                if( sprite is MonsterSprite )
                {
                    MonsterSprite m = sprite as MonsterSprite;
                    if( m.Filenames.Count > 1 )
                    {
                        for( int i = 1; i < m.Filenames.Count; i++ )
                        {
                            const int sp2Length = 32768;
                            string lengthString = sp2Length.ToString();
                            listView1.Items.Add( new ListViewItem( new string[] {
                                sprite.Filenames[i],
                                lengthString,
                                lengthString,
                                lengthString } ) );
                        }
                    }
                }
            }

            listView1.EndUpdate();
        }
    }
}
