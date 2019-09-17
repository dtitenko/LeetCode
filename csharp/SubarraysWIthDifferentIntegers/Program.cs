using System;
using System.Collections.Generic;

namespace SubarraysWIthDifferentIntegers
{
    class Program
    {
        static void Main(string[] args)
        {
            var solution = new Solution();
            // A = [1,2,1,2,3], K = 2
            // var A = new[] { 2, 1, 2, 1, 2 };
            var A = new[] {1, 2, 1, 2, 3};
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
            var map1 = new Dictionary<int, int>();
            var map2 = new Dictionary<int, int>();
            int left1 = 0;
            int left2 = 0;

            for (int right = 0; right < n; right++)
            {
                int val = A[right];
                map1[val] = map1.GetValueOrDefault(val, 0) + 1;
                map2[val] = map2.GetValueOrDefault(val, 0) + 1;

                while (map1.Count > K)
                {
                    int l1 = map1.GetValueOrDefault(A[left1], 0);
                    if (l1 - 1 <= 0)
                    {
                        map1.Remove(A[left1]);
                    }
                    else
                    {
                        map1[A[left1]] = l1 - 1;
                    }
                    left1++;
                }

                while (map2.Count >= K)
                {
                    int l2 = map2.GetValueOrDefault(A[left2], 0);
                    if (l2 - 1 <= 0)
                    {
                        map2.Remove(A[left2]);
                    }
                    else
                    {
                        map2[A[left2]] = l2 - 1;
                    }
                    left2++;
                }

                res += left2 - left1;
            }

            return res;
        }
    }
}