using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace WindowsFormsSample.GridSamples
{
	/// <summary>
	/// Summary description for frmSample23.
	/// </summary>
	[Sample("SourceGrid - Real Grid - Samples", 23, "Real Time Data Refresh")]
	public class frmSample23 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private SourceGrid3.Grid grid;
		private System.Windows.Forms.TrackBar trackBarInterval;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmSample23()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
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
			this.grid = new SourceGrid3.Grid();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.trackBarInterval = new System.Windows.Forms.TrackBar();
			((System.ComponentModel.ISupportInitialize)(this.trackBarInterval)).BeginInit();
			this.SuspendLayout();
			// 
			// grid
			// 
			this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.grid.AutoSizeMinHeight = 10;
			this.grid.AutoSizeMinWidth = 10;
			this.grid.AutoStretchColumnsToFitWidth = false;
			this.grid.AutoStretchRowsToFitHeight = false;
			this.grid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.grid.CustomSort = false;
			this.grid.GridToolTipActive = true;
			this.grid.Location = new System.Drawing.Point(4, 56);
			this.grid.Name = "grid";
			this.grid.OverrideCommonCmdKey = true;
			this.grid.Size = new System.Drawing.Size(284, 204);
			this.grid.SpecialKeys = SourceGrid3.GridSpecialKeys.Default;
			this.grid.StyleGrid = null;
			this.grid.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(4, 4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(108, 28);
			this.label1.TabIndex = 2;
			this.label1.Text = "Interval (Millisecond 500 - 5000)";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.label2.Location = new System.Drawing.Point(4, 36);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(280, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "Processes";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// trackBarInterval
			// 
			this.trackBarInterval.AutoSize = false;
			this.trackBarInterval.Location = new System.Drawing.Point(116, 4);
			this.trackBarInterval.Minimum = 1;
			this.trackBarInterval.Name = "trackBarInterval";
			this.trackBarInterval.Size = new System.Drawing.Size(168, 20);
			this.trackBarInterval.TabIndex = 4;
			this.trackBarInterval.Value = 1;
			this.trackBarInterval.Scroll += new System.EventHandler(this.trackBarInterval_Scroll);
			// 
			// frmSample23
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 266);
			this.Controls.Add(this.trackBarInterval);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.grid);
			this.Name = "frmSample23";
			this.Text = "Real Time Data Refresh";
			((System.ComponentModel.ISupportInitialize)(this.trackBarInterval)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		//I have used a Thread just to show how to syncronize a Windows Forms control with a multithread event source.
		private System.Threading.Thread mThread;
		private bool mbClosing = false;
		int mInterval = 1000;

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad (e);

			//Create Headers Informations
			grid.FixedRows = 1;
			grid.RowsCount = 1;
			grid.ColumnsCount = 4;
			grid[0, 0] = new SourceGrid3.Cells.Real.ColumnHeader("ID");
			grid[0, 1] = new SourceGrid3.Cells.Real.ColumnHeader("Name");
			grid[0, 2] = new SourceGrid3.Cells.Real.ColumnHeader("Virtual Memory Size");
			grid[0, 3] = new SourceGrid3.Cells.Real.ColumnHeader("Physical Memory Size");
			grid.AutoSize();

			//I have used a Thread just to show how to syncronize a Windows Forms control with a multithread event source.
			mThread = new System.Threading.Thread(new System.Threading.ThreadStart(DoAsyncWork));
			mThread.Start();
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing (e);

			mbClosing = true;
			
			Cursor = Cursors.WaitCursor;
			if (mThread.IsAlive)
				mThread.Join(10000);
			Cursor = Cursors.Default;
		}

		private void DoAsyncWork()
		{
			while (mbClosing == false)
			{
				//Use Invoke to syncronize the manipulation of the UI code with the main UI Thread
				Invoke(new MyDelegate(ManipulateUI));

				System.Threading.Thread.Sleep(mInterval);
			}
		}

		private delegate void MyDelegate();
		private void ManipulateUI()
		{
			grid.RowsCount = grid.FixedRows;

			System.Diagnostics.Process[] currentProcesses = System.Diagnostics.Process.GetProcesses();
			grid.RowsCount = grid.FixedRows + currentProcesses.Length;
			for (int i = 0; i < currentProcesses.Length; i++)
			{
				grid[i + grid.FixedRows, 0] = new SourceGrid3.Cells.Real.Cell( currentProcesses[i].Id );
				grid[i + grid.FixedRows, 1] = new SourceGrid3.Cells.Real.Cell( currentProcesses[i].ProcessName );
				grid[i + grid.FixedRows, 2] = new SourceGrid3.Cells.Real.Cell( FormatBytes( currentProcesses[i].VirtualMemorySize ) );
				grid[i + grid.FixedRows, 3] = new SourceGrid3.Cells.Real.Cell( FormatBytes( currentProcesses[i].WorkingSet ) );
			}
		}

		private string FormatBytes(int bytes)
		{
			return string.Format("{0}", bytes/1024);
		}

		private void trackBarInterval_Scroll(object sender, System.EventArgs e)
		{
			mInterval = trackBarInterval.Value * 500;
		}
	}
}
