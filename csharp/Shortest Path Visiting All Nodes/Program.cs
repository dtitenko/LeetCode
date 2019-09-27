using System;
using System.Linq;
using Microsoft.VisualBasic;
using Newtonsoft.Json;

class Program
{
    static void Main(string[] args)
    {
        var input1 = JsonConvert.DeserializeObject<int[][]>("[[1,2,3],[0],[0],[0]]");
        Solution(input1);
        var input2 = JsonConvert.DeserializeObject<int[][]>("[[1],[0,2,4,5],[1,3,4],[2],[1,2],[1]]");
        Solution(input2);
    }

    static void Solution(int[][] input)
    {
        var graph = new Graph(input);

        var dfp = new DepthFirstPathsAllNodes(graph);
        var path = dfp.Dfs(graph);
        Console.WriteLine(string.Join(" -> ", path));
    }
}