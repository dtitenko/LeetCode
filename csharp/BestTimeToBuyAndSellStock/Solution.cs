﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;

class Program2
{
    public static void Main2()
    {
        var cases = new Dictionary<string, int>
        {
            { "[3,3,5,0,0,3,1,4]", 6 },
            { "[1,2,3,4,5]", 4 },
            { "[7,6,4,3,1]", 0 },
            { "[2,1,4,5,2,9,7]", 11 },
            { "[1,2,4,2,5,7,2,4,9,0]", 13 },
            { "[1,2]", 1 },
        };
        var solution = new Solution();
        foreach (var (input, expected) in cases)
        {
            var prices = JsonConvert.DeserializeObject<int[]>(input);
            var output = solution.MaxProfit(prices);
            var color = Console.ForegroundColor;
            Console.ForegroundColor = expected == output ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine($"{input} expected: {expected}, result {output}");
            Console.ForegroundColor = color;
        }
    }
}

public class Solution
{
    public int MaxProfit(int[] prices)
    {
        return MaxProfit(2, prices);
    }

    public int MaxProfit(int k, int[] prices)
    {
        int len = prices.Length;
        if (len <= 1) return 0;
        if (k >= len / 2) return QuickSolve(prices);

        var t = new int[k + 1][];
        for (var i = 0; i < t.Length; i++)
            t[i] = new int[len];

        for (var i = 1; i <= k; i++)
        {
            int max = -prices[0];
            for (var j = 1; j < len; j++)
            {
                t[i][j] = Math.Max(t[i][j - 1], prices[j] + max);
                max = Math.Max(max, t[i - 1][j - 1] - prices[j]);
            }
        }

        return t[k][len - 1];
    }

    private int QuickSolve(int[] prices)
    {
        int len = prices.Length, profit = 0;
        for (int i = 1; i < len; i++)
            // as long as there is a price gap, we gain a profit.
            if (prices[i] > prices[i - 1])
                profit += prices[i] - prices[i - 1];
        return profit;
    }
}