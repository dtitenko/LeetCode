using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic;
using Newtonsoft.Json;

class Program
{
    static void Main(string[] args)
    {
        var input1 = JsonConvert.DeserializeObject<List<int>[]>("[[1,2,3],[0],[0],[0]]");
        RemoveCycles(input1);
        var input2 = JsonConvert.DeserializeObject<List<int>[]>("[[1],[0,2,4,5],[1,3,4],[2],[1,2],[1]]");
        RemoveCycles(input2);
        var input3 = JsonConvert.DeserializeObject<List<int>[]>("[[2,3],[7],[0,6],[0,4,7],[3,8],[7],[2],[5,3,1],[4]]");
        RemoveCycles(input3);
        var input4 = JsonConvert.DeserializeObject<List<int>[]>("[[1],[0,2,4],[1,3],[2],[1,5],[4]]");
        RemoveCycles(input4);
        var input5 =
            JsonConvert.DeserializeObject<List<int>[]>(
                "[[1,4],[0,3,10],[3],[1,2,6,7],[0,5],[4],[3],[3],[10],[10],[1,9,8]]");
        RemoveCycles(input5);
        var input6 = JsonConvert.DeserializeObject<List<int>[]>(
            "[[1,2,3,4,5,6,7,8,9,10,11],[0,2,3,4,5,6,7,8,9,10,11],[0,1,3,4,5,6,7,8,9,10,11],[0,1,2,4,5,6,7,8,9,10,11],[0,1,2,3,5,6,7,8,9,10,11],[0,1,2,3,4,6,7,8,9,10,11],[0,1,2,3,4,5,7,8,9,10,11],[0,1,2,3,4,5,6,8,9,10,11],[0,1,2,3,4,5,6,7,9,10,11],[0,1,2,3,4,5,6,7,8,10,11],[0,1,2,3,4,5,6,7,8,9,11],[0,1,2,3,4,5,6,7,8,9,10]]");
        RemoveCycles(input6);
    }

    static void BFS(List<int>[] graph)
    {
        var queue = new Queue<int>();
        var visited = new bool[graph.Length];
        queue.Enqueue(0);
        while (queue.Count > 0)
        {
            var v = queue.Dequeue();
            if (visited[v]) continue;
            visited[v] = true;
            Console.WriteLine(v);
            var adjacentV = graph[v];
            foreach (var node in adjacentV)
            {
                queue.Enqueue(node);
            }
        }
    }

    private static void RemoveCycles(List<int>[] graph)
    {
        Console.WriteLine(JsonConvert.SerializeObject(graph));
        while (RemoveCycles(graph, 0, new bool[graph.Length], -1))
        {
        }

        Console.WriteLine(JsonConvert.SerializeObject(graph));
    }

    private static bool RemoveCycles(List<int>[] graph, int v, bool[] visited, int parent)
    {
        visited[v] = true;

        for (var index = graph[v].Count - 1; index >= 0; index--)
        {
            var i = graph[v][index];
            if (!visited[i])
            {
                if (RemoveCycles(graph, i, visited, v))
                    return true;
            }
            else if (i != parent)
            {
                graph[v].RemoveAt(index);
                graph[i].Remove(v);
                return true;
            }
        }

        return false;
    }
}