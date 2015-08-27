using UnityEngine;
using System.Collections.Generic;

public class thrustsystem : MonoBehaviour {

	public thruster[] thrusters;
	public Vector3 accForwardSideBack, maxVelForwardSideBack;

	private Rigidbody2D r = null;
	private Vector2 forceDirection = Vector2.zero;

	void Awake()
	{
		r = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate()
	{
		forceDirection.x = Input.GetAxisRaw (inputconstants.thrust_x_axis);
		forceDirection.y = Input.GetAxisRaw (inputconstants.thrust_y_axis);

		// visuelle effekte der thruster
		Vector2 forceDirectionNormalized = forceDirection.sqrMagnitude > 1f ? forceDirection.normalized : forceDirection;
		foreach (var t in thrusters) {
			t.DisplayThrust(forceDirectionNormalized);
		}

		if (forceDirection.sqrMagnitude > 0f) {
			// Acceleration Vectors
			Vector2 up = Vector2.up * accForwardSideBack.x;
			Vector2 right = Vector2.right * accForwardSideBack.y;
			Vector2 down = Vector2.down * accForwardSideBack.z;
			Vector2 left = -right;

			// calculate angle for rotation
			float angleRadians = Mathf.Atan2(transform.up.y, transform.up.x) - 90f * Mathf.Deg2Rad;

			// Rotate Vectors accordingly
			up = extensions.RotateCounterClockwise(up, angleRadians);
			right = extensions.RotateCounterClockwise(right, angleRadians);
			down = extensions.RotateCounterClockwise(down, angleRadians);
			left = extensions.RotateCounterClockwise(left, angleRadians);

			// Kumulative Werte für alle vier Richtungen berechnen
			float yMax, yMin, xMax, xMin;

			yMax = up.y.PosValue() + right.y.PosValue() + down.y.PosValue() + left.y.PosValue();
			yMin = up.y.NegValue() + right.y.NegValue() + down.y.NegValue() + left.y.NegValue();
			xMax = up.x.PosValue() + right.x.PosValue() + down.x.PosValue() + left.x.PosValue();
			xMin = up.x.NegValue() + right.x.NegValue() + down.x.NegValue() + left.x.NegValue();

			// Square for flatter acceleration profile, but keep sign
			forceDirection.y = Mathf.Sign (forceDirection.y) * forceDirection.y * forceDirection.y;
			forceDirection.x = Mathf.Sign (forceDirection.x) * forceDirection.x * forceDirection.x;

			// Interpolieren
			forceDirection.y = Mathf.Abs (forceDirection.y.NegValue()) * yMin + forceDirection.y.PosValue() * yMax;
			forceDirection.x = Mathf.Abs (forceDirection.x.NegValue()) * xMin + forceDirection.x.PosValue() * xMax;

			// Timescale anpassen
			forceDirection *= Time.fixedDeltaTime;

			// Maximalspeed vektoren
			up = Vector2.up * maxVelForwardSideBack.x;
			right = Vector2.right * maxVelForwardSideBack.y;
			down = Vector2.down * maxVelForwardSideBack.z;
			left = -right;

			// Vektoren rotieren
			up = extensions.RotateCounterClockwise(up, angleRadians);
			right = extensions.RotateCounterClockwise(right, angleRadians);
			down = extensions.RotateCounterClockwise(down, angleRadians);
			left = extensions.RotateCounterClockwise(left, angleRadians);

			// Maximale Werte erzeugen
			yMax = Mathf.Max (up.y, right.y, down.y, left.y);
			yMin = Mathf.Min (up.y, right.y, down.y, left.y);	
			xMax = Mathf.Max (up.x, right.x, down.x, left.x);
			xMin = Mathf.Min (up.x, right.x, down.x, left.x);	

			// Beschleunigung cappen
			if (forceDirection.y.IsPositive() && r.velocity.y + forceDirection.y > yMax)
				forceDirection.y = (yMax - r.velocity.y).PosValue();

			if (forceDirection.y.IsNegative() && r.velocity.y + forceDirection.y < yMin)
				forceDirection.y = (yMin - r.velocity.y).NegValue();

			if (forceDirection.x.IsPositive() && r.velocity.x + forceDirection.x > xMax)
				forceDirection.x = (xMax - r.velocity.x).PosValue();

			if (forceDirection.x.IsNegative() && r.velocity.x + forceDirection.x < xMin)
				forceDirection.x = (xMin - r.velocity.x).NegValue();

			// zur velocity hinzufügen
			r.velocity += forceDirection;
		}
	}
}
