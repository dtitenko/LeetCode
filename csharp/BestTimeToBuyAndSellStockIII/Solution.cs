using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Xunit;

class Program
{
    public static void Main()
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
        var len = prices.Length;
        if (len <= 1) return 0;

        var sellingProfit = new int[len];

        var sell = prices[len - 1];
        for (var i = len - 2; i >= 0; i--)
        {
            sell = Math.Max(sell, prices[i]);
            sellingProfit[i] = Math.Max(sellingProfit[i + 1], sell - prices[i]);
        }

        var buy = prices[0];
        var maxProfit = 0;
        for (var i = 1; i < len; i++)
        {
            buy = Math.Min(prices[i], buy);
            maxProfit = Math.Max(maxProfit, prices[i] - buy + sellingProfit[i]);
        }

        return maxProfit;
    }
}

public class Tests
{
    [Theory]
    [InlineData("[3,3,5,0,0,3,1,4]", 6)]
    [InlineData("[1,2,3,4,5]", 4)]
    [InlineData("[7,6,4,3,1]", 0)]
    public void ShouldSucceed(string @case, int expected)
    {
        var solution = new Solution();
        var prices = JsonConvert.DeserializeObject<int[]>(@case);

        var output = solution.MaxProfit(prices);

        Assert.Equal(expected, output);
    }
}