using System;
using System.Collections.Generic;
using System.Linq;
using LeetCode;
using Newtonsoft.Json;

public class FindMedianFromDataStream : ISolution<string, string>
{
    public string Name => "295. Find Median from Data Stream";
    public string Link => "https://leetcode.com/problems/find-median-from-data-stream/";

    public (string, string)[] TestCases => new[]
    {
        ("[[\"MedianFinder\",\"addNum\",\"addNum\",\"findMedian\",\"addNum\",\"findMedian\"], [null,1,2,null,3,null]]",
            "[null,null,null,1.5,null,2.0]")
    };

    public string Execute(string input)
    {
        var solution = new MedianFinder();
        var data = JsonConvert.DeserializeObject<object[][]>(input);
        var operations = data[0].OfType<string>().ToArray();
        var values = data[1];
        var results = new List<double?>();
        for (var i = 0; i < operations.Length; i++)
        {
            switch (operations[i])
            {
                case "addNum":
                    solution.AddNum(Convert.ToInt32(values[i]));
                    results.Add(null);
                    break;
                case "findMedian":
                    results.Add(solution.FindMedian());
                    break;
                default:
                    results.Add(null);
                    break;
            }
        }

        return JsonConvert.SerializeObject(results);
    }
}

public class MedianFinder
{
    private readonly SortedList<int, int> _minHeap = new SortedList<int, int>();
    private readonly SortedList<int, int> _maxHeap = new SortedList<int, int>();
    private int _minCount = 0;
    private int _maxCount = 0;

    /** initialize your data structure here. */
    public MedianFinder()
    {
    }

    public void AddNum(int num)
    {
        Add(_maxHeap, num, ref _maxCount);
        while (_maxCount > _minCount)
        {
            var min = _maxHeap.Keys[0];
            Add(_minHeap, min, ref _minCount);
            Remove(_maxHeap, min, ref _maxCount);
        }

        if (_minCount > 0 && _maxCount > 0)
        {
            while (_maxHeap.Keys[0] < _minHeap.Keys[_minHeap.Count - 1])
            {
                var min = _maxHeap.Keys[0];
                var max = _minHeap.Keys[_minHeap.Count - 1];
                Remove(_maxHeap, min, ref _maxCount);
                Remove(_minHeap, max, ref _minCount);
                Add(_maxHeap, max, ref _maxCount);
                Add(_minHeap, min, ref _minCount);
            }
        }
    }

    private void Add(SortedList<int, int> heap, int key, ref int count)
    {
        int value;
        if (!heap.TryGetValue(key, out value))
        {
            value = 1;
            heap.Add(key, value);
        }
        else
        {
            value++;
            heap[key] = value;
        }

        count++;
    }

    private void Remove(SortedList<int, int> heap, int key, ref int count)
    {
        int value;
        if (heap.TryGetValue(key, out value))
        {
            if (value > 1)
            {
                value--;
                heap[key] = value;
            }
            else
            {
                heap.Remove(key);
            }
        }

        count--;
    }

    public double FindMedian()
    {
        if (_minCount > _maxCount)
        {
            return _minHeap.Keys[_minHeap.Count - 1];
        }

        return (_minHeap.Keys[_minHeap.Count - 1] + _maxHeap.Keys[0]) / 2.0;
    }
}