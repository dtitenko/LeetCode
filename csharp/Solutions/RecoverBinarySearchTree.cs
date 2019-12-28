using System;
using System.Collections.Generic;
using LeetCode;
using Newtonsoft.Json;

public class RecoverBinarySearchTree : ISolution<string, string>
{
    public string Name => "99. Recover Binary Search Tree";
    public string Link => "https://leetcode.com/problems/recover-binary-search-tree/";

    public (string, string)[] TestCases => new[]
    {
        ("[1,3,null,null,2]", "[3,1,null,null,2]"),
        ("[3,1,4,null,null,2]", "[2,1,4,null,null,3]"),
        ("[4,2,null,1,null,null,3]", "[4,3,null,1,null,null,2]"),
    };

    public string Execute(string input)
    {
        var nodeValues = JsonConvert.DeserializeObject<int?[]>(input);
        if (nodeValues.Length == 0 || !nodeValues[0].HasValue)
        {
            return "[]";
        }

        var root = new TreeNode(nodeValues[0].Value);
        Fill(nodeValues, 0, new Queue<TreeNode>(new[] { root }));
        var solution = new Solution();
        solution.RecoverTree(root);
        var result = root.Print();
        return JsonConvert.SerializeObject(result);
    }

    private void Fill(int?[] values, int index, Queue<TreeNode> level)
    {
        while (level.Count > 0)
        {
            var node = level.Dequeue();
            index++;
            var leftVal = values.Length > index ? values[index] : null;
            index++;
            var rightVal = values.Length > index ? values[index] : null;
            var leftNode = leftVal.HasValue ? new TreeNode(leftVal.Value) : null;
            var rightNode = rightVal.HasValue ? new TreeNode(rightVal.Value) : null;
            if (node != null && leftNode != null)
            {
                node.left = leftNode;
                level.Enqueue(leftNode);
            }

            if (node != null && rightNode != null)
            {
                node.right = rightNode;
                level.Enqueue(rightNode);
            }

            if (node == null && index >= values.Length)
            {
                break;
            }
        }
    }
}

public class Solution
{
    public void RecoverTree(TreeNode root)
    {
        var queue = new Queue<TreeNode>();
        queue.Enqueue(root);
        while (queue.TryDequeue(out var sentinel))
        {
            if (sentinel.left != null) queue.Enqueue(sentinel.left);
            if (sentinel.right != null) queue.Enqueue(sentinel.right);
            var inner = new Queue<TreeNode>();
            inner.Enqueue(root);
            while (inner.TryDequeue(out var node))
            {
                if (node.left != null) inner.Enqueue(node.left);
                if (node.right != null) inner.Enqueue(node.right);
                if (sentinel == node) continue;
                var temp = sentinel.val;
                sentinel.val = node.val;
                node.val = temp;

                if (IsValid(root)) break;

                temp = sentinel.val;
                sentinel.val = node.val;
                node.val = temp;
            }

            if (IsValid(root)) break;
        }
    }

    private bool IsValid(TreeNode root)
    {
        if (root == null) return true;
        return (root.left == null || root.left.val < root.val && IsValid(root.left))
            && (root.right == null || root.right.val > root.val && IsValid(root.right));
    }
}

public class TreeNode
{
    public int val;
    public TreeNode left;
    public TreeNode right;

    public TreeNode(int x)
    {
        val = x;
    }

    public int?[] Print()
    {
        var queue = new Queue<TreeNode>();
        var list = new List<int?>();
        queue.Enqueue(this);
        while (queue.Count > 0)
        {
            var node = queue.Dequeue();
            list.Add(node?.val);
            if (node == null) continue;
            queue.Enqueue(node.left);
            queue.Enqueue(node.right);
        }

        for (int i = list.Count - 1; i >= 0; i--)
        {
            if (!list[i].HasValue)
            {
                list.RemoveAt(i);
            }
            else
            {
                break;
            }
        }

        return list.ToArray();
    }

    public override string ToString() => val.ToString();
}