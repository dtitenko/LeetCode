using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace LinkedListCycle
{
    class Program
    {
        static void Main(string[] args)
        {
            var cases = new Dictionary<string, int>
            {
                { "[3,2,0,-4]", 1 },
                { "[1,2]", 0 },
                { "[1]", -1 },
            };
            var solution = new Solution();
            foreach (var (@case, expected) in cases)
            {
                var input = JsonConvert.DeserializeObject<int[]>(@case);
                var nodes = input.Select(val => new ListNode(val)).ToArray();
                var head = GetNode(nodes, expected);
                var result = solution.DetectCycle(head);
                var index = -1;
                if (result != null)
                {
                    index = Array.IndexOf(nodes, result);
                }

                var color = Console.ForegroundColor;
                Console.ForegroundColor = expected == index ? ConsoleColor.Green : ConsoleColor.Red;
                Console.WriteLine($"{@case} expected: {expected}, result {index}");
                Console.ForegroundColor = color;
            }
        }

        static ListNode GetNode(ListNode[] input, int expected)
        {
            var head = input[0];
            var next = head;
            for (var i = 1; i < input.Length; i++)
            {
                var node = input[i];
                next.next = node;
                next = node;
            }

            if (expected != -1)
                next.next = input[expected];

            return head;
        }
    }

    public class Solution
    {
        public ListNode DetectCycle(ListNode head)
        {
            if (head == null) return null;

            var slow = head;
            var fast = head;

            while (fast?.next != null)
            {
                slow = slow.next;
                fast = fast.next.next;

                if (slow == fast) break;
            }

            if (fast?.next == null)
            {
                return null;
            }

            // find the entrance of the cycle
            slow = head;
            while (slow != fast)
            {
                slow = slow.next;
                fast = fast.next;
            }

            return slow;
        }
    }

    public class ListNode
    {
        public int val;
        public ListNode next;

        public ListNode(int x)
        {
            val = x;
            next = null;
        }
    }
}