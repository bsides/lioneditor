using System.Windows.Forms;
using FFTPatcher.Datatypes;

namespace FFTPatcher.Editors
{
    public partial class InflictStatusEditor : UserControl
    {
        private readonly string[] flags = new string[] { 
            "AllOrNothing", "Random", "Separate", "Cancel", 
            "Blank1", "Blank2", "Blank3", "Blank4" };
        private bool ignoreChanges = false;
        private InflictStatus status;
        public InflictStatus InflictStatus
        {
            get { return status; }
            set
            {
                if( value == null )
                {
                    status = null;
                    this.Enabled = false;
                }
                else if( value != status )
                {
                    status = value;
                    this.Enabled = true;
                    UpdateView();
                }
            }
        }

        public InflictStatusEditor()
        {
            InitializeComponent();
            flagsCheckedListBox.ItemCheck += flagsCheckedListBox_ItemCheck;
        }

        private void flagsCheckedListBox_ItemCheck( object sender, ItemCheckEventArgs e )
        {
            if( !ignoreChanges )
            {
                Utilities.SetFlag( status, flags[e.Index], e.NewValue == CheckState.Checked );
            }
        }

        private void UpdateView()
        {
            ignoreChanges = true;
            SuspendLayout();
            flagsCheckedListBox.SuspendLayout();
            inflictStatusesEditor.SuspendLayout();

            for( int i = 0; i < 8; i++ )
            {
                flagsCheckedListBox.SetItemChecked( i, Utilities.GetFlag( status, flags[i] ) );
            }
            inflictStatusesEditor.Statuses = status.Statuses;

            inflictStatusesEditor.ResumeLayout();
            flagsCheckedListBox.ResumeLayout();
            ResumeLayout();
            ignoreChanges = false;
        }

    }
}
