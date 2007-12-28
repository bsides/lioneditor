namespace FFTPatcher.Editors
{
    partial class ItemAttributeEditor
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if( disposing && (components != null) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Label paLabel;
            System.Windows.Forms.Label maLabel;
            System.Windows.Forms.Label speedLabel;
            System.Windows.Forms.Label moveLabel;
            System.Windows.Forms.Label jumpLabel;
            this.paSpinner = new System.Windows.Forms.NumericUpDown();
            this.maSpinner = new System.Windows.Forms.NumericUpDown();
            this.moveSpinner = new System.Windows.Forms.NumericUpDown();
            this.speedSpinner = new System.Windows.Forms.NumericUpDown();
            this.jumpSpinner = new System.Windows.Forms.NumericUpDown();
            this.strongElementsEditor = new FFTPatcher.Editors.ElementsEditor();
            this.weakElementsEditor = new FFTPatcher.Editors.ElementsEditor();
            this.halfElementsEditor = new FFTPatcher.Editors.ElementsEditor();
            this.cancelElementsEditor = new FFTPatcher.Editors.ElementsEditor();
            this.absorbElementsEditor = new FFTPatcher.Editors.ElementsEditor();
            this.startingStatusesEditor = new FFTPatcher.Editors.StatusesEditor();
            this.statusImmunityEditor = new FFTPatcher.Editors.StatusesEditor();
            this.permanentStatusesEditor = new FFTPatcher.Editors.StatusesEditor();
            paLabel = new System.Windows.Forms.Label();
            maLabel = new System.Windows.Forms.Label();
            speedLabel = new System.Windows.Forms.Label();
            moveLabel = new System.Windows.Forms.Label();
            jumpLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.paSpinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maSpinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.moveSpinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speedSpinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.jumpSpinner)).BeginInit();
            this.SuspendLayout();
            // 
            // paLabel
            // 
            paLabel.AutoSize = true;
            paLabel.Location = new System.Drawing.Point( 3, 6 );
            paLabel.Name = "paLabel";
            paLabel.Size = new System.Drawing.Size( 21, 13 );
            paLabel.TabIndex = 13;
            paLabel.Text = "PA";
            // 
            // maLabel
            // 
            maLabel.AutoSize = true;
            maLabel.Location = new System.Drawing.Point( 3, 31 );
            maLabel.Name = "maLabel";
            maLabel.Size = new System.Drawing.Size( 23, 13 );
            maLabel.TabIndex = 14;
            maLabel.Text = "MA";
            // 
            // speedLabel
            // 
            speedLabel.AutoSize = true;
            speedLabel.Location = new System.Drawing.Point( 3, 57 );
            speedLabel.Name = "speedLabel";
            speedLabel.Size = new System.Drawing.Size( 38, 13 );
            speedLabel.TabIndex = 15;
            speedLabel.Text = "Speed";
            // 
            // moveLabel
            // 
            moveLabel.AutoSize = true;
            moveLabel.Location = new System.Drawing.Point( 3, 83 );
            moveLabel.Name = "moveLabel";
            moveLabel.Size = new System.Drawing.Size( 34, 13 );
            moveLabel.TabIndex = 16;
            moveLabel.Text = "Move";
            // 
            // jumpLabel
            // 
            jumpLabel.AutoSize = true;
            jumpLabel.Location = new System.Drawing.Point( 3, 110 );
            jumpLabel.Name = "jumpLabel";
            jumpLabel.Size = new System.Drawing.Size( 32, 13 );
            jumpLabel.TabIndex = 17;
            jumpLabel.Text = "Jump";
            // 
            // paSpinner
            // 
            this.paSpinner.Location = new System.Drawing.Point( 48, 3 );
            this.paSpinner.Maximum = new decimal( new int[] {
            255,
            0,
            0,
            0} );
            this.paSpinner.Name = "paSpinner";
            this.paSpinner.Size = new System.Drawing.Size( 47, 20 );
            this.paSpinner.TabIndex = 8;
            this.paSpinner.Tag = "PA";
            this.paSpinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.paSpinner.Value = new decimal( new int[] {
            255,
            0,
            0,
            0} );
            // 
            // maSpinner
            // 
            this.maSpinner.Location = new System.Drawing.Point( 48, 29 );
            this.maSpinner.Maximum = new decimal( new int[] {
            255,
            0,
            0,
            0} );
            this.maSpinner.Name = "maSpinner";
            this.maSpinner.Size = new System.Drawing.Size( 47, 20 );
            this.maSpinner.TabIndex = 9;
            this.maSpinner.Tag = "MA";
            this.maSpinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.maSpinner.Value = new decimal( new int[] {
            255,
            0,
            0,
            0} );
            // 
            // moveSpinner
            // 
            this.moveSpinner.Location = new System.Drawing.Point( 48, 81 );
            this.moveSpinner.Maximum = new decimal( new int[] {
            255,
            0,
            0,
            0} );
            this.moveSpinner.Name = "moveSpinner";
            this.moveSpinner.Size = new System.Drawing.Size( 47, 20 );
            this.moveSpinner.TabIndex = 10;
            this.moveSpinner.Tag = "Move";
            this.moveSpinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.moveSpinner.Value = new decimal( new int[] {
            255,
            0,
            0,
            0} );
            // 
            // speedSpinner
            // 
            this.speedSpinner.Location = new System.Drawing.Point( 48, 55 );
            this.speedSpinner.Maximum = new decimal( new int[] {
            255,
            0,
            0,
            0} );
            this.speedSpinner.Name = "speedSpinner";
            this.speedSpinner.Size = new System.Drawing.Size( 47, 20 );
            this.speedSpinner.TabIndex = 11;
            this.speedSpinner.Tag = "Speed";
            this.speedSpinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.speedSpinner.Value = new decimal( new int[] {
            255,
            0,
            0,
            0} );
            // 
            // jumpSpinner
            // 
            this.jumpSpinner.Location = new System.Drawing.Point( 48, 107 );
            this.jumpSpinner.Maximum = new decimal( new int[] {
            255,
            0,
            0,
            0} );
            this.jumpSpinner.Name = "jumpSpinner";
            this.jumpSpinner.Size = new System.Drawing.Size( 47, 20 );
            this.jumpSpinner.TabIndex = 12;
            this.jumpSpinner.Tag = "Jump";
            this.jumpSpinner.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.jumpSpinner.Value = new decimal( new int[] {
            255,
            0,
            0,
            0} );
            // 
            // strongElementsEditor
            // 
            this.strongElementsEditor.AutoSize = true;
            this.strongElementsEditor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.strongElementsEditor.GroupBoxText = "Strengthen";
            this.strongElementsEditor.Location = new System.Drawing.Point( 501, 0 );
            this.strongElementsEditor.Name = "strongElementsEditor";
            this.strongElementsEditor.Size = new System.Drawing.Size( 94, 162 );
            this.strongElementsEditor.TabIndex = 7;
            // 
            // weakElementsEditor
            // 
            this.weakElementsEditor.AutoSize = true;
            this.weakElementsEditor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.weakElementsEditor.GroupBoxText = "Weak";
            this.weakElementsEditor.Location = new System.Drawing.Point( 401, 0 );
            this.weakElementsEditor.Name = "weakElementsEditor";
            this.weakElementsEditor.Size = new System.Drawing.Size( 94, 162 );
            this.weakElementsEditor.TabIndex = 6;
            // 
            // halfElementsEditor
            // 
            this.halfElementsEditor.AutoSize = true;
            this.halfElementsEditor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.halfElementsEditor.GroupBoxText = "Half";
            this.halfElementsEditor.Location = new System.Drawing.Point( 301, 0 );
            this.halfElementsEditor.Name = "halfElementsEditor";
            this.halfElementsEditor.Size = new System.Drawing.Size( 94, 162 );
            this.halfElementsEditor.TabIndex = 5;
            // 
            // cancelElementsEditor
            // 
            this.cancelElementsEditor.AutoSize = true;
            this.cancelElementsEditor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cancelElementsEditor.GroupBoxText = "Cancel";
            this.cancelElementsEditor.Location = new System.Drawing.Point( 201, 0 );
            this.cancelElementsEditor.Name = "cancelElementsEditor";
            this.cancelElementsEditor.Size = new System.Drawing.Size( 94, 162 );
            this.cancelElementsEditor.TabIndex = 4;
            // 
            // absorbElementsEditor
            // 
            this.absorbElementsEditor.AutoSize = true;
            this.absorbElementsEditor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.absorbElementsEditor.GroupBoxText = "Absorb";
            this.absorbElementsEditor.Location = new System.Drawing.Point( 101, 0 );
            this.absorbElementsEditor.Name = "absorbElementsEditor";
            this.absorbElementsEditor.Size = new System.Drawing.Size( 94, 162 );
            this.absorbElementsEditor.TabIndex = 3;
            // 
            // startingStatusesEditor
            // 
            this.startingStatusesEditor.Location = new System.Drawing.Point( 0, 526 );
            this.startingStatusesEditor.Name = "startingStatusesEditor";
            this.startingStatusesEditor.Size = new System.Drawing.Size( 505, 178 );
            this.startingStatusesEditor.Status = "Starting Status";
            this.startingStatusesEditor.Statuses = null;
            this.startingStatusesEditor.TabIndex = 2;
            // 
            // statusImmunityEditor
            // 
            this.statusImmunityEditor.Location = new System.Drawing.Point( 0, 347 );
            this.statusImmunityEditor.Name = "statusImmunityEditor";
            this.statusImmunityEditor.Size = new System.Drawing.Size( 505, 178 );
            this.statusImmunityEditor.Status = "Status Immunity";
            this.statusImmunityEditor.Statuses = null;
            this.statusImmunityEditor.TabIndex = 1;
            // 
            // permanentStatusesEditor
            // 
            this.permanentStatusesEditor.Location = new System.Drawing.Point( 0, 168 );
            this.permanentStatusesEditor.Name = "permanentStatusesEditor";
            this.permanentStatusesEditor.Size = new System.Drawing.Size( 505, 178 );
            this.permanentStatusesEditor.Status = "Permanent Status";
            this.permanentStatusesEditor.Statuses = null;
            this.permanentStatusesEditor.TabIndex = 0;
            // 
            // ItemAttributeEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add( jumpLabel );
            this.Controls.Add( moveLabel );
            this.Controls.Add( speedLabel );
            this.Controls.Add( maLabel );
            this.Controls.Add( paLabel );
            this.Controls.Add( this.jumpSpinner );
            this.Controls.Add( this.speedSpinner );
            this.Controls.Add( this.moveSpinner );
            this.Controls.Add( this.maSpinner );
            this.Controls.Add( this.paSpinner );
            this.Controls.Add( this.strongElementsEditor );
            this.Controls.Add( this.weakElementsEditor );
            this.Controls.Add( this.halfElementsEditor );
            this.Controls.Add( this.cancelElementsEditor );
            this.Controls.Add( this.absorbElementsEditor );
            this.Controls.Add( this.startingStatusesEditor );
            this.Controls.Add( this.statusImmunityEditor );
            this.Controls.Add( this.permanentStatusesEditor );
            this.Name = "ItemAttributeEditor";
            this.Size = new System.Drawing.Size( 598, 707 );
            ((System.ComponentModel.ISupportInitialize)(this.paSpinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maSpinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.moveSpinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speedSpinner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.jumpSpinner)).EndInit();
            this.ResumeLayout( false );
            this.PerformLayout();

        }

        #endregion

        private StatusesEditor permanentStatusesEditor;
        private StatusesEditor statusImmunityEditor;
        private StatusesEditor startingStatusesEditor;
        private ElementsEditor absorbElementsEditor;
        private ElementsEditor cancelElementsEditor;
        private ElementsEditor halfElementsEditor;
        private ElementsEditor weakElementsEditor;
        private ElementsEditor strongElementsEditor;
        private System.Windows.Forms.NumericUpDown paSpinner;
        private System.Windows.Forms.NumericUpDown maSpinner;
        private System.Windows.Forms.NumericUpDown moveSpinner;
        private System.Windows.Forms.NumericUpDown speedSpinner;
        private System.Windows.Forms.NumericUpDown jumpSpinner;
    }
}
