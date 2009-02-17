
namespace FFTPatcher.TextEditor
{
    /// <summary>
    /// A datatype representing a named section in a <see cref="IStringSectioned"/> file.
    /// </summary>
    public class NamedSection
    {

        #region Properties (6)


        /// <summary>
        /// Gets a value indicating whether this instance is a representative sample of <see cref="SectionType"/>.
        /// </summary>
        public bool IsRepresentativeSample { get; private set; }

        /// <summary>
        /// Gets the start index at which entries should be copied to this section.
        /// </summary>
        public int Offset { get; private set; }

        /// <summary>
        /// Gets the number of elements in this named section to use as the sample set.
        /// </summary>
        public int SampleLength { get; private set; }

        /// <summary>
        /// Gets the index of this section in <see cref="Owner"/>.
        /// </summary>
        public int SectionIndex { get; private set; }

        /// <summary>
        /// Gets what type of section this is.
        /// </summary>
        public SectionType SectionType { get; private set; }


        #endregion Properties

        #region Methods (3)


        /// <summary>
        /// Initializes a new instance of the <see cref="NamedSection"/> class.
        /// </summary>
        /// <param name="owner">The owner of this named section.</param>
        /// <param name="type">The type of section this is.</param>
        /// <param name="index">The index of this section in <paramref name="owner"/>.</param>
        /// <param name="representative">If this instance is a representative sample of <paramref name="type"/></param>
        /// <param name="sampleLength">Length of the sample if it is</param>
        internal NamedSection( SectionType type, int index, bool representative, int sampleLength )
        {
            //Owner = owner;
            SectionType = type;
            SectionIndex = index;
            IsRepresentativeSample = representative;
            SampleLength = sampleLength;
            Offset = -1;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedSection"/> class.
        /// </summary>
        /// <param name="owner">The owner of this named section.</param>
        /// <param name="type">The type of section this is.</param>
        /// <param name="index">The index of this section in <paramref name="owner"/>.</param>
        /// <param name="offset">The start index for copying to this section</param>
        internal NamedSection( SectionType type, int index, int offset )
            : this( /*owner,*/ type, index, false, -1 )
        {
            Offset = offset;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedSection"/> class.
        /// </summary>
        /// <param name="owner">The owner of this named section.</param>
        /// <param name="type">The type of section this is.</param>
        /// <param name="index">The index of this section in <paramref name="owner"/>.</param>
        internal NamedSection( SectionType type, int index )
            : this( /*owner,*/ type, index, false, -1 )
        {
        }


        #endregion Methods

    }
}
