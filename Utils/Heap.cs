using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class Heap<T> where T : IComparable<T>
    {
        public Heap()
        {
            items = new List<T>();
        }
        private List<T> items;

        public void Add(T item)
        {
            items.Add(item);
            int idx = items.Count - 1;
            int compareIdx;
            while (true)
            {
                if (idx == 0) { return; }
                compareIdx = idx % 2 == 0 ? idx / 2 - 1 : idx / 2;
                if (items[compareIdx].CompareTo(items[idx]) <= 0)
                {
                    return;
                }
                Swap(idx, compareIdx);
                idx = compareIdx;
            }
        }

        public T Pop()
        {
            T item = items[0];

            items[0] = items[items.Count - 1];
            items.RemoveAt(items.Count - 1);
            int idx = 0;
            int compareIdx;
            while (true)
            {
                compareIdx = idx * 2 + 2;
                if (compareIdx - 1 >= items.Count)
                {
                    return item;
                }
                if (compareIdx >= items.Count)
                {
                    if (items[compareIdx - 1].CompareTo(items[idx]) < 0)
                    {
                        Swap(idx, compareIdx - 1);
                    }
                    return item;
                }
                compareIdx += items[compareIdx].CompareTo(items[compareIdx - 1]) < 0 ? 0 : -1;
                if (items[idx].CompareTo(items[compareIdx]) <= 0)
                {
                    return item;
                }
                Swap(idx, compareIdx);
                idx = compareIdx;
            }
        }

        public int Count()
        {
            return items.Count;
        }

        private void Swap(int idx1, int idx2)
        {
            T temp = items[idx1];
            items[idx1] = items[idx2];
            items[idx2] = temp;
        }

    }
}
