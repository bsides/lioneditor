using System;
using System.Collections.Generic;
using System.Text;
using LionEditor.Properties;
using System.Xml;

namespace LionEditor
{
    public struct JobInfoEntry
    {
        public string Name;
        public bool Enabled;
    }

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

    public class JobsAndAbilities
    {
        public class Job
        {
            public uint Level;
            public ushort JP;
            public ushort TotalJP;
            public byte[] Skills;
            public bool Unlocked;

            public bool[] actions1 = new bool[8];
            public bool[] actions2 = new bool[8];
            public bool[] theRest = new bool[8];
        }

        public Job[] jobs = new Job[22];

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
                if( array[7-i] )
                {
                    b |= (byte)(1 << i);
                }
            }

            return b;
        }

        public JobsAndAbilities( byte[] bytes )
        {
            for( int i = 0; i < 22; i++ )
            {
                Job newJob = new Job();
                newJob.Unlocked = (bytes[i / 8] & (1 << (7 - (i % 8)))) > 0;
                newJob.Skills = new byte[3];

                CopyByteToBoolArray( bytes[i * 3 + 3], newJob.actions1 );
                CopyByteToBoolArray( bytes[i * 3 + 3 + 1], newJob.actions2 );
                CopyByteToBoolArray( bytes[i * 3 + 3 + 2], newJob.theRest );

                switch( i % 2 )
                {
                    case 0:
                        newJob.Level = (uint)((bytes[69 + i / 2] & 0xF0) >> 4);
                        break;
                    case 1:
                        newJob.Level = (uint)((bytes[69 + i / 2] & 0x0F));
                        break;
                }

                newJob.JP = (ushort)((ushort)(bytes[81 + i * 2]) + (ushort)((ushort)bytes[81 + i * 2 + 1] << 8));
                newJob.TotalJP = (ushort)((ushort)(bytes[127 + i * 2]) + (ushort)((ushort)bytes[127 + i * 2 + 1] << 8));
                jobs[i] = newJob;
            }
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
    }
}
