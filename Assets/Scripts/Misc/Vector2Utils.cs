using UnityEngine;

public static class Vector2Utils
{
    public static Vector2 VectorFromAngle(this float angle)
    {
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }

    public static Vector2 Rotate(this Vector2 v, float angle)
    {
        float cos = Mathf.Cos(angle);
        float sin = Mathf.Sin(angle);

        return new Vector2(cos - sin, sin + cos) * v;
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

	/// <summary>
	/// returns angle in degrees
	/// </summary>
	/// <param name="vector"></param>
	/// <returns></returns>
	public static float AngleFromVector(this Vector3 vector)
    {
        return Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
    }
}
