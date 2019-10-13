using System;
using System.Collections.Generic;
using LeetCode;
using Newtonsoft.Json;

public class MaxPointsOnALine : ISolution<string, int>
{
    public string Name => "149. Max Points on a Line";
    public string Link => "https://leetcode.com/problems/max-points-on-a-line/";

    public (string, int)[] TestCases => new[]
    {
        ("[[0,0],[94911151,94911150],[94911152,94911151]]", 2)
    };

    public int Execute(string input)
    {
        var points = JsonConvert.DeserializeObject<int[][]>(input);
        return MaxPoints(points);
    }

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
                    decimal slope = points[j][1] == points[i][1]
                        ? 0.0M
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