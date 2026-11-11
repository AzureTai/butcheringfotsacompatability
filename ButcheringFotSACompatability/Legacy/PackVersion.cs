using System;
using System.Globalization;

namespace ButcheringFotSACompatability.Legacy
{
    /// <summary>
    /// Compares the numeric core of a mod version ("1.0.20", "1.0.20-rc.1", "1.0.20+build").
    /// Any pre-release or build suffix is ignored, so "1.0.20-rc.1" counts as 1.0.20.
    /// </summary>
    public static class PackVersion
    {
        public static bool TryParse(string? text, out int[] parts)
        {
            parts = Array.Empty<int>();
            if (string.IsNullOrWhiteSpace(text))
            {
                return false;
            }

            string core = text.Trim();
            if (core.StartsWith("v", StringComparison.OrdinalIgnoreCase))
            {
                core = core.Substring(1);
            }

            int suffix = core.IndexOfAny(new[] { '-', '+' });
            if (suffix >= 0)
            {
                core = core.Substring(0, suffix);
            }

            string[] pieces = core.Split('.');
            int[] parsed = new int[pieces.Length];
            for (int index = 0; index < pieces.Length; index++)
            {
                if (!int.TryParse(pieces[index], NumberStyles.None, CultureInfo.InvariantCulture, out parsed[index]))
                {
                    return false;
                }
            }

            parts = parsed;
            return true;
        }

        /// <summary>Returns a negative number, zero or a positive number, padding missing parts with zero.</summary>
        public static int Compare(int[] left, int[] right)
        {
            ArgumentNullException.ThrowIfNull(left);
            ArgumentNullException.ThrowIfNull(right);
            int length = Math.Max(left.Length, right.Length);
            for (int index = 0; index < length; index++)
            {
                int a = index < left.Length ? left[index] : 0;
                int b = index < right.Length ? right[index] : 0;
                if (a != b)
                {
                    return a < b ? -1 : 1;
                }
            }

            return 0;
        }
    }
}
