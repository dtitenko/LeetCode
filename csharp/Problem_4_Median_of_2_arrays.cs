using System;
using System.Diagnostics;

namespace LeetCode
{
    public class Problem_4_Median_of_2_arrays
    {
        public void Run()
        {
            Debug.Assert(FindMedianSortedArrays(new[] { 1, 2 }, new[] { 3, 4 }) == 2.5d);
            Debug.Assert(FindMedianSortedArrays(new[] { 1, 3 }, new[] { 2 }) == 2d);
            Debug.Assert(FindMedianSortedArrays(new[] { 2, 4 }, new int[0]) == 3d);
            Debug.Assert(FindMedianSortedArrays(new[] { 2 }, new int[0]) == 2d);
            Debug.Assert(FindMedianSortedArrays(new int[0], new[] { 2 }) == 2d);
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
}