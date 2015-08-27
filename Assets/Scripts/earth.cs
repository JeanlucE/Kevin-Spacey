using UnityEngine;
using System.Collections;

public class earth : MonoBehaviour {

	public player player;
	public visibleuniverse universe;

	private Vector3 offset = Vector3.zero;

	void Update () {
		offset.x = player.gameObject.transform.position.x;
		offset.y = player.gameObject.transform.position.y;

		transform.position = offset;

		Vector2 vel = player.GetVelocity();
		float velX = vel.x;
		vel.x = - vel.y / universe.mapDimensions.y;
		vel.y = velX / universe.mapDimensions.x;

		vel *= Time.deltaTime * 360f;

		transform.RotateAround (Vector3.zero, Vector3.right, vel.x);
		transform.RotateAround (Vector3.zero, Vector3.up, vel.y);
	}
}
