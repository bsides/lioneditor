using System.Collections.Generic;
using System.Windows.Forms;
using FFTPatcher.Datatypes;

namespace FFTPatcher.Editors
{
    public partial class StatusesEditor : UserControl
    {
        private Statuses statuses;
        private bool ignoreChanges = false;

        public string Status { get { return statusGroupBox.Text; } set { statusGroupBox.Text = value; } }

        public Statuses Statuses
        {
            get { return statuses; }
            set
            {
                if( value == null )
                {
                    this.Enabled = false;
                    statuses = null;
                }
                else if( statuses != value )
                {
                    this.Enabled = true;
                    statuses = value;
                    UpdateView();
                }
            }
        }

        public StatusesEditor()
        {
            InitializeComponent();
            statusesCheckedListBox.ItemCheck += statusesCheckedListBox_ItemCheck;
        }

        private void statusesCheckedListBox_ItemCheck( object sender, ItemCheckEventArgs e )
        {
            if( !ignoreChanges )
            {
                Utilities.SetFlag( statuses, Statuses.FieldNames[e.Index], e.NewValue == CheckState.Checked );
            }
        }

        private void UpdateView()
        {
            this.SuspendLayout();
            statusesCheckedListBox.SuspendLayout();

            ignoreChanges = true;
            for( int i = 0; i < statusesCheckedListBox.Items.Count; i++ )
            {
                statusesCheckedListBox.SetItemChecked( i, Utilities.GetFlag( statuses, Statuses.FieldNames[i] ) );
            }
            ignoreChanges = false;
            statusesCheckedListBox.ResumeLayout();
            this.ResumeLayout();
        }
    }
}
