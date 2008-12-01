using System;
using System.Collections.Generic;
using System.Text;

namespace FFTPatcher
{
    public class Set<T>
    {
        private class SetEqualityComparer<T> : IEqualityComparer<T>
        {
            private Comparison<T> comparison;
            public SetEqualityComparer( Comparison<T> comparison )
            {
                this.comparison = comparison;
            }

            public bool Equals( T x, T y )
            {
                return comparison( x, y ) == 0;
            }

            public int GetHashCode( T obj )
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

        public Set( IList<T> items )
            : this()
        {
            items.ForEach( i => Add( i ) );
        }

        public Set( IList<T> items, IEqualityComparer<T> comparer )
            : this( comparer )
        {
            items.ForEach( i => Add( i ) );
        }

        public Set( IList<T> items, Comparison<T> comparer )
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
    }
}
