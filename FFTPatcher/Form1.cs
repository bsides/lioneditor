using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FFTPatcher.Datatypes;
using FFTPatcher.Properties;

namespace FFTPatcher
{
    public partial class Form1 : Form
    {
        public static AllAbilities AllAbilities { get; private set; }
        public static AllJobs AllJobs { get; private set; }
        public static AllSkillSets AllSkillSets { get; private set; }
        public static AllMonsterSkills AllMonsterSkills { get; private set; }
        public static AllActionMenus AllActionMenus { get; private set; }
        public static AllStatusAttributes AllStatusAttributes { get; private set; }
        public static AllPoachProbabilities AllPoachProbabilities { get; private set; }
        public static JobLevels JobLevels { get; private set; }
        public static AllItems AllItems { get; private set; }
        public Form1()
        {
            InitializeComponent();
            AllAbilities = allAbilitiesEditor1.AllAbilities;
            AllJobs = allJobsEditor1.AllJobs;
            AllSkillSets = allSkillSetsEditor1.AllSkillSets;
            AllMonsterSkills = allMonsterSkillsEditor1.MonsterSkills;
            AllActionMenus = allActionMenusEditor1.AllActionMenus;
            AllStatusAttributes = allStatusAttributesEditor1.AllStatusAttributes;
            AllPoachProbabilities = allPoachProbabilitiesEditor1.PoachProbabilities;
            JobLevels = jobLevelsEditor1.JobLevels;
            AllItems = allItemsEditor1.AllItems;
        }
    }
}
