using UnityEngine;

public class BulletManager : MonoBehaviour
{
    Vector2 prevPos;
    public LayerMask layermask;
    public GameObject particle;
    public float particleOffset;
    private void OnEnable()
    {
        prevPos = transform.position;
    }
    void Update()
    {
        bool didHit = false;
        Vector2 diff = (Vector2)transform.position - prevPos;
        RaycastHit2D hit = Physics2D.Raycast(prevPos, diff.normalized, diff.magnitude, layermask);
        if (hit.collider != null)
        {
            GameObject particleObj = Instantiate(particle, hit.point + hit.normal * particleOffset, Quaternion.FromToRotation(Vector2.up, hit.normal));
            particle.GetComponent<ParticleSystem>().Play();
            didHit = true;
            if (hit.transform.TryGetComponent<Rigidbody2D>(out Rigidbody2D outRB))
            {
                outRB.AddForceAtPosition(-hit.normal * 100, hit.point);
            }
            if (hit.transform.TryGetComponent<SpriteDestruction>(out SpriteDestruction outSD))
            {
                outSD.DamageSprite(5, hit.point);
            }
        }
        if (didHit)
            Destroy(gameObject);
        prevPos = transform.position;
    }
}
