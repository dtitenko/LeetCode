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
            if (path.Count < min)
            {
                minPath = path;
                min = path.Count;
            }
        }

        return minPath;
    }

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

        int[] toRevert = null;
        foreach (var child in children.OrderBy(c => c.Count))
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

            path.AddRange(child);
            if (child.Count > 1)
            {
                toRevert = new int[child.Count - 1];
                Array.Copy(child.ToArray(), toRevert, child.Count - 1);
            }
        }

        _marked[v] = false;

        return path;
    }
}