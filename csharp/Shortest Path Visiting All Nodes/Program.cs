using System;
using System.Linq;
using Microsoft.VisualBasic;
using Newtonsoft.Json;

class Program
{
    static void Main(string[] args)
    {
        var input = JsonConvert.DeserializeObject<int[][]>("[[1,2,3],[0],[0],[0]]");
        // var input = JsonConvert.DeserializeObject<int[][]>("[[1],[0,2,4],[1,3,4],[2],[1,2]]");
        var graph = new Graph(input.Length);
        for (var i = 0; i < input.Length; i++)
        {
            foreach (var vertex in input[i])
            {
                if (!graph.AdjacentVertices(i).Contains(vertex))
                    graph.AddEdge(i, vertex);
            }
        }

        var dfp = new DepthFirstPathsAllNodes(graph, 1);
        var path = dfp.Dfs(graph, 0, -1);
        Console.WriteLine(string.Join(" -> ", path));
    }
}