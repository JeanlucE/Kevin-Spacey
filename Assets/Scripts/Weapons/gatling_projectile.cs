using UnityEngine;
using System.Collections;

public class gatling_projectile : MonoBehaviour, projectile {

    public float damage;
    public float startVelocity;
	public spheremath pos;
	
    private Rigidbody2D r;

    public void OnHitEffect()
    {
        // Fancy shit
    }

    public float? GetDamage()
    {
        return damage;
    }

	void Start()
	{
		transform.parent = pos.GetParent ();
	}

	public void Shoot (Vector2 direction) {

		pos.SetVelocity (startVelocity * direction);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
