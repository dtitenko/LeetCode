﻿using System;
using System.Collections.Generic;
using System.IO;
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
            { "[[1,4],[0,3,10],[3],[1,2,6,7],[0,5],[4],[3],[3],[10],[10],[1,9,8]]", 15 },
            {
                "[[1,2,3,4,5,6,7,8,9,10,11],[0,2,3,4,5,6,7,8,9,10,11],[0,1,3,4,5,6,7,8,9,10,11],[0,1,2,4,5,6,7,8,9,10,11],[0,1,2,3,5,6,7,8,9,10,11],[0,1,2,3,4,6,7,8,9,10,11],[0,1,2,3,4,5,7,8,9,10,11],[0,1,2,3,4,5,6,8,9,10,11],[0,1,2,3,4,5,6,7,9,10,11],[0,1,2,3,4,5,6,7,8,10,11],[0,1,2,3,4,5,6,7,8,9,11],[0,1,2,3,4,5,6,7,8,9,10]]",
                11
            },
            { "[[1,2,3,5],[0,5],[0,4],[0],[5,2],[0,4,1]]", 5 },
            { "[[2,5,7],[2,4],[0,1],[5],[5,6,1],[4,10,8,0,3],[4,9],[0],[5],[6],[5]]", 13 },
            { "[[2,3,5,7],[2,3,7],[0,1],[0,1],[7],[0],[10],[9,10,0,1,4],[9],[7,8],[7,6]]", 14 },
            { "[[2,10],[2,7],[0,1,3,4,5,8],[2],[2],[2],[8],[9,11,8,1],[7,6,2],[7],[11,0],[7,10]]", 15 },
            { "[[1,8,9,11],[0,6],[3,9],[2,11],[8],[9],[1,10],[11],[0,4],[0,2,5],[6],[0,3,7]]", 14 },
        };
        var solution = new Solution();
        foreach (var @case in tc)
        {
            var tw = new StringWriter();
            var origOut = Console.Out;
            Console.SetOut(tw);
            var graph = JsonConvert.DeserializeObject<int[][]>(@case.Key);
            var result = solution.ShortestPathLength(graph);
            Console.SetOut(origOut);
            var key = @case.Key;
            key = key.Length > 30 ? $"{key.Substring(0, 10)}...{key.Substring(@case.Key.Length - 10)}" : key;
            if (result != @case.Value)
            {
                Console.WriteLine("----------------------");
                Console.WriteLine(tw);
                Console.WriteLine($"           {key}");
                Console.WriteLine($"{result == @case.Value} result: {result}; expected: {@case.Value};");
                Console.WriteLine("----------------------");
            }
            else
            {
                Console.WriteLine($"{key}: true");
            }
        }
    }
}