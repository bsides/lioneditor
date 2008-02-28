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

namespace FFTPatcher.Datatypes
{
    public class SubArray<T> : ICollection<T>, IList<T> 
    {
        private IList<T> baseArray;
        private int start;
        private int stop;

        public T this[int index]
        {
            get
            {
                CheckIndex( index );
                return baseArray[index + start];
            }
            set
            {
                CheckIndex( index );
                baseArray[index + start] = value;
            }
        }

        public int Count
        {
            get { return stop - start + 1; }
        }

        public SubArray( SubArray<T> baseArray, int start, int stop )
            : this( baseArray.baseArray, start + baseArray.start, stop + baseArray.start )
        {
        }

        public SubArray( IList<T> baseArray, int start, int stop )
        {
            if( stop < start )
            {
                throw new ArgumentException();
            }
            if( stop > baseArray.Count - 1 )
            {
                throw new ArgumentException();
            }
            if( start > baseArray.Count - 1 )
            {
                throw new ArgumentException();
            }

            this.start = start;
            this.stop = stop;
            this.baseArray = baseArray;
        }

        public SubArray( IList<T> baseArray, int start )
            : this( baseArray, start, baseArray.Count - 1 )
        {
        }

        public SubArray( IList<T> baseArray )
            : this( baseArray, 0 )
        {
        }

        private void CheckIndex( int index )
        {
            if( index > (stop - start) )
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public T[] ToArray()
        {
            T[] result = new T[this.Count];
            for( int i = 0; i < this.Count; i++ )
            {
                result[i] = this[i];
            }
            return result;
        }


        #region ICollection<T> Members

        public void Add( T item )
        {
        }

        public void Clear()
        {
        }

        public bool Contains( T item )
        {
            int index = baseArray.IndexOf( item );
            return ((index != -1) && (index >= start) && (index <= stop));
        }

        public void CopyTo( T[] array, int arrayIndex )
        {
            for( int i = 0; i < Count; i++ )
            {
                array[i + arrayIndex] = baseArray[start + i];
            }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove( T item )
        {
            return false;
        }

        #endregion

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return new SubArrayEnumerator<T>( this );
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new SubArrayEnumerator<T>( this );
        }

        #endregion

        #region IList<T> Members

        public int IndexOf( T item )
        {
            throw new NotImplementedException();
        }

        public void Insert( int index, T item )
        {
            throw new NotImplementedException();
        }

        public void RemoveAt( int index )
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class SubArrayEnumerator<T> : IEnumerator<T>
    {
        SubArray<T> array;

        public SubArrayEnumerator( SubArray<T> array )
        {
            this.array = array;
        }

        #region IEnumerator<T> Members

        int currentIndex = -1;
        public T Current
        {
            get
            {
                if( (currentIndex != -1) && (currentIndex < array.Count) )
                {
                    return array[currentIndex];
                }

                return default( T );
            }
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
            get
            {
                if( (currentIndex != -1) && (currentIndex < array.Count) )
                {
                    return array[currentIndex];
                }

                return null;
            }
        }

        public bool MoveNext()
        {
            currentIndex++;
            if( currentIndex >= array.Count )
            {
                currentIndex = array.Count;
                return false;
            }

            return true;
        }

        public void Reset()
        {
            currentIndex = 0;
        }

        #endregion
    }
}