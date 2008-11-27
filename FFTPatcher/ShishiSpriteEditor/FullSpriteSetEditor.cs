using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FFTPatcher.SpriteEditor
{
    public partial class FullSpriteSetEditor : UserControl
    {
        public FullSpriteSet FullSpriteSet { get; private set; }
        private ListViewGroup type1Group = new ListViewGroup( "TYPE1" );
        private ListViewGroup type2Group = new ListViewGroup( "TYPE2" );
        private ListViewGroup shortGroup = new ListViewGroup( "Short" );
        private ListViewGroup specialGroup = new ListViewGroup( "Special" );
        private ListViewGroup monsterGroup = new ListViewGroup( "MON" );

        public FullSpriteSetEditor()
        {
            InitializeComponent();
            listView1.Enabled = false;
            listView1.Activation = ItemActivation.Standard;
            listView1.ItemActivate += new EventHandler( listView1_ItemActivate );
            listView1.Groups.Add( type1Group );
            listView1.Groups.Add( type2Group );
            listView1.Groups.Add( specialGroup );
            listView1.Groups.Add( shortGroup );
            listView1.Groups.Add( monsterGroup );
        }

        public class ImageEventArgs : EventArgs
        {
            public AbstractSprite Sprite { get; private set; }
            public ImageEventArgs( AbstractSprite sprite )
            {
                Sprite = sprite;
            }
        }

        public event EventHandler<ImageEventArgs> ImageActivated;

        void listView1_ItemActivate( object sender, EventArgs e )
        {
            if ( ImageActivated != null )
            {
                ImageActivated( this, new ImageEventArgs( FullSpriteSet.Sprites[listView1.SelectedIndices[0]] ) );
            }
            
        }

        private ListViewItem[] items;

        /// <summary>
        /// Loads the full sprite set.
        /// </summary>
        /// <param name="set">The set.</param>
        public void LoadFullSpriteSet( FullSpriteSet set )
        {
            listView1.BeginUpdate();
            listView1.ShowGroups = true;
            items = new ListViewItem[set.Sprites.Count];

            listView1.LargeImageList = set.Thumbnails;

            IList<AbstractSprite> sprites = set.Sprites;
            for ( int i = 0; i < sprites.Count; i++ )
            {
                items[i] = new ListViewItem( sprites[i].Name, i );
                if ( sprites[i] is TYPE1Sprite )
                {
                    items[i].Group = type1Group;
                }
                else if ( sprites[i] is TYPE2Sprite )
                {
                    items[i].Group = type2Group;
                }
                else if ( sprites[i] is MonsterSprite )
                {
                    items[i].Group = monsterGroup;
                }
                else if ( sprites[i] is ShortSprite )
                {
                    items[i].Group = shortGroup;
                }
                else
                {
                    items[i].Group = specialGroup;
                }

                sprites[i].PixelsChanged += new EventHandler( FullSpriteSetEditor_PixelsChanged );
            }

            listView1.Items.Clear();
            listView1.Items.AddRange( items );

            listView1.Enabled = true;
            
            FullSpriteSet = set;
            listView1.EndUpdate();

        }

        private void FullSpriteSetEditor_PixelsChanged( object sender, EventArgs e )
        {
            int idx = FullSpriteSet.Sprites.IndexOf( sender as AbstractSprite );
            listView1.LargeImageList.Images[idx] = FullSpriteSet.Sprites[idx].GetThumbnail();
        }

    }
}
