using System.Collections.Generic;

namespace Helper
{
    public static class DictionaryExtension
    {
        public static bool ContainsAnyKeys<TKey, TValue>(this Dictionary<TKey, TValue> dict, params TKey[] keys)
        {
            if (keys.Length == 0)
                return false;

            foreach (TKey key in keys)
            {
                if (dict.ContainsKey(key))
                    return true;
            }

            return false;
        }

        public static bool ContainsAllKeys<TKey, TValue>(this Dictionary<TKey, TValue> dict, params TKey[] keys)
        {
            if (keys.Length == 0)
                return false;

            int count = 0;
            foreach (TKey key in keys)
            {
                if (dict.ContainsKey(key))
                    count++;
            }

            return count == keys.Length;
        }
    }
}