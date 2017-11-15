using System;
using System.Collections.Generic;

public class Solution
{
    public string FractionToDecimal(int numerator, int denominator)
    {
        bool isNegative = (numerator < 0 && denominator > 0) || (numerator > 0 && denominator < 0);
        long numeratorL = Math.Abs((long)numerator);
        long denominatorL = Math.Abs((long)denominator);
        IDictionary<long, int> previousRemains = new Dictionary<long, int>();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        long quotian = numeratorL / denominatorL;
        sb.Append(quotian);

        numeratorL %= denominatorL;

        if (numeratorL != 0)
        {
            sb.Append(".");
        }

        int dotIndex = sb.Length - 1;
        int quotianIndex = 0;
        while (numeratorL != 0)
        {
            numeratorL *= 10;
            quotian = numeratorL / denominatorL;
            if (!previousRemains.ContainsKey(numeratorL))
            {
                sb.Append(quotian);
                previousRemains.Add(numeratorL, quotianIndex++);
            }
            else
            {
                //int firstIndex = 1 + previousRemains.Get(numeratorL) + sb.indexOf(".");
                //sb.Insert(firstIndex, '(');
                int firstIndex = 1 + previousRemains[(int)numeratorL] + dotIndex;
                sb.Insert(firstIndex, '(');

                sb.Append(")");
                break;
            }

            numeratorL %= denominatorL;
        }

        if (isNegative)
        {
            sb.Insert(0, "-");
        }

        return sb.ToString();
    }
}