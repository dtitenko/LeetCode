using System;
using System.Collections.Generic;
using System.Text;

public class Graph
{
    private static readonly string Newline = Environment.NewLine;

    private readonly int _vertices;
    private int _edges;
    private readonly List<int>[] _adjacentVertices;

    public Graph(int vertices)
    {
        if (vertices < 0) throw new InvalidOperationException("Number of vertices must be nonnegative");
        _vertices = vertices;
        _edges = 0;
        _adjacentVertices = new List<int>[vertices];
        for (var v = 0; v < vertices; v++)
        {
            _adjacentVertices[v] = new List<int>();
        }
    }

    public int Vertices()
    {
        return _vertices;
    }

    public int Edges()
    {
        return _edges;
    }

    private void ValidateVertex(int v)
    {
        if (v < 0 || v >= _vertices)
            throw new InvalidOperationException("vertex " + v + " is not between 0 and " + (_vertices - 1));
    }

    public void AddEdge(int v, int w)
    {
        ValidateVertex(v);
        ValidateVertex(w);
        _edges++;
        _adjacentVertices[v].Add(w);
        _adjacentVertices[w].Add(v);
    }

    public IReadOnlyList<int> AdjacentVertices(int v)
    {
        ValidateVertex(v);
        return _adjacentVertices[v];
    }

    public int Degree(int v)
    {
        ValidateVertex(v);
        return _adjacentVertices[v].Count;
    }

    public override string ToString()
    {
        StringBuilder s = new StringBuilder();
        s.Append(_vertices + " vertices, " + _edges + " edges " + Newline);
        for (int v = 0; v < _vertices; v++)
        {
            s.Append(v + ": ");
            foreach (int w in _adjacentVertices[v])
            {
                s.Append(w + " ");
            }

            s.Append(Newline);
        }

        return s.ToString();
    }
}