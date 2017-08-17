# There are two sorted arrays nums1 and nums2 of size m and n respectively.
# Find the median of the two sorted arrays. The overall run time complexity should be O(log (m+n)).

class Solution(object):
    def findMedianSortedArrays(self, nums1, nums2):
        len1, len2 = len(nums1), len(nums2)
        i, j, preLast, last = 0, 0, 0, 0
        sum = len1 + len2
        stopAt = int(sum / 2) + 1
        for x in range(0, stopAt):
            if len1 > i and (len2 <= j or nums1[i] <= nums2[j]):
                preLast = last
                last = nums1[i]
                i = i + 1
            else:
                preLast = last
                last = nums2[j]
                j = j + 1

        return (last + preLast) / 2.0 if sum % 2 == 0 else float(last)



if __name__ == "__main__":
    solution = Solution()
    print(solution.findMedianSortedArrays([1, 3], [2]))
    print(solution.findMedianSortedArrays([1, 3, 5, 7], [2, 4, 6]))
    print(solution.findMedianSortedArrays([1, 3, 5], [2, 4, 6]))
