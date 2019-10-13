using System.Linq;
using System.Text;

namespace LeetCode
{
    public class ListNode
    {
        public static ListNode Create(string source)
        {
            var nodes = source.Split("->")
                .Select(val => new ListNode(int.Parse(val)))
                .ToArray();
            for (var i = 1; i < nodes.Length; i++)
            {
                nodes[i - 1].next = nodes[i];
            }

            return nodes[0];
        }
        
        public int val;
        public ListNode next;

        public ListNode(int x)
        {
            val = x;
        }

        public override string ToString()
        {
            var head = this;
            var sb = new StringBuilder();
            while (head != null)
            {
                if (sb.Length > 0)
                {
                    sb.Append("->");
                }

                sb.Append(head.val);
                head = head.next;
            }

            return sb.ToString();
        }
    }
}