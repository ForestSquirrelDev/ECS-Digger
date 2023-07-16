using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Utils {
    public static class RectTransformUtils
    {
        public static bool IsInsideWithAnyCorner(this RectTransform tested, RectTransform insideArea) {
            var testedWorldCorners = new Vector3[4];
            tested.GetWorldCorners(testedWorldCorners);

            var insideAreaWorldCorners = new Vector3[4];
            insideArea.GetWorldCorners(insideAreaWorldCorners);

            return testedWorldCorners.Any(testedWorldCorner => IsPointInsideRect(testedWorldCorner, insideAreaWorldCorners));
        }
        
        private static bool IsPointInsideRect(Vector2 point, IList<Vector3> rectCorners) {
            return point.x > rectCorners[0].x && point.x < rectCorners[3].x && point.y > rectCorners[0].y && point.y < rectCorners[1].y;
        }
    }
}