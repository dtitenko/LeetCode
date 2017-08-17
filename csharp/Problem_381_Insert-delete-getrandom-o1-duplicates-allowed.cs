using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCode
{
    public class Problem_381_Insert_delete_getrandom_o1_duplicates_allowed
    {
        public void Run()
        {
            RandomizedCollection collection = new RandomizedCollection();
            collection.Insert(10);
            collection.Insert(10);
            collection.Insert(20);
            collection.Insert(20);
            collection.Insert(30);
            collection.Insert(30);
            collection.Remove(10);
            collection.Remove(10);
            collection.Remove(30);
            collection.Remove(30);
        }
    }

    public class RandomizedCollection
    {
        private List<int> _values = new List<int>();
        private Dictionary<int, List<int>> _index = new Dictionary<int, List<int>>();
        private Random _random = new Random();

        public bool Insert(int val)
        {
            _values.Add(val);
            return PushIndex(val, _values.Count - 1);
        }

        public bool Remove(int val)
        {
            if (!IsIndexed(val))
            {
                return false;
            }

            var index = PopIndex(val);

            // move value to the end of array
            if (index < _values.Count - 1)
            {
                var lastValue = _values.Last();
                PopIndex(lastValue);
                _values[index] = lastValue;
                PushIndex(lastValue, index);
            }

            // always remove value from last position
            _values.RemoveAt(_values.Count - 1);

            return true;
        }

        private bool PushIndex(int val, int index)
        {
            if (!_index.ContainsKey(val))
            {
                _index.Add(val, new List<int> { index });
                return true;
            }
            else if (_index[val].Last() > index)
            {
                var newLastIndex = _index[val].Last();
                _index[val][_index[val].Count - 1] = index;
                _index[val].Add(newLastIndex);
                return false;
            }
            else
            {
                _index[val].Add(index);
                return false;
            }
        }

        private int PopIndex(int val)
        {
            var index = _index[val].Last();
            _index[val].RemoveAt(_index[val].Count - 1);
            if (_index[val].Count == 0)
            {
                _index.Remove(val);
            }
            return index;
        }

        private bool IsIndexed(int val)
        {
            return _index.ContainsKey(val);
        }

        public int GetRandom()
        {
            var index = _random.Next(_values.Count);
            return _values[index];
        }
    }
}
