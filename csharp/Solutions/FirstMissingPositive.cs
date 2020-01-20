using System;
using LeetCode;
using Newtonsoft.Json;

class FindFirstMissingPositive : ISolution<string, int>
{
    public string Name => "41. First Missing Positive";

    public string Link => "https://leetcode.com/problems/first-missing-positive/";

    public (string, int)[] TestCases =>
         new[]
        {
            ("[1,2,0]", 3),
            ("[3,4,-1,1]", 2),
            ("[7,8,9,11,12]", 1),
            ("[1,2,3,10,2147483647,9]", 4)
        };

    public int Execute(string input)
    {
        int[] inputNums = JsonConvert.DeserializeObject<int[]>(input);
        return FirstMissingPositive(inputNums);
    }

    public int FirstMissingPositive(int[] nums)
    {
        var min = int.MaxValue;
        var max = 0;
        var positive = 0;
        foreach (var num in nums)
        {
            if (num < 1) continue;
            min = Math.Min(min, num);
            max = Math.Max(max, num);
            positive++;
        }

        max = Math.Min(max, min + positive);
        Console.WriteLine(max);

        if (min > 1)
        {
            return 1;
        }

        var found = new bool[max + 1];
        foreach (var num in nums)
        {
            if (num < 1 || num > max) continue;
            if (found[num - 1]) continue;
            found[num - 1] = true;
        }

        for (var i = 0; i < found.Length; i++)
        {
            if (!found[i])
            {
                return i + 1;
            }
        }

        return 1;
    }

}
