using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCode
{
    public class Problem_92_ReverseLinkedList2
    {
        public void Run()
        {
            var solution = new Solution();
            var head = new ListNode(1);
            head.Next(2).Next(3).Next(4).Next(5);
            solution.ReverseBetween(head, 1, 5).Print();
            /* Expected: 5 -> 4 -> 3 -> 2 -> 1 */

            head = new ListNode(1);
            head.Next(2).Next(3).Next(4).Next(5);
            solution.ReverseBetween(head, 2, 4).Print();
            /* Expected: 1 -> 4 -> 3 -> 2 -> 5 */

            head = new ListNode(1);
            head.Next(2).Next(3);
            solution.ReverseBetween(head, 1, 2).Print();
            /* Expected: 2 -> 1 -> 3 */
        }

        public class Solution
        {
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
                            pre_m.next = node;
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

        public class ListNode
        {
            public int val;
            public ListNode next;
            public ListNode(int x) { val = x; }

            public ListNode Next(int x)
            {
                next = new ListNode(x);
                return next;
            }

            public void Print()
            {
                var node = this;
                while (node != null)
                {
                    Console.Write(node.val);
                    node = node.next;
                    if (node != null)
                    {
                        Console.Write(" -> ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
