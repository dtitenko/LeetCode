using System;
using System.Collections.Generic;

public class DepthFirstPaths
{
    private readonly bool[] _marked;
    private readonly int[] _edgeTo;
    private readonly int _s;

    public DepthFirstPaths(Graph G, int s)
    {
        _s = s;
        _edgeTo = new int[G.Vertices()];
        _marked = new bool[G.Vertices()];
        ValidateVertex(s);
        Dfs(G, s);
    }

    private void Dfs(Graph graph, int v)
    {
        _marked[v] = true;
        foreach (int w in graph.AdjacentVertices(v))
        {
            if (!_marked[w])
            {
                _edgeTo[w] = v;
                Dfs(graph, w);
            }
        }
    }

    public bool HasPathTo(int v)
    {
        ValidateVertex(v);
        return _marked[v];
    }

    public IEnumerable<int> PathTo(int v)
    {
        ValidateVertex(v);
        if (!HasPathTo(v)) return null;
        Stack<int> path = new Stack<int>();
        for (int x = v; x != _s; x = _edgeTo[x])
            path.Push(x);
        path.Push(_s);
        return path;
    }

    private void ValidateVertex(int v)
    {
        var vertices = _marked.Length;
        if (v < 0 || v >= vertices)
            throw new InvalidOperationException("vertex " + v + " is not between 0 and " + (vertices - 1));
    }
}