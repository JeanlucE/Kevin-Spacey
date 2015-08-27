using UnityEngine;

public static class extensions {

	public static Vector2 ComponentWiseMultiplication(Vector2 left, Vector2 right)
	{
		left.x *= right.x;
		left.y *= right.y;

		return left;
	}

	public static Vector2 RotateCounterClockwise(Vector2 me, float radians)
	{
		float sn = Mathf.Sin (radians);
		float cs = Mathf.Cos (radians);

		float tempX = me.x * cs - me.y * sn;
		me.y = me.x * sn + me.y * cs;
		me.x = tempX;

		return me;
	}

	public static void Clamp(ref float me, float min, float max)
	{
		me = Mathf.Max(min, Mathf.Min(max, me));
	}

	public static float PosValue(this float me)
	{
		return Mathf.Max (0f, me);
	}

	public static float NegValue(this float me)
	{
		return Mathf.Min(0f, me);
	}

	public static bool IsPositive(this float me)
	{
		return me > 0f;
	}

	public static bool IsNegative(this float me)
	{
		return me < 0f;
	}
}
