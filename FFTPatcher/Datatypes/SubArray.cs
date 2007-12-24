using System;
using System.Collections.Generic;
using System.Text;

namespace FFTPatcher
{
    public class SubArray<T> : ICollection<T>
    {
        private IList<T> baseArray;
        private int start;
        private int stop;

        public T this[int index]
        {
            get
            {
                CheckIndex(index);
                return baseArray[index + start];
            }
            set
            {
                CheckIndex(index);
                baseArray[index + start] = value;
            }
        }

        public int Count
        {
            get { return stop - start + 1; }
        }

        public SubArray(SubArray<T> baseArray, int start, int stop)
            : this(baseArray.baseArray, start + baseArray.start, stop + baseArray.start)
        {
        }

        public SubArray(IList<T> baseArray, int start, int stop)
        {
            if (stop < start)
            {
                throw new ArgumentException();
            }
            if (stop > baseArray.Count - 1)
            {
                throw new ArgumentException();
            }
            if (start > baseArray.Count - 1)
            {
                throw new ArgumentException();
            }

            this.start = start;
            this.stop = stop;
            this.baseArray = baseArray;
        }

        public SubArray(IList<T> baseArray, int start)
            : this(baseArray, start, baseArray.Count - 1)
        {
        }

        private void CheckIndex(int index)
        {
            if (index > (stop - start))
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        #region ICollection<T> Members

        public void Add(T item)
        {
        }

        public void Clear()
        {
        }

        public bool Contains(T item)
        {
            int index = baseArray.IndexOf(item);
            return ((index != -1) && (index >= start) && (index <= stop));
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            for (int i = 0; i < Count; i++)
            {
                array[i + arrayIndex] = baseArray[start + i];
            }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            return false;
        }

        #endregion

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return new SubArrayEnumerator<T>(this);
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new SubArrayEnumerator<T>(this);
        }

        #endregion
    }

    public class SubArrayEnumerator<T> : IEnumerator<T>
    {
        SubArray<T> array;

        public SubArrayEnumerator(SubArray<T> array)
        {
            this.array = array;
        }

        #region IEnumerator<T> Members

        int currentIndex = -1;
        public T Current
        {
            get
            {
                if ((currentIndex != -1) && (currentIndex < array.Count))
                {
                    return array[currentIndex];
                }

                return default(T);
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
                if ((currentIndex != -1) && (currentIndex < array.Count))
                {
                    return array[currentIndex];
                }

                return null;
            }
        }

        public bool MoveNext()
        {
            currentIndex++;
            if (currentIndex >= array.Count)
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