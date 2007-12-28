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

using System.Windows.Forms;
using FFTPatcher.Datatypes;

namespace FFTPatcher
{
    public partial class MainForm : Form
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
        public static AllInflictStatuses AllInflictStatuses { get; private set; }
        public static AllItemAttributes AllItemAttributes { get; private set; }

        public MainForm()
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
            AllInflictStatuses = allInflictStatusesEditor1.AllInflictStatuses;
            AllItemAttributes = allItemAttributesEditor1.AllItemAttributes;
        }
    }
}
