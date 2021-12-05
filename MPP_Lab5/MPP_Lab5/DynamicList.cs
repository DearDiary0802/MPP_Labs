using System;
using System.Collections;
using System.Collections.Generic;

namespace MPP_Lab5
{
    public class DynamicList<T> : IEnumerable<T>
    {
        public int Count { get => arr.Length; }
        private T[] arr;
        public DynamicList()
        {
            arr = Array.Empty<T>();
        }
        public DynamicList(int count)
        {
            arr = new T[count];
        }
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                {
                    throw new IndexOutOfRangeException();
                }
                return arr[index];
            }
            set
            {
                if (index < 0 || index >= Count)
                {
                    throw new IndexOutOfRangeException();
                }
                arr[index] = value;
            }
        }

        public void Add(T item) 
        {
            T[] temp = new T[Count + 1];
            Array.Copy(arr, temp, Count);
            temp[Count] = item;
            arr = temp;
        }
        public void Remove(T item) 
        {
            while (Array.IndexOf(arr, item) != -1)
            {
                int index = Array.IndexOf(arr, item);
                RemoveAt(index);
            }
        }
        public void RemoveAt(int index) 
        {
            if (index > Count - 1)
                index = Count - 1;
            if (index < 0)
                index = 0;

            T[] temp = new T[Count - 1];
            for (int i = 0; i < index; i++)
            {
                temp[i] = arr[i];
            }
            for (int i = index + 1; i < Count; i++)
            {
                temp[i - 1] = arr[i];
            }
            arr = temp;
        }
        public void Clear() 
        {
            arr = Array.Empty<T>();
        }
        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable<T>)this).GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            int i = 0;
            while (i < Count)
            {
                yield return arr[i++];
            }
        }
    }
}
