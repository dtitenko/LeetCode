using System;
using System.Collections.Generic;
using System.Linq;

// 0 - 1 - 2 - 3
//      \ /
//       4
public class DepthFirstPathsAllNodes
{
    private readonly bool[] _marked;
    private int _start;

    public DepthFirstPathsAllNodes(Graph graph)
    {
        _marked = new bool[graph.Vertices()];
        _start = 0;
        var min = int.MaxValue;
        for (int i = 0; i < graph.Vertices(); i++)
        {
            if (graph.AdjacentVertices(i).Count < min)
            {
                _start = i;
                min = graph.AdjacentVertices(i).Count;
            }
        }
    }

    public List<int> Dfs(Graph graph) =>
        Dfs(graph, _start, null);

    private List<int> Dfs(Graph graph, int v, int? from)
    {
        var path = new List<int>(new[] {v});
        _marked[v] = true;
        var children = new List<List<int>>();

        var adjacent = graph.AdjacentVertices(v);
        for (int i = 0; i < adjacent.Count; i++)
        {
            var w = adjacent[i];
            if (w == from || _marked[w]) continue;
            var childPath = Dfs(graph, w, v);
            var found = false;
            for (int j = 0; j < children.Count; j++)
            {
                var child = children[j];
                if (!childPath.Except(child).Any())
                {
                    found = true;
                    if (childPath.Count < child.Count)
                    {
                        children[j] = childPath;
                    }
                }
            }

            if (!found)
            {
                children.Add(childPath);
            }
        }

        foreach (var child in children.OrderBy(c => c.Count))
        {
            if (path.Count > 1)
            {
                path.Add(v);
            }
            path.AddRange(child);
        }

        _marked[v] = false;

        return path;
    }
}