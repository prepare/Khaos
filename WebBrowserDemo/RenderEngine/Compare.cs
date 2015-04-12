using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RenderDemo
{
    public static class MultiCompare
    {
        /// <summary>
        /// Takes an array of items and tests if they are all equal to each other
        /// </summary>
        /// <typeparam name="T">Array type, type must implement IComparable</typeparam>
        /// <param name="items">The items to compare</param>
        /// <returns>True: if they are all equal</returns>
        public static bool Compare<T>(params T[] items) where T : IComparable<T>
        {
            int total = 0;
            for (int i = 0; i < items.Length; i += 2)
            {
                // Compare the current and next item
                total += items[i].CompareTo(items[i + 1]);

                // Link the current group with the next group
                if (i + 2 < items.Length)
                    total += items[i + 1].CompareTo(items[i + 2]);

            }
            return (total == 0);
        }

        /// <summary>
        /// Takes an array of items and tests if they are all equal to each other
        /// </summary>
        public static bool Compare(params double[] items)
        {
            int total = 0;
            for (int i = 0; i < items.Length; i += 2)
            {
                // Compare the current and next item
                if (items[i] != (items[i + 1])) total++;

                // Link the current group with the next group
                if (i + 2 < items.Length)
                    if (items[i + 1] != (items[i + 2])) total++;
            }
            return (total == 0);
        }

        /// <summary>
        /// Takes an array of items and tests if they are all equal to each other
        /// </summary>
        public static bool Compare(params float[] items)
        {
            int total = 0;
            for (int i = 0; i < items.Length; i += 2)
            {
                // Compare the current and next item
                if (items[i] != (items[i + 1])) total++;

                // Link the current group with the next group
                if (i + 2 < items.Length)
                    if (items[i + 1] != (items[i + 2])) total++;
            }
            return (total == 0);
        }

        /// <summary>
        /// Takes an array of items and tests if they are all equal to each other
        /// </summary>
        public static bool Compare(params int[] items)
        {
            int total = 0;
            for (int i = 0; i < items.Length; i += 2)
            {
                // Compare the current and next item
                if (items[i] != (items[i + 1])) total++;

                // Link the current group with the next group
                if (i + 2 < items.Length)
                    if (items[i + 1] != (items[i + 2])) total++;
            }
            return (total == 0);
        }

    }
}
