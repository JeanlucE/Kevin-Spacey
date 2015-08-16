using UnityEngine;
using System.Collections;

public class gatling_projectile : MonoBehaviour, projectile {

    public float damage;
    public Vector2 startForce;

    private Rigidbody2D r;

    public void OnHitEffect()
    {
        // Fancy shit
    }

    public float? GetDamage()
    {
        return damage;
    }

	// Use this for initialization
	void Awake () {
        r = GetComponent<Rigidbody2D>();
        r.AddRelativeForce(startForce);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
