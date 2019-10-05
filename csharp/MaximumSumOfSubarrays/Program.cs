using System;
using Newtonsoft.Json;

class Program
{
    static void Main(string[] args)
    {
        // [1,2,1,2,6,7,5,1], 2
        var solution = new Solution();
        var nums = JsonConvert.DeserializeObject<int[]>("[1,2,1,2,6,7,5,1]");
        var ans = solution.MaxSumOfThreeSubarrays(nums, 2);
        Console.WriteLine(JsonConvert.SerializeObject(ans));
    }
}

public class Solution
{
    public int[] MaxSumOfThreeSubarrays(int[] nums, int k)
    {
        var len = nums.Length;
        var sums = new int[len - k + 1];
        var left = new int[len - k + 1];
        var right = new int[len - k + 1];
        for (int i = 0, sum = 0; i < len; i++)
        {
            sum += nums[i];
            if (i >= k)
            {
                sum -= nums[i - k];
            }

            if (i >= k - 1)
            {
                sums[i - k + 1] = sum;
            }
        }

        for (int i = 0, j = 0; i < len - k + 1; i++)
        {
            if (sums[i] > sums[j])
            {
                j = i;
            }

            left[i] = j;
        }

        for (int i = len - k, j = len - k; i >= 0; i--)
        {
            if (sums[i] >= sums[j])
            {
                j = i;
            }

            right[i] = j;
        }

        Console.WriteLine($"nums: {JsonConvert.SerializeObject(nums)}");
        Console.WriteLine($"sums: {JsonConvert.SerializeObject(sums)}");
        Console.WriteLine($"left: {JsonConvert.SerializeObject(left)}");
        Console.WriteLine($"right: {JsonConvert.SerializeObject(right)}");

        var ans = new int[0];
        for (int m = k, sum = 0; m < len - 2 * k + 1; m++)
        {
            if (sums[left[m - k]] + sums[m] + sums[right[m + k]] <= sum) continue;
            ans = new[] {left[m - k], m, right[m + k]};
            sum = sums[left[m - k]] + sums[m] + sums[right[m + k]];
        }

        return ans;
    }
}