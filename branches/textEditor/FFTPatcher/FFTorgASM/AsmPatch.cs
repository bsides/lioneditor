using System;
using System.Collections.Generic;
using PatcherLib.Datatypes;

namespace FFTorgASM
{
    class AsmPatch : IList<PatchedByteArray>
    {
        List<PatchedByteArray> innerList;
        public string Name { get; private set; }
        public string Description { get; private set; }
        public IList<KeyValuePair<string,PatchedByteArray>> Variables { get; private set; }
        private IEnumerator<PatchedByteArray> enumerator;

        public AsmPatch( string name, string description, IEnumerable<PatchedByteArray> patches )
        {
            enumerator = new AsmPatchEnumerator( this );
            this.Name = name;
            Description = description;
            innerList = new List<PatchedByteArray>( patches );
            Variables = new KeyValuePair<string,PatchedByteArray>[0];
        }

        public AsmPatch( string name, string description, IEnumerable<PatchedByteArray> patches, IList<KeyValuePair<string,PatchedByteArray>> variables )
            : this( name, description, patches )
        {
            KeyValuePair<string, PatchedByteArray>[] myVars = new KeyValuePair<string, PatchedByteArray>[variables.Count];
            variables.CopyTo( myVars, 0 );
            Variables = myVars;
        }

        public int IndexOf( PatchedByteArray item )
        {
            return innerList.IndexOf( item );
        }

        public void Insert( int index, PatchedByteArray item )
        {
            throw new NotImplementedException();
        }

        public void RemoveAt( int index )
        {
            throw new InvalidOperationException( "collection is readonly" );
        }

        public PatchedByteArray this[int index]
        {
            get
            {
                if ( index < innerList.Count )
                {
                    return innerList[index];
                }
                else
                {
                    return Variables[index - innerList.Count].Value;
                }
            }
            set
            {
                throw new InvalidOperationException( "collection is readonly" );
            }
        }

        public void Add( PatchedByteArray item )
        {
            throw new InvalidOperationException( "collection is readonly" );
        }

        public void Clear()
        {
            throw new InvalidOperationException( "collection is readonly" );
        }

        public bool Contains( PatchedByteArray item )
        {
            return innerList.Contains( item );
        }

        public void CopyTo( PatchedByteArray[] array, int arrayIndex )
        {
            innerList.CopyTo( array, arrayIndex );
        }

        public int Count
        {
            get { return innerList.Count + Variables.Count; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public bool Remove( PatchedByteArray item )
        {
            throw new InvalidOperationException( "collection is readonly" );
        }

        public IEnumerator<PatchedByteArray> GetEnumerator()
        {
            return enumerator;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return enumerator as System.Collections.IEnumerator;
        }

        public override string ToString()
        {
            return Name;
        }

        private class AsmPatchEnumerator : IEnumerator<PatchedByteArray>
        {
            private int index = -1;
            private AsmPatch owner;
            public AsmPatchEnumerator( AsmPatch owner )
            {
                this.owner = owner;
            }
            #region IEnumerator<PatchedByteArray> Members

            public PatchedByteArray Current
            {
                get { return owner[index]; }
            }

            #endregion


            #region IDisposable Members

            public void Dispose()
            {
            }

            #endregion

            #region IEnumerator Members

            object System.Collections.IEnumerator.Current
            {
                get { return Current; }
            }

            public bool MoveNext()
            {
                index++;
                return index < owner.Count;
            }

            public void Reset()
            {
                index = -1;
            }

            #endregion
        }
    }
}
