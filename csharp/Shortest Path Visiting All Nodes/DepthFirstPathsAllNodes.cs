using System;
using System.Collections.Generic;
using System.Linq;

public class DepthFirstPathsAllNodes
{
    private readonly int _s;

    public DepthFirstPathsAllNodes(Graph G, int s)
    {
        _s = s;
        Dfs(G, s, -1);
    }

    public List<int> Path { get; } = new List<int>();

    private void Dfs(Graph graph, int v, int from)
    {
        Path.Add(v);
        var adjacent = graph.AdjacentVertices(v);
        for (int i = 0; i < adjacent.Count; i++)
        {
            var w = adjacent[i];
            if (w != from)
            {
                Console.WriteLine($"{w} from {v}");
                Dfs(graph, w, v);
                if (i + 1 < adjacent.Count)
                    Path.Add(v);
            }
        }
    }

    public override string ToString()
    {
        return string.Join(" -> ", Path);
    }
}