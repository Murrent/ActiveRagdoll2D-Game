using UnityEngine;

public class FirstGunTest : MonoBehaviour
{
    public Transform shootOrigin;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("shooting");
            RaycastHit2D hit = Physics2D.Raycast(shootOrigin.position, shootOrigin.right);
            if (hit.transform.TryGetComponent<Rigidbody2D>(out Rigidbody2D outRB))
            {
                outRB.AddForceAtPosition(-hit.normal * 1000, hit.point);
            }
            if (hit.transform.TryGetComponent<SpriteDestruction>(out SpriteDestruction outSD))
            {
                outSD.DamageSprite(30, hit.point);
            }
        }
    }
}
