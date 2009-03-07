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

using System.Collections.Generic;
using System.Xml;
using LionEditor.Properties;

namespace LionEditor
{
    /// <summary>
    /// Represents a ability a job can have
    /// </summary>
    public struct JobInfoEntry
    {
        public string Name;
        public bool Enabled;
    }

    /// <summary>
    /// Represents a Job and all of its abilities
    /// </summary>
    public class JobInfo
    {
        public JobInfoEntry[] action;
        public JobInfoEntry[] reaction;
        public JobInfoEntry[] support;
        public JobInfoEntry[] movement;

        static JobInfoEntry[] commonReaction;
        static JobInfoEntry[] commonSupport;
        static JobInfoEntry[] commonMovement;

        public string Name;

        public override string ToString()
        {
            return Name;
        }

        private static List<JobInfo> jobs;

        /// <summary>
        /// Gets a list of JobInfo for EVERY job
        /// </summary>
        public static List<JobInfo> Jobs
        {
            get
            {
                if( jobs == null )
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml( Resources.MainJobBasedOnSprite );

                    XmlNodeList common = doc.SelectNodes( "//Common/reaction/skill" );
                    commonReaction = new JobInfoEntry[common.Count];
                    for (int i = 0; i < common.Count; i++)
                    {
                        commonReaction[i] = new JobInfoEntry();
                        commonReaction[i].Name = common[i].InnerText;
                        commonReaction[i].Enabled = false;
                    }

                    common = doc.SelectNodes( "//Common/support/skill" );
                    commonSupport = new JobInfoEntry[common.Count];
                    for( int i = 0; i < common.Count; i++ )
                    {
                        commonSupport[i] = new JobInfoEntry();
                        commonSupport[i].Name = common[i].InnerText;
                        commonSupport[i].Enabled = false;
                    }

                    common = doc.SelectNodes( "//Common/movement/skill" );
                    commonMovement = new JobInfoEntry[common.Count];
                    for( int i = 0; i < common.Count; i++ )
                    {
                        commonMovement[i] = new JobInfoEntry();
                        commonMovement[i].Name = common[i].InnerText;
                        commonMovement[i].Enabled = false;
                    }

                    jobs = new List<JobInfo>( 256 );

                    for( int i = 1; i <= 0xFF; i++ )
                    {
                        JobInfo newJobInfo = new JobInfo();

                        XmlNode node = doc.SelectSingleNode( string.Format( "//Special/Job[byte='{0}']", i.ToString( "X2" ) ) );
                        XmlNode nameNode = node.SelectSingleNode( "@name" );
                        newJobInfo.Name = nameNode.InnerText;

                        XmlNodeList actionNodes = node.SelectNodes( "action/skill" );
                        newJobInfo.action = new JobInfoEntry[actionNodes.Count];
                        for( int j = 0; j < actionNodes.Count; j++ )
                        {
                            newJobInfo.action[j].Name = actionNodes[j].InnerText;
                            newJobInfo.action[j].Enabled = false;
                        }

                        if( ((node.Attributes["useCommon"] != null) && (node.Attributes["useCommon"].InnerText != "false"))
                            || (node.Attributes["useCommon"] == null))
                        {
                            newJobInfo.reaction = new JobInfoEntry[commonReaction.Length];
                            commonReaction.CopyTo( newJobInfo.reaction, 0 );

                            newJobInfo.support = new JobInfoEntry[commonSupport.Length];
                            commonSupport.CopyTo( newJobInfo.support, 0 );

                            newJobInfo.movement = new JobInfoEntry[commonMovement.Length];
                            commonMovement.CopyTo( newJobInfo.movement, 0 );
                        }
                        else
                        {
                            XmlNodeList reactionNodes = node.SelectNodes( "reaction/skill" );
                            newJobInfo.reaction = new JobInfoEntry[reactionNodes.Count];
                            for( int j = 0; j < reactionNodes.Count; j++ )
                            {
                                newJobInfo.reaction[j].Name = reactionNodes[j].InnerText;
                                newJobInfo.reaction[j].Enabled = false;
                            }

                            XmlNodeList supportNodes = node.SelectNodes( "support/skill" );
                            newJobInfo.support = new JobInfoEntry[supportNodes.Count];
                            for( int j = 0; j < supportNodes.Count; j++ )
                            {
                                newJobInfo.support[j].Name = supportNodes[j].InnerText;
                                newJobInfo.support[j].Enabled = false;
                            }
                            XmlNodeList movementNodes = node.SelectNodes( "movement/skill" );
                            newJobInfo.movement = new JobInfoEntry[movementNodes.Count];
                            for( int j = 0; j < movementNodes.Count; j++ )
                            {
                                newJobInfo.movement[j].Name = movementNodes[j].InnerText;
                                newJobInfo.movement[j].Enabled = false;
                            }
                        }

                        jobs.Add( newJobInfo );
                    }
                }

                return jobs;
            }
        }

        private static List<JobInfo> genericJobs;

        /// <summary>
        /// Gets a list of JobInfo for generic jobs
        /// </summary>
        public static List<JobInfo> GenericJobs
        {
            get
            {
                if( genericJobs == null )
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml( Resources.MainJobBasedOnSprite );

                    genericJobs = new List<JobInfo>( 22 );

                    for( int i = 0; i <= 21; i++ )
                    {
                        JobInfo newJobInfo = new JobInfo();

                        XmlNode node = doc.SelectSingleNode( string.Format( "//Generic/Job[offset='{0}']", i.ToString() ) );
                        XmlNode nameNode = node.SelectSingleNode( "@name" );
                        newJobInfo.Name = nameNode.InnerText;

                        XmlNodeList actionNodes = node.SelectNodes( "action/skill" );
                        newJobInfo.action = new JobInfoEntry[actionNodes.Count];
                        for( int j = 0; j < actionNodes.Count; j++ )
                        {
                            newJobInfo.action[j].Name = actionNodes[j].InnerText;
                            newJobInfo.action[j].Enabled = false;
                        }

                        XmlNodeList reactionNodes = node.SelectNodes( "reaction/skill" );
                        newJobInfo.reaction = new JobInfoEntry[reactionNodes.Count];
                        for( int j = 0; j < reactionNodes.Count; j++ )
                        {
                            newJobInfo.reaction[j].Name = reactionNodes[j].InnerText;
                            newJobInfo.reaction[j].Enabled = false;
                        }

                        XmlNodeList supportNodes = node.SelectNodes( "support/skill" );
                        newJobInfo.support = new JobInfoEntry[supportNodes.Count];
                        for( int j = 0; j < supportNodes.Count; j++ )
                        {
                            newJobInfo.support[j].Name = supportNodes[j].InnerText;
                            newJobInfo.support[j].Enabled = false;
                        }
                        XmlNodeList movementNodes = node.SelectNodes( "movement/skill" );
                        newJobInfo.movement = new JobInfoEntry[movementNodes.Count];
                        for( int j = 0; j < movementNodes.Count; j++ )
                        {
                            newJobInfo.movement[j].Name = movementNodes[j].InnerText;
                            newJobInfo.movement[j].Enabled = false;
                        }

                        genericJobs.Add( newJobInfo );
                    }
                }

                return genericJobs;
            }
        }
    }

    /// <summary>
    /// Represents a character's jobs, enabled abilities, JP, and total JP
    /// </summary>
    public class JobsAndAbilities
    {
        /// <summary>
        /// Represents a specific job's enabled abilities, level, and JP
        /// </summary>
        public class Job
        {
            //public JobsOffset Job;

            public Job[] OtherJobs;

            public uint Level;
            public ushort JP;
            public ushort TotalJP;
            public bool Unlocked;

            public bool[] actions1 = new bool[8];
            public bool[] actions2 = new bool[8];
            public bool[] theRest = new bool[8];

            //public uint GetLevel()
            //{
            //    if (Job == JobsOffset.OnionKnight)
            //    {
            //        return GetLevelFromOtherJobs();
            //    }
            //    else
            //    {
            //        return GetLevelFromTotalJP();
            //    }
            //}

            //private uint GetLevelFromOtherJobs()
            //{
            //    for (int i = 2; i < 
            //}

            public uint GetLevelFromTotalJP()
            {
                if (TotalJP >= 3000) return 8;
                if (TotalJP >= 2200) return 7;
                if (TotalJP >= 1600) return 6;
                if (TotalJP >= 1100) return 5;
                if (TotalJP >= 700) return 4;
                if (TotalJP >= 400) return 3;
                if (TotalJP >= 200) return 2;
                return 1;
            }
        }

        private enum JobsOffset
        {
            Base,
            Chemist,
            Knight,
            Archer,
            Monk,
            WhiteMage,
            BlackMage,
            TimeMage,
            Summoner,
            Thief,
            Orator,
            Mystic,
            Geomancer,
            Dragoon,
            Samurai,
            Ninja,
            Arithmetician,
            Bard,
            Dancer,
            Mime,
            DarkKnight,
            OnionKnight
        }

        public Job[] jobs = new Job[22];

        public JobsAndAbilities( byte[] bytes )
        {
            for( int i = 0; i < 22; i++ )
            {
                Job newJob = new Job();
                newJob.OtherJobs = jobs;
                //newJob.Job = (JobsOffset)i;

                newJob.Unlocked = (bytes[i / 8] & (1 << (7 - (i % 8)))) > 0;

                CopyByteToBoolArray( bytes[i * 3 + 3], newJob.actions1 );
                CopyByteToBoolArray( bytes[i * 3 + 3 + 1], newJob.actions2 );
                CopyByteToBoolArray( bytes[i * 3 + 3 + 2], newJob.theRest );

                newJob.JP = (ushort)((ushort)(bytes[81 + i * 2]) + (ushort)((ushort)bytes[81 + i * 2 + 1] << 8));
                newJob.TotalJP = (ushort)((ushort)(bytes[127 + i * 2]) + (ushort)((ushort)bytes[127 + i * 2 + 1] << 8));
                //newJob.Level = newJob.GetLevelFromTotalJP();

                jobs[i] = newJob;
            }
        }

        #region Utilities

        private void CopyByteToBoolArray( byte b, bool[] array )
        {
            for( int i = 0; i < 8; i++ )
            {
                array[7 - i] = (b & (1 << i)) > 0;
            }
        }

        private byte MakeByteFromBoolArray( bool[] array )
        {
            byte b = 0x00;

            for( int i = 0; i < 8; i++ )
            {
                if( array[7 - i] )
                {
                    b |= (byte)(1 << i);
                }
            }

            return b;
        }

        public byte[] ToByteArray()
        {
            byte[] result = new byte[173];
            for( int i = 0; i < 22; i++ )
            {
                if( jobs[i].Unlocked )
                {
                    result[i / 8] |= (byte)(1 << (7 - (i % 8)));
                }

                result[i * 3 + 3] = MakeByteFromBoolArray( jobs[i].actions1 );
                result[i * 3 + 3 + 1] = MakeByteFromBoolArray( jobs[i].actions2 );
                result[i * 3 + 3 + 2] = MakeByteFromBoolArray( jobs[i].theRest );
                switch( i % 2 )
                {
                    case 0:
                        result[69 + i / 2] |= (byte)(jobs[i].Level << 4);
                        break;
                    case 1:
                        result[69 + i / 2] |= (byte)(jobs[i].Level);
                        break;
                }

                result[81 + i * 2] = (byte)(jobs[i].JP & 0xFF);
                result[81 + i * 2 + 1] = (byte)((jobs[i].JP & 0xFF00) >> 8);
                result[127 + i * 2] = (byte)(jobs[i].TotalJP & 0xFF);
                result[127 + i * 2 + 1] = (byte)((jobs[i].TotalJP & 0xFF00) >> 8);
            }

            return result;
        }

        #endregion
    }
}
