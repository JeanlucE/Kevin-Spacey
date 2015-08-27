using UnityEngine;
using System.Collections;

public class visibleuniverse : MonoBehaviour {

	public Color gizmo;
	public Vector2 mapDimensions = Vector2.zero;
	
	void OnDrawGizmos()
	{
		Gizmos.color = gizmo;
		Gizmos.DrawCube (Vector3.zero, mapDimensions);
	}
}
