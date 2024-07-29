using Code.Runtime.Data.PlayerData;
using Newtonsoft.Json;
using UnityEngine;

namespace Code.Runtime.Data
{
    public static class DataExtensions
    {
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj, JsonSettings());
        }

        public static T ToDeserialized<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json, JsonSettings());
        }

        private static JsonSerializerSettings JsonSettings()
        {
            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Full,
                Formatting = Formatting.Indented
            };
            return settings;
        }
        
    }
}