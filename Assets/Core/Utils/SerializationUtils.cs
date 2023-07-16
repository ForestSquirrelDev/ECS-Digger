using System;
using System.Collections.Generic;

namespace Core.Utils {
    public static class SerializationUtils
    {
        public static int GetInt(this Dictionary<string, object> node, string key, int defaultValue = 0) {
            if (node.TryGetValue(key, out var value))
                return Convert.ToInt32(value);
            return defaultValue;
        }

        public static Dictionary<string, object> GetNode(this Dictionary<string, object> json, string key) {
            if (json.TryGetValue(key, out var node)) {
                return (Dictionary<string, object>)node;
            }
            return new Dictionary<string, object>();
        }
    }
}