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

        private class FlickerFreeListView : ListView
        {
            public FlickerFreeListView()
                : base()
            {
                DoubleBuffered = true;
            }
        }

        public FullSpriteSetEditor()
        {
            InitializeComponent();
            listView1.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler( listView1_RetrieveVirtualItem );
            listView1.CacheVirtualItems += new CacheVirtualItemsEventHandler( listView1_CacheVirtualItems );
            listView1.Enabled = false;
        }

        void listView1_CacheVirtualItems( object sender, CacheVirtualItemsEventArgs e )
        {
        }

        private List<ListViewItem> items;

        public void LoadFullSpriteSet( FullSpriteSet set )
        {
            items = new List<ListViewItem>();

            listView1.LargeImageList = set.Thumbnails;

            IList<AbstractSprite> sprites = set.Sprites;
            for( int i = 0; i < sprites.Count; i++ )
            {
                items.Add( new ListViewItem( sprites[i].Name, i ) );
            }

            listView1.Enabled = true;
            listView1.VirtualListSize = set.Sprites.Count;

            FullSpriteSet = set;

        }

        private void listView1_RetrieveVirtualItem( object sender, RetrieveVirtualItemEventArgs e )
        {
            if( items != null && e.ItemIndex < items.Count )
            {
                e.Item = items[e.ItemIndex];
            }
        }
    }
}
