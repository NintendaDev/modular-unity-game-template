using UnityEngine;

namespace Modules.Extensions
{
    public static class JsonExtensions
    {
        public static string ToJson(this object sourceObject) =>
            JsonUtility.ToJson(sourceObject);

        public static T ToDeserialized<T>(this string json) =>
            JsonUtility.FromJson<T>(json);
    }
}