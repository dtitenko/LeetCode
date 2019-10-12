using LeetCode;
using Newtonsoft.Json;

public class MedianOf2SortedArrays : ISolution<string, double>
{
    public string Name => "4. Median of Two Sorted Arrays";
    public string Link => "https://leetcode.com/problems/median-of-two-sorted-arrays/";

    public (string, double)[] TestCases => new[]
    {
        ("[[1,2],[3,4]]", 2.5d),
        ("[[1,3],[2]]", 2d),
        ("[[2,4],[]]", 3d),
        ("[[2],[]]", 2d),
        ("[[],[2]]", 2d),
    };

    public double Execute(string input)
    {
        var nums = JsonConvert.DeserializeObject<int[][]>(input);
        return FindMedianSortedArrays(nums[0], nums[1]);
    }

    public double FindMedianSortedArrays(int[] nums1, int[] nums2)
    {
        int i = 0, j = 0, preLast = 0, last = 0;
        int sum = nums1.Length + nums2.Length;
        int stopAt = sum / 2;
        for (int k = 0; k <= stopAt; k++)
        {
            if (nums1.Length > i && (nums2.Length <= j || nums1[i] <= nums2[j]))
            {
                preLast = last;
                last = nums1[i];
                i++;
            }
            else
            {
                preLast = last;
                last = nums2[j];
                j++;
            }
        }

        return sum % 2 == 0 ? (last + preLast) / 2d : last;
    }
}
