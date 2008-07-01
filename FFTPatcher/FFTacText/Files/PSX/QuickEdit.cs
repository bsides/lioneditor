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
    /// <summary>
    /// A special StringSectioned that edits multiple sections in multiple other files at the same time.
    /// </summary>
    public class QuickEdit : BasePSXSectionedFile, IQuickEdit
    {

		#region Fields (13) 

        private Dictionary<IStringSectioned, NamedSection> abilityDescriptions = new Dictionary<IStringSectioned, NamedSection>();
        private Dictionary<IStringSectioned, NamedSection> abilityNames = new Dictionary<IStringSectioned, NamedSection>();
        private Dictionary<IStringSectioned, NamedSection> abilityQuotes = new Dictionary<IStringSectioned, NamedSection>();
        private const string filename = "Quick Edit";
        private Dictionary<IStringSectioned, NamedSection> itemDescriptions = new Dictionary<IStringSectioned, NamedSection>();
        private Dictionary<IStringSectioned, NamedSection> itemNames = new Dictionary<IStringSectioned, NamedSection>();
        private Dictionary<IStringSectioned, NamedSection> jobDescriptions = new Dictionary<IStringSectioned, NamedSection>();
        private Dictionary<IStringSectioned, NamedSection> jobNames = new Dictionary<IStringSectioned, NamedSection>();
        private Dictionary<IStringSectioned, NamedSection> jobRequirements = new Dictionary<IStringSectioned, NamedSection>();
        private static Dictionary<int, long> locations;
        private Dictionary<IStringSectioned, NamedSection> skillsetDescriptions = new Dictionary<IStringSectioned, NamedSection>();
        private Dictionary<IStringSectioned, NamedSection> skillsetNames = new Dictionary<IStringSectioned, NamedSection>();
        private Dictionary<SectionType, Dictionary<IStringSectioned, NamedSection>> types =
            new Dictionary<SectionType, Dictionary<IStringSectioned, NamedSection>>();

		#endregion Fields 

		#region Constructors (1) 

        public QuickEdit( FFTText text )
        {
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

            Sections = new IList<string>[NumberOfSections];

            foreach ( IStringSectioned sectioned in text.SectionedFiles )
            {
                var namedSections = sectioned.GetNamedSections();
                foreach ( NamedSection namedSection in namedSections )
                {
                    types[namedSection.SectionType].Add( namedSection.Owner, namedSection );
                    if ( namedSection.IsRepresentativeSample )
                    {
                        AddSection( 
                            namedSection.SectionType, 
                            namedSection.Owner.Sections[(int)namedSection.SectionIndex].Sub( 0, namedSection.SampleLength - 1 ) );
                    }
                }
            }
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
        public override IDictionary<int, long> Locations
        {
            get
            {
                if ( locations == null )
                {
                    locations = new Dictionary<int, long>();
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
            get { return Enum.GetValues( typeof( SectionType ) ).Length; }
        }

		#endregion Properties 

		#region Methods (2) 


		// Private Methods (2) 

        private void AddSection( SectionType type, IList<string> list )
        {
            NotifyStringList theList = new NotifyStringList( type, list );
            theList.ListMemberChanged += theList_ListMemberChanged;
            Sections[(int)type] = theList;
        }

        private void theList_ListMemberChanged( object sender, NotifyStringList.ListMemberChangedEventArgs e )
        {
            NotifyStringList list = sender as NotifyStringList;

            foreach ( KeyValuePair<IStringSectioned, NamedSection> kvp in types[list.Type] )
            {
                if ( kvp.Value.Offset == -1 || kvp.Value.Offset <= e.Index )
                {
                    kvp.Key[kvp.Value.SectionIndex, e.Index] = list[e.Index];
                }
            }
        }


		#endregion Methods 

    }
}
