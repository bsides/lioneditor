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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LionEditor
{
    public partial class JobsAndAbilitiesEditor : Form
    {
        JobsAndAbilities ja;
        byte spriteSet;

        public JobsAndAbilitiesEditor( JobsAndAbilities ja, byte currentSpriteSet )
            : this()
        {
            this.ja = ja;
            this.spriteSet = currentSpriteSet;

            if( (currentSpriteSet != 0x80) &&
                (currentSpriteSet != 0x81) &&
                (currentSpriteSet != 0x82) )
            {
                this.jobSelector.Items.Add( JobInfo.Jobs[currentSpriteSet-1] );
            }
            else
            {
                this.jobSelector.Items.Add( JobInfo.GenericJobs[0] );
            }
            for( int i = 1; i < JobInfo.GenericJobs.Count; i++ )
            {
                this.jobSelector.Items.Add( JobInfo.GenericJobs[i] );
            }

            jobSelector.SelectedIndexChanged += jobSelector_SelectedIndexChanged;
            jobSelector.SelectedIndex = 0;

            jobEditor.DataChangedEvent += new EventHandler( jobEditor1_DataChangedEvent );
        }

        bool changed = false;

        public bool ChangesMade
        {
            get { return changed; }
        }
        void jobEditor1_DataChangedEvent( object sender, EventArgs e )
        {
            changed = true;
        }

        void jobSelector_SelectedIndexChanged( object sender, EventArgs e )
        {
            jobEditor.UpdateView( ja.jobs[jobSelector.SelectedIndex], jobSelector.SelectedItem as JobInfo );
        }

        public JobsAndAbilitiesEditor()
        {
            InitializeComponent();
        }
    }
}