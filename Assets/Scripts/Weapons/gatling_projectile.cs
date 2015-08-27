using UnityEngine;
using System.Collections;

public class gatling_projectile : MonoBehaviour, projectile {

    public float damage;
    public float startVelocity;
	
    private Rigidbody2D r;

	void Start()
	{
		r = GetComponent<Rigidbody2D> ();

		r.AddForce ((Vector2)transform.up * startVelocity + player.GetVelocity(), ForceMode2D.Impulse);
	}

    public void OnHitEffect()
    {
        // Fancy shit
    }

    public float GetDamage()
    {
        return damage;
    }
}
