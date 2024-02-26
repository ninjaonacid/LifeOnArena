using Code.Runtime.Data.PlayerData;
using Newtonsoft.Json;
using UnityEngine;

namespace Code.Runtime.Data
{
    public static class DataExtensions
    {
        public static Vector3Data AsVectorData(this Vector3 vector)
        {
            return new Vector3Data(vector.x, vector.y, vector.z);
        }

        public static Vector3 AsUnityVector(this Vector3Data vector3Data)
        {
            return new Vector3(vector3Data.X, vector3Data.Y, vector3Data.Z);
        }

        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);

        }

        public static T ToDeserialized<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static Vector3 AddY(this Vector3 vector, float y)
        {
            vector.y += y;
            return vector;
        }
    }
}