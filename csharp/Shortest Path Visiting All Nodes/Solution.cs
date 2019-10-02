using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

public class Solution
{
    public int ShortestPathLength(int[][] source)
    {
        var graph = source.Select(v => v.ToList()).ToArray();
        while (RemoveCycles(graph, 0, new bool[graph.Length], -1, new Stack<int>(), false))
        {
        }

        Console.WriteLine(JsonConvert.SerializeObject(graph));

        var threeNodes = graph.Sum(v => Math.Max(v.Count - 2, 0));
        var depthSum = DepthSum(graph);
        var sum = threeNodes + depthSum;

        graph = source.Select(v => v.ToList()).ToArray();
        while (RemoveCycles(graph, 0, new bool[graph.Length], -1, new Stack<int>(), true))
        {
        }

        Console.WriteLine(JsonConvert.SerializeObject(graph));

        threeNodes = graph.Sum(v => Math.Max(v.Count - 2, 0));
        depthSum = DepthSum(graph);
        sum = Math.Min(threeNodes + depthSum, sum);

        // edges + sum of the edges above 2 for every vertex
        return (graph.Length - 1) + sum;
    }

    private int DepthSum(List<int>[] graph)
    {
        var roots = new Dictionary<int, List<int>>();
        var min = int.MaxValue;
        var maxNodes = 0;
        var root = -1;
        for (var index = 0; index < graph.Length; index++)
        {
            var depths = new List<int>();
            roots.Add(index, depths);
            var v = graph[index];
            if (v.Count <= 2) continue;
            var max = 0;
            var depthCount = 0;
            Console.WriteLine($"index: {index}");
            foreach (var node in v)
            {
                var visited = new bool[graph.Length];
                visited[index] = true;
                var depth = Depth(graph, node, visited);
                if (depth > 1)
                {
                    depthCount++;
                    depths.Add(depth);
                    Console.WriteLine($"   depth: {depth}");
                }

                max = Math.Max(depth, max);
            }

            if (depthCount > maxNodes)
            {
                root = index;
                min = max;
                maxNodes = depthCount;
            }
        }

        var depthSum = 0;
        if (root != -1)
        {
            var rootNodes = root != -1 ? roots[root] : new List<int>();
            Console.WriteLine(root);
            Console.WriteLine(JsonConvert.SerializeObject(rootNodes));
            if (rootNodes.Count > 0)
                rootNodes.Remove(rootNodes.Max());
            if (rootNodes.Count > 0)
                rootNodes.Remove(rootNodes.Max());
            depthSum = rootNodes.Count > 0 ? rootNodes.Sum(n => Math.Max(n - 1, 0)) : 0;
        }

        return depthSum;
    }

    private int Depth(List<int>[] graph, int v, bool[] visited)
    {
        if (visited[v]) return 0;
        visited[v] = true;
        var max = 0;
        foreach (var node in graph[v])
        {
            var depth = Depth(graph, node, visited);
            max = Math.Max(max, depth);
        }

        return max + 1;
    }

    private bool RemoveCycles(List<int>[] graph, int v, bool[] visited, int parent, Stack<int> path, bool rightChild)
    {
        visited[v] = true;
        path.Push(v);

        for (var index = graph[v].Count - 1; index >= 0; index--)
        {
            var i = graph[v][index];
            if (!visited[i])
            {
                if (RemoveCycles(graph, i, visited, v, path, rightChild))
                    return true;
            }
            else if (i != parent)
            {
                RemoveCycle(graph, path, i, rightChild);
                return true;
            }
        }

        path.Pop();

        return false;
    }

    private void RemoveCycle(List<int>[] graph, Stack<int> path, int currentNode, bool rightChild)
    {
        // backtrack the cycle to find one with max adjacent vertices
        var node = currentNode;
        var maxNode = 0;
        var maxEdges = graph[node].Count;
        var cycle = new List<int> { currentNode };
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
        if (graph[cycle[leftNeighbor]].Count == graph[cycle[rightNeighbor]].Count)
        {
            maxNodeNeighbor = rightChild ? rightNeighbor : leftNeighbor;
        }

        var v1 = cycle[maxNode];
        var v2 = cycle[maxNodeNeighbor];
        graph[v1].Remove(v2);
        graph[v2].Remove(v1);
    }
}