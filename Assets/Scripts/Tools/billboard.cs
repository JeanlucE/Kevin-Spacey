using UnityEngine;
using System.Collections;

public class billboard : MonoBehaviour {

	void LateUpdate() {
		transform.LookAt(Camera.main.transform.position, -Vector3.up);
	}
}
