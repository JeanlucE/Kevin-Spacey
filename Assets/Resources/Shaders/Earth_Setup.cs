using UnityEngine;
using System.Collections;

public class Earth_Setup : MonoBehaviour {

	public float cloudSpeed;

	private Renderer r;
	private Vector2 cloudOffset;

	void Start () {
		r = GetComponent<Renderer> ();
	}

	void Update() {
		cloudOffset.x += Time.deltaTime * cloudSpeed;

		r.sharedMaterial.SetTextureOffset ("_Cloud", cloudOffset);

		Vector3 cam = Camera.main.transform.position;
		cam.Normalize ();
		r.sharedMaterial.SetVector ("_CamDir", new Vector4 (cam.x, cam.y, cam.z, 0f));
	}
}
