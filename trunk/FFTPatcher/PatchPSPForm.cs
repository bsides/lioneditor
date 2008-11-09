/*
    Copyright 2007, Joe Davidson <joedavidson@gmail.com>

    This file is part of FFTPatcher.

    FFTPatcher is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    FFTPatcher is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with FFTPatcher.  If not, see <http://www.gnu.org/licenses/>.
*/

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
		#region Instance Variables (4) 

        Bitmap blankICON0 = new Bitmap( 144, 80 );
        private bool[] bootBinPatchable = new bool[Enum.GetValues( typeof( BootBinPatchable ) ).Length];
        private byte[] ICON0_PNG = new byte[17506];
        private Bitmap icon0Preview = new Bitmap( 144, 80 );

		#endregion Instance Variables 

		#region Public Properties (27) 

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

        public bool ActionMenus
        {
            get { return bootBinPatchable[(int)BootBinPatchable.ActionMenus]; }
            set { bootBinPatchable[(int)BootBinPatchable.ActionMenus] = value; }
        }

        public string CustomICON0FileName
        {
            get { return icon0FileNameTextBox.Text; }
        }

        public bool[] ENTD { get { return new bool[] { ENTD1, ENTD2, ENTD3, ENTD4, ENTD5 }; } }

        public bool ENTD1 { get; private set; }

        public bool ENTD2 { get; private set; }

        public bool ENTD3 { get; private set; }

        public bool ENTD4 { get; private set; }

        public bool ENTD5 { get; private set; }

        public string FileName { get { return isoPathTextBox.Text; } }

        public bool FONT { get; private set; }

        public bool FontWidths
        {
            get { return bootBinPatchable[(int)BootBinPatchable.FontWidths]; }
            set { bootBinPatchable[(int)BootBinPatchable.FontWidths] = value; }
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

        public bool InflictStatus
        {
            get { return bootBinPatchable[(int)BootBinPatchable.InflictStatus]; }
            set { bootBinPatchable[(int)BootBinPatchable.InflictStatus] = value; }
        }

        public bool ItemAttributes
        {
            get { return bootBinPatchable[(int)BootBinPatchable.ItemAttributes]; }
            set { bootBinPatchable[(int)BootBinPatchable.ItemAttributes] = value; }
        }

        public bool Items
        {
            get { return bootBinPatchable[(int)BootBinPatchable.Items]; }
            set { bootBinPatchable[(int)BootBinPatchable.Items] = value; }
        }

        public bool JobLevels
        {
            get { return bootBinPatchable[(int)BootBinPatchable.JobLevels]; }
            set { bootBinPatchable[(int)BootBinPatchable.JobLevels] = value; }
        }

        public bool Jobs
        {
            get { return bootBinPatchable[(int)BootBinPatchable.Jobs]; }
            set { bootBinPatchable[(int)BootBinPatchable.Jobs] = value; }
        }

        public bool MonsterSkills
        {
            get { return bootBinPatchable[(int)BootBinPatchable.MonsterSkills]; }
            set { bootBinPatchable[(int)BootBinPatchable.MonsterSkills] = value; }
        }

        public bool MoveFindItems
        {
            get { return bootBinPatchable[(int)BootBinPatchable.MoveFindItems]; }
            set { bootBinPatchable[(int)BootBinPatchable.MoveFindItems] = value; }
        }

        public IList<PatchedByteArray> OtherPatches
        {
            get
            {
                if ( ICON0 == CustomICON0.NoChange )
                {
                    return new PatchedByteArray[0];
                }
                else
                {
                    int length = ICON0_PNG.Length;
                    byte[] lengthBytes = { 
                                             (byte)(( length & 0xFF000000 ) >> 24), 
                                             (byte)(( length & 0xFF0000 ) >> 16), 
                                             (byte)(( length & 0xFF00 ) >> 8), 
                                             (byte)(length & 0xFF) };
                    byte[] lengthBytesBackwards = { lengthBytes[3], lengthBytes[2], lengthBytes[1], lengthBytes[0] };
                    return new PatchedByteArray[] { 
                        new PatchedByteArray( PspIso.Files.PSP_GAME.ICON0_PNG, 0, ICON0_PNG ),
                        new PatchedByteArray( 23, 0x6A, lengthBytesBackwards ),
                        new PatchedByteArray( 23, 0x6E, lengthBytes )
                    };
                }
            }
        }

        public int PatchCount
        {
            get
            {
                int result = 0;
                ENTD.ForEach( b => result += b ? 1 : 0 );
                new bool[] { FONT, Abilities, AbilityEffects, FontWidths, MoveFindItems,
                    Jobs, JobLevels, Skillsets, MonsterSkills, ActionMenus,
                    StatusAttributes, InflictStatus, Poach }.ForEach( b => result += b ? 2 : 0 );
                if( RegenECC ) result++;
                if( ICON0 != CustomICON0.NoChange ) result += 3;
                if( ItemAttributes ) result += 4;
                if( Items ) result += 4;

                return result;
            }
        }

        public bool Poach
        {
            get { return bootBinPatchable[(int)BootBinPatchable.PoachProbabilities]; }
            set { bootBinPatchable[(int)BootBinPatchable.PoachProbabilities] = value; }
        }

        public bool RegenECC { get; private set; }

        public bool Skillsets
        {
            get { return bootBinPatchable[(int)BootBinPatchable.Skillsets]; }
            set { bootBinPatchable[(int)BootBinPatchable.Skillsets] = value; }
        }

        public bool StatusAttributes
        {
            get { return bootBinPatchable[(int)BootBinPatchable.StatusAttributes]; }
            set { bootBinPatchable[(int)BootBinPatchable.StatusAttributes] = value; }
        }

		#endregion Public Properties 

		#region Constructors (1) 

        public PatchPSPForm()
        {
            InitializeComponent();
        }

		#endregion Constructors 

		#region Public Methods (1) 

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

		#endregion Public Methods 

		#region Private Methods (10) 

        private void BuildICON0Preview(Image i)
        {
            pictureBox1.Image = i;
        }

        private static Color BytesToColor( byte first, byte second )
        {
            int b = (second & 0x7C) << 1;
            int g = (second & 0x03) << 6 | (first & 0xE0) >> 2;
            int r = (first & 0x1F) << 3;

            return Color.FromArgb( r, g, b );
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
                ICON0_PNG = PSPResources.ICON0;
                BuildICON0Preview( PSPResources.ICON0_PNG );
            }

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

        private bool ValidateISO( string filename )
        {
            return
                filename != string.Empty &&
                File.Exists( filename );
        }

		#endregion Private Methods 

        public enum CustomICON0
        {
            NoChange,
            Default,
            Custom
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
    }
}
