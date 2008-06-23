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

namespace FFTPatcher.TextEditor.Files.PSX
{
    public class QuickEdit : BasePSXSectionedFile
    {

		#region Fields (13) 

        private IList<IList<string>> abilityDescriptions = new List<IList<string>>();
        private IList<IList<string>> abilityNames = new List<IList<string>>();
        private IList<IList<string>> abilityQuotes = new List<IList<string>>();
        private const string filename = "Quick Edit";
        private IList<IList<string>> itemDescriptions = new List<IList<string>>();
        private IList<IList<string>> itemNames = new List<IList<string>>();
        private IList<IList<string>> jobDescriptions = new List<IList<string>>();
        private IList<IList<string>> jobNames = new List<IList<string>>();
        private IList<IList<string>> jobRequirements = new List<IList<string>>();
        private static Dictionary<string, long> locations;
        private IList<IList<string>> skillsetDescriptions = new List<IList<string>>();
        private IList<IList<string>> skillsetNames = new List<IList<string>>();
        private Dictionary<SectionType, IList<IList<string>>> types = 
            new Dictionary<SectionType, IList<IList<string>>>();

		#endregion Fields 

		#region Enums (1) 

        private enum SectionType
        {
            AbilityNames = 0,
            AbilityDescriptions = 1,
            AbilityQuotes = 2,
            ItemNames = 3,
            ItemDescriptions = 4,
            JobNames = 5,
            JobDescriptions = 6,
            JobRequirements = 7,
            SkillsetNames = 8,
            SkillsetDescriptions = 9
        }

		#endregion Enums 

		#region Constructors (1) 

        public QuickEdit(FFTText text)
        {
            ProcessSPELLMES(
                text.SectionedFiles.Find(delegate(IStringSectioned file) { return file is SPELLMES; }) as SPELLMES);
            ProcessJOBSTTSOUT(
                text.SectionedFiles.Find(delegate(IStringSectioned file) { return file is JOBSTTSOUT; }) as JOBSTTSOUT);
            ProcessJOINLZW(
                text.SectionedFiles.Find(delegate(IStringSectioned file) { return file is JOINLZW; }) as JOINLZW);
            ProcessSAMPLELZW(
                text.SectionedFiles.Find(delegate(IStringSectioned file) { return file is SAMPLELZW; }) as SAMPLELZW);
            ProcessBUNITOUT(
                text.SectionedFiles.Find(delegate(IStringSectioned file) { return file is BUNITOUT; }) as BUNITOUT);
            ProcessHELPLZW(
                text.SectionedFiles.Find(delegate(IStringSectioned file) { return file is HELPLZW; }) as HELPLZW);
            ProcessHELPMENUOUT(
                text.SectionedFiles.Find(delegate(IStringSectioned file) { return file is HELPMENUOUT; }) as HELPMENUOUT);
            ProcessATCHELPLZW(
                text.SectionedFiles.Find(delegate(IStringSectioned file) { return file is ATCHELPLZW; }) as ATCHELPLZW);
            ProcessATTACKOUT(
                text.SectionedFiles.Find(delegate(IStringSectioned file) { return file is ATTACKOUT; }) as ATTACKOUT);
            ProcessEQUIPOUT(
                text.SectionedFiles.Find(delegate(IStringSectioned file) { return file is EQUIPOUT; }) as EQUIPOUT);
            ProcessWLDHELPLZW(
                text.SectionedFiles.Find(delegate(IStringSectioned file) { return file is WLDHELPLZW; }) as WLDHELPLZW);
            ProcessWORLDBIN(
                text.SectionedFiles.Find(delegate(IStringSectioned file) { return file is WORLDBIN; }) as WORLDBIN);
            ProcessWORLDLZW(
                text.SectionedFiles.Find(delegate(IStringSectioned file) { return file is WORLDLZW; }) as WORLDLZW);

            Sections = new List<IList<string>>(10);

            AddSection(SectionType.AbilityNames, abilityNames[1].Sub(0, 511));
            AddSection(SectionType.AbilityDescriptions, abilityDescriptions[0].Sub(0, 511));
            AddSection(SectionType.AbilityQuotes, abilityQuotes[0].Sub(0, 511));

            AddSection(SectionType.ItemNames, itemNames[0].Sub(0, 255));
            AddSection(SectionType.ItemDescriptions, itemDescriptions[0].Sub(0, 255));

            AddSection(SectionType.JobNames, jobNames[0].Sub(0, 154));
            AddSection(SectionType.JobDescriptions, jobDescriptions[0].Sub(0, 159));
            AddSection(SectionType.JobRequirements, jobRequirements[0].Sub(0, 93));

            AddSection(SectionType.SkillsetNames, skillsetNames[0].Sub(0, 188));
            AddSection(SectionType.SkillsetDescriptions, skillsetDescriptions[0].Sub(0, 187));

            types[SectionType.AbilityDescriptions] = abilityDescriptions;
            types[SectionType.AbilityNames] = abilityNames;
            types[SectionType.AbilityQuotes] = abilityQuotes;
            types[SectionType.ItemDescriptions] = itemDescriptions;
            types[SectionType.ItemNames] = itemNames;
            types[SectionType.JobDescriptions] = jobDescriptions;
            types[SectionType.JobNames] = jobNames;
            types[SectionType.JobRequirements] = jobRequirements;
            types[SectionType.SkillsetDescriptions] = skillsetDescriptions;
            types[SectionType.SkillsetNames] = skillsetNames;
        }

