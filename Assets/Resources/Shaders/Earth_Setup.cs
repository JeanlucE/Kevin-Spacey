using UnityEngine;
using System.Collections;

public class Earth_Setup : MonoBehaviour {

	public float cloudSpeed;

	private Renderer r;
	private Vector2 cloudOffset;

	void Start () {
		r = GetComponent<Renderer> ();
		r.sharedMaterial.SetVector ("_CamDir", new Vector4 (0f, 0f, -1f, 0f));
	}

	void Update() {
		cloudOffset.x += Time.deltaTime * cloudSpeed;

		r.sharedMaterial.SetTextureOffset ("_Cloud", cloudOffset);
	}
}
