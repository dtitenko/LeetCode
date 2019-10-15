using System;
using LeetCode;
using Newtonsoft.Json;

public class MaximalRectangleSolution : ISolution<string, int>
{
    public string Name => "85. Maximal Rectangle";
    public string Link => "https://leetcode.com/problems/maximal-rectangle/";

    public (string, int)[] TestCases => new[]
    {
        ("[[\"1\",\"0\",\"1\",\"0\",\"0\"],[\"1\",\"0\",\"1\",\"1\",\"1\"],[\"1\",\"1\",\"1\",\"1\",\"1\"],[\"1\",\"0\",\"0\",\"1\",\"0\"]]", 6),
        ("[[\"1\"]]", 1),
        ("[[\"1\",\"1\",\"1\",\"1\",\"1\",\"1\",\"1\",\"1\"],[\"1\",\"1\",\"1\",\"1\",\"1\",\"1\",\"1\",\"0\"],[\"1\",\"1\",\"1\",\"1\",\"1\",\"1\",\"1\",\"0\"],[\"1\",\"1\",\"1\",\"1\",\"1\",\"0\",\"0\",\"0\"],[\"0\",\"1\",\"1\",\"1\",\"1\",\"0\",\"0\",\"0\"]]", 21),
    };

    public int Execute(string input)
    {
        var matrix = JsonConvert.DeserializeObject<char[][]>(input);
        return MaximalRectangle(matrix);
    }
    
    public int MaximalRectangle(char[][] matrix)
    {
        var max = 0;
        // var res = new int[matrix.Length][];
        for (int i = 0; i < matrix.Length; i++)
        {
            // res[i] = new int[values[i].Length];
            for (int j = 0; j < matrix[i].Length; j++)
            {
                // res[i][j] = DFS(values, i, j);
                max = Math.Max(max, DFS(matrix, i, j));
            }
        }

        // Console.WriteLine(JsonConvert.SerializeObject(res));

        return max;
    }

    private int DFS(char[][] values, int i, int j)
    {
        if (values[i][j] != '1') return 0;

        var max = 0;
        var min = int.MaxValue;
        var columns = 0;
        for (int k = i; k < values.Length; k++)
        {
            var depth = 0;
            for (int l = j; l < values[i].Length; l++)
            {
                if (values[k][l] != '1') break;
                depth++;
            }
            if (depth == 0) break;
            columns++;

            min = Math.Min(min, depth);
            max = Math.Max(max, min * columns);
        }

        return max;
    }
}