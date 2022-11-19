using UnityEngine;

public class MilitaryGunManager : MonoBehaviour
{
    public Transform shootOrigin;

    public bool triggerDown;

    Rigidbody2D rb;

    public enum FireMode { semi, full, bolt }

    [Header("Fire Modes")]
    public FireMode fireMode;
    public float fireRate;

    [Header("Misc")]
    public float recoil;
    public Transform recoilPos;
    public GameObject bulletPrefab;
    public float shotVelocity;
    public ParticleSystem flashParticle;

    float timeSinceShot;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        switch (fireMode)
        {
            case FireMode.full:
                timeSinceShot += Time.deltaTime;
                if (triggerDown && timeSinceShot > fireRate)
                {
                    timeSinceShot = 0.0f;
                    Fire();
                }
                break;
            case FireMode.semi:
                if (triggerDown)
                {
                    Fire();
                    triggerDown = false;
                }
                break;
            case FireMode.bolt:
                if (triggerDown)
                {
                    Fire();
                    triggerDown = false;
                }
                break;
        }
    }
    public void Fire()
    {
        Debug.Log(-transform.right * recoil);
        flashParticle.Play();
        rb.AddForceAtPosition(-transform.right * recoil, recoilPos.position);
        GameObject shot = Instantiate(bulletPrefab, shootOrigin.position, Quaternion.identity);
        shot.GetComponent<Rigidbody2D>().velocity = transform.right * shotVelocity;
    }
}
