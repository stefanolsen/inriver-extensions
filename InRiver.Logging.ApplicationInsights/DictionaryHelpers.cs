using System;
using System.Collections.Generic;

namespace InRiver.Logging.ApplicationInsights
{
    internal static class DictionaryHelpers
    {
        public static void CopyDictionary<TKey, TValue>(
            IDictionary<TKey, TValue> source,
            IDictionary<TKey, TValue> destination)
        {
            if (source == null)
            {
                return;
            }

            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            foreach (KeyValuePair<TKey, TValue> kvp in source)
            {
                destination.Add(kvp.Key, kvp.Value);
            }
        }
    }
}
