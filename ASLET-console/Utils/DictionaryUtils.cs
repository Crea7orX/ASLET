namespace ASLET.Utils;

public static class DictionaryUtils
{
    public static void Put<TKey, TValue>(Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        where TKey : notnull
    {
        if (dictionary.ContainsKey(key))
        {
            dictionary[key] = value;
        }
        else
        {
            dictionary.Add(key, value);
        }
    }

    public static void PutAll<TKey, TValue>(Dictionary<TKey, TValue> dictionary, Dictionary<TKey, TValue> putDictionary)
        where TKey : notnull
    {
        foreach (KeyValuePair<TKey, TValue> keyValuePair in putDictionary)
        {
            Put(dictionary, keyValuePair.Key, keyValuePair.Value);
        }
    }
}