using UnityEngine;

namespace YaEm
{
    public static class Vector2Utils
    {
        public static Vector2 VectorFromAngle(this float angle)
        {
            return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        }

        /// <summary>
        /// returns angle in degrees
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static float AngleFromVector(this Vector2 vector)
        {
            return Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
        }
    }
}