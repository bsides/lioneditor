using System.Windows.Forms;

namespace FFTPatcher.SpriteEditor
{
    internal class FlickerFreeListView : ListView
    {
        public FlickerFreeListView()
            : base()
        {
            DoubleBuffered = true;
        }
    }

}
