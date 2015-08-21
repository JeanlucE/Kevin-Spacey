using UnityEngine;
using System.Collections;

public class selfdestruct : MonoBehaviour {

    public float lifeTime;

    private float startTime;

	void Awake () {
		StartCoroutine (Destruct ());
	}

	private IEnumerator Destruct()
	{
		yield return new WaitForSeconds(lifeTime);

		if (transform.parent != null)
			Destroy (transform.parent.gameObject);
		else
			Destroy (gameObject);
	}
}
