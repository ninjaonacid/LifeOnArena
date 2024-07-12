using System;
using System.Collections.Generic;
using System.Text;

namespace InstantGamesBridge.Common
{
    public static class JsonHelper
    {
        public static string SurroundWithBraces(this string json)
        {
            return "{" + json + "}";
        }

        public static string SurroundWithKey(this string json, string key, bool quotes = false)
        {
            if (quotes)
            {
                json = "\"" + json + "\"";
            }
            
            return "\"" + key + "\":" + json;
        }

        public static string ConvertBooleanToCSharp(this string json)
        {
            return json.Replace("true", "True").Replace("false", "False");
        }

        public static string ToJson(this Dictionary<string, object> data)
        {
            var sb = new StringBuilder();
            var isFirst = true;

            foreach (var item in data)
            {
                if (!isFirst)
                {
                    sb.Append(",");
                }
                isFirst = false;

                sb.Append(item.Value.ToJson().SurroundWithKey(item.Key));
            }

            return sb.ToString().SurroundWithBraces();
        }

        private static string ToJson(this Array data)
        {
            var sb = new StringBuilder();
            sb.Append("[");
            var isFirst = true;

            foreach (var item in data)
            {
                if (!isFirst)
                {
                    sb.Append(",");
                }
                isFirst = false;

                sb.Append(item.ToJson());
            }

            sb.Append("]");
            return sb.ToString();
        }

        private static string ToJson(this object data)
        {
            return data switch
            {
                null => "null",
                string s => "\"" + EscapeString(s) + "\"",
                int or float or double => data.ToString(),
                bool b => b ? "true" : "false",
                Array array => array.ToJson(),
                Dictionary<string, object> objects => objects.ToJson(),
                _ => "null"
            };
        }

        private static string EscapeString(string str)
        {
            return str.Replace("\\", "\\\\").Replace("\"", "\\\"");
        }
    }
}