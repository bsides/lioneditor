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

namespace FFTPatcher
{
    using FFTPatcher.Datatypes;

    public static partial class ExtensionMethods
    {

		#region Methods (4) 


        public static int IndexOf<T>( this IList<T> list, IList<T> find ) where T : IEquatable<T>
        {
            if( find.Count > list.Count )
            {
                return -1;
            }

            for( int i = 0; i + find.Count < list.Count; i++ )
            {
                if( list.Sub( i, i + find.Count - 1 ).Equals( find ) )
                {
                    return i;
                }
            }

            return -1;
        }

        public static IList<IList<T>> Split<T>( this IList<T> members, T value ) where T : IEquatable<T>
        {
            List<IList<T>> result = new List<IList<T>>();

            int start = 0;
            int stop = 0;

            for( int i = 0; i < members.Count; i++ )
            {
                if( members[i].Equals( value ) )
                {
                    stop = i;
                    result.Add( members.Sub( start, stop ) );
                    start = i + 1;
                }
            }

            if( !members[members.Count - 1].Equals( value ) )
            {
                result.Add( members.Sub( start, members.Count - 1 ) );
            }

            return result;
        }

        public static IList<T> Sub<T>( this IList<T> list, int start )
        {
            return new SubArray<T>( list, start );
        }

        public static IList<T> Sub<T>( this IList<T> list, int start, int stop )
        {
            return new SubArray<T>( list, start, stop );
        }


		#endregion Methods 

    }
}

namespace FFTPatcher.Datatypes
{
    public class SubArray<T> : ICollection<T>, IList<T>, IDisposable
    {

		#region Fields (3) 

        private IList<T> baseArray;
        private int start;
        private int stop;

		#endregion Fields 

		#region Properties (3) 


        public int Count
        {
            get { return stop - start + 1; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

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


		#endregion Properties 

		#region Constructors (4) 

        public SubArray( IList<T> baseArray )
            : this( baseArray, 0 )
        {
        }

        public SubArray( IList<T> baseArray, int start )
            : this( baseArray, start, baseArray.Count - 1 )
        {
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

		#endregion Constructors 

		#region Methods (17) 


        private void CheckIndex( int index )
        {
            if( index > (stop - start) )
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new SubArrayEnumerator<T>( this );
        }

        public static bool operator !=( SubArray<T> left, SubArray<T> right )
        {
            return !(left == right);
        }

        public static bool operator ==( SubArray<T> left, SubArray<T> right )
        {
            return
                (object.ReferenceEquals( left, null ) && object.ReferenceEquals( right, null )) ||
                (!object.ReferenceEquals( left, null ) && !object.ReferenceEquals( right, null ) && left.Equals( right ));
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

        public void Dispose()
        {
            baseArray = null;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new SubArrayEnumerator<T>( this );
        }

        public int IndexOf( T item )
        {
            for( int i = 0; i < Count; i++ )
            {
                if( this[i].Equals( item ) )
                {
                    return i;
                }
            }

            return -1;
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



        public override bool Equals( object obj )
        {
            IList<T> other = obj as IList<T>;

            if( (other == null) || (other.Count != Count) || !(this[0] is IEquatable<T>) )
            {
                return false;
            }

            for( int i = 0; i < Count; i++ )
            {
                IEquatable<T> mine = this[i] as IEquatable<T>;
                IEquatable<T> theirs = other[i] as IEquatable<T>;
                if( !mine.Equals( theirs ) )
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        void ICollection<T>.Add( T item )
        {
        }

        void ICollection<T>.Clear()
        {
        }

        void IList<T>.Insert( int index, T item )
        {
        }

        bool ICollection<T>.Remove( T item )
        {
            return false;
        }

        void IList<T>.RemoveAt( int index )
        {
            throw new NotImplementedException();
        }

		#endregion Methods 

    }

    public class SubArrayEnumerator<T> : IEnumerator<T>
    {

		#region Fields (2) 

        private SubArray<T> array;
        private int currentIndex = -1;

		#endregion Fields 

		#region Properties (2) 


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


		#endregion Properties 

		#region Constructors (1) 

        public SubArrayEnumerator( SubArray<T> array )
        {
            this.array = array;
        }

		#endregion Constructors 

		#region Methods (3) 


        public void Dispose()
        {
            array = null;
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


		#endregion Methods 

    }
}