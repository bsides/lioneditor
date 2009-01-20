namespace ComponentTest
{
    partial class Form2
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
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dualList1 = new ReflectionIT.Common.Windows.Forms.DualList( this.components );
            this.SuspendLayout();
            // 
            // dualList1
            // 
            this.dualList1.Button = null;
            this.dualList1.ListBoxFrom = null;
            this.dualList1.ListBoxTo = null;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 284, 264 );
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout( false );

        }

        #endregion

        private ReflectionIT.Common.Windows.Forms.DualList dualList1;
    }
}