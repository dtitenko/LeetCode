using System;
using System.Collections.Generic;
using LeetCode;
using Newtonsoft.Json;

public class MaximumFrequencyStack : ISolution<string, string>
{
    public string Name => "895. Maximum Frequency Stack";
    public string Link => "https://leetcode.com/problems/maximum-frequency-stack/";

    public (string, string)[] TestCases => new[]
    {
        ("[\"FreqStack\",\"push\",\"push\",\"push\",\"push\",\"push\",\"push\",\"pop\",\"pop\",\"pop\",\"pop\"], [[],[5],[7],[5],[7],[4],[5],[],[],[],[]]",
            "[null,null,null,null,null,null,null,5,7,5,4]")
    };

    public string Execute(string input)
    {
        var operationsStr = input.Substring(0, input.IndexOf("],") + 1);
        var valuesStr = input.Substring(input.IndexOf("],") + 2);
        var operations = JsonConvert.DeserializeObject<string[]>(operationsStr);
        var values = JsonConvert.DeserializeObject<int[][]>(valuesStr);
        var results = new int?[operations.Length];
        var stack = new FreqStack();
        for (int i = 0; i < operations.Length; i++)
        {
            var operation = operations[i];
            if (operation == "FreqStack")
            {
                results[i] = null;
                continue;
            }

            if (operation == "push")
            {
                stack.Push(values[i][0]);
                results[i] = null;
                continue;
            }

            if (operation == "pop")
            {
                var result = stack.Pop();
                results[i] = result;
                Console.WriteLine(result);
            }
        }

        return JsonConvert.SerializeObject(results);
    }
}

public class FreqStack
{
    private readonly Dictionary<int, int> _freq = new Dictionary<int, int>();
    private readonly Dictionary<int, Stack<int>> _groups = new Dictionary<int, Stack<int>>();
    private int _maxFreq;

    public void Push(int x)
    {
        _freq.TryGetValue(x, out var freq);
        freq += 1;
        _freq[x] = freq;
        _maxFreq = Math.Max(_maxFreq, freq);

        if (!_groups.TryGetValue(freq, out var group))
        {
            group = new Stack<int>();
            _groups[freq] = group;
        }

        group.Push(x);
    }

    public int Pop()
    {
        var group = _groups[_maxFreq];
        var x = group.Pop();
        if (group.Count == 0)
        {
            _maxFreq -= 1;
        }

        _freq[x] -= 1;
        return x;
    }
}