﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic;
using Newtonsoft.Json;

class Program
{
    static void Main(string[] args)
    {
        var tc = new Dictionary<string, int>
        {
            {"[[1,2,3],[0],[0],[0]]", 4},
            {"[[1],[0,2,4,5],[1,3,4],[2],[1,2],[1]]", 6},
            {"[[2,3],[7],[0,6],[0,4,7],[3,8],[7],[2],[5,3,1],[4]]", 11},
            {"[[1],[0,2,4],[1,3],[2],[1,5],[4]]", 6},
            {"[[1,4],[0,3,10],[3],[1,2,6,7],[0,5],[4],[3],[3],[10],[10],[1,9,8]]", 15},
            {
                "[[1,2,3,4,5,6,7,8,9,10,11],[0,2,3,4,5,6,7,8,9,10,11],[0,1,3,4,5,6,7,8,9,10,11],[0,1,2,4,5,6,7,8,9,10,11],[0,1,2,3,5,6,7,8,9,10,11],[0,1,2,3,4,6,7,8,9,10,11],[0,1,2,3,4,5,7,8,9,10,11],[0,1,2,3,4,5,6,8,9,10,11],[0,1,2,3,4,5,6,7,9,10,11],[0,1,2,3,4,5,6,7,8,10,11],[0,1,2,3,4,5,6,7,8,9,11],[0,1,2,3,4,5,6,7,8,9,10]]",
                11
            },
            {"[[1,2,3,5],[0,5],[0,4],[0],[5,2],[0,4,1]]", 5}
        };
        var solution = new Solution();
        foreach (var @case in tc)
        {
            var graph = JsonConvert.DeserializeObject<int[][]>(@case.Key);
            var result = solution.ShortestPathLength(graph);
            Console.WriteLine($"           {@case.Key}");
            Console.WriteLine($"{result == @case.Value} result: {result}; expected: {@case.Value};");
        }
    }
}