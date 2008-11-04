using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using FFTPatcher.Datatypes;

namespace FFTPatcher
{
    public partial class PatchPSPForm : Form, IGeneratePatchList
    {
        private bool[] bootBinPatchable = new bool[Enum.GetValues( typeof( BootBinPatchable ) ).Length];
        public bool ENTD1 { get; private set; }
        public bool ENTD2 { get; private set; }
        public bool ENTD3 { get; private set; }
        public bool ENTD4 { get; private set; }
        public bool ENTD5 { get; private set; }
        public bool FONT { get; private set; }
        public bool RegenECC { get; private set; }
        public string FileName { get { return isoPathTextBox.Text; } }
        public bool[] ENTD { get { return new bool[] { ENTD1, ENTD2, ENTD3, ENTD4, ENTD5 }; } }

        public bool Abilities
        {
            get { return bootBinPatchable[(int)BootBinPatchable.Abilities]; }
            set { bootBinPatchable[(int)BootBinPatchable.Abilities] = value; }
        }

        public bool AbilityEffects
        {
            get { return bootBinPatchable[(int)BootBinPatchable.AbilityEffects]; }
            set { bootBinPatchable[(int)BootBinPatchable.AbilityEffects] = value; }
        }

        public bool MoveFindItems
        {
            get { return bootBinPatchable[(int)BootBinPatchable.MoveFindItems]; }
            set { bootBinPatchable[(int)BootBinPatchable.MoveFindItems] = value; }
        }

        public bool FontWidths
        {
            get { return bootBinPatchable[(int)BootBinPatchable.FontWidths]; }
            set { bootBinPatchable[(int)BootBinPatchable.FontWidths] = value; }
        }
        public enum CustomICON0
        {
            NoChange,
            Default,
            Custom
        }

        public string CustomICON0FileName
        {
            get { return icon0FileNameTextBox.Text; }
        }

        public bool Items
        {
            get { return bootBinPatchable[(int)BootBinPatchable.Items]; }
            set { bootBinPatchable[(int)BootBinPatchable.Items] = value; }
        }

        public bool ItemAttributes
        {
            get { return bootBinPatchable[(int)BootBinPatchable.ItemAttributes]; }
            set { bootBinPatchable[(int)BootBinPatchable.ItemAttributes] = value; }
        }

        public bool Jobs
        {
            get { return bootBinPatchable[(int)BootBinPatchable.Jobs]; }
            set { bootBinPatchable[(int)BootBinPatchable.Jobs] = value; }
        }

        public bool JobLevels
        {
            get { return bootBinPatchable[(int)BootBinPatchable.JobLevels]; }
            set { bootBinPatchable[(int)BootBinPatchable.JobLevels] = value; }
        }

        public bool Skillsets
        {
            get { return bootBinPatchable[(int)BootBinPatchable.Skillsets]; }
            set { bootBinPatchable[(int)BootBinPatchable.Skillsets] = value; }
        }

        public bool MonsterSkills
        {
            get { return bootBinPatchable[(int)BootBinPatchable.MonsterSkills]; }
            set { bootBinPatchable[(int)BootBinPatchable.MonsterSkills] = value; }
        }

        public bool ActionMenus
        {
            get { return bootBinPatchable[(int)BootBinPatchable.ActionMenus]; }
            set { bootBinPatchable[(int)BootBinPatchable.ActionMenus] = value; }
        }

        public bool StatusAttributes
        {
            get { return bootBinPatchable[(int)BootBinPatchable.StatusAttributes]; }
            set { bootBinPatchable[(int)BootBinPatchable.StatusAttributes] = value; }
        }

        public bool InflictStatus
        {
            get { return bootBinPatchable[(int)BootBinPatchable.InflictStatus]; }
            set { bootBinPatchable[(int)BootBinPatchable.InflictStatus] = value; }
        }

        public bool Poach
        {
            get { return bootBinPatchable[(int)BootBinPatchable.PoachProbabilities]; }
            set { bootBinPatchable[(int)BootBinPatchable.PoachProbabilities] = value; }
        }

        public PatchPSPForm()
        {
            InitializeComponent();
        }

