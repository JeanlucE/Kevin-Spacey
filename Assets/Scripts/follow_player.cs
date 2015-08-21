using UnityEngine;
using System.Collections;

public class follow_player : MonoBehaviour {

	public Transform player;

	void Update () {
		transform.position = player.position - player.forward * 5f;
		transform.LookAt (Vector3.zero, transform.up);
	}
}
