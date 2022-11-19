using UnityEngine;

public class PlayerMovementManager : MonoBehaviour
{
    public float walkingSpeed, runMultiplier, jumpSpeed;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 finalVel = rb.velocity;
        if (Input.GetAxis("Horizontal") != 0)
        {
            finalVel.x = Input.GetAxis("Horizontal") * walkingSpeed;
            if (Input.GetAxis("RunKey") > 0)
                finalVel.x *= runMultiplier;
        }

        if (Input.GetAxis("Jump") > 0)
        {
            finalVel.y = jumpSpeed;
        }

        rb.velocity = finalVel;
    }
}