using System;
using LeetCode;
using Newtonsoft.Json;

public class CouplesHoldingHands : ISolution<string, int>
{
    public string Name => "765. Couples Holding Hands";
    public string Link => "https://leetcode.com/problems/couples-holding-hands/";

    public (string, int)[] TestCases => new[]
    {
        ("[0, 2, 1, 3]", 1),
        ("[3, 2, 0, 1]", 0),
        ("[3, 1, 2, 6, 4, 5, 0, 7]", 2),
        ("[6, 1, 2, 3, 8, 5, 4, 7, 0, 9]", 3),
        ("[6,2,1,7,4,5,3,8,0,9]", 3),
        ("[1,4,0,5,8,7,6,3,2,9]", 3)
    };

    public int Execute(string input)
    {
        return MinSwapsCouples(JsonConvert.DeserializeObject<int[]>(input));
    }

    public int MinSwapsCouples(int[] row)
    {
        int swap = 0;
        var places = new int[row.Length];
        for (var i = 0; i < row.Length; i++)
        {
            places[row[i]] = i;
        }

        for (var i = 0; i < row.Length; i += 2)
        {
            var el1 = row[i];
            var el2 = el1 % 2 == 0 ? el1 + 1 : el1 - 1;
            if (row[i + 1] == el2) continue;

            row[places[el2]] = row[i + 1];
            row[i + 1] = el2;

            var temp = row[places[el2]];
            var tempIndex = places[el2];
            places[el2] = i + 1;
            places[temp] = tempIndex;

            swap += 1;
        }

        return swap;
    }
}