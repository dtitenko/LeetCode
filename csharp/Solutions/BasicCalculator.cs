using System;
using System.Collections.Generic;
using LeetCode;

public class BasicCalculator : ISolution<string, int>
{
    public string Name => "224. Basic Calculator";
    public string Link => "https://leetcode.com/problems/basic-calculator/";

    public (string, int)[] TestCases => new[]
    {
        ("1 + 1", 2),
        (" 2-1 + 2 ", 3),
        ("(1+(4+5+2)-3)+(6+8)", 23),
        ("2147483647", 2147483647),
        ("23 - 3 + 8", 28),
    };

    public int Execute(string input) => Calculate(input);
    
    private static readonly Dictionary<char, Func<int, int, int>> s_operationsDict =
        new Dictionary<char, Func<int, int, int>>
    {
        { '+', (a, b) => a + b },
        { '-', (a, b) => a - b },
        { '*', (a, b) => a * b },
        { '/', (a, b) => a / b },
    };

    public int Calculate(string s)
    {
        var index = s.Length - 1;
        return CalculateEquation(s, ref index);
    }

    private int CalculateEquation(string s, ref int index)
    {
        var operations = new Stack<Func<int, int, int>>();
        var numbers = new Stack<int>();
        var numLen = 0;
        var num = 0;
        for (; index >= 0; index--)
        {
            var c = s[index];
            if (c >= 48 && c <= 57)
            {
                num =  (int)Math.Pow(10, numLen) * (c - '0') + num;
                numLen++;
            }
            else if (numLen > 0)
            {
                numbers.Push(num);
                num = 0;
                numLen = 0;
            }
            if (s_operationsDict.TryGetValue(c, out var operation))
                operations.Push(operation);
            if (c == '(')
                break;

            if (c == ')')
            {
                var i = index - 1;
                var result = CalculateEquation(s, ref i);
                index = i;
                numbers.Push(result);
            }
        }
        
        if (numLen > 0)
        {
            numbers.Push(num);
        }

        while (operations.Count > 0)
        {
            var a = numbers.Pop();
            var b = numbers.Pop();
            var o = operations.Pop();
            var result = o(a, b);
            numbers.Push(result);
        }

        return numbers.Pop();
    }
}