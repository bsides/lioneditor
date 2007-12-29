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

namespace FFTPatcher.Editors
{
    public partial class CodeCreator : UserControl
    {
        public CodeCreator()
        {
            InitializeComponent();
        }

        protected override void OnVisibleChanged( EventArgs e )
        {
            StringBuilder sb = new StringBuilder();
            if( FFTPatch.Abilities != null )
            {
                sb.AppendLine( "_C0 Abilities" );
                sb.AppendLine( FFTPatch.Abilities.GenerateCodes() );
            }
            if( FFTPatch.Jobs != null )
            {
                sb.AppendLine( "_C0 Jobs" );
                sb.AppendLine( FFTPatch.Jobs.GenerateCodes() );
            }
            if( FFTPatch.SkillSets != null )
            {
                sb.AppendLine( "_C0 Skill Sets" );
                sb.AppendLine( FFTPatch.SkillSets.GenerateCodes() );
            }
            if( FFTPatch.MonsterSkills != null )
            {
                sb.AppendLine( "_C0 Monster Skill Sets" );
                sb.AppendLine( FFTPatch.MonsterSkills.GenerateCodes() );
            }
            if( FFTPatch.ActionMenus != null )
            {
                sb.AppendLine( "_C0 Action Menus" );
                sb.AppendLine( FFTPatch.ActionMenus.GenerateCodes() );
            }
            if( FFTPatch.StatusAttributes != null )
            {
                sb.AppendLine( "_C0 Status Effects" );
                sb.AppendLine( FFTPatch.StatusAttributes.GenerateCodes() );
            }
            if( FFTPatch.PoachProbabilities != null )
            {
                sb.AppendLine( "_C0 Poaching" );
                sb.AppendLine( FFTPatch.PoachProbabilities.GenerateCodes() );
            }
            if( FFTPatch.JobLevels != null )
            {
                sb.AppendLine( "_C0 Job Levels" );
                sb.AppendLine( FFTPatch.JobLevels.GenerateCodes() );
            }
            if( FFTPatch.Items != null )
            {
                sb.AppendLine( "_C0 Items" );
                sb.AppendLine( FFTPatch.Items.GenerateCodes() );
            }
            if( FFTPatch.ItemAttributes != null )
            {
                sb.AppendLine( "_C0 Item Attributes" );
                sb.AppendLine( FFTPatch.ItemAttributes.GenerateCodes() );
            }
            if( FFTPatch.InflictStatuses != null )
            {
                sb.AppendLine( "_C0 Inflict Statuses" );
                sb.AppendLine( FFTPatch.InflictStatuses.GenerateCodes() );
            }
            textBox1.Text = sb.ToString();
            base.OnVisibleChanged( e );
        }
    }
}
