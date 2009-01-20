using System;
using System.Collections.Generic;
using System.Text;

namespace FFTPatcher
{
    public class Set<T> : IEnumerable<T>
    {
        private class SetEqualityComparer<U> : IEqualityComparer<U>
        {
            private Comparison<U> comparison;
            public SetEqualityComparer( Comparison<U> comparison )
            {
                this.comparison = comparison;
            }

            public bool Equals( U x, U y )
            {
                return comparison( x, y ) == 0;
            }

            public int GetHashCode( U obj )
            {
                return obj.GetHashCode();
            }
        }


        Dictionary<T, bool> backing;

        public Set()
        {
            backing = new Dictionary<T, bool>();
        }

        public Set( IEqualityComparer<T> comparer )
        {
            backing = new Dictionary<T, bool>( comparer );
        }

        public Set( IEnumerable<T> items )
            : this()
        {
            items.ForEach( i => Add( i ) );
        }

        public Set( IEnumerable<T> items, IEqualityComparer<T> comparer )
            : this( comparer )
        {
            items.ForEach( i => Add( i ) );
        }

        public Set( IEnumerable<T> items, Comparison<T> comparer )
            : this( items, new SetEqualityComparer<T>( comparer ) )
        {
        }

        public Set( Comparison<T> comparer )
            : this( new SetEqualityComparer<T>( comparer ) )
        {
        }

        public bool Contains( T item )
        {
            return backing.ContainsKey( item );
        }

        public void Add( T item )
        {
            backing[item] = true;
        }

        public void AddRange( IEnumerable<T> items )
        {
            items.ForEach( i => Add( i ) );
        }

        public void Remove( T item )
        {
            if ( Contains( item ) )
            {
                backing.Remove( item );
            }
        }

        public IList<T> GetElements()
        {
            return new List<T>( backing.Keys ).AsReadOnly();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return backing.Keys.GetEnumerator() as IEnumerator<T>;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return backing.Keys.GetEnumerator();
        }

        public int Count { get { return backing.Count; } }
    }
}
