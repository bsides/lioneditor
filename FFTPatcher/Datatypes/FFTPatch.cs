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
using System.Xml;
using FFTPatcher.Properties;

namespace FFTPatcher.Datatypes
{
    public static class FFTPatch
    {
        public static Context Context { get; private set; }
        public static AllAbilities Abilities { get; private set; }
        public static AllItems Items { get; private set; }
        public static AllItemAttributes ItemAttributes { get; private set; }
        public static AllJobs Jobs { get; private set; }
        public static JobLevels JobLevels { get; private set; }
        public static AllSkillSets SkillSets { get; private set; }
        public static AllMonsterSkills MonsterSkills { get; private set; }
        public static AllActionMenus ActionMenus { get; private set; }
        public static AllStatusAttributes StatusAttributes { get; private set; }
        public static AllInflictStatuses InflictStatuses { get; private set; }
        public static AllPoachProbabilities PoachProbabilities { get; private set; }

        public static event EventHandler DataChanged;

        private static void BuildFromContext()
        {
            switch( Context )
            {
                case Context.US_PSP:
                    Abilities = new AllAbilities( new SubArray<byte>( Resources.AbilitiesBin ) );
                    Items = new AllItems(
                        new SubArray<byte>( Resources.OldItemsBin ),
                        new SubArray<byte>( Resources.NewItemsBin ) );
                    ItemAttributes = new AllItemAttributes(
                        new SubArray<byte>( Resources.OldItemAttributesBin ),
                        new SubArray<byte>( Resources.NewItemAttributesBin ) );
                    Jobs = new AllJobs( Context, new SubArray<byte>( Resources.JobsBin ) );
                    JobLevels = new JobLevels( Context, new SubArray<byte>( Resources.JobLevelsBin ) );
                    SkillSets = new AllSkillSets( Context, new SubArray<byte>( Resources.SkillSetsBin ) );
                    MonsterSkills = new AllMonsterSkills( new SubArray<byte>( Resources.MonsterSkillsBin ) );
                    ActionMenus = new AllActionMenus( new SubArray<byte>( Resources.ActionEventsBin ) );
                    StatusAttributes = new AllStatusAttributes( new SubArray<byte>( Resources.StatusAttributesBin ) );
                    InflictStatuses = new AllInflictStatuses( new SubArray<byte>( Resources.InflictStatusesBin ) );
                    PoachProbabilities = new AllPoachProbabilities( new SubArray<byte>( Resources.PoachProbabilitiesBin ) );
                    break;
                case Context.US_PSX:
                    Abilities = new AllAbilities( new SubArray<byte>( PSXResources.AbilitiesBin ) );
                    Items = new AllItems( new SubArray<byte>( PSXResources.OldItemsBin ), null );
                    ItemAttributes = new AllItemAttributes( new SubArray<byte>( PSXResources.OldItemAttributesBin ), null );
                    Jobs = new AllJobs( Context, new SubArray<byte>( PSXResources.JobsBin ) );
                    JobLevels = new JobLevels( Context, new SubArray<byte>( PSXResources.JobLevelsBin ) );
                    SkillSets = new AllSkillSets( Context, new SubArray<byte>( PSXResources.SkillSetsBin ) );
                    MonsterSkills = new AllMonsterSkills( new SubArray<byte>( PSXResources.MonsterSkillsBin ) );
                    ActionMenus = new AllActionMenus( new SubArray<byte>( PSXResources.ActionEventsBin ) );
                    StatusAttributes = new AllStatusAttributes( new SubArray<byte>( PSXResources.StatusAttributesBin ) );
                    InflictStatuses = new AllInflictStatuses( new SubArray<byte>( PSXResources.InflictStatusesBin ) );
                    PoachProbabilities = new AllPoachProbabilities( new SubArray<byte>( PSXResources.PoachProbabilitiesBin ) );
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        public static void New( Context context )
        {
            Context = context;
            BuildFromContext();
            FireDataChangedEvent();
        }

        private static void FireDataChangedEvent()
        {
            if( DataChanged != null )
            {
                DataChanged( null, EventArgs.Empty );
            }
        }

        public static void Load( XmlDocument doc )
        {
        }
    }
}
