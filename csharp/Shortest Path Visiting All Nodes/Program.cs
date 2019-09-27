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
        var input3 = JsonConvert.DeserializeObject<int[][]>("[[2,3],[7],[0,6],[0,4,7],[3,8],[7],[2],[5,3,1],[4]]");
        Solution(input3);
        var input4 = JsonConvert.DeserializeObject<int[][]>("[[1],[0,2,4],[1,3],[2],[1,5],[4]]");
        Solution(input4);
        var input5 = JsonConvert.DeserializeObject<int[][]>("[[1,4],[0,3,10],[3],[1,2,6,7],[0,5],[4],[3],[3],[10],[10],[1,9,8]]");
        Solution(input5);
    }

    static void Solution(int[][] input)
    {
        var graph = new Graph(input);

        var dfp = new DepthFirstPathsAllNodes(graph);
        var path = dfp.Dfs(graph);
        Console.WriteLine(string.Join(" -> ", path));
    }
}