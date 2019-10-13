using System;
using LeetCode;

public class AddTwoNumbers : ISolution<(string node1Source, string node2Source), string>
{
    public string Name => "2. Add Two Numbers";
    public string Link => "https://leetcode.com/problems/add-two-numbers/";

    public ((string, string), string)[] TestCases =>
        new[]
        {
            (("2->4->3", "5->6->4"), "7->0->8")
        };

    public string Execute((string node1Source, string node2Source) input)
    {
        var node1 = ListNode.Create(input.node1Source);
        var node2 = ListNode.Create(input.node2Source);
        return Execute(node1, node2).ToString();
    }

    public ListNode Execute(ListNode node1, ListNode node2)
    {
        ListNode result = null;
        ListNode next = null;
        int dec = 0;
        while (node1 != null || node2 != null)
        {
            var sum = (node1?.val ?? 0) + (node2?.val ?? 0) + dec;
            dec = 0;

            if (sum >= 10)
            {
                dec = 1;
                sum -= 10;
            }

            if (next == null)
            {
                next = new ListNode(sum);
            }
            else
            {
                next.next = new ListNode(sum);
                next = next.next;
            }

            if (result == null)
            {
                result = next;
            }

            node1 = node1?.next;
            node2 = node2?.next;
        }

        if (dec != 0 && next != null)
        {
            var nextNode = new ListNode(dec);
            next.next = nextNode;
        }

        return result;
    }
}