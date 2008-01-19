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
    public static class Resources
    {
        static Dictionary<string, object> dict = new Dictionary<string, object>();

        static Resources()
        {
            dict["AbilitiesBin"] = GZip.Decompress( FFTPatcher.Properties.Resources.AbilitiesBin );
            dict["ActionEventsBin"] = GZip.Decompress( FFTPatcher.Properties.Resources.ActionEventsBin );
            dict["ENTD1"] = GZip.Decompress( FFTPatcher.Properties.Resources.ENTD1 );
            dict["ENTD2"] = GZip.Decompress( FFTPatcher.Properties.Resources.ENTD2 );
            dict["ENTD3"] = GZip.Decompress( FFTPatcher.Properties.Resources.ENTD3 );
            dict["ENTD4"] = GZip.Decompress( FFTPatcher.Properties.Resources.ENTD4 );
            dict["ENTD5"] = GZip.Decompress( FFTPatcher.Properties.Resources.ENTD5 );
            dict["FontBin"] = GZip.Decompress( FFTPatcher.Properties.Resources.FontBin );
            dict["FontWidthsBin"] = GZip.Decompress( FFTPatcher.Properties.Resources.FontWidthsBin );
            dict["InflictStatusesBin"] = GZip.Decompress( FFTPatcher.Properties.Resources.InflictStatusesBin );
            dict["JobLevelsBin"] = GZip.Decompress( FFTPatcher.Properties.Resources.JobLevelsBin );
            dict["JobsBin"] = GZip.Decompress( FFTPatcher.Properties.Resources.JobsBin );
            dict["MonsterSkillsBin"] = GZip.Decompress( FFTPatcher.Properties.Resources.MonsterSkillsBin );
            dict["NewItemAttributesBin"] = GZip.Decompress( FFTPatcher.Properties.Resources.NewItemAttributesBin );
            dict["NewItemsBin"] = GZip.Decompress( FFTPatcher.Properties.Resources.NewItemsBin );
            dict["StatusAttributesBin"] = GZip.Decompress( FFTPatcher.Properties.Resources.StatusAttributesBin );
            dict["OldItemAttributesBin"] = GZip.Decompress( FFTPatcher.Properties.Resources.OldItemAttributesBin );
            dict["OldItemsBin"] = GZip.Decompress( FFTPatcher.Properties.Resources.OldItemsBin );
            dict["SkillSetsBin"] = GZip.Decompress( FFTPatcher.Properties.Resources.SkillSetsBin );
            dict["PoachProbabilitiesBin"] = GZip.Decompress( FFTPatcher.Properties.Resources.PoachProbabilitiesBin );

            dict["SkillSets"] = GZip.Decompress( FFTPatcher.Properties.Resources.SkillSets ).ToUTF8String();
            dict["Abilities"] = GZip.Decompress( FFTPatcher.Properties.Resources.Abilities ).ToUTF8String();
            dict["EventNames"] = GZip.Decompress( FFTPatcher.Properties.Resources.EventNames ).ToUTF8String();
            dict["Items"] = GZip.Decompress( FFTPatcher.Properties.Resources.Items ).ToUTF8String();
            dict["Jobs"] = GZip.Decompress( FFTPatcher.Properties.Resources.Jobs ).ToUTF8String();
            dict["SpecialNames"] = GZip.Decompress( FFTPatcher.Properties.Resources.SpecialNames ).ToUTF8String();
            dict["SpriteSets"] = GZip.Decompress( FFTPatcher.Properties.Resources.SpriteSets ).ToUTF8String();
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
        public static byte[] ENTD1 { get { return dict["ENTD1"] as byte[]; } }
        public static byte[] ENTD2 { get { return dict["ENTD2"] as byte[]; } }
        public static byte[] ENTD3 { get { return dict["ENTD3"] as byte[]; } }
        public static byte[] ENTD4 { get { return dict["ENTD4"] as byte[]; } }
        public static byte[] ENTD5 { get { return dict["ENTD5"] as byte[]; } }
        public static byte[] FontBin { get { return dict["FontBin"] as byte[]; } }
        public static byte[] FontWidthsBin { get { return dict["FontWidthsBin"] as byte[]; } }
        public static byte[] InflictStatusesBin { get { return dict["InflictStatusesBin"] as byte[]; } }
        public static byte[] JobLevelsBin { get { return dict["JobLevelsBin"] as byte[]; } }
        public static byte[] JobsBin { get { return dict["JobsBin"] as byte[]; } }
        public static byte[] MonsterSkillsBin { get { return dict["MonsterSkillsBin"] as byte[]; } }
        public static byte[] NewItemAttributesBin { get { return dict["NewItemAttributesBin"] as byte[]; } }
        public static byte[] NewItemsBin { get { return dict["NewItemsBin"] as byte[]; } }
        public static byte[] StatusAttributesBin { get { return dict["StatusAttributesBin"] as byte[]; } }
        public static byte[] OldItemAttributesBin { get { return dict["OldItemAttributesBin"] as byte[]; } }
        public static byte[] OldItemsBin { get { return dict["OldItemsBin"] as byte[]; } }
        public static byte[] SkillSetsBin { get { return dict["SkillSetsBin"] as byte[]; } }
    }
}