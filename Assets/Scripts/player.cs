using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {

    public gun primary;
	
	void Update () {
        Vector3 rotationToCrosshair = Vector3.zero;
        Vector2 direction = crosshair.crossHairPos - (Vector2)transform.position;
        rotationToCrosshair.z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(rotationToCrosshair);

        if (Input.GetButtonDown(inputconstants.primary_fire))
        {
            primary.StartShooting();
        }
        else if (Input.GetButtonUp(inputconstants.primary_fire))
        {
            primary.StopShooting();
        }
	}
}
