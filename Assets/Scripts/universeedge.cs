using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
[ExecuteInEditMode]
public class universeedge : MonoBehaviour {

	public Transform player;
	public visibleuniverse universe;
	public Vector2 teleportationVector = Vector2.zero;
	public Color gizmo;

	private Collider2D c;

	void Awake()
	{
		c = GetComponent<Collider2D> ();
	}

	void Update()
	{
		if (c.OverlapPoint(player.transform.position))
		{
			Vector2 tp;
			tp.x = teleportationVector.x * universe.mapDimensions.x;
			tp.y = teleportationVector.y * universe.mapDimensions.y;

			player.Translate(tp, Space.World);
		}
	}
	
	void OnDrawGizmos()
	{
		Gizmos.color = gizmo;
		Gizmos.DrawCube (transform.position, c.bounds.size);
	}
}
