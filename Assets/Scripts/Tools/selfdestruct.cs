using UnityEngine;
using System.Collections;

public class selfdestruct : MonoBehaviour {

    public float lifeTime;

    private float startTime;

	void Awake () {
        Destroy(gameObject, lifeTime);
	}
}
