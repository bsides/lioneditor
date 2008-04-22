/*
    Copyright 2007, Joe Davidson <joedavidson@gmail.com>

    This file is part of LionEditor.

    LionEditor is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    LionEditor is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with LionEditor.  If not, see <http://www.gnu.org/licenses/>.
*/

namespace LionEditor
{
    partial class OptionsEditor
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
            System.Windows.Forms.TableLayoutPanel table;
            System.Windows.Forms.Label cursorMovementLabel;
            System.Windows.Forms.Label cursorRepeatRateLabel;
            System.Windows.Forms.Label multiheightToggleLabel;
            System.Windows.Forms.Label menuCursorSpeedLabel;
            System.Windows.Forms.Label messageSpeedLabel;
            System.Windows.Forms.Label soundLabel;
            this.cursorMovement = new System.Windows.Forms.ComboBox();
            this.cursorRepeatRate = new System.Windows.Forms.ComboBox();
            this.multiheightToggleRate = new System.Windows.Forms.ComboBox();
            this.menuCursorSpeed = new System.Windows.Forms.ComboBox();
            this.messageSpeed = new System.Windows.Forms.ComboBox();
            this.displayAbilityNames = new System.Windows.Forms.CheckBox();
            this.displayEffectMessages = new System.Windows.Forms.CheckBox();
            this.displayEarnedExpJp = new System.Windows.Forms.CheckBox();
            this.targetColors = new System.Windows.Forms.CheckBox();
            this.displayUnequippableItems = new System.Windows.Forms.CheckBox();
            this.optimizeOnJobChange = new System.Windows.Forms.CheckBox();
            this.sound = new System.Windows.Forms.ComboBox();
            this.battlePrompts = new System.Windows.Forms.CheckBox();
            table = new System.Windows.Forms.TableLayoutPanel();
            cursorMovementLabel = new System.Windows.Forms.Label();
            cursorRepeatRateLabel = new System.Windows.Forms.Label();
            multiheightToggleLabel = new System.Windows.Forms.Label();
            menuCursorSpeedLabel = new System.Windows.Forms.Label();
            messageSpeedLabel = new System.Windows.Forms.Label();
            soundLabel = new System.Windows.Forms.Label();
            table.SuspendLayout();
            this.SuspendLayout();
            // 
            // table
            // 
            table.ColumnCount = 2;
            table.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 55.23013F ) );
            table.ColumnStyles.Add( new System.Windows.Forms.ColumnStyle( System.Windows.Forms.SizeType.Percent, 44.76987F ) );
            table.Controls.Add( this.cursorMovement, 1, 0 );
            table.Controls.Add( this.cursorRepeatRate, 1, 1 );
            table.Controls.Add( this.multiheightToggleRate, 1, 2 );
            table.Controls.Add( this.menuCursorSpeed, 1, 3 );
            table.Controls.Add( this.messageSpeed, 1, 4 );
            table.Controls.Add( this.displayAbilityNames, 0, 6 );
            table.Controls.Add( this.displayEffectMessages, 0, 7 );
            table.Controls.Add( this.displayEarnedExpJp, 0, 8 );
            table.Controls.Add( this.targetColors, 0, 9 );
            table.Controls.Add( this.displayUnequippableItems, 0, 10 );
            table.Controls.Add( this.optimizeOnJobChange, 0, 11 );
            table.Controls.Add( this.sound, 1, 12 );
            table.Controls.Add( cursorMovementLabel, 0, 0 );
            table.Controls.Add( cursorRepeatRateLabel, 0, 1 );
            table.Controls.Add( multiheightToggleLabel, 0, 2 );
            table.Controls.Add( menuCursorSpeedLabel, 0, 3 );
            table.Controls.Add( messageSpeedLabel, 0, 4 );
            table.Controls.Add( soundLabel, 0, 12 );
            table.Controls.Add( this.battlePrompts, 0, 5 );
            table.Dock = System.Windows.Forms.DockStyle.Top;
            table.Location = new System.Drawing.Point( 0, 0 );
            table.Name = "table";
            table.RowCount = 13;
            table.RowStyles.Add( new System.Windows.Forms.RowStyle() );
            table.RowStyles.Add( new System.Windows.Forms.RowStyle() );
            table.RowStyles.Add( new System.Windows.Forms.RowStyle() );
            table.RowStyles.Add( new System.Windows.Forms.RowStyle() );
            table.RowStyles.Add( new System.Windows.Forms.RowStyle() );
            table.RowStyles.Add( new System.Windows.Forms.RowStyle() );
            table.RowStyles.Add( new System.Windows.Forms.RowStyle() );
            table.RowStyles.Add( new System.Windows.Forms.RowStyle() );
            table.RowStyles.Add( new System.Windows.Forms.RowStyle() );
            table.RowStyles.Add( new System.Windows.Forms.RowStyle() );
            table.RowStyles.Add( new System.Windows.Forms.RowStyle() );
            table.RowStyles.Add( new System.Windows.Forms.RowStyle() );
            table.RowStyles.Add( new System.Windows.Forms.RowStyle() );
            table.Size = new System.Drawing.Size( 460, 324 );
            table.TabIndex = 0;
            // 
            // cursorMovement
            // 
            this.cursorMovement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cursorMovement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cursorMovement.FormattingEnabled = true;
            this.cursorMovement.Location = new System.Drawing.Point( 257, 3 );
            this.cursorMovement.Name = "cursorMovement";
            this.cursorMovement.Size = new System.Drawing.Size( 200, 21 );
            this.cursorMovement.TabIndex = 0;
            // 
            // cursorRepeatRate
            // 
            this.cursorRepeatRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cursorRepeatRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cursorRepeatRate.FormattingEnabled = true;
            this.cursorRepeatRate.Location = new System.Drawing.Point( 257, 30 );
            this.cursorRepeatRate.Name = "cursorRepeatRate";
            this.cursorRepeatRate.Size = new System.Drawing.Size( 200, 21 );
            this.cursorRepeatRate.TabIndex = 1;
            // 
            // multiheightToggleRate
            // 
            this.multiheightToggleRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.multiheightToggleRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.multiheightToggleRate.FormattingEnabled = true;
            this.multiheightToggleRate.Location = new System.Drawing.Point( 257, 57 );
            this.multiheightToggleRate.Name = "multiheightToggleRate";
            this.multiheightToggleRate.Size = new System.Drawing.Size( 200, 21 );
            this.multiheightToggleRate.TabIndex = 2;
            // 
            // menuCursorSpeed
            // 
            this.menuCursorSpeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuCursorSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.menuCursorSpeed.FormattingEnabled = true;
            this.menuCursorSpeed.Location = new System.Drawing.Point( 257, 84 );
            this.menuCursorSpeed.Name = "menuCursorSpeed";
            this.menuCursorSpeed.Size = new System.Drawing.Size( 200, 21 );
            this.menuCursorSpeed.TabIndex = 3;
            // 
            // messageSpeed
            // 
            this.messageSpeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messageSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.messageSpeed.FormattingEnabled = true;
            this.messageSpeed.Location = new System.Drawing.Point( 257, 111 );
            this.messageSpeed.Name = "messageSpeed";
            this.messageSpeed.Size = new System.Drawing.Size( 200, 21 );
            this.messageSpeed.TabIndex = 4;
            // 
            // displayAbilityNames
            // 
            this.displayAbilityNames.AutoSize = true;
            table.SetColumnSpan( this.displayAbilityNames, 2 );
            this.displayAbilityNames.Dock = System.Windows.Forms.DockStyle.Fill;
            this.displayAbilityNames.Location = new System.Drawing.Point( 3, 161 );
            this.displayAbilityNames.Name = "displayAbilityNames";
            this.displayAbilityNames.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.displayAbilityNames.Size = new System.Drawing.Size( 454, 17 );
            this.displayAbilityNames.TabIndex = 6;
            this.displayAbilityNames.Text = "Display Ability Names";
            this.displayAbilityNames.UseVisualStyleBackColor = true;
            // 
            // displayEffectMessages
            // 
            this.displayEffectMessages.AutoSize = true;
            table.SetColumnSpan( this.displayEffectMessages, 2 );
            this.displayEffectMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.displayEffectMessages.Location = new System.Drawing.Point( 3, 184 );
            this.displayEffectMessages.Name = "displayEffectMessages";
            this.displayEffectMessages.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.displayEffectMessages.Size = new System.Drawing.Size( 454, 17 );
            this.displayEffectMessages.TabIndex = 7;
            this.displayEffectMessages.Text = "Display Effect Messages";
            this.displayEffectMessages.UseVisualStyleBackColor = true;
            // 
            // displayEarnedExpJp
            // 
            this.displayEarnedExpJp.AutoSize = true;
            table.SetColumnSpan( this.displayEarnedExpJp, 2 );
            this.displayEarnedExpJp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.displayEarnedExpJp.Location = new System.Drawing.Point( 3, 207 );
            this.displayEarnedExpJp.Name = "displayEarnedExpJp";
            this.displayEarnedExpJp.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.displayEarnedExpJp.Size = new System.Drawing.Size( 454, 17 );
            this.displayEarnedExpJp.TabIndex = 8;
            this.displayEarnedExpJp.Text = "Display Earned EXP/JP";
            this.displayEarnedExpJp.UseVisualStyleBackColor = true;
            // 
            // targetColors
            // 
            this.targetColors.AutoSize = true;
            table.SetColumnSpan( this.targetColors, 2 );
            this.targetColors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.targetColors.Location = new System.Drawing.Point( 3, 230 );
            this.targetColors.Name = "targetColors";
            this.targetColors.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.targetColors.Size = new System.Drawing.Size( 454, 17 );
            this.targetColors.TabIndex = 9;
            this.targetColors.Text = "Target Colors";
            this.targetColors.UseVisualStyleBackColor = true;
            // 
            // displayUnequippableItems
            // 
            this.displayUnequippableItems.AutoSize = true;
            table.SetColumnSpan( this.displayUnequippableItems, 2 );
            this.displayUnequippableItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.displayUnequippableItems.Location = new System.Drawing.Point( 3, 253 );
            this.displayUnequippableItems.Name = "displayUnequippableItems";
            this.displayUnequippableItems.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.displayUnequippableItems.Size = new System.Drawing.Size( 454, 17 );
            this.displayUnequippableItems.TabIndex = 10;
            this.displayUnequippableItems.Text = "Display Unequippable Items";
            this.displayUnequippableItems.UseVisualStyleBackColor = true;
            // 
            // optimizeOnJobChange
            // 
            this.optimizeOnJobChange.AutoSize = true;
            table.SetColumnSpan( this.optimizeOnJobChange, 2 );
            this.optimizeOnJobChange.Dock = System.Windows.Forms.DockStyle.Fill;
            this.optimizeOnJobChange.Location = new System.Drawing.Point( 3, 276 );
            this.optimizeOnJobChange.Name = "optimizeOnJobChange";
            this.optimizeOnJobChange.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.optimizeOnJobChange.Size = new System.Drawing.Size( 454, 17 );
            this.optimizeOnJobChange.TabIndex = 11;
            this.optimizeOnJobChange.Text = "Optimize on Job Change";
            this.optimizeOnJobChange.UseVisualStyleBackColor = true;
            // 
            // sound
            // 
            this.sound.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sound.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sound.FormattingEnabled = true;
            this.sound.Location = new System.Drawing.Point( 257, 299 );
            this.sound.Name = "sound";
            this.sound.Size = new System.Drawing.Size( 200, 21 );
            this.sound.TabIndex = 12;
            // 
            // cursorMovementLabel
            // 
            cursorMovementLabel.AutoSize = true;
            cursorMovementLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            cursorMovementLabel.Location = new System.Drawing.Point( 3, 0 );
            cursorMovementLabel.Name = "cursorMovementLabel";
            cursorMovementLabel.Size = new System.Drawing.Size( 248, 27 );
            cursorMovementLabel.TabIndex = 13;
            cursorMovementLabel.Text = "Cursor Movement";
            cursorMovementLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cursorRepeatRateLabel
            // 
            cursorRepeatRateLabel.AutoSize = true;
            cursorRepeatRateLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            cursorRepeatRateLabel.Location = new System.Drawing.Point( 3, 27 );
            cursorRepeatRateLabel.Name = "cursorRepeatRateLabel";
            cursorRepeatRateLabel.Size = new System.Drawing.Size( 248, 27 );
            cursorRepeatRateLabel.TabIndex = 14;
            cursorRepeatRateLabel.Text = "Cursor Repeat Rate";
            cursorRepeatRateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // multiheightToggleLabel
            // 
            multiheightToggleLabel.AutoSize = true;
            multiheightToggleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            multiheightToggleLabel.Location = new System.Drawing.Point( 3, 54 );
            multiheightToggleLabel.Name = "multiheightToggleLabel";
            multiheightToggleLabel.Size = new System.Drawing.Size( 248, 27 );
            multiheightToggleLabel.TabIndex = 15;
            multiheightToggleLabel.Text = "Multi-height Toggle Rate";
            multiheightToggleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // menuCursorSpeedLabel
            // 
            menuCursorSpeedLabel.AutoSize = true;
            menuCursorSpeedLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            menuCursorSpeedLabel.Location = new System.Drawing.Point( 3, 81 );
            menuCursorSpeedLabel.Name = "menuCursorSpeedLabel";
            menuCursorSpeedLabel.Size = new System.Drawing.Size( 248, 27 );
            menuCursorSpeedLabel.TabIndex = 16;
            menuCursorSpeedLabel.Text = "Menu Cursor Speed";
            menuCursorSpeedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // messageSpeedLabel
            // 
            messageSpeedLabel.AutoSize = true;
            messageSpeedLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            messageSpeedLabel.Location = new System.Drawing.Point( 3, 108 );
            messageSpeedLabel.Name = "messageSpeedLabel";
            messageSpeedLabel.Size = new System.Drawing.Size( 248, 27 );
            messageSpeedLabel.TabIndex = 17;
            messageSpeedLabel.Text = "Message Speed";
            messageSpeedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // soundLabel
            // 
            soundLabel.AutoSize = true;
            soundLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            soundLabel.Location = new System.Drawing.Point( 3, 296 );
            soundLabel.Name = "soundLabel";
            soundLabel.Size = new System.Drawing.Size( 248, 28 );
            soundLabel.TabIndex = 18;
            soundLabel.Text = "Sound";
            soundLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // battlePrompts
            // 
            this.battlePrompts.AutoSize = true;
            table.SetColumnSpan( this.battlePrompts, 2 );
            this.battlePrompts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.battlePrompts.Location = new System.Drawing.Point( 3, 138 );
            this.battlePrompts.Name = "battlePrompts";
            this.battlePrompts.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.battlePrompts.Size = new System.Drawing.Size( 454, 17 );
            this.battlePrompts.TabIndex = 5;
            this.battlePrompts.Text = "Battle Prompts";
            this.battlePrompts.UseVisualStyleBackColor = true;
            // 
            // OptionsEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( table );
            this.Name = "OptionsEditor";
            this.Size = new System.Drawing.Size( 460, 363 );
            table.ResumeLayout( false );
            table.PerformLayout();
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.ComboBox cursorMovement;
        private System.Windows.Forms.ComboBox cursorRepeatRate;
        private System.Windows.Forms.ComboBox multiheightToggleRate;
        private System.Windows.Forms.ComboBox menuCursorSpeed;
        private System.Windows.Forms.ComboBox messageSpeed;
        private System.Windows.Forms.CheckBox battlePrompts;
        private System.Windows.Forms.CheckBox displayAbilityNames;
        private System.Windows.Forms.CheckBox displayEffectMessages;
        private System.Windows.Forms.CheckBox displayEarnedExpJp;
        private System.Windows.Forms.CheckBox targetColors;
        private System.Windows.Forms.CheckBox displayUnequippableItems;
        private System.Windows.Forms.CheckBox optimizeOnJobChange;
        private System.Windows.Forms.ComboBox sound;
    }
}