        public DialogResult CustomShowDialog( IWin32Window owner )
        {
            bootBinCheckedListBox.SetItemChecked( (int)BootBinPatchable.Abilities, FFTPatch.Abilities.HasChanged );
            bootBinCheckedListBox.SetItemChecked( (int)BootBinPatchable.ActionMenus, FFTPatch.ActionMenus.HasChanged );
            bootBinCheckedListBox.SetItemChecked( (int)BootBinPatchable.InflictStatus, FFTPatch.InflictStatuses.HasChanged );
            bootBinCheckedListBox.SetItemChecked( (int)BootBinPatchable.ItemAttributes, FFTPatch.ItemAttributes.HasChanged );
            bootBinCheckedListBox.SetItemChecked( (int)BootBinPatchable.Items, FFTPatch.Items.HasChanged );
            bootBinCheckedListBox.SetItemChecked( (int)BootBinPatchable.JobLevels, FFTPatch.JobLevels.HasChanged );
            bootBinCheckedListBox.SetItemChecked( (int)BootBinPatchable.Jobs, FFTPatch.Jobs.HasChanged );
            bootBinCheckedListBox.SetItemChecked( (int)BootBinPatchable.MonsterSkills, FFTPatch.MonsterSkills.HasChanged );
            bootBinCheckedListBox.SetItemChecked( (int)BootBinPatchable.PoachProbabilities, FFTPatch.PoachProbabilities.HasChanged );
            bootBinCheckedListBox.SetItemChecked( (int)BootBinPatchable.Skillsets, FFTPatch.SkillSets.HasChanged );
            bootBinCheckedListBox.SetItemChecked( (int)BootBinPatchable.StatusAttributes, FFTPatch.StatusAttributes.HasChanged );
            bootBinCheckedListBox.SetItemChecked( (int)BootBinPatchable.MoveFindItems, FFTPatch.MoveFind.HasChanged );
            bootBinCheckedListBox.SetItemChecked( (int)BootBinPatchable.AbilityEffects, FFTPatch.Abilities.AllEffects.HasChanged );
            bootBinCheckedListBox.SetItemChecked( (int)BootBinPatchable.FontWidths, false );

            dontChangeIcon0RadioButton.Checked = true;

            entd1CheckBox.Checked = FFTPatch.ENTDs.ENTDs[0].HasChanged;
            entd2CheckBox.Checked = FFTPatch.ENTDs.ENTDs[1].HasChanged;
            entd3CheckBox.Checked = FFTPatch.ENTDs.ENTDs[2].HasChanged;
            entd4CheckBox.Checked = FFTPatch.ENTDs.ENTDs[3].HasChanged;
            entd5CheckBox.Checked = FFTPatch.ENTDs.PSPEvent.Exists( e => e.HasChanged );
            decryptCheckBox.Checked = true;

            UpdateNextEnabled();

            return ShowDialog( owner );
        }

        private void icon0RadioButton_CheckedChanged( object sender, EventArgs e )
        {
            if ( sender == useCustomIcon0RadioButton && useCustomIcon0RadioButton.Checked )
            {
                icon0FileNameTextBox.Enabled = true;
                icon0BrowseButton.Enabled = true;
                if ( !ValidateICON0( icon0FileNameTextBox.Text ) )
                {
                    icon0BrowseButton_Click( icon0BrowseButton, EventArgs.Empty );
                }

                if( ValidateICON0( icon0OpenFileDialog.FileName ) )
                {
                    using( FileStream stream = new FileStream( icon0OpenFileDialog.FileName, FileMode.Open ) )
                    {
                        stream.Read( ICON0_PNG, 0, 17506 );
                    }
                    using( MemoryStream stream = new MemoryStream( ICON0_PNG, 0, 17506, false ) )
                    using( Image i = Image.FromFile( icon0OpenFileDialog.FileName ) )
                    {
                        BuildICON0Preview( i );
                    }
                }
            }
            else if ( sender == dontChangeIcon0RadioButton && dontChangeIcon0RadioButton.Checked )
            {
                icon0FileNameTextBox.Enabled = false;
                icon0BrowseButton.Enabled = false;
                ICON0_PNG = new byte[17506];
                BuildICON0Preview( blankICON0 );
            }
            else if ( sender == useDefaultIcon0RadioButton && useDefaultIcon0RadioButton.Checked )
            {
                icon0FileNameTextBox.Enabled = false;
                icon0BrowseButton.Enabled = false;
                BuildICON0Preview( Resources.ICON0_PNG );
            }

            UpdateNextEnabled();
        }

        Bitmap blankICON0 = new Bitmap( 144, 80 );

        private void icon0BrowseButton_Click( object sender, EventArgs e )
        {
            icon0OpenFileDialog.FileName = icon0FileNameTextBox.Text;
            while (
                icon0OpenFileDialog.ShowDialog( this ) == DialogResult.OK &&
                !ValidateICON0( icon0OpenFileDialog.FileName ) )
                ;
            icon0FileNameTextBox.Text = icon0OpenFileDialog.FileName;

            UpdateNextEnabled();
        }

