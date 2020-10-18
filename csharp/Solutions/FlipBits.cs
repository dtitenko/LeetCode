using LeetCode;

/// <summary>
/// Task: given an array of bits, find the minimum number of bits that must be flipped to get a balanced array
/// </summary>
public class FlipBits : ISolution<int[], int>
{
    public string Name => "-1. Minimum number of bits to flip";
    public string Link => "none";

    public (int[], int)[] TestCases => new[]
    {
        (new int[0], 0),
        (new[] { 0, 1, 0 }, 0),
        (new[] { 1, 0, 1, 1 }, 1),
        (new[] { 0, 1, 1, 0 }, 2),
        (new[] { 1, 1, 0, 1, 1 }, 2),
    };

    public int Execute(int[] input)
    {
        if (input == null || input.Length == 0)
        {
            return 0;
        }

        // finding max subarray with correct sequence
        var (maxStart, maxEnd) = (0, 0);
        var (start, end) = (0, 0);
        var isZero = input[0] == 0;
        for (var i = 1; i < input.Length; i++)
        {
            isZero = !isZero;
            if (DoesMatchSequence(i))
            {
                end = i;
            }
            else
            {
                if (maxEnd - maxStart < end - start)
                {
                    (maxStart, maxEnd) = (start, end);
                }

                isZero = input[i] == 0;
                (start, end) = (i, i);
            }
        }

        // count the reverses from found subarray { x, x, <- [subarray in order] -> x, x }
        var count = 0;
        isZero = input[maxEnd] == 0;
        for (var i = maxEnd + 1; i < input.Length; i++)
        {
            isZero = !isZero;
            if (!DoesMatchSequence(i))
            {
                count++;
            }
        }

        isZero = input[maxStart] == 0;
        for (var i = maxStart - 1; i >= 0; i--)
        {
            isZero = !isZero;
            if (!DoesMatchSequence(i))
            {
                count++;
            }
        }

        return count;

        bool DoesMatchSequence(int index) => isZero && input[index] == 0 || !isZero && input[index] == 1;
    }
}