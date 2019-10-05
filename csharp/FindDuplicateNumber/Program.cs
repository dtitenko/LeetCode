using System;
using Newtonsoft.Json;

class Program
{
    static void Main(string[] args)
    {
        var solution = new Solution();
        Console.WriteLine(solution.FindDuplicate(JsonConvert.DeserializeObject<int[]>("[1,3,4,2,2]")));
        Console.WriteLine(solution.FindDuplicate(JsonConvert.DeserializeObject<int[]>("[3,1,3,4,2]")));
        Console.WriteLine(solution.FindDuplicate(JsonConvert.DeserializeObject<int[]>("[1,1,2]")));
    }
}

public class Solution
{
    public int FindDuplicate(int[] nums)
    {
        var found = new bool[nums.Length - 1];
        foreach (var num in nums)
        {
            if (found[num - 1])
            {
                return num;
            }

            found[num - 1] = true;
        }

        return -1;
    }
}