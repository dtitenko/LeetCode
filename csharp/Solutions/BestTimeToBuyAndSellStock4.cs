using System;
using System.Collections.Generic;
using System.Linq;
using LeetCode;
using Newtonsoft.Json;

public class BestTimeToBuyAndSellStock4 : ISolution<int[], int>
{
    public string Name => "188. Best Time to Buy and Sell Stock IV";
    public string Link => "https://leetcode.com/problems/best-time-to-buy-and-sell-stock-iv/";

    public (int[], int)[] TestCases
    {
        get
        {
            var cases = new Dictionary<string, int>
            {
                {"[3,3,5,0,0,3,1,4]", 6},
                {"[1,2,3,4,5]", 4},
                {"[7,6,4,3,1]", 0},
                {"[2,1,4,5,2,9,7]", 11},
                {"[1,2,4,2,5,7,2,4,9,0]", 13},
                {"[1,2]", 1},
            };
            return cases.Select(c => (JsonConvert.DeserializeObject<int[]>(c.Key), c.Value)).ToArray();
        }
    }

    public int Execute(int[] prices)
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