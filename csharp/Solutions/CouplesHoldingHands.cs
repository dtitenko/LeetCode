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
    };

    public int Execute(string input)
    {
        return MinSwapsCouples(JsonConvert.DeserializeObject<int[]>(input));
    }

    public int MinSwapsCouples(int[] row)
    {
        int swap = 0;
        for (var i = 0; i < row.Length; i += 2)
        {
            var el1 = row[i];
            var el2 = el1 % 2 == 0 ? el1 + 1 : el1 - 1;
            Console.WriteLine($"{row[i]}:{row[i + 1]}");
            if (row[i + 1] == el2) continue;
            Console.WriteLine($"el2: {el2}");
            for (int j = i + 1; j < row.Length; j++)
            {
                if (row[j] == el2)
                {
                    var temp = row[i + 1];
                    row[i + 1] = row[j];
                    row[j] = temp;
                    swap += 1;
                    Console.WriteLine(JsonConvert.SerializeObject(row));
                    break;
                }
            }
        }

        return swap;
    }
}