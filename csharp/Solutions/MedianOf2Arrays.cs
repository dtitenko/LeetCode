using LeetCode;

public class MedianOf2Arrays : ISolution<(int[], int[]), double>
{
    public string Name => "4. Median of Two Sorted Arrays";
    public string Link => "https://leetcode.com/problems/median-of-two-sorted-arrays/";

    public ((int[], int[]), double)[] TestCases => new[]
    {
        ((new[] { 1, 2 }, new[] { 3, 4 }), 2.5d),
        ((new[] { 1, 3 }, new[] { 2 }), 2d),
        ((new[] { 2, 4 }, new int[0]), 3d),
        ((new[] { 2 }, new int[0]), 2d),
        ((new int[0], new[] { 2 }), 2d)
    };

    public double Execute((int[], int[]) input)
    {
        var (nums1, nums2) = input;

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