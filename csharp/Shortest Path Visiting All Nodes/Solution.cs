using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

public class Solution
{
    public int ShortestPathLength(int[][] source)
    {
        var graph = source.Select(v => v.ToList()).ToArray();
        while (RemoveCycles(graph, 0, new bool[graph.Length], -1, new Stack<int>()))
        {
        }

        // edges + sum of the edges above 2 for every vertex
        return (graph.Length - 1) + graph.Sum(v => Math.Max(v.Count - 2, 0));
    }

    private bool RemoveCycles(List<int>[] graph, int v, bool[] visited, int parent, Stack<int> path)
    {
        visited[v] = true;
        path.Push(v);

        for (var index = graph[v].Count - 1; index >= 0; index--)
        {
            var i = graph[v][index];
            if (!visited[i])
            {
                if (RemoveCycles(graph, i, visited, v, path))
                    return true;
            }
            else if (i != parent)
            {
                RemoveCycle(graph, path, i);
                return true;
            }
        }

        path.Pop();

        return false;
    }

    private void RemoveCycle(List<int>[] graph, Stack<int> path, int currentNode)
    {
        // backtrack the cycle to find one with max adjacent vertices
        var node = currentNode;
        var maxNode = 0;
        var maxEdges = graph[node].Count;
        var cycle = new List<int> {currentNode};
        while (path.Count > 0)
        {
            node = path.Pop();
            if (node == currentNode) break;
            cycle.Add(node);
            if (maxEdges < graph[node].Count)
            {
                maxEdges = graph[node].Count;
                maxNode = cycle.Count - 1;
            }
        }

        // finding the neighbor with most edges
        var leftNeighbor = maxNode > 0 ? maxNode - 1 : cycle.Count - 1;
        var rightNeighbor = maxNode + 1 < cycle.Count ? maxNode + 1 : 0;
        var maxNodeNeighbor = graph[cycle[leftNeighbor]].Count > graph[cycle[rightNeighbor]].Count
            ? leftNeighbor
            : rightNeighbor;

        var v1 = cycle[maxNode];
        var v2 = cycle[maxNodeNeighbor];
        graph[v1].Remove(v2);
        graph[v2].Remove(v1);
    }
}