using System;
using System.Collections.Generic;
using Newtonsoft.Json;

class Program
{
    public static void Main()
    {
        var cases = new Dictionary<string, int>
        {
            {"[1,2,0]", 3},
            {"[3,4,-1,1]", 2},
            {"[7,8,9,11,12]", 1},
            {"[1,2,3,10,2147483647,9]", 4},
        };
        var solution = new Solution();
        foreach (var (input, expected) in cases)
        {
            var nums = JsonConvert.DeserializeObject<int[]>(input);
            var output = solution.FirstMissingPositive(nums);
            var color = Console.ForegroundColor;
            Console.ForegroundColor = expected == output ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine($"{input} expected: {expected}, result {output}");
            Console.ForegroundColor = color;
        }
    }
}

public class Solution
{
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