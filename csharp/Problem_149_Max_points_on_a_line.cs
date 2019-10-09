/// Given n points on a 2D plane, find the maximum number of points that lie on the same straight line.

using System;
using System.Collections.Generic;

namespace LeetCode
{
    public class Solution
    {
        public int MaxPoints(int[][] points)
        {
            if (points == null || points.Length == 0) return 0;

            Dictionary<decimal, int> result = new Dictionary<decimal, int>();
            int max = 0;

            for (int i = 0; i < points.Length; i++)
            {
                int duplicate = 1;
                int vertical = 0;
                for (int j = i + 1; j < points.Length; j++)
                {
                    // handle duplicates and vertical
                    if (points[i][0] == points[j][0])
                    {
                        if (points[i][1] == points[j][1])
                        {
                            duplicate++;
                        }
                        else
                        {
                            vertical++;
                        }
                    }
                    else
                    {
                        decimal slope = points[j][1] == points[i][1] ? 0.0M
                                : (1.0M * (points[j][1] - points[i][1]))
                                / (points[j][0] - points[i][0]);

                        if (result.TryGetValue(slope, out var count))
                        {
                            result[slope] = count + 1;
                        }
                        else
                        {
                            result.Add(slope, 1);
                        }
                    }
                }

                foreach (int count in result.Values)
                {
                    if (count + duplicate > max)
                    {
                        max = count + duplicate;
                    }
                }

                max = Math.Max(vertical + duplicate, max);
                result.Clear();
            }


            return max;
        }
    }
}

var solution = new Solution();
var points = new[] {
    new[] { 0,0 },
    new[] { 94911151,94911150 },
    new[] { 94911152,94911151 }
};
var res = solution.MaxPoints(points);
Console.WriteLine(res);
