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
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using SourceGrid.Cells;

namespace FFTPatcher.TextEditor
{
    public partial class StringListEditor : UserControl
    {


        #region Fields (2)

        private IList<string> names;
        private IList<string> values;

        #endregion Fields


        #region Constructors (1)

        public StringListEditor()
        {
            InitializeComponent();
            grid1.BorderStyle = BorderStyle.FixedSingle;
            grid1.ColumnsCount = 3;
            grid1.FixedRows = 1;
            grid1.SizeChanged += grid1_SizeChanged;
            grid1.Controller.AddController( textBoxController );
            textBoxController.EditStarting += new EventHandler<TextBoxController.CellEditEventArgs>( textBoxController_EditStarting );
            textBoxController.EditEnded += new EventHandler<TextBoxController.CellEditEventArgs>( textBoxController_EditEnded );
        }

        void textBoxController_EditEnded( object sender, StringListEditor.TextBoxController.CellEditEventArgs e )
        {
            SourceGrid.Cells.Editors.TextBox editor = sender as SourceGrid.Cells.Editors.TextBox;
            editor.Control.TextChanged -= Control_TextChanged;
        }

        void textBoxController_EditStarting( object sender, StringListEditor.TextBoxController.CellEditEventArgs e )
        {
            SourceGrid.Cells.Editors.TextBox editor = sender as SourceGrid.Cells.Editors.TextBox;
            editor.Control.TextChanged += new EventHandler( Control_TextChanged );
        }

        void Control_TextChanged( object sender, EventArgs e )
        {
            DevAge.Windows.Forms.DevAgeTextBox tb = sender as DevAge.Windows.Forms.DevAgeTextBox;

            ValidateCell( null, new CellValidatingEventArgs( grid1.Selection.ActivePosition.Column, grid1.Selection.ActivePosition.Row,
                tb.Text ) );
        }

        TextBoxController textBoxController = new TextBoxController();

        void grid1_SizeChanged( object sender, EventArgs e )
        {
            grid1.AutoSizeCells();
        }

        static StringListEditor()
        {
            cellView.WordWrap = true;
        }

        private static SourceGrid.Cells.Views.Cell cellView = new SourceGrid.Cells.Views.Cell();

        #endregion Constructors


        #region Events (1)

        public event EventHandler<CellValidatingEventArgs> CellTextChanged;

        #endregion Events

        public class CellValidatingEventArgs : CancelEventArgs
        {
            public int ColumnIndex { get; private set; }
            public int RowIndex { get; private set; }
            public object FormattedValue { get; private set; }

            public CellValidatingEventArgs( int columnIndex, int rowIndex, object formattedValue )
            {
                ColumnIndex = columnIndex;
                RowIndex = rowIndex;
                FormattedValue = formattedValue;
            }
        }

        #region Methods (7)

        private void ValidateCell( object sender, CellValidatingEventArgs args )
        {
            if( CellTextChanged != null )
            {
                CellTextChanged( sender, args );
            }

            //if( args.Cancel )
            //{
            //    DataGridViewCell cell = dataGridView[args.ColumnIndex, args.RowIndex];
            //    cell.ErrorText = "Error";
            //    if( cell.Tag == null )
            //    {
            //        cell.Tag = cell.Style.Padding;
            //        cell.Style.Padding = new Padding( 0, 0, 18, 0 );
            //        cellInError = new Point( args.ColumnIndex, args.ColumnIndex );
            //    }
            //    if( errorToolTip == null )
            //    {
            //        errorToolTip = new ToolTip();
            //        errorToolTip.InitialDelay = 0;
            //        errorToolTip.ReshowDelay = 0;
            //        errorToolTip.Active = false;
            //    }
            //}
        }

        public void BindTo( IList<string> names, IList<string> values )
        {
            //if( names.Count != values.Count )
            //{
            //    throw new ArgumentException( "names and values must have same count" );
            //}

            //dataGridView.SuspendLayout();
            //dataGridView.Rows.Clear();
            //for( int i = 0; i < names.Count; i++ )
            //{
            //    dataGridView.Rows.Add( names[i], values[i] );
            //}

            //dataGridView.ResumeLayout();

            BindToEx( names, values );
        }

