using System;
using System.Collections.Generic;
using System.Linq;

namespace ShortestSubarray
{
    class Program
    {
        static void Main(string[] args)
        {
            var solution = new Solution();
            var res = solution.ShortestSubarray(new[] { 1, -1, 1, -1, 1, 2, 2, -1, 1, -1, 1, 4, -1, 1 }, 5);
            Console.WriteLine(res);
        }
    }

    public class Solution
    {
        public int ShortestSubarray(int[] A, int K)
        {
            var n = A.Length;
            var sum = new long[n + 1];
            for (var i = 0; i < n; i++)
            {
                sum[i + 1] = sum[i] + A[i];
                Console.Write($"{i + 1}:{sum[i] + A[i]}; ");
            }

            Console.WriteLine();

            var minSub = n + 1;
            var range = new LinkedList<int>();

            for (var y = 0; y < sum.Length; y++)
            {
                // extending range if sum is not growing
                while (range.Count > 0 && sum[y] <= sum[range.Last.Value])
                {
                    range.RemoveLast();
                }
                // move the start forward if current sum is GE the condition
                while (range.Count > 0 && sum[y] >= sum[range.First.Value] + K)
                {
                    minSub = Math.Min(minSub, y - range.First.Value);
                    range.RemoveFirst();
                }
                
                range.AddLast(y);
                Console.WriteLine(string.Join(",", range));
            }

            return minSub < n + 1 ? minSub : -1;
        }
    }
}