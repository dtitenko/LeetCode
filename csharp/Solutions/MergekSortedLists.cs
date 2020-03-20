using System.Collections.Generic;
using System.Linq;
using LeetCode;
using Newtonsoft.Json;

public class MergekSortedLists : ISolution<string, string>
{
    public string Name => "23. Merge k Sorted Lists";
    public string Link => "https://leetcode.com/problems/merge-k-sorted-lists/";

    public (string, string)[] TestCases =>
        new[]
        {
            ("[\"1->4->5\", \"1->3->4\", \"2->6\"]", "1->1->2->3->4->4->5->6")
        };

    public string Execute(string input)
    {
        var lists = JsonConvert.DeserializeObject<string[]>(input);
        var nodes = lists.Select(list => ListNode.Create(list)).ToArray();
        return MergeKLists(nodes).ToString();
    }

    public ListNode MergeKLists(ListNode[] lists)
    {
        if (lists == null || lists.Length == 0) return null;
        if (lists.Length == 1) return lists[0];
        if (lists.Length == 2) return MergeTwoLists(lists[0], lists[1]);
        var queue = new Queue<ListNode>(lists.Length);
        foreach (var node in lists)
        {
            queue.Enqueue(node);
        }

        while (queue.Count > 1)
        {
            var node = MergeTwoLists(queue.Dequeue(), queue.Dequeue());
            queue.Enqueue(node);
        }

        return queue.Dequeue();
    }

    private ListNode MergeTwoLists(ListNode node1, ListNode node2)
    {
        if (node1 == null && node2 == null) return null;
        if (node1 == null) return node2;
        if (node2 == null) return node1;
        var node = new ListNode(0);
        var head = node;

        while (node1 != null || node2 != null)
        {
            if (node2 == null || (node1 != null && node1.val <= node2.val))
            {
                node.next = node1;
                node1 = node1.next;
            }
            else
            {
                node.next = node2;
                node2 = node2.next;
            }

            node = node.next;
        }

        return head.next;
    }
}