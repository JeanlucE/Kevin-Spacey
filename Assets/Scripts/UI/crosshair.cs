using UnityEngine;
using System.Collections;

public class crosshair : MonoBehaviour {

	public static Vector3 worldPos = Vector3.zero;

	private Vector2 screenDimensions;

    void Start()
    {
        Cursor.visible = false;

		screenDimensions = CalculateScreenSizeInWorldCoords ();
    }

	Vector2 CalculateScreenSizeInWorldCoords () {
		Vector3 p1 = Camera.main.ViewportToWorldPoint(new Vector3(0,0,Camera.main.nearClipPlane));  
		Vector3 p2 = Camera.main.ViewportToWorldPoint(new Vector3(1,0,Camera.main.nearClipPlane));
		Vector3 p3 = Camera.main.ViewportToWorldPoint(new Vector3(1,1,Camera.main.nearClipPlane));
		
		float width = (p2 - p1).magnitude;
		float height = (p3 - p2).magnitude;
		
		Vector2 dimensions = new Vector2(width,height);
		
		return dimensions / 2f;
	}

	void Update () {
        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition) * 2f - Vector3.one;

		pos.x *= screenDimensions.x;
		pos.y *= screenDimensions.y;
		pos.z = 1f;

		transform.localPosition = pos;
        worldPos = pos;

		worldPos = transform.position;
	}
}
