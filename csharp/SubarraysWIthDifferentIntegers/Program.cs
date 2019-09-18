using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace
internal static class Program
{
    static void Main(string[] args)
    {
        var solution = new Solution();
        // A = [1,2,1,2,3], K = 2
        // var A = new[] { 2, 1, 2, 1, 2 };
        var A = new[] { 1, 2, 1, 2, 3 };
        var K = 2;
        var res = solution.SubarraysWithKDistinct(A, K);
        Console.WriteLine(res);
    }
}

public class Solution
{
    public int SubarraysWithKDistinct(int[] A, int K)
    {
        var n = A.Length;
        var res = 0;
        var unique = new Dictionary<int, int>();
        var rightWindow = new Dictionary<int, int>();
        int left1 = 0;
        int left2 = 0;

        for (int right = 0; right < n; right++)
        {
            int val = A[right];
            Console.WriteLine($"=== {val} ===");
            unique[val] = unique.GetValueOrDefault(val, 0) + 1;
            rightWindow[val] = rightWindow.GetValueOrDefault(val, 0) + 1;

            while (unique.Count > K)
            {
                var leftVal = A[left1];
                if (unique.TryGetValue(leftVal, out var leftCount))
                {
                    if (leftCount > 1) unique[leftVal] = leftCount - 1;
                    else unique.Remove(leftVal);
                }

                left1++;
            }

            while (rightWindow.Count >= K)
            {
                var leftVal = A[left2];
                if (rightWindow.TryGetValue(leftVal, out var leftCount))
                {
                    if (leftCount > 1) rightWindow[leftVal] = leftCount - 1;
                    else rightWindow.Remove(leftVal);
                }

                left2++;
            }

            Console.WriteLine("unique: " + string.Join(", ", unique.Select(kv => $"{kv.Key}:{kv.Value}")));
            Console.WriteLine("rightWindow: " + string.Join(", ", rightWindow.Select(kv => $"{kv.Key}:{kv.Value}")));
            Console.WriteLine($"{left2} - {left1} = {left2 - left1}");

            res += left2 - left1;
            Console.WriteLine($"res: {res}");

            Console.WriteLine();
        }

        return res;
    }
}
