using UnityEngine;
using System.Collections;

public class gatling : MonoBehaviour
{

    public float shootInterval;
    public GameObject projectile;
    public Vector3 projectileOrigin;
    public float maxHeat; // 1 shot = 1 heat
    public float heatPerShot;
    public float heatLoss; // per sec
    public AnimationCurve indicatorGradient;
    public Color cold, medium, hot;
    public SpriteRenderer indicator;

    private float shootingCooldown = 0;
    public bool shooting = false;
    private float heat = 0;
    private Animator anim = null;
    private AudioSource sound;

    void Awake()
    {
        sound = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // indicator color
        Color indCol;

        float eval = indicatorGradient.Evaluate(heat / maxHeat) * 2f;

        if (eval <= 1)
        {
            indCol = (1f - eval) * cold + eval * medium;
        }
        else
        {
            eval -= 1f;

            indCol = (1f - eval) * medium + eval * hot;
        }

        indicator.color = indCol;

        shootingCooldown -= Time.deltaTime;
        heat = Mathf.Max(0, heat - heatLoss * Time.deltaTime);

        if (shooting && shootingCooldown < 0 && heat < maxHeat)
        {
            sound.Play();
            anim.Play("anim", 0, 0);
            Instantiate(projectile, transform.position + projectileOrigin, Quaternion.identity);
            shootingCooldown = shootInterval;
            heat += heatPerShot;
        }
    }
}
