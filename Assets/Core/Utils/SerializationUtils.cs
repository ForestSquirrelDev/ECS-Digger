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

        public static Dictionary<string, object> TryGetNode(this Dictionary<string, object> json, string key) {
            if (json.TryGetValue(key, out var node)) {
                return (Dictionary<string, object>)node;
            }
            return new Dictionary<string, object>();
        }

        public static Dictionary<string, object> GetOrCreateNode(this Dictionary<string, object> json, string key) {
            if (json.TryGetValue(key, out var node))
                return (Dictionary<string, object>) node;
            
            var newNode = new Dictionary<string, object>();
            json.Add(key, newNode);
            return newNode;
        }

        public static IList<object> GetOrCreateNodeList(this Dictionary<string, object> json, string key) {
            if (json.TryGetValue(key, out var nodeList))
                return (IList<object>) nodeList;

            var newList = new List<object>();
            json.Add(key, newList);
            return newList;
        }
    }
}