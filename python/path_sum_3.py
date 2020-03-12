# 437. Path Sum III
# https://leetcode.com/problems/path-sum-iii/
# You are given a binary tree in which each node contains an integer value.
# Find the number of paths that sum to a given value.
# The path does not need to start or end at the root or a leaf, but it must go downwards (traveling only from parent nodes to child nodes).
# The tree has no more than 1,000 nodes and the values are in the range -1,000,000 to 1,000,000.

from queue import Queue
import json

class TreeNode:
    def __init__(self, x: int):
        self.val = x
        self.left = None
        self.right = None

    @staticmethod
    def fromList(input: list):
        if not input:
            return None
        root = TreeNode(input[0])
        queue = Queue()
        queue.put(root)
        index = 1
        while queue and index < len(input):
            node = queue.get()
            if (index < len(input)):
                if input[index]:
                    node.left = TreeNode(input[index])
                    queue.put(node.left)
                index += 1
            if (index < len(input)):
                if input[index]:
                    node.right = TreeNode(input[index])
                    queue.put(node.right)
                index += 1
        return root

class Solution:
    def pathSum(self, root: TreeNode, sum: int) -> int:
        return self.__pathSum(root, sum, 0, {})

    def __pathSum(self, node: TreeNode, sum: int, currentSum: int, pathCount: dict):
        if node == None:
            return 0
        currentSum += node.val
        diff = currentSum - sum
        paths = pathCount.get(diff, 0)
        if currentSum == sum:
            paths += 1

        self.__increment(pathCount, currentSum, 1)
        paths += self.__pathSum(node.left, sum, currentSum, pathCount)
        paths += self.__pathSum(node.right, sum, currentSum, pathCount)
        self.__increment(pathCount, currentSum, -1)

        return paths

    def __increment(self, hashTable: dict, key: int, delta: int):
        newCount = hashTable.get(key, 0) + delta
        if newCount == 0:
            hashTable.pop(key)
        else:
            hashTable[key] = newCount


# print(json.dumps(root, default=lambda o: o.__dict__, indent=4))

solution = Solution()
paths = solution.pathSum(TreeNode.fromList([10, 5, -3, 3, 2, None, 11, 3, -2, None, 1]), 8)
print(paths)
paths = solution.pathSum(TreeNode.fromList([1,-2,-3]), -1)
print(paths)
