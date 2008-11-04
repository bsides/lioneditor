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
    public interface IGeneratePatchList
    {
        bool[] ENTD { get; }
        bool FONT { get; }
        bool RegenECC { get; }
        string FileName { get; }
        bool Abilities { get; }
        bool AbilityEffects { get; }
        bool FontWidths { get; }
        bool MoveFindItems { get; }

        bool Items { get; }
        bool ItemAttributes { get; }
        bool Jobs { get; }
        bool JobLevels { get; }
        bool Skillsets { get; }
        bool MonsterSkills { get; }
        bool ActionMenus { get; }
        bool StatusAttributes { get; }
        bool InflictStatus { get; }
        bool Poach { get; }

        IList<PatchedByteArray> OtherPatches { get; }
    }

    public partial class PatchPSXForm : Form, IGeneratePatchList
    {
        private bool[] scusPatchable = new bool[Enum.GetValues( typeof( SCUSPatchable ) ).Length];
        private bool[] battlePatchable = new bool[Enum.GetValues( typeof( BATTLEPatchable ) ).Length];
        public bool ENTD1 { get; private set; }
        public bool ENTD2 { get; private set; }
        public bool ENTD3 { get; private set; }
        public bool ENTD4 { get; private set; }
        public bool FONT { get; private set; }
        public bool RegenECC { get; private set; }
        public string FileName { get { return isoPathTextBox.Text; } }
        public bool[] ENTD { get { return new bool[] { ENTD1, ENTD2, ENTD3, ENTD4 }; } }

        public bool Abilities
        {
            get { return scusPatchable[(int)SCUSPatchable.Abilities]; }
            set { scusPatchable[(int)SCUSPatchable.Abilities] = value; }
        }

        public bool AbilityEffects
        {
            get { return battlePatchable[(int)BATTLEPatchable.AbilityEffects]; }
            set { battlePatchable[(int)BATTLEPatchable.AbilityEffects] = value; }
        }

        public bool MoveFindItems
        {
            get { return battlePatchable[(int)BATTLEPatchable.MoveFindItems]; }
            set { battlePatchable[(int)BATTLEPatchable.MoveFindItems] = value; }
        }

        public bool FontWidths
        {
            get { return battlePatchable[(int)BATTLEPatchable.FontWidths]; }
            set { battlePatchable[(int)BATTLEPatchable.FontWidths] = value; }
        }
        public enum CustomSCEAP
        {
            NoChange,
            Default,
            Custom
        }

        public string CustomSCEAPFileName
        {
            get { return sceapFileNameTextBox.Text; }
        }

        public bool Items
        {
            get { return scusPatchable[(int)SCUSPatchable.Items]; }
            set { scusPatchable[(int)SCUSPatchable.Items] = value; }
        }

        public bool ItemAttributes
        {
            get { return scusPatchable[(int)SCUSPatchable.ItemAttributes]; }
            set { scusPatchable[(int)SCUSPatchable.ItemAttributes] = value; }
        }

        public bool Jobs
        {
            get { return scusPatchable[(int)SCUSPatchable.Jobs]; }
            set { scusPatchable[(int)SCUSPatchable.Jobs] = value; }
        }

        public bool JobLevels
        {
            get { return scusPatchable[(int)SCUSPatchable.JobLevels]; }
            set { scusPatchable[(int)SCUSPatchable.JobLevels] = value; }
        }

        public bool Skillsets
        {
            get { return scusPatchable[(int)SCUSPatchable.Skillsets]; }
            set { scusPatchable[(int)SCUSPatchable.Skillsets] = value; }
        }

        public bool MonsterSkills
        {
            get { return scusPatchable[(int)SCUSPatchable.MonsterSkills]; }
            set { scusPatchable[(int)SCUSPatchable.MonsterSkills] = value; }
        }

        public bool ActionMenus
        {
            get { return scusPatchable[(int)SCUSPatchable.ActionMenus]; }
            set { scusPatchable[(int)SCUSPatchable.ActionMenus] = value; }
        }

        public bool StatusAttributes
        {
            get { return scusPatchable[(int)SCUSPatchable.StatusAttributes]; }
            set { scusPatchable[(int)SCUSPatchable.StatusAttributes] = value; }
        }

        public bool InflictStatus
        {
            get { return scusPatchable[(int)SCUSPatchable.InflictStatus]; }
            set { scusPatchable[(int)SCUSPatchable.InflictStatus] = value; }
        }

        public bool Poach
        {
            get { return scusPatchable[(int)SCUSPatchable.Poach]; }
            set { scusPatchable[(int)SCUSPatchable.Poach] = value; }
        }

        public PatchPSXForm()
        {
            InitializeComponent();
        }

        public DialogResult CustomShowDialog( IWin32Window owner )
        {
            scusCheckedListBox.SetItemChecked( (int)SCUSPatchable.Abilities, FFTPatch.Abilities.HasChanged );
            scusCheckedListBox.SetItemChecked( (int)SCUSPatchable.ActionMenus, FFTPatch.ActionMenus.HasChanged );
            scusCheckedListBox.SetItemChecked( (int)SCUSPatchable.InflictStatus, FFTPatch.InflictStatuses.HasChanged );
            scusCheckedListBox.SetItemChecked( (int)SCUSPatchable.ItemAttributes, FFTPatch.ItemAttributes.HasChanged );
            scusCheckedListBox.SetItemChecked( (int)SCUSPatchable.Items, FFTPatch.Items.HasChanged );
            scusCheckedListBox.SetItemChecked( (int)SCUSPatchable.JobLevels, FFTPatch.JobLevels.HasChanged );
            scusCheckedListBox.SetItemChecked( (int)SCUSPatchable.Jobs, FFTPatch.Jobs.HasChanged );
            scusCheckedListBox.SetItemChecked( (int)SCUSPatchable.MonsterSkills, FFTPatch.MonsterSkills.HasChanged );
            scusCheckedListBox.SetItemChecked( (int)SCUSPatchable.Poach, FFTPatch.PoachProbabilities.HasChanged );
            scusCheckedListBox.SetItemChecked( (int)SCUSPatchable.Skillsets, FFTPatch.SkillSets.HasChanged );
            scusCheckedListBox.SetItemChecked( (int)SCUSPatchable.StatusAttributes, FFTPatch.StatusAttributes.HasChanged );

            battleCheckedListBox.SetItemChecked( (int)BATTLEPatchable.AbilityEffects, FFTPatch.Abilities.AllEffects.HasChanged );
            battleCheckedListBox.SetItemChecked( (int)BATTLEPatchable.FontWidths, false );
            battleCheckedListBox.SetItemChecked( (int)BATTLEPatchable.MoveFindItems, FFTPatch.MoveFind.HasChanged );

            dontChangeSceapRadioButton.Checked = true;

            entd1CheckBox.Checked = FFTPatch.ENTDs.ENTDs[0].HasChanged;
            entd2CheckBox.Checked = FFTPatch.ENTDs.ENTDs[1].HasChanged;
            entd3CheckBox.Checked = FFTPatch.ENTDs.ENTDs[2].HasChanged;
            entd4CheckBox.Checked = FFTPatch.ENTDs.ENTDs[3].HasChanged;
            fontCheckBox.Checked = false;
            eccCheckBox.Checked = true;

            UpdateNextEnabled();

            return ShowDialog( owner );
        }

        private void sceapRadioButton_CheckedChanged( object sender, EventArgs e )
        {
            if ( sender == useCustomSceapRadioButton && useCustomSceapRadioButton.Checked )
            {
                sceapFileNameTextBox.Enabled = true;
                sceapBrowseButton.Enabled = true;
                if ( !ValidateSCEAP( sceapFileNameTextBox.Text ) )
                {
                    sceapBrowseButton_Click( sceapBrowseButton, EventArgs.Empty );
                }

                if ( ValidateSCEAP( sceapOpenFileDialog.FileName ) )
                {
                    using ( FileStream stream = new FileStream( sceapFileNameTextBox.Text, FileMode.Open ) )
                    {
                        stream.Read( SCEAP_DAT, 0, 20480 );
                    }
                    BuildSCEAPPreview();
                }
            }
            else if ( sender == dontChangeSceapRadioButton && dontChangeSceapRadioButton.Checked )
            {
                sceapFileNameTextBox.Enabled = false;
                sceapBrowseButton.Enabled = false;
                SCEAP_DAT = new byte[20480];
                BuildSCEAPPreview();
            }
            else if ( sender == useDefaultSceapRadioButton && useDefaultSceapRadioButton.Checked )
            {
                sceapFileNameTextBox.Enabled = false;
                sceapBrowseButton.Enabled = false;
                PSXResources.SCEAPDAT.CopyTo( SCEAP_DAT, 0 );
                BuildSCEAPPreview();
            }

            UpdateNextEnabled();
        }

        private void sceapBrowseButton_Click( object sender, EventArgs e )
        {
            sceapOpenFileDialog.FileName = sceapFileNameTextBox.Text;
            while (
                sceapOpenFileDialog.ShowDialog( this ) == DialogResult.OK &&
                !ValidateSCEAP( sceapOpenFileDialog.FileName ) )
                ;
            sceapFileNameTextBox.Text = sceapOpenFileDialog.FileName;

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

        private bool ValidateSCEAP( string filename )
        {
            return 
                filename != string.Empty &&
                File.Exists( filename ) &&
                new FileInfo( filename ).Length == 20480;
        }

        private enum Checkboxes
        {
            ENTD1,
            ENTD2,
            ENTD3,
            ENTD4,
            FONT,
            RegenECC,
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
                case Checkboxes.FONT:
                    FONT = box.Checked;
                    break;
                case Checkboxes.RegenECC:
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
            if ( (string)clb.Tag == "SCUS_942.21" )
            {
                scusPatchable[e.Index] = e.NewValue == CheckState.Checked;
            }
            else if ( (string)clb.Tag == "BATTLE.BIN" )
            {
                battlePatchable[e.Index] = e.NewValue == CheckState.Checked;
            }

            UpdateNextEnabled();
        }
        private enum SCUSPatchable
        {
            Abilities,
            Items,
            ItemAttributes,
            Jobs,
            JobLevels,
            Skillsets,
            MonsterSkills,
            ActionMenus,
            StatusAttributes,
            InflictStatus,
            Poach
        }


        private void UpdateNextEnabled()
        {
            bool enabled = true;
            enabled = enabled &&
                ( !useCustomSceapRadioButton.Checked ||
                 ValidateSCEAP( sceapFileNameTextBox.Text ) );
            enabled = enabled && ValidateISO( isoPathTextBox.Text );
            enabled = enabled &&
                ( ENTD1 || ENTD2 || ENTD3 || ENTD4 || FONT || RegenECC || Abilities || Items ||
                  ItemAttributes || Jobs || JobLevels || Skillsets || MonsterSkills || ActionMenus ||
                  StatusAttributes || InflictStatus || Poach || ( SCEAP != CustomSCEAP.NoChange ) ||
                  AbilityEffects || FontWidths || MoveFindItems );

            okButton.Enabled = enabled;
        }

        private enum BATTLEPatchable
        {
            AbilityEffects,
            FontWidths,
            MoveFindItems
        }

        private byte[] SCEAP_DAT = new byte[20480];
        private Bitmap sceapPreview = new Bitmap( 320, 32 );

        private void BuildSCEAPPreview()
        {
            for ( int i = 0; i < 20480; i += 2 )
            {
                sceapPreview.SetPixel( 
                    ( i / 2 ) % 320, 
                    ( i / 2 ) / 320, 
                    BytesToColor( SCEAP_DAT[i], SCEAP_DAT[i + 1] ) );
            }
            pictureBox1.Image = sceapPreview;
            pictureBox1.Invalidate();
        }

        public CustomSCEAP SCEAP
        {
            get
            {
                return
                    useCustomSceapRadioButton.Checked ? CustomSCEAP.Custom :
                    dontChangeSceapRadioButton.Checked ? CustomSCEAP.NoChange :
                                                         CustomSCEAP.Default;

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
                if ( SCEAP != CustomSCEAP.NoChange )
                {
                    return new PatchedByteArray[] { new PatchedByteArray( PsxIso.Sectors.SCEAP_DAT, 0, SCEAP_DAT ) };
                }
                else
                {
                    return new PatchedByteArray[0];
                }
            }
        }
    }
}
