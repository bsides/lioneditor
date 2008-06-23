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
using System.Threading;
using FFTPatcher.TextEditor.Files;

namespace FFTPatcher.TextEditor
{
    /// <summary>
    /// An editor for <see cref="IStringSectioned"/> objects that implement <see cref="ICompressed"/>.
    /// </summary>
    public partial class CompressedStringSectionedEditor : StringSectionedEditor
    {

        #region Properties (2)


        /// <summary>
        /// Gets the length label format string.
        /// </summary>
        /// <value>The length label format string.</value>
        protected override string LengthLabelFormatString
        {
            get { return "Estimated length: {0} bytes"; }
        }

        /// <summary>
        /// Gets or sets the <see cref="IStringSectioned"/> object being edited.
        /// </summary>
        public override IStringSectioned Strings
        {
            get { return base.Strings; }
            set
            {
                if( value != null )
                {
                    if( !(value is ICompressed) )
                    {
                        throw new ArgumentException( "Must implement ICompressed" );
                    }

                    if( Strings != null )
                    {
                        (Strings as ICompressed).ProgressChanged -= compressed_ProgressChanged;
                        (Strings as ICompressed).CompressionFinished -= compressed_CompressionFinished;
                    }

                    compressButton.Text = "Get Compressed Size";
                    base.Strings = value;
                }
            }
        }


        #endregion Properties

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the <see cref="CompressedStringSectionedEditor"/> class.
        /// </summary>
        public CompressedStringSectionedEditor()
        {
            InitializeComponent();
            compressButton.Click += compressButton_Click;
            errorLabel.VisibleChanged += errorLabel_VisibleChanged;
        }

        #endregion Constructors

        #region Delegates (2)

        private delegate void FinishedCallback( ICompressed compressed, IList<byte> result );
        private delegate void ProgressCallback( int progress );

        #endregion Delegates

        #region Methods (4)


        private void compressButton_Click( object sender, EventArgs e )
        {
            compressionProgressBar.Value = 0;
            compressionProgressBar.Visible = true;
            ICompressed compressed = Strings as ICompressed;
            compressed.ProgressChanged += compressed_ProgressChanged;
            compressed.CompressionFinished += compressed_CompressionFinished;

            Thread t = new Thread( new ParameterizedThreadStart(
                delegate
                {
                    compressed.Compress();
                } ) );

            this.Enabled = false;
            t.Start();
        }

        private void compressed_CompressionFinished( object sender, CompressionEventArgs e )
        {
            compressionProgressBar.Invoke(
                new FinishedCallback(
                    delegate( ICompressed compressed, IList<byte> result )
                    {
                        compressButton.Text = string.Format( "Compressed size: {0} bytes", result.Count );
                        compressionProgressBar.Visible = false;
                        compressed.ProgressChanged -= compressed_ProgressChanged;
                        compressed.CompressionFinished -= compressed_CompressionFinished;
                        this.Enabled = true;
                    } ), sender as ICompressed, e.Result );
        }

        private void compressed_ProgressChanged( object sender, CompressionEventArgs e )
        {
            compressionProgressBar.Invoke(
                new ProgressCallback(
                    delegate( int progress )
                    {
                        compressionProgressBar.Value = progress;
                    } ), e.Progress );
        }

        private void errorLabel_VisibleChanged( object sender, EventArgs e )
        {
            compressButton.Enabled = !error;
        }


        #endregion Methods

    }
}
