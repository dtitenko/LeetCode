using System;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        var input = "[[1],[0,2,4],[1,3,4],[2],[1,2]]"
            .Trim('[', ']').Split(',')
            .Select(s => s.Trim('[', ']').Split(',').Select(int.Parse).ToArray()).ToArray();
        var graph = new Graph(input.Length);
        Console.WriteLine(graph);
        return;
        for (var i = 0; i < input.Length; i++)
        {
            foreach (var vertex in input[i])
            {
                if (!graph.AdjacentVertices(i).Contains(vertex))
                    graph.AddEdge(i, vertex);
            }
        }

        var dfp = new DepthFirstPathsAllNodes(graph, 1);
        Console.WriteLine(dfp);
    }
}