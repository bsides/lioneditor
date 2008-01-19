/*
    Copyright 2007, Joe Davidson <joedavidson@gmail.com>

    This file is part of FFTPatcher.

    FFTPatcher is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    FFTPatcher is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with FFTPatcher.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace FFTPatcher
{
    public static class PSXResources
    {
        static Dictionary<string, object> dict = new Dictionary<string, object>();

        static PSXResources()
        {
            dict["AbilitiesBin"] = GZip.Decompress( FFTPatcher.Properties.PSXResources.AbilitiesBin );
            dict["ActionEventsBin"] = GZip.Decompress( FFTPatcher.Properties.PSXResources.ActionEventsBin );
            dict["FontBin"] = GZip.Decompress( FFTPatcher.Properties.PSXResources.FontBin );
            dict["FontWidthsBin"] = GZip.Decompress( FFTPatcher.Properties.PSXResources.FontWidthsBin );
            dict["InflictStatusesBin"] = GZip.Decompress( FFTPatcher.Properties.PSXResources.InflictStatusesBin );
            dict["JobLevelsBin"] = GZip.Decompress( FFTPatcher.Properties.PSXResources.JobLevelsBin );
            dict["JobsBin"] = GZip.Decompress( FFTPatcher.Properties.PSXResources.JobsBin );
            dict["MonsterSkillsBin"] = GZip.Decompress( FFTPatcher.Properties.PSXResources.MonsterSkillsBin );
            dict["StatusAttributesBin"] = GZip.Decompress( FFTPatcher.Properties.PSXResources.StatusAttributesBin );
            dict["OldItemAttributesBin"] = GZip.Decompress( FFTPatcher.Properties.PSXResources.OldItemAttributesBin );
            dict["OldItemsBin"] = GZip.Decompress( FFTPatcher.Properties.PSXResources.OldItemsBin );
            dict["SkillSetsBin"] = GZip.Decompress( FFTPatcher.Properties.PSXResources.SkillSetsBin );
            dict["PoachProbabilitiesBin"] = GZip.Decompress( FFTPatcher.Properties.PSXResources.PoachProbabilitiesBin );

            dict["SkillSets"] = GZip.Decompress( FFTPatcher.Properties.PSXResources.SkillSets ).ToUTF8String();
            dict["Abilities"] = GZip.Decompress( FFTPatcher.Properties.PSXResources.Abilities ).ToUTF8String();
            dict["EventNames"] = GZip.Decompress( FFTPatcher.Properties.PSXResources.EventNames ).ToUTF8String();
            dict["Items"] = GZip.Decompress( FFTPatcher.Properties.PSXResources.Items ).ToUTF8String();
            dict["Jobs"] = GZip.Decompress( FFTPatcher.Properties.PSXResources.Jobs ).ToUTF8String();
            dict["SpecialNames"] = GZip.Decompress( FFTPatcher.Properties.PSXResources.SpecialNames ).ToUTF8String();
            dict["SpriteSets"] = GZip.Decompress( FFTPatcher.Properties.PSXResources.SpriteSets ).ToUTF8String();
        }

        public static string SkillSets { get { return dict["SkillSets"] as string; } }
        public static string Abilities { get { return dict["Abilities"] as string; } }
        public static string EventNames { get { return dict["EventNames"] as string; } }
        public static string Items { get { return dict["Items"] as string; } }
        public static string Jobs { get { return dict["Jobs"] as string; } }
        public static string SpecialNames { get { return dict["SpecialNames"] as string; } }
        public static string SpriteSets { get { return dict["SpriteSets"] as string; } }

        public static byte[] AbilitiesBin { get { return dict["AbilitiesBin"] as byte[]; } }
        public static byte[] PoachProbabilitiesBin { get { return dict["PoachProbabilitiesBin"] as byte[]; } }
        public static byte[] ActionEventsBin { get { return dict["ActionEventsBin"] as byte[]; } }
        public static byte[] FontBin { get { return dict["FontBin"] as byte[]; } }
        public static byte[] FontWidthsBin { get { return dict["FontWidthsBin"] as byte[]; } }
        public static byte[] InflictStatusesBin { get { return dict["InflictStatusesBin"] as byte[]; } }
        public static byte[] JobLevelsBin { get { return dict["JobLevelsBin"] as byte[]; } }
        public static byte[] JobsBin { get { return dict["JobsBin"] as byte[]; } }
        public static byte[] MonsterSkillsBin { get { return dict["MonsterSkillsBin"] as byte[]; } }
        public static byte[] StatusAttributesBin { get { return dict["StatusAttributesBin"] as byte[]; } }
        public static byte[] OldItemAttributesBin { get { return dict["OldItemAttributesBin"] as byte[]; } }
        public static byte[] OldItemsBin { get { return dict["OldItemsBin"] as byte[]; } }
        public static byte[] SkillSetsBin { get { return dict["SkillSetsBin"] as byte[]; } }
    }
}