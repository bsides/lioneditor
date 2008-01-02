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

using System;
using System.Text;
using System.Windows.Forms;
using FFTPatcher.Datatypes;
using System.Collections.Generic;

namespace FFTPatcher.Editors
{
    public partial class CodeCreator : UserControl
    {
        public CodeCreator()
        {
            InitializeComponent();
        }

        public void UpdateView()
        {
            OnVisibleChanged( null );
        }

        protected override void OnVisibleChanged( EventArgs e )
        {
            StringBuilder sb = new StringBuilder();
            if( FFTPatch.Abilities != null )
            {
                sb.AddGroups( 25, FFTPatch.Context == Context.US_PSP ? "_C0 Abilities" : "", FFTPatch.Abilities.GenerateCodes() );
            }
            if( FFTPatch.Jobs != null )
            {
                sb.AddGroups( 25, FFTPatch.Context == Context.US_PSP ? "_C0 Jobs" : "", FFTPatch.Jobs.GenerateCodes() );
            }
            if( FFTPatch.SkillSets != null )
            {
                sb.AddGroups( 25, FFTPatch.Context == Context.US_PSP ? "_C0 Skill Sets" : "", FFTPatch.SkillSets.GenerateCodes() );
            }
            if( FFTPatch.MonsterSkills != null )
            {
                sb.AddGroups( 25, FFTPatch.Context == Context.US_PSP ? "_C0 Monster Skill Sets" : "", FFTPatch.MonsterSkills.GenerateCodes() );
            }
            if( FFTPatch.ActionMenus != null )
            {
                sb.AddGroups( 25, FFTPatch.Context == Context.US_PSP ? "_C0 Action Menus" : "", FFTPatch.ActionMenus.GenerateCodes() );
            }
            if( FFTPatch.StatusAttributes != null )
            {
                sb.AddGroups( 25, FFTPatch.Context == Context.US_PSP ? "_C0 Status Effects" : "", FFTPatch.StatusAttributes.GenerateCodes() );
            }
            if( FFTPatch.PoachProbabilities != null )
            {
                sb.AddGroups( 25, FFTPatch.Context == Context.US_PSP ? "_C0 Poaching" : "", FFTPatch.PoachProbabilities.GenerateCodes() );
            }
            if( FFTPatch.JobLevels != null )
            {
                sb.AddGroups( 25, FFTPatch.Context == Context.US_PSP ? "_C0 Job Levels" : "", FFTPatch.JobLevels.GenerateCodes() );
            }
            if( FFTPatch.Items != null )
            {
                sb.AddGroups( 25, FFTPatch.Context == Context.US_PSP ? "_C0 Items" : "", FFTPatch.Items.GenerateCodes() );
            }
            if( FFTPatch.ItemAttributes != null )
            {
                sb.AddGroups( 25, FFTPatch.Context == Context.US_PSP ? "_C0 Item Attributes" : "", FFTPatch.ItemAttributes.GenerateCodes() );
            }
            if( FFTPatch.InflictStatuses != null )
            {
                sb.AddGroups( 25, FFTPatch.Context == Context.US_PSP ? "_C0 Inflict Statuses" : "", FFTPatch.InflictStatuses.GenerateCodes() );
            }
            textBox1.Text = sb.ToString();
            base.OnVisibleChanged( e );
        }
    }
}
