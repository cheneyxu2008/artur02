using System.Collections;

namespace ClassInspector
{
    public static class Extensions
    {
        /// <summary>
        /// Merges two dictionaries
        /// </summary>
        /// <param name="to">The values are merged into</param>
        /// <param name="from">The source of the values</param>
        public static void Merge(this IDictionary to, IDictionary from)
        {
            foreach (var key in from.Keys)
            {
                to.Add(key, from[key]);
            }
        }
    }
}
