using System;


namespace AddTwoNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            var solution = new Solution();
            var first = new ListNode(0);
            // first.Add(8);
            var second = new ListNode(1);
            second.Add(8);
            var node = solution.AddTwoNumbers(first, second);

            while (node != null)
            {
                Console.WriteLine(node.val);
                node = node.next;
            }
        }
    }

    public class Solution
    {
        public ListNode AddTwoNumbers(ListNode node1, ListNode node2)
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

    public class ListNode
    {
        public int val;
        public ListNode next;

        public ListNode(int x)
        {
            val = x;
        }

        public ListNode Add(int val)
        {
            var nextNode = new ListNode(val);
            this.next = nextNode;
            return nextNode;
        }
    }
}