/*
    Copyright 2007, Joe Davidson <joedavidson@gmail.com>

    This file is part of FFTPatcher.

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

using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using FFTPatcher.Datatypes;

namespace FFTPatcher.Editors
{
    public partial class ElementsEditor : UserControl
    {
        private Elements elements = new Elements( 0 );
        public Elements Elements
        {
            get { return elements; }
            set
            {
                if( value == null )
                {
                    elements = null;
                    this.Enabled = false;
                }
                else if( elements != value )
                {
                    elements = value;
                    this.Enabled = true;
                    UpdateView();
                }
            }
        }

        public string GroupBoxText
        {
            get { return elementsGroupBox.Text; }
            set { elementsGroupBox.Text = value; }
        }

        public ElementsEditor()
        {
            InitializeComponent();
            elementsCheckedListBox.ItemCheck += elementsCheckedListBox_ItemCheck;
        }

        private void elementsCheckedListBox_ItemCheck( object sender, ItemCheckEventArgs e )
        {
            if( !ignoreChanges )
            {
                string s = elementsCheckedListBox.Items[e.Index].ToString();
                PropertyInfo pi = elements.GetType().GetProperty( s );
                pi.SetValue( elements, e.NewValue == CheckState.Checked, null );
            }
        }

        private bool ignoreChanges = false;

        private void UpdateView()
        {
            this.SuspendLayout();
            elementsCheckedListBox.SuspendLayout();

            ignoreChanges = true;
            for( int i = 0; i < elementsCheckedListBox.Items.Count; i++ )
            {
                string s = elementsCheckedListBox.Items[i].ToString();
                PropertyInfo pi = elements.GetType().GetProperty( s );
                elementsCheckedListBox.SetItemChecked( i, (bool)pi.GetValue( elements, null ) );
            }
            ignoreChanges = false;

            elementsCheckedListBox.ResumeLayout();
            this.ResumeLayout();
        }

        #region CheckedListBox

        private class ElementsCheckedListBox : CheckedListBox
        {
            private enum Elements
            {
                Fire,
                Lightning,
                Ice,
                Wind,
                Earth,
                Water,
                Holy,
                Dark
            }

            public ElementsCheckedListBox()
                : base()
            {
            }

            protected override void OnDrawItem( DrawItemEventArgs e )
            {
                Brush backColorBrush = Brushes.White;
                Brush foreColorBrush = Brushes.Black;

                switch( (Elements)e.Index )
                {
                    case Elements.Fire:
                        backColorBrush = Brushes.Red;
                        foreColorBrush = Brushes.White;
                        break;
                    case Elements.Lightning:
                        backColorBrush = Brushes.Purple; // TODO: find a better color
                        foreColorBrush = Brushes.White;
                        break;
                    case Elements.Ice:
                        backColorBrush = Brushes.LightCyan;
                        foreColorBrush = Brushes.Black;
                        break;
                    case Elements.Wind:
                        backColorBrush = Brushes.Yellow;
                        foreColorBrush = Brushes.Black;
                        break;
                    case Elements.Earth:
                        backColorBrush = Brushes.Green;
                        foreColorBrush = Brushes.White;
                        break;
                    case Elements.Water:
                        backColorBrush = Brushes.LightBlue;
                        foreColorBrush = Brushes.Black;
                        break;
                    case Elements.Holy:
                        backColorBrush = Brushes.White;
                        foreColorBrush = Brushes.Black;
                        break;
                    case Elements.Dark:
                        backColorBrush = Brushes.Black;
                        foreColorBrush = Brushes.White;
                        break;
                    default:
                        // empty
                        break;
                }

                e.Graphics.FillRectangle( backColorBrush, e.Bounds );
                CheckBoxState state = this.GetItemChecked( e.Index ) ? CheckBoxState.CheckedNormal : CheckBoxState.UncheckedNormal;
                Size checkBoxSize = CheckBoxRenderer.GetGlyphSize( e.Graphics, state );
                Point loc = new Point( 1, (e.Bounds.Height - (checkBoxSize.Height + 1)) / 2 + 1 );
                CheckBoxRenderer.DrawCheckBox( e.Graphics, new Point( loc.X + e.Bounds.X, loc.Y + e.Bounds.Y ), state );
                e.Graphics.DrawString( this.Items[e.Index].ToString(), e.Font, foreColorBrush, new PointF( loc.X + checkBoxSize.Width + 1 + e.Bounds.X, loc.Y + e.Bounds.Y ) );
            }
        }

        #endregion
    }
}
