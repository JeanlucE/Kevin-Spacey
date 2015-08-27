using UnityEngine;
using System.Collections;

public class thruster : MonoBehaviour {

	public float maxDirectionalVelocity;
	public float maxAccelerationPerSecond;
	public AnimationCurve accelerationProfile; // set this to clamp!

	public ParticleSystem ps;
	public Light light;
	public float maxParticleSpeed, maxLightIntensity;

	public void Awake()
	{
		ps.startSize *= transform.localScale.z / 0.1854878f;
	}

	public void DisplayThrust(Vector2 targetDirectionWorldNormalized)
	{
		float interpolated = Mathf.Max(0f, Vector2.Dot (targetDirectionWorldNormalized, transform.up));

		ps.startSpeed = interpolated * maxParticleSpeed;
		light.intensity = interpolated * maxLightIntensity;
	}
}
