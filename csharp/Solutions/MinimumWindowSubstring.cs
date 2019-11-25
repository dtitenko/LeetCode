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
        int left = 0;
        int right = 0;
        int formed = 0;
        var window = new Dictionary<char, int>();
        var tChars = new Dictionary<char, int>();

        foreach (var tth in t)
        {
            tChars.TryGetValue(tth, out var count);
            tChars[tth] = count + 1;
        }

        int[] ans = {-1, 0, 0};
        while (right < s.Length)
        {
            var c = s[right];
            window.TryGetValue(c, out var count);
            window[c] = count + 1;
            if (tChars.ContainsKey(c) && window[c] == tChars[c])
            {
                formed++;
            }

            while (left <= right && formed == tChars.Count)
            {
                c = s[left];
                if (ans[0] == -1 || right - left + 1 < ans[0])
                {
                    ans[0] = right - left + 1;
                    ans[1] = left;
                    ans[2] = right;
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