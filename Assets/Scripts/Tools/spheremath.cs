using UnityEngine;
using System.Collections;

public class spheremath : MonoBehaviour {

	private Transform origin = null;
	private Vector2 velocity, deltaVelocity = Vector2.zero;

	public float maxSpeed, maxAcceleration;
	
	void Awake () {
		origin = new GameObject("_origin").transform;
	}

	public Transform GetParent()
	{
		return origin;
	}

	public void SetVelocity(Vector2 v) // ingores acceleration constraints
	{
		velocity = v;
	}

	public void AddForceRaw(Vector2 acceleration) // 0 to 1 on x and y axis
	{
		if (acceleration.sqrMagnitude > 1f)
			acceleration.Normalize ();

		velocity += acceleration * maxAcceleration * Time.smoothDeltaTime;

		if (velocity.sqrMagnitude > maxSpeed * maxSpeed)
			velocity = velocity.normalized * maxSpeed;
	}

	void Update()
	{
		deltaVelocity = velocity * Time.smoothDeltaTime;
		
		origin.rotation *= Quaternion.Euler (deltaVelocity.y, -deltaVelocity.x, 0f);
	}
}