        private void isoBrowseButton_Click( object sender, EventArgs e )
        {
            patchIsoDialog.FileName = isoPathTextBox.Text;
            while (
                patchIsoDialog.ShowDialog( this ) == DialogResult.OK &&
                !ValidateISO( patchIsoDialog.FileName ) )
                ;
            isoPathTextBox.Text = patchIsoDialog.FileName;
            UpdateNextEnabled();
        }

        private bool ValidateISO( string filename )
        {
            return
                filename != string.Empty &&
                File.Exists( filename );
        }

        private bool ValidateICON0( string filename )
        {
            try
            {
                using( Image i = Image.FromFile( filename ) )
                {
                    return
                        filename != string.Empty &&
                        File.Exists( filename ) &&
                        i.Width == 144 &&
                        i.Height == 80 &&
                        new FileInfo( filename ).Length < 17506;
                }
            }
            catch( Exception )
            {
                return false;
            }
        }

        private enum Checkboxes
        {
            ENTD1,
            ENTD2,
            ENTD3,
            ENTD4,
            ENTD5,
            Decrypt,
        }

        private void entd2CheckBox_CheckedChanged( object sender, EventArgs e )
        {
            CheckBox box = (CheckBox)sender;
            Checkboxes cb = (Checkboxes)Enum.Parse( typeof( Checkboxes ), box.Tag as string );
            switch ( cb )
            {
                case Checkboxes.ENTD1:
                    ENTD1 = box.Checked;
                    break;
                case Checkboxes.ENTD2:
                    ENTD2 = box.Checked;
                    break;
                case Checkboxes.ENTD3:
                    ENTD3 = box.Checked;
                    break;
                case Checkboxes.ENTD4:
                    ENTD4 = box.Checked;
                    break;
                case Checkboxes.ENTD5:
                    ENTD5 = box.Checked;
                    break;
                case Checkboxes.Decrypt:
                    RegenECC = box.Checked;
                    break;
                default:
                    break;
            }
            UpdateNextEnabled();
        }

        private void checkedListBox1_ItemCheck( object sender, ItemCheckEventArgs e )
        {
            CheckedListBox clb = (CheckedListBox)sender;
            if ( (string)clb.Tag == "BOOT.BIN" )
            {
                bootBinPatchable[e.Index] = e.NewValue == CheckState.Checked;
            }

            UpdateNextEnabled();
        }

        private enum BootBinPatchable
        {
            Abilities,
            AbilityEffects,
            Items,
            ItemAttributes,
            Jobs,
            JobLevels,
            Skillsets,
            MonsterSkills,
            ActionMenus,
            StatusAttributes,
            InflictStatus,
            PoachProbabilities,
            MoveFindItems,
            Font,
            FontWidths
        }

        private void UpdateNextEnabled()
        {
            bool enabled = true;
            enabled = enabled &&
                ( !useCustomIcon0RadioButton.Checked ||
                 ValidateICON0( icon0FileNameTextBox.Text ) );
            enabled = enabled && ValidateISO( isoPathTextBox.Text );
            enabled = enabled &&
                ( ENTD1 || ENTD2 || ENTD3 || ENTD4 || ENTD5 || FONT || RegenECC || Abilities || Items ||
                  ItemAttributes || Jobs || JobLevels || Skillsets || MonsterSkills || ActionMenus ||
                  StatusAttributes || InflictStatus || Poach || ( ICON0 != CustomICON0.NoChange ) ||
                  AbilityEffects || FontWidths || MoveFindItems );

            okButton.Enabled = enabled;
        }

        private byte[] ICON0_PNG = new byte[17506];
        private Bitmap icon0Preview = new Bitmap( 144, 80 );

        private void BuildICON0Preview(Image i)
        {
            pictureBox1.Image = i;
        }

        public CustomICON0 ICON0
        {
            get
            {
                return
                    useCustomIcon0RadioButton.Checked ? CustomICON0.Custom :
                    dontChangeIcon0RadioButton.Checked ? CustomICON0.NoChange :
                                                         CustomICON0.Default;

            }
        }

        private static Color BytesToColor( byte first, byte second )
        {
            int b = (second & 0x7C) << 1;
            int g = (second & 0x03) << 6 | (first & 0xE0) >> 2;
            int r = (first & 0x1F) << 3;

            return Color.FromArgb( r, g, b );
        }

        public IList<PatchedByteArray> OtherPatches
        {
            get
            {
                if( ICON0 == CustomICON0.NoChange )
                {
                    return new PatchedByteArray[0];
                }
                else
                {
                    return new PatchedByteArray[] { new PatchedByteArray( PspIso.Files.PSP_GAME.ICON0_PNG, 0, ICON0_PNG ) };
                }
            }
        }
    }
}
