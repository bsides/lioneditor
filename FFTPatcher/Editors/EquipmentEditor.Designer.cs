namespace FFTPatcher.Editors
{
    partial class EquipmentEditor
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.GroupBox equipmentGroupBox;
            this.equipmentCheckedListBox = new System.Windows.Forms.CheckedListBox();
            equipmentGroupBox = new System.Windows.Forms.GroupBox();
            equipmentGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // equipmentGroupBox
            // 
            equipmentGroupBox.Controls.Add( this.equipmentCheckedListBox );
            equipmentGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            equipmentGroupBox.Location = new System.Drawing.Point( 0, 0 );
            equipmentGroupBox.Name = "equipmentGroupBox";
            equipmentGroupBox.Size = new System.Drawing.Size( 494, 185 );
            equipmentGroupBox.TabIndex = 51;
            equipmentGroupBox.TabStop = false;
            equipmentGroupBox.Text = "Equipment";
            // 
            // equipmentCheckedListBox
            // 
            this.equipmentCheckedListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.equipmentCheckedListBox.FormattingEnabled = true;
            this.equipmentCheckedListBox.Items.AddRange( new object[] {
            "",
            "Knife",
            "Ninja Blade",
            "Sword",
            "Knight\'s Sword",
            "Katana",
            "Axe",
            "Rod",
            "Staff",
            "Flail",
            "Gun",
            "Crossbow",
            "Bow",
            "Instrument",
            "Book",
            "Polearm",
            "Pole",
            "Bag",
            "Cloth",
            "Shield",
            "Helmet",
            "Hat",
            "Hair Adornment",
            "Armor",
            "Clothing",
            "Robe",
            "Shoes",
            "Armguard",
            "Ring",
            "Armlet",
            "Cloak",
            "Perfume",
            "Unknown1",
            "Unknown2",
            "Unknown3",
            "Fell Sword",
            "Lip Rouge",
            "Unknown6",
            "Unknown7",
            "Unknown8"} );
            this.equipmentCheckedListBox.Location = new System.Drawing.Point( 3, 16 );
            this.equipmentCheckedListBox.MultiColumn = true;
            this.equipmentCheckedListBox.Name = "equipmentCheckedListBox";
            this.equipmentCheckedListBox.Size = new System.Drawing.Size( 488, 154 );
            this.equipmentCheckedListBox.TabIndex = 50;
            // 
            // EquipmentEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( equipmentGroupBox );
            this.Name = "EquipmentEditor";
            this.Size = new System.Drawing.Size( 494, 185 );
            equipmentGroupBox.ResumeLayout( false );
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.CheckedListBox equipmentCheckedListBox;
    }
}
