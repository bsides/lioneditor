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

namespace FFTPatcher.TextEditor.Files.PSP
{
    /// <summary>
    /// A special StringSectioned that edits multiple sections in multiple other files at the same time.
    /// </summary>
    public class QuickEdit : BasePSPSectionedFile, IQuickEdit
    {

		#region Static Fields (2) 

        private static Dictionary<int, long> locations;
        private static List<SectionType> ourSectionTypes;

		#endregion Static Fields 

		#region Fields (11) 

        private Dictionary<IStringSectioned, int> abilityDescriptions = new Dictionary<IStringSectioned, int>();
        private Dictionary<IStringSectioned, int> abilityNames = new Dictionary<IStringSectioned, int>();
        private const string filename = "Quick Edit";
        private Dictionary<IStringSectioned, int> itemDescriptions = new Dictionary<IStringSectioned, int>();
        private Dictionary<IStringSectioned, int> itemNames = new Dictionary<IStringSectioned, int>();
        private Dictionary<IStringSectioned, int> jobDescriptions = new Dictionary<IStringSectioned, int>();
        private Dictionary<IStringSectioned, int> jobNames = new Dictionary<IStringSectioned, int>();
        private Dictionary<IStringSectioned, int> jobRequirements = new Dictionary<IStringSectioned, int>();
        private Dictionary<IStringSectioned, int> skillsetDescriptions = new Dictionary<IStringSectioned, int>();
        private Dictionary<IStringSectioned, int> skillsetNames = new Dictionary<IStringSectioned, int>();
        private Dictionary<SectionType, Dictionary<IStringSectioned, int>> types =
            new Dictionary<SectionType, Dictionary<IStringSectioned, int>>();

		#endregion Fields 

		#region Properties (4) 


        /// <summary>
        /// Gets the number of sections.
        /// </summary>
        /// <value>The number of sections.</value>
        protected override int NumberOfSections
        {
            get { return ourSectionTypes.Count; }
        }

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
                if( locations == null )
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


		#endregion Properties 

		#region Constructors (2) 

        static QuickEdit()
        {
            ourSectionTypes = new List<SectionType>( Enum.GetValues( typeof( SectionType ) ) as SectionType[] );
            ourSectionTypes.Remove( SectionType.AbilityQuotes );
        }

        public QuickEdit( FFTText text )
        {
            types[SectionType.AbilityDescriptions] = abilityDescriptions;
            types[SectionType.AbilityNames] = abilityNames;
            types[SectionType.ItemDescriptions] = itemDescriptions;
            types[SectionType.ItemNames] = itemNames;
            types[SectionType.JobDescriptions] = jobDescriptions;
            types[SectionType.JobNames] = jobNames;
            types[SectionType.JobRequirements] = jobRequirements;
            types[SectionType.SkillsetDescriptions] = skillsetDescriptions;
            types[SectionType.SkillsetNames] = skillsetNames;

            Sections = new IList<string>[NumberOfSections];

            foreach( IStringSectioned sectioned in text.SectionedFiles )
            {
                var namedSections = sectioned.GetNamedSections();
                foreach( NamedSection namedSection in namedSections )
                {
                    types[namedSection.SectionType].Add( namedSection.Owner, namedSection.SectionIndex );
                    if( namedSection.IsRepresentativeSample )
                    {
                        AddSection(
                            namedSection.SectionType,
                            namedSection.Owner.Sections[(int)namedSection.SectionIndex].Sub( 0, namedSection.SampleLength - 1 ) );
                    }
                }
            }
        }

		#endregion Constructors 

		#region Methods (2) 


        private void AddSection( SectionType type, IList<string> list )
        {
            NotifyStringList theList = new NotifyStringList( type, list );
            theList.ListMemberChanged += theList_ListMemberChanged;
            Sections[ourSectionTypes.IndexOf( type )] = theList;
        }

        private void theList_ListMemberChanged( object sender, NotifyStringList.ListMemberChangedEventArgs e )
        {
            NotifyStringList list = sender as NotifyStringList;

            foreach( KeyValuePair<IStringSectioned, int> kvp in types[list.Type] )
            {
                kvp.Key[kvp.Value, e.Index] = list[e.Index];
            }
        }


		#endregion Methods 

    }
}
