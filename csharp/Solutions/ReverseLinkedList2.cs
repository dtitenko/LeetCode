using System;
using System.Collections.Generic;
using System.Linq;
using LeetCode;

public class ReverseLinkedList2 : ISolution<(string headSource, int m, int n), string>
{
    public string Name => "92. Reverse Linked List II";
    public string Link => "https://leetcode.com/problems/reverse-linked-list-ii/";

    public ((string, int, int), string)[] TestCases =>
        new[]
        {
            (("1->2->3->4->5", 2, 4), "1->4->3->2->5"),
            (("1->2->3->4->5", 1, 5), "5->4->3->2->1"),
            (("1->2->3", 1, 2), "2->1->3"),
        };

    public string Execute((string headSource, int m, int n) input)
    {
        var head = ListNode.Create(input.headSource);
        return ReverseBetween(head, input.m, input.n).ToString();
    }

    public ListNode ReverseBetween(ListNode head, int m, int n)
    {
        int i = 1;
        ListNode node = head;
        ListNode pre_m = null;
        ListNode prev = null;
        ListNode mnode = null;
        while (node != null && i <= n)
        {
            if (i == m)
            {
                pre_m = prev;
                mnode = node;
            }

            if (i > m && i <= n)
            {
                if (pre_m != null)
                {
                    pre_m.next = node;
                }

                var node_next = node.next;
                node.next = mnode;
                prev.next = node_next;

                mnode = node;
                node = prev;
            }

            prev = node;
            node = node.next;

            i++;
        }

        return pre_m != null ? head : mnode;
    }
}