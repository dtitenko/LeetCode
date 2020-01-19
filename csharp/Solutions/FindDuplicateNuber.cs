using System;
using LeetCode;
using Newtonsoft.Json;

namespace LeetCode.Solutions
{
    public class FindDuplicateNuber : ISolution<string, int>
    {
        public string Name => "287. Find the Duplicate Number";

        public string Link => "https://leetcode.com/problems/find-the-duplicate-number/";

        public (string, int)[] TestCases =>
             new[]
            {
                ("[1,3,4,2,2]", 2),
                ("[3,1,3,4,2]", 3),
                ("[1,1,2]", 1),
                ("[2,2,2,2,2]", 2)
            };

        public int Execute(string input)
        {
            int[] inputNums = JsonConvert.DeserializeObject<int[]>(input);
            return FindDuplicate(inputNums);
        }

        public int FindDuplicate(int[] nums)
        {
            var found = new bool[nums.Length - 1];
            foreach (var num in nums)
            {
                if (found[num - 1])
                {
                    return num;
                }

                found[num - 1] = true;
            }

            return -1;
        }

        public int FindDuplicate_Floyds_loop_detection(int[] nums)
        {
            int a = 0, b = 0;
            do
            {
                a = nums[nums[a]];
                b = nums[b];
            } while (a != b);

            b = 0;
            while (a != b)
            {
                a = nums[a];
                b = nums[b];
            }

            return a;
        }
    }
}
