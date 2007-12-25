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

            textBox1.Text = sb.ToString();
            base.OnVisibleChanged( e );
        }
    }
}
