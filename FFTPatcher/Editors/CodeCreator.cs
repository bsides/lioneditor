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
            if( Form1.AllAbilities != null )
            {
                sb.AppendLine( "_C0 Abilities" );
                sb.AppendLine( Form1.AllAbilities.GenerateCodes() );
            }
            if( Form1.AllJobs != null )
            {
                sb.AppendLine( "_C0 Jobs" );
                sb.AppendLine( Form1.AllJobs.GenerateCodes() );
            }
            if( Form1.AllSkillSets != null )
            {
                sb.AppendLine( "_C0 Skill Sets" );
                sb.AppendLine( Form1.AllSkillSets.GenerateCodes() );
            }
            if( Form1.AllMonsterSkills != null )
            {
                sb.AppendLine( "_C0 Monster Skill Sets" );
                sb.AppendLine( Form1.AllMonsterSkills.GenerateCodes() );
            }
            if( Form1.AllActionMenus != null )
            {
                sb.AppendLine( "_C0 Action Menus" );
                sb.AppendLine( Form1.AllActionMenus.GenerateCodes() );
            }
            if( Form1.AllStatusAttributes != null )
            {
                sb.AppendLine( "_C0 Status Effects" );
                sb.AppendLine( Form1.AllStatusAttributes.GenerateCodes() );
            }
            if( Form1.AllPoachProbabilities != null )
            {
                sb.AppendLine( "_C0 Poaching" );
                sb.AppendLine( Form1.AllPoachProbabilities.GenerateCodes() );
            }
            if( Form1.JobLevels != null )
            {
                sb.AppendLine( "_C0 Job Levels" );
                sb.AppendLine( Form1.JobLevels.GenerateCodes() );
            }
            if( Form1.AllItems != null )
            {
                sb.AppendLine( "_C0 Items" );
                sb.AppendLine( Form1.AllItems.GenerateCodes() );
            }
            textBox1.Text = sb.ToString();
            base.OnVisibleChanged( e );
        }
    }
}