        private class TextCell : Cell
        {
            private static SourceGrid.Cells.Editors.TextBox editor = new SourceGrid.Cells.Editors.TextBox( typeof( string ) );

            public static event EventHandler TextChanged;

            static TextCell()
            {
                editor.EditableMode = SourceGrid.EditableMode.Focus;
                editor.Control.Multiline = true;
                editor.Control.WordWrap = true;
                editor.Control.TextChanged += Control_TextChanged;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="TextCell"/> class.
            /// </summary>
            /// <param name="value">The value.</param>
            /// <param name="editable">if set to <c>true</c>, this cell will be editable. Default: [b]true[/b]</param>
            public TextCell( string value, bool editable )
                : base( value )
            {
                View = cellView;

                if( editable )
                {
                    Editor = editor;
                    editor.Control.TextChanged += Control_TextChanged;
                }
            }


            public string Text
            {
                get { return Value.ToString(); }
                set { Value = value; }
            }

            protected static void OnTextChanged( EventArgs e )
            {
                EventHandler handler = TextChanged;
                if( handler != null )
                {
                    handler( null, e );
                }
            }

            private static void Control_TextChanged( object sender, EventArgs e )
            {
                OnTextChanged( e );
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="TextCell"/> class.
            /// </summary>
            /// <param name="value">The value.</param>
            public TextCell( string value )
                : this( value, true )
            {
            }

            public static TextCell CreateCell( SourceGrid.Grid grid, int row, int column, string value )
            {
                return CreateCell( grid, row, column, value, true );
            }

            public static TextCell CreateCell( SourceGrid.Grid grid, int row, int column, string value, bool editable )
            {
                TextCell result = new TextCell( value, editable );
                grid[row, column] = result;
                return result;
            }
        }

        public void BindToEx( IList<string> names, IList<string> values )
        {
            grid1.Rows.Clear();
            grid1.Rows.Insert( 0 );
            grid1[0, 0] = new SourceGrid.Cells.ColumnHeader( "Entry" );
            grid1[0, 1] = new SourceGrid.Cells.ColumnHeader( "" );
            grid1[0, 2] = new SourceGrid.Cells.ColumnHeader( "Text" );
            for( int i = 0; i < names.Count || i < values.Count; i++ )
            {
                grid1.Rows.Insert( i + 1 );
                grid1.Rows[i + 1].AutoSizeMode = SourceGrid.AutoSizeMode.EnableStretch | SourceGrid.AutoSizeMode.EnableAutoSize;

                TextCell.CreateCell( grid1, i + 1, 0, i.ToString( "X4" ), false );
                TextCell.CreateCell( grid1, i + 1, 1, names[i], false );
                TextCell.CreateCell( grid1, i + 1, 2, values[i], true );
            }

            grid1.AutoSizeCells();
        }

        private void TextCell_TextChanged( object sender, EventArgs e )
        {
            ValidateCell( null, new CellValidatingEventArgs( grid1.Selection.ActivePosition.Column, grid1.Selection.ActivePosition.Row,
                (grid1.GetCell( grid1.Selection.ActivePosition ).Editor as SourceGrid.Cells.Editors.TextBox).Control.Text ) );
        }

        private class TextBoxController : SourceGrid.Cells.Controllers.ControllerBase
        {
            public class CellEditEventArgs : EventArgs
            {
                public SourceGrid.Position Position { get; private set; }
                internal CellEditEventArgs( SourceGrid.Position position )
                {
                    Position = position;
                }
            }

            public event EventHandler<CellEditEventArgs> EditStarting;
            public event EventHandler<CellEditEventArgs> EditEnded;

            public TextBoxController()
            {
            }

            public override void OnEditStarting( SourceGrid.CellContext sender, CancelEventArgs e )
            {
                base.OnEditStarting( sender, e );
                if( EditStarting != null )
                {
                    EditStarting( sender.Cell.Editor, new CellEditEventArgs( sender.Position ) );
                }
            }

            public override void OnEditEnded( SourceGrid.CellContext sender, EventArgs e )
            {
                base.OnEditEnded( sender, e );
                if( EditEnded != null )
                {
                    EditEnded( sender.Cell.Editor, new CellEditEventArgs( sender.Position ) );
                }
            }
        }

        #endregion Methods


    }
}
