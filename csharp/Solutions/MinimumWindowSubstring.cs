using System;
using System.Collections.Generic;
using LeetCode;
using Newtonsoft.Json;

public class MinimumWindowSubstring : ISolution<(string, string), string>
{
    public string Name => "76. Minimum Window Substring";
    public string Link => "https://leetcode.com/problems/minimum-window-substring/";

    public ((string, string), string)[] TestCases => new[]
    {
        (("ADOBECODEBANC", "ABC"), "BANC")
    };

    public string Execute((string, string) input)
    {
        return MinWindow(input.Item1, input.Item2);
    }

    public string MinWindow(string s, string t)
    {
        if (s.Length == 0 || t.Length == 0) return "";
        
        int left = 0;
        int right = 0;
        int formed = 0;
        var window = new Dictionary<char, int>();
        var tChars = new Dictionary<char, int>();
        var source = new List<Tuple<int, char>>();

        foreach (var tth in t)
        {
            tChars.TryGetValue(tth, out var count);
            tChars[tth] = count + 1;
        }

        for (var i = 0; i < s.Length; i++)
        {
            var sth = s[i];
            if (tChars.ContainsKey(sth))
            {
                source.Add(Tuple.Create(i, sth));
            }
        }

        int[] ans = {-1, 0, 0};
        while (right < source.Count)
        {
            var c = source[right].Item2;
            window.TryGetValue(c, out var count);
            window[c] = count + 1;
            if (tChars.ContainsKey(c) && window[c] == tChars[c])
            {
                formed++;
            }

            while (left <= right && formed == tChars.Count)
            {
                c = source[left].Item2;
                var end = source[right].Item1;
                var start = source[left].Item1;
                if (ans[0] == -1 || end - start + 1 < ans[0])
                {
                    ans[0] = end - start + 1;
                    ans[1] = start;
                    ans[2] = end;
                }

                window[c]--;
                if (tChars.ContainsKey(c) && window[c] < tChars[c])
                {
                    formed--;
                }

                left++;
            }

            right++;
        }

        return ans[0] == -1 ? "" : s.Substring(ans[1], ans[2] - ans[1] + 1);
    }
}