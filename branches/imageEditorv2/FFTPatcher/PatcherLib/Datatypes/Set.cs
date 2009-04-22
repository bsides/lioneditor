using System;
using System.Collections.Generic;
using PatcherLib.Utilities;

namespace PatcherLib.Datatypes
{
    public class Set<T> : IEnumerable<T>
    {
        public class SetEqualityComparer<U> : IEqualityComparer<U>
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

        List<T> backingList;
        Dictionary<T, bool> backing;
        IEqualityComparer<T> comparer;

        public Set()
        {
            backing = new Dictionary<T, bool>();
            backingList = new List<T>();
        }

        public Set( IEqualityComparer<T> comparer )
        {
            backing = new Dictionary<T, bool>( comparer );
            backingList = new List<T>();
            this.comparer = comparer;
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
            if ( !backing.ContainsKey( item ) )
            {
                backingList.Add( item );
                backing[item] = true;
            }
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
                backingList.Remove( item );
            }
        }

        public IList<T> GetElements()
        {
            return backingList.AsReadOnly();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return backingList.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return backingList.GetEnumerator();
        }

        public int Count { get { return backingList.Count; } }

        public int IndexOf( T item )
        {
            if ( comparer != null )
            {
                return backingList.FindIndex( x => comparer.Equals( x, item ) );
            }
            else
            {
                return backingList.IndexOf( item );
            }
        }

        public T this[int index]
        {
            get { return backingList[index]; }
        }
    }
}