		#endregion Constructors 

		#region Properties (4) 

        /// <summary>
        /// Gets the filename.
        /// </summary>
        public override string Filename
        {
            get { return filename; }
        }

        /// <summary>
        /// Gets the filenames and locations for this file.
        /// </summary>
        public override IDictionary<string, long> Locations
        {
            get 
            {
                if (locations == null)
                {
                    locations = new Dictionary<string, long>();
                }

                return locations;
            }
        }

        /// <summary>
        /// Gets the maximum length of this file as a byte array.
        /// </summary>
        public override int MaxLength
        {
            get { return Int32.MaxValue; }
        }

        /// <summary>
        /// Gets the number of sections.
        /// </summary>
        /// <value>The number of sections.</value>
        protected override int NumberOfSections
        {
            get { return 10; }
        }

		#endregion Properties 

		#region Methods (18) 


		// Private Methods (18) 

        private void AddSection(SectionType type, IList<string> list)
        {
            MyList theList = new MyList(type, list);
            theList.ListMemberChanged += new EventHandler<MyList.ListMemberChangedEventArgs>(theList_ListMemberChanged);
            Sections.Add(theList);
        }

        private void ProcessATCHELPLZW(ATCHELPLZW atc)
        {
            ProcessBUNITOUTorHELPLZWorHELPMENUOUTorATCHELPLZWorWLDHELPLZW(atc);
        }

        private void ProcessATTACKOUT(ATTACKOUT attack)
        {
            jobNames.Add(attack.Sections[2]);
        }

        private void ProcessBUNITOUT(BUNITOUT bunit)
        {
            ProcessBUNITOUTorHELPLZWorHELPMENUOUTorATCHELPLZWorWLDHELPLZW(bunit);
        }

        private void ProcessBUNITOUTorHELPLZWorHELPMENUOUTorATCHELPLZWorWLDHELPLZW(IStringSectioned file)
        {
            jobDescriptions.Add(file.Sections[12]);
            itemDescriptions.Add(file.Sections[13]);
            abilityDescriptions.Add(file.Sections[15]);
            skillsetDescriptions.Add(file.Sections[19]);
        }

        private void ProcessEQUIPOUT(EQUIPOUT equip)
        {
            itemDescriptions.Add(equip.Sections[13]);
        }

        private void ProcessHELPLZW(HELPLZW help)
        {
            ProcessBUNITOUTorHELPLZWorHELPMENUOUTorATCHELPLZWorWLDHELPLZW(help);
        }

        private void ProcessHELPMENUOUT(HELPMENUOUT help)
        {
            ProcessBUNITOUTorHELPLZWorHELPMENUOUTorATCHELPLZWorWLDHELPLZW(help);
        }

        private void ProcessHELPMENUOUT(ATCHELPLZW help)
        {
            ProcessBUNITOUTorHELPLZWorHELPMENUOUTorATCHELPLZWorWLDHELPLZW(help);
        }

        private void ProcessJOBSTTSOUT(JOBSTTSOUT jobstts)
        {
            jobDescriptions.Add(jobstts.Sections[12]);
            abilityDescriptions.Add(jobstts.Sections[15]);
        }

        private void ProcessJOINLZW(JOINLZW join)
        {
            jobNames.Add(join.Sections[4]);
        }

        private void ProcessSAMPLELZW(SAMPLELZW sample)
        {
            ProcessWORLDLZWorSAMPLELZW(sample);
        }

        private void ProcessSPELLMES(SPELLMES spellmes)
        {
            abilityQuotes.Add(spellmes.Sections[0]);
        }

        private void ProcessWLDHELPLZW(WLDHELPLZW help)
        {
            ProcessBUNITOUTorHELPLZWorHELPMENUOUTorATCHELPLZWorWLDHELPLZW(help);
        }

        private void ProcessWORLDBIN(WORLDBIN world)
        {
            skillsetNames.Add(world.Sections[0]);
            jobNames.Add(world.Sections[1]);
            abilityNames.Add(world.Sections[2]);
            jobRequirements.Add(world.Sections[5]);
        }

        private void ProcessWORLDLZW(WORLDLZW worldLzw)
        {
            ProcessWORLDLZWorSAMPLELZW(worldLzw);
        }

        private void ProcessWORLDLZWorSAMPLELZW(IStringSectioned file)
        {
            jobNames.Add(file.Sections[6]);
            itemNames.Add(file.Sections[7]);
            abilityNames.Add(file.Sections[14]);
            skillsetNames.Add(file.Sections[22]);
        }

        private void theList_ListMemberChanged(object sender, MyList.ListMemberChangedEventArgs e)
        {
            MyList list = sender as MyList;

            foreach (IList<string> siblingList in types[list.Type])
            {
                siblingList[e.Index] = list[e.Index];
            }
        }


		#endregion Methods 

		#region Nested Classes (1) 


        private class MyList : IList<string>
        {

		    #region Fields (1) 

            private List<string> innerList;

		    #endregion Fields 

		    #region Constructors (3) 

            public MyList(SectionType type, IEnumerable<string> collection)
            {
                Type = type;
                innerList = new List<string>(collection);
            }

            public MyList(SectionType type, int capacity)
            {
                Type = type;
                innerList = new List<string>(capacity);
            }

            public MyList(SectionType type)
            {
                Type = type;
                innerList = new List<string>();
            }

		    #endregion Constructors 

		    #region Properties (1) 

            public SectionType Type { get; private set; }

		    #endregion Properties 


            #region IList<string> Members

            public int IndexOf(string item)
            {
                return innerList.IndexOf(item);
            }

            public void Insert(int index, string item)
            {
                innerList.Insert(index, item);
            }

            public void RemoveAt(int index)
            {
                innerList.RemoveAt(index);
            }

            #endregion

            #region ICollection<string> Members

            public void Add(string item)
            {
                innerList.Add(item);
            }

            public void Clear()
            {
                innerList.Clear();
            }

            public bool Contains(string item)
            {
                return innerList.Contains(item);
            }

            public void CopyTo(string[] array, int arrayIndex)
            {
                innerList.CopyTo(array, arrayIndex);
            }

            public int Count
            {
                get { return innerList.Count; }
            }

            public bool IsReadOnly
            {
                get { return (innerList as IList<string>).IsReadOnly; }
            }

            public bool Remove(string item)
            {
                return innerList.Remove(item);
            }

            #endregion

            #region IEnumerable<string> Members

            public IEnumerator<string> GetEnumerator()
            {
                return innerList.GetEnumerator();
            }

            #endregion

            #region IEnumerable Members

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return innerList.GetEnumerator();
            }

            #endregion

            #region IList<string> Members


            public string this[int index]
            {
                get
                {
                    return innerList[index];
                }
                set
                {
                    innerList[index] = value;
                    OnListMemberChanged(index);
                }
            }

            private void OnListMemberChanged(int index)
            {
                if (ListMemberChanged != null)
                {
                    ListMemberChanged(this, new ListMemberChangedEventArgs(index));
                }
            }

            public event EventHandler<ListMemberChangedEventArgs> ListMemberChanged;

            public class ListMemberChangedEventArgs : EventArgs
            {
                public int Index { get; private set; }
                public ListMemberChangedEventArgs(int index)
                {
                    Index = index;
                }
            }

            #endregion
        }

		#endregion Nested Classes 

    }
}
