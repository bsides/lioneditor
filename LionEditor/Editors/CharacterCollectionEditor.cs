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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace LionEditor.Editors
{
    /// <summary>
    /// A UserControl for editing collections of characters, with drag/drop, rearranging, etc.
    /// </summary>
    public partial class CharacterCollectionEditor : UserControl
    {
        #region Fields

        private List<Character> characterCollection;
        private Character characterInVirtualClipBoard = null;
        private int lastSelectedIndex;
        private bool contextMenuEnabled = true;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the collection of characters being edited by this instance
        /// </summary>
        public List<Character> CharacterCollection
        {
            get { return characterCollection; }
            set
            {
                characterCollection = value;
                characterSelector.Items.Clear();
                foreach (Character c in characterCollection)
                {
                    characterSelector.Items.Add(c, c.IsPresent);
                }

                characterSelector.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Gets or sets whether the user can right click on a character's name in the CheckedListBox
        /// </summary>
        public bool ContextMenuEnabled
        {
            get { return contextMenuEnabled; }
            set 
            { 
                contextMenuEnabled = value;
                characterSelector.ContextMenu = contextMenuEnabled ? characterRightClickMenu : null;
            }
        }

        #endregion

        public CharacterCollectionEditor()
        {
            InitializeComponent();

            characterSelector.SelectedIndexChanged += characterSelector_SelectedIndexChanged;
            characterSelector.CheckOnClick = false;
            characterSelector.ItemCheck += characterSelector_ItemCheck;
            
            characterSelector.ContextMenu = characterRightClickMenu;

            characterSelector.MouseDown += characterSelector_MouseDown;
            characterSelector.DragEnter += characterSelector_DragEnter;
            characterSelector.DragDrop += characterSelector_DragDrop;

            characterCopyMenuItem.Click += characterCopyMenuItem_Click;
            characterPasteMenuItem.Click += characterPasteMenuItem_Click;
            characterMoveUpMenuItem.Click += characterMoveUpMenuItem_Click;
            characterMoveDownMenuItem.Click += characterMoveDownMenuItem_Click;
            characterRightClickMenu.Popup += characterRightClickMenu_Popup;

            characterEditor.DataChangedEvent += characterEditor_DataChangedEvent;
        }

        #region Events

        #region Drag/Drop support

        private void characterSelector_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Effect == DragDropEffects.Move)
            {
                if (e.Data.GetDataPresent(typeof(Character)))
                {
                    CheckedListBox listBox = sender as CheckedListBox;
                    Character c = e.Data.GetData(typeof(Character)) as Character;
                    int index = listBox.IndexFromPoint(listBox.PointToClient(new Point(e.X, e.Y)));
                    if (index > -1)
                    {
                        listBox.Items.Insert(index, c);
                        int oldSelectedIndex = listBox.SelectedIndex;
                        //listBox.SelectedIndex = index;
                        listBox.Items.RemoveAt(oldSelectedIndex);
                        listBox.SelectedItem = c;
                        characterEditor.Character = c;
                    }
                }
            }
        }

        private void characterSelector_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Character)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void characterSelector_MouseDown(object sender, MouseEventArgs e)
        {
            // Determine if the click was on the checkbox
            Size s;
            int index = characterSelector.IndexFromPoint(e.X, e.Y);
            using (Graphics g = characterSelector.CreateGraphics())
            {
                s = CheckBoxRenderer.GetGlyphSize(g, characterSelector.CheckedIndices.Contains(index) ? CheckBoxState.CheckedNormal : CheckBoxState.UncheckedNormal);
            }
            Rectangle bounds = characterSelector.GetItemRectangle(index);
            int num2 = characterSelector.Font.Height + 2;
            int num3 = Math.Max((num2 - s.Width) / 2, 0);
            if ((num3 + s.Width) > bounds.Height)
            {
                num3 = bounds.Height - s.Width;
            }

            Rectangle rectangle = new Rectangle(bounds.X + 1, bounds.Y + num3, s.Width, s.Width);

            CheckedListBox listBox = sender as CheckedListBox;

            // If not, do whatever
            if ((e.Button == MouseButtons.Left) && (listBox.SelectedIndex == lastSelectedIndex) && (!rectangle.Contains(e.Location)))
            {
                if (listBox.Items.Count == 0)
                {
                    return;
                }

                Character c = listBox.Items[index] as Character;
                DragDropEffects dde = DoDragDrop(c, DragDropEffects.Move);
                lastSelectedIndex = listBox.SelectedIndex;

                // Reassign everyone's index and checkstates
                for (int i = 0; i < listBox.Items.Count; i++)
                {
                    c = listBox.Items[i] as Character;
                    listBox.SetItemChecked(i, c.IsPresent);
                    c.Index = (byte)i;
                }

                // TODO: "dropped" character's index displays wrong in groupbox
                characterEditor.Character = characterSelector.SelectedItem as Character;
            }
            else if (e.Button == MouseButtons.Right)
            {
                characterSelector.SelectedIndex = index;
            }
        }

        #endregion

        #region Context menu

        private void characterRightClickMenu_Popup(object sender, EventArgs e)
        {
            characterMoveDownMenuItem.Enabled = (characterSelector.SelectedIndex != (characterSelector.Items.Count - 1));
            characterMoveUpMenuItem.Enabled = (characterSelector.SelectedIndex != 0);
            characterPasteMenuItem.Enabled = (characterInVirtualClipBoard != null);
        }

        private void characterMoveDownMenuItem_Click(object sender, EventArgs e)
        {
            this.SuspendLayout();
            Character moved = characterSelector.SelectedItem as Character;
            Character notMoved = characterSelector.Items[characterSelector.SelectedIndex + 1] as Character;
            moved.Index += 1;
            notMoved.Index -= 1;

            characterSelector.Items[characterSelector.SelectedIndex] = notMoved;
            characterSelector.Items[characterSelector.SelectedIndex + 1] = moved;
            characterSelector.SelectedItem = moved;

            FireDataChangedEvent();
            this.ResumeLayout();
        }

        private void characterMoveUpMenuItem_Click(object sender, EventArgs e)
        {
            this.SuspendLayout();
            Character moved = characterSelector.SelectedItem as Character;
            Character notMoved = characterSelector.Items[characterSelector.SelectedIndex - 1] as Character;
            moved.Index -= 1;
            notMoved.Index += 1;
            characterSelector.Items[characterSelector.SelectedIndex] = notMoved;
            characterSelector.Items[characterSelector.SelectedIndex - 1] = moved;
            characterSelector.SelectedItem = moved;
            FireDataChangedEvent();

            this.ResumeLayout();
        }

        private void characterPasteMenuItem_Click(object sender, EventArgs e)
        {
            if (characterInVirtualClipBoard != null)
            {
                this.SuspendLayout();
                Character newChar = characterInVirtualClipBoard.Clone() as Character;
                newChar.Index = (byte)characterSelector.SelectedIndex;
                characterCollection[characterSelector.SelectedIndex] = newChar;
                characterSelector.Items[characterSelector.SelectedIndex] = newChar;
                characterEditor.Character = newChar;
                FireDataChangedEvent();

                this.ResumeLayout();
            }
        }

        private void characterCopyMenuItem_Click(object sender, EventArgs e)
        {
            characterPasteMenuItem.Enabled = true;
            characterInVirtualClipBoard = characterSelector.SelectedItem as Character;
        }

        #endregion

        private void characterEditor_DataChangedEvent(object sender, EventArgs e)
        {
            FireDataChangedEvent();
        }

        private void characterSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            Character c = characterSelector.SelectedItem as Character;
            characterEditor.Character = c;
            lastSelectedIndex = characterSelector.SelectedIndex;
        }

        private void characterSelector_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue != CheckState.Indeterminate)
            {
                Character c = characterSelector.Items[e.Index] as Character;
                c.IsPresent = (e.NewValue == CheckState.Checked);
                FireDataChangedEvent();
            }
        }

        public event EventHandler DataChangedEvent;

        private void FireDataChangedEvent()
        {
            if (DataChangedEvent != null)
            {
                DataChangedEvent(this, EventArgs.Empty);
            }
        }


        #endregion
    }
}
