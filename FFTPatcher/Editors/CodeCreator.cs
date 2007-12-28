using System;
using System.Text;
using System.Windows.Forms;

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
            if( MainForm.AllAbilities != null )
            {
                sb.AppendLine( "_C0 Abilities" );
                sb.AppendLine( MainForm.AllAbilities.GenerateCodes() );
            }
            if( MainForm.AllJobs != null )
            {
                sb.AppendLine( "_C0 Jobs" );
                sb.AppendLine( MainForm.AllJobs.GenerateCodes() );
            }
            if( MainForm.AllSkillSets != null )
            {
                sb.AppendLine( "_C0 Skill Sets" );
                sb.AppendLine( MainForm.AllSkillSets.GenerateCodes() );
            }
            if( MainForm.AllMonsterSkills != null )
            {
                sb.AppendLine( "_C0 Monster Skill Sets" );
                sb.AppendLine( MainForm.AllMonsterSkills.GenerateCodes() );
            }
            if( MainForm.AllActionMenus != null )
            {
                sb.AppendLine( "_C0 Action Menus" );
                sb.AppendLine( MainForm.AllActionMenus.GenerateCodes() );
            }
            if( MainForm.AllStatusAttributes != null )
            {
                sb.AppendLine( "_C0 Status Effects" );
                sb.AppendLine( MainForm.AllStatusAttributes.GenerateCodes() );
            }
            if( MainForm.AllPoachProbabilities != null )
            {
                sb.AppendLine( "_C0 Poaching" );
                sb.AppendLine( MainForm.AllPoachProbabilities.GenerateCodes() );
            }
            if( MainForm.JobLevels != null )
            {
                sb.AppendLine( "_C0 Job Levels" );
                sb.AppendLine( MainForm.JobLevels.GenerateCodes() );
            }
            if( MainForm.AllItems != null )
            {
                sb.AppendLine( "_C0 Items" );
                sb.AppendLine( MainForm.AllItems.GenerateCodes() );
            }
            if( MainForm.AllItemAttributes != null )
            {
                sb.AppendLine( "_C0 Item Attributes" );
                sb.AppendLine( MainForm.AllItemAttributes.GenerateCodes() );
            }
            if( MainForm.AllInflictStatuses != null )
            {
                sb.AppendLine( "_C0 Inflict Statuses" );
                sb.AppendLine( MainForm.AllInflictStatuses.GenerateCodes() );
            }
            textBox1.Text = sb.ToString();
            base.OnVisibleChanged( e );
        }
    }
}
