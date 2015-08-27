using UnityEngine;
using System.Collections;

public class crosshair : MonoBehaviour {

	public static Vector3 viewPortPos = Vector3.zero;

	public float gamepadAimingThreshhold;

	private bool gamepadMode = false;
	private Vector2 screenCenterViewport;

    void Start()
    {
        Cursor.visible = false;

		screenCenterViewport = new Vector2 (0.5f, 0.5f);

		viewPortPos = screenCenterViewport; // TODO: AUch wenn switch auf controller input setzen
    }

	void Update () {
		Vector3 worldPos;

		if (Input.GetMouseButtonDown(0))
			gamepadMode = false;
		else if (Mathf.Abs ( Input.GetAxisRaw(inputconstants.aiming_x_axis) ) > gamepadAimingThreshhold || Mathf.Abs(Input.GetAxisRaw(inputconstants.aiming_y_axis)) > gamepadAimingThreshhold)
			gamepadMode = true;

		if (!gamepadMode) {

			viewPortPos = Camera.main.ScreenToViewportPoint (Input.mousePosition);

		} else {

			Vector2 direction;
			direction.x = Input.GetAxisRaw(inputconstants.aiming_x_axis);
			direction.y = Input.GetAxisRaw(inputconstants.aiming_y_axis);

			float directionSqrMag = direction.sqrMagnitude;

			if (directionSqrMag > gamepadAimingThreshhold)
			{
				direction.Normalize();

				direction.x /= 2f;
				direction.y /= 2f;

				direction.x += 1f;
				direction.y += 1f;

				direction.x *= Screen.width / 2f;
				direction.y *= Screen.height / 2f;

				viewPortPos = Camera.main.ScreenToViewportPoint(direction);
			}
		}

		worldPos = Camera.main.ViewportToWorldPoint (viewPortPos);

		worldPos.z = Camera.main.transform.position.z + 1f;
		transform.position = worldPos;
	}
}
