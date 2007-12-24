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
            // equipmentCheckedListBox
            // 
            this.equipmentCheckedListBox.FormattingEnabled = true;
            this.equipmentCheckedListBox.Items.AddRange(new object[] {
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
            "Unknown8"});
            this.equipmentCheckedListBox.Location = new System.Drawing.Point(6, 19);
            this.equipmentCheckedListBox.MultiColumn = true;
            this.equipmentCheckedListBox.Name = "equipmentCheckedListBox";
            this.equipmentCheckedListBox.Size = new System.Drawing.Size(498, 154);
            this.equipmentCheckedListBox.TabIndex = 50;
            // 
            // equipmentGroupBox
            // 
            equipmentGroupBox.AutoSize = true;
            equipmentGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            equipmentGroupBox.Controls.Add(this.equipmentCheckedListBox);
            equipmentGroupBox.Location = new System.Drawing.Point(3, 3);
            equipmentGroupBox.Name = "equipmentGroupBox";
            equipmentGroupBox.Size = new System.Drawing.Size(510, 192);
            equipmentGroupBox.TabIndex = 51;
            equipmentGroupBox.TabStop = false;
            equipmentGroupBox.Text = "Equipment";
            // 
            // EquipmentEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(equipmentGroupBox);
            this.Name = "EquipmentEditor";
            this.Size = new System.Drawing.Size(516, 198);
            equipmentGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox equipmentCheckedListBox;
    }
}
