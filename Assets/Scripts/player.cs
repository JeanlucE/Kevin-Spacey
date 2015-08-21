using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {

    public gun primary;
	public spheremath pos;

	private Vector2 rawForce;
	private Vector3 rotationTemp;

	void Start()
	{
		transform.parent = pos.GetParent ();
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

		rawForce.x = Input.GetAxisRaw ("Horizontal");
		rawForce.y = Input.GetAxisRaw ("Vertical");

		pos.AddForceRaw (rawForce);
		transform.LookAt (Vector3.zero, crosshair.worldPos - transform.position);
	}
}
