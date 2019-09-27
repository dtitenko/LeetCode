using System;
using System.Collections.Generic;
using System.Linq;

// 0 - 1 - 2 - 3
//      \ /
//       4
public class DepthFirstPathsAllNodes
{
    private readonly int _s;
    private readonly bool[] _marked;

    public DepthFirstPathsAllNodes(Graph graph, int s)
    {
        _s = s;
        _marked = new bool[graph.Vertices()];
    }

    public List<int> Dfs(Graph graph, int v, int from)
    {
        var path = new List<int>(new[] { v });

        var adjacent = graph.AdjacentVertices(v);
        for (int i = 0; i < adjacent.Count; i++)
        {
            var w = adjacent[i];
            var childPath = Dfs(graph, w, v);
            if (i + 1 < adjacent.Count)
            {
                path.Add(v);
            }
        }

        return path;
    }
}