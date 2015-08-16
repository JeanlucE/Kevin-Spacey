using UnityEngine;
using System.Collections;

public class crosshair : MonoBehaviour {

    public static Vector2 crossHairPos = Vector2.zero;

    void Awake()
    {
        Cursor.visible = false;
    }

	void Update () {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        transform.position = pos;

        crossHairPos = pos;
	}
}
