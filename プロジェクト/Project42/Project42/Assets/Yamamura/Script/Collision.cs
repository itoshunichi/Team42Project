using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{

    #region 線と円
    /// <summary>
    /// 線と円
    /// </summary>
    /// <param name="lineP1"></param>
    /// <param name="lineP2"></param>
    /// <param name="circleCenter"></param>
    /// <param name="radius"></param>
    /// <returns></returns>
    public static bool LineCircleCollision(Vector2 lineP1, Vector2 lineP2, Vector2 circleCenter, float radius)
    {
        float r = radius * radius;
        Vector2 v = lineP2 - lineP1;
        Vector2 v1 = circleCenter - lineP1;
        float t = Vector2.Dot(v, v1) / Vector2.Dot(v, v);

        float v1Dot = v1.x * v1.x + v1.y * v1.y;

        if (t < 0 && v1Dot <= r) return true;

        Vector2 v2 = circleCenter - lineP2;
        float v2Dot = v2.x * v2.x + v2.y * v2.y;

        if (1 < t && v2Dot <= r) return true;

        Vector2 vn = v1 - t * v;
        float vnDot = vn.x * vn.x + vn.y * vn.y;

        if (0 < t && t < 1 && vnDot <= r) return true;

        return false;
    }
    #endregion

    #region 多角形内
    /// <summary>
    /// 多角形内に点が存在するかどうか
    /// </summary>
    /// <param name="p"></param>
    /// <param name="poly"></param>
    /// <returns></returns>
    public static bool PointPolygon(Vector2 p, List<Vector2> poly)
    {
        Vector2 p1, p2;
        Vector2 oldPoint = poly[poly.Count - 1];
        bool inside = false;
        for (int i = 0; i < poly.Count; i++)
        {
            Vector2 newPoint = poly[i];
            if (newPoint.x > oldPoint.x)
            {
                p1 = oldPoint;
                p2 = newPoint;
            }
            else
            {
                p1 = newPoint;
                p2 = oldPoint;
            }

            if ((p1.x < p.x) == (p.x <= p2.x) && (p.y - p1.y) * (p2.x - p1.x) < (p2.y - p1.y) * (p.x - p1.x))
            {
                inside = !inside;
            }
            oldPoint = newPoint;
        }
        return inside;
    }
    #endregion

   
}
