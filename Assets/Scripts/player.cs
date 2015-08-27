using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {

    public gun primary;

	private Vector3 rotationTemp = Vector3.zero;
	private static Rigidbody2D r = null;

	void Awake()
	{
		r = GetComponent<Rigidbody2D> ();
	}

	void Update () {
		if (Input.GetButtonDown(inputconstants.primary_fire))
        {
            primary.StartShooting();
        }
        else if (Input.GetButtonUp(inputconstants.primary_fire))
        {
            primary.StopShooting();
        }

		Vector2 viewPortPosStretched = (Vector2)crosshair.viewPortPos * 2f - Vector2.one;
		viewPortPosStretched.x *= Screen.width;
		viewPortPosStretched.y *= Screen.height;

		rotationTemp.z = Mathf.Atan2 (viewPortPosStretched.y, viewPortPosStretched.x) * Mathf.Rad2Deg - 90f;
		transform.rotation = Quaternion.Euler(rotationTemp);
	}

	public static Vector2 GetVelocity()
	{
		return r.velocity;
	}
}
