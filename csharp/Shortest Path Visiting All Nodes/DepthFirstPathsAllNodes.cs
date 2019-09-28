using System;
using System.Collections.Generic;
using System.Linq;

// 0 - 1 - 2 - 3
//      \ /
//       4
public class DepthFirstPathsAllNodes
{
    private readonly bool[] _marked;

    public DepthFirstPathsAllNodes(Graph graph)
    {
        _marked = new bool[graph.Vertices()];
    }

    public List<int> Dfs(Graph graph)
    {
        var min = int.MaxValue;
        List<int> minPath = null;
        for (int i = 0; i < graph.Vertices(); i++)
        {
            var path = Dfs(graph, i, null);
            if (path.Path.Count < min)
            {
                minPath = path.Path;
                min = path.Path.Count;
            }
        }

        return minPath;
    }

    private (List<int> Path, List<int> Return) Dfs(Graph graph, int v, int? from)
    {
        var path = new List<int>(new[] {v});
        var returnPath = new List<int>();
        _marked[v] = true;
        var children = new List<(List<int> Path, List<int> Return)>();

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
                if (!childPath.Path.Except(child.Path).Any())
                {
                    found = true;
                    if (!child.Path.Except(childPath.Path).Any() && childPath.Path.Count < child.Path.Count)
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

        List<int> toRevert = null;
        var childrenToMerge = children.OrderBy(c => c.Path.Count).ToArray();
        foreach (var child in childrenToMerge)
        {
            if (toRevert != null)
            {
                path.AddRange(toRevert);
                toRevert = null;
            }

            if (path.Count > 1)
            {
                path.Add(v);
            }

            path.AddRange(child.Path);
            if (child.Path.Count > 1)
            {
                toRevert = child.Return;
            }
        }

        if (childrenToMerge.Any())
        {
            returnPath.AddRange(childrenToMerge[^1].Return);
            returnPath.Add(v);
        }

        _marked[v] = false;

        return (path, returnPath);
    }
}