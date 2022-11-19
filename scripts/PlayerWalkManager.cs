using UnityEngine;

public class PlayerWalkManager : MonoBehaviour
{
    public MuscleManager muscleManager;
    bool interact;
    Vector2 walkDir;
    bool facingRight;
    Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    public void SetLookPosition(Vector2 position)
    {
        if (muscleManager.bodyMuscles[2].bone.transform.position.x > cam.ScreenToWorldPoint(position).x)
            facingRight = false;
        else
            facingRight = true;
    }

    public void SetWalkDirection(Vector2 direction)
    {
        walkDir = direction;
        if (walkDir.y < 0)
        {
            walkAnim = crouchAnim;
        }
        else
        {
            walkAnim = defaultAnim;
        }
    }

    private void FixedUpdate()
    {
        UpdateWalking();
    }

    //---------WALKING----------
    public PlayerFeetManager leftFoot, rightFoot;
    private float stepTime = 0.5f, timeStepped = 0.0f;
    private bool rightStep, leftStep = true, lastWalkDirection;

    public float runSpeedMultiplier, walkForce;

    public WalkingAnimation walkAnim, crouchAnim, defaultAnim;

    public void UpdateWalking()
    {
        if (walkDir.y < 0)
            walkAnim = crouchAnim;
        else
            walkAnim = defaultAnim;

        if (facingRight)
        {
            if (walkDir.x > 0)
            {
                //Normal animation
                UpdateStep(walkAnim, 1, 1);
            }
            else if (walkDir.x < 0)
            {
                //Normal animation backwards
                UpdateStep(walkAnim, 1, -1);
            }
        }
        else
        {
            if (walkDir.x < 0)
            {
                //Negative animation
                UpdateStep(walkAnim, -1, 1);
            }
            else if (walkDir.x > 0)
            {
                //Negative animation backwards
                UpdateStep(walkAnim, -1, -1);
            }
        }

        if (walkDir.y < 0 && walkDir.x == 0)
        {
            if (lastWalkDirection)
            {
                muscleManager.legMuscles[0].restRotation = 90;
                muscleManager.legMuscles[1].restRotation = 0;
                muscleManager.legMuscles[2].restRotation = 0;
                muscleManager.legMuscles[3].restRotation = -90;
            }
            else
            {
                muscleManager.legMuscles[0].restRotation = 0;
                muscleManager.legMuscles[1].restRotation = -90;
                muscleManager.legMuscles[2].restRotation = 90;
                muscleManager.legMuscles[3].restRotation = 0;
            }
        }
        else if (walkDir.x == 0)
        {
            muscleManager.legMuscles[0].restRotation = 5;
            muscleManager.legMuscles[1].restRotation = -5;
            muscleManager.legMuscles[2].restRotation = 5;
            muscleManager.legMuscles[3].restRotation = -5;
        }
    }

    void UpdateStep(WalkingAnimation animation, int direction, float speed)
    {
        if (direction == 0) return;
        muscleManager.bodyMuscles[2].bone
            .AddForce(new Vector2(direction * speed * walkForce * Time.fixedDeltaTime, 0.0f));

        lastWalkDirection = true;
        if (rightStep)
        {
            if (speed > 0
                    ? timeStepped > animation.stepTime
                    : (timeStepped > animation.stepTime / 2 &&
                       timeStepped <= animation.stepTime))
            {
                if (speed > 0)
                {
                    muscleManager.legMuscles[0].restRotation = direction * animation.thighBack;
                    muscleManager.legMuscles[1].restRotation = direction * animation.thighForward;
                    muscleManager.legMuscles[2].restRotation = direction * animation.calfBack;
                    muscleManager.legMuscles[3].restRotation = direction * animation.calfMidAir;
                    leftStep = true;
                    rightStep = false;
                    timeStepped = 0.0f;
                    return;
                }
                else
                {
                    if (timeStepped > animation.stepTime * 3 / 4)
                    {
                        muscleManager.legMuscles[0].restRotation = direction * animation.thighForward;
                        muscleManager.legMuscles[1].restRotation = direction * animation.thighBack;
                    }

                    muscleManager.legMuscles[3].restRotation = direction * animation.calfMidAir;
                }
            }
            else if (speed > 0 ? timeStepped > animation.stepTime / 2 : timeStepped > animation.stepTime)
            {
                if (speed > 0)
                {
                    muscleManager.legMuscles[2].restRotation = direction * animation.calfForward;
                }
                else
                {
                    muscleManager.legMuscles[0].restRotation = direction * animation.thighForward;
                    muscleManager.legMuscles[1].restRotation = direction * animation.thighBack;
                    muscleManager.legMuscles[2].restRotation = direction * animation.calfForward;
                    muscleManager.legMuscles[3].restRotation = direction * animation.calfBack;
                    leftStep = true;
                    rightStep = false;
                    timeStepped = 0.0f;
                    return;
                }
            }
        }
        else if (leftStep)
        {
            if (speed > 0
                    ? timeStepped > animation.stepTime
                    : (timeStepped > animation.stepTime / 2 &&
                       timeStepped <= animation.stepTime))
            {
                if (speed > 0)
                {
                    muscleManager.legMuscles[0].restRotation = direction * animation.thighForward;
                    muscleManager.legMuscles[1].restRotation = direction * animation.thighBack;
                    muscleManager.legMuscles[2].restRotation = direction * animation.calfMidAir;
                    muscleManager.legMuscles[3].restRotation = direction * animation.calfBack;
                    leftStep = false;
                    rightStep = true;
                    timeStepped = 0.0f;
                    return;
                }
                else
                {
                    if (timeStepped > animation.stepTime * 3 / 4)
                    {
                        muscleManager.legMuscles[0].restRotation = direction * animation.thighBack;
                        muscleManager.legMuscles[1].restRotation = direction * animation.thighForward;
                    }

                    muscleManager.legMuscles[2].restRotation = direction * animation.calfMidAir;
                }
            }
            else if (speed < 0 ? timeStepped > animation.stepTime : timeStepped > animation.stepTime / 2)
            {
                if (speed > 0)
                {
                    muscleManager.legMuscles[3].restRotation = direction * animation.calfForward;
                }
                else
                {
                    muscleManager.legMuscles[0].restRotation = direction * animation.thighBack;
                    muscleManager.legMuscles[1].restRotation = direction * animation.thighForward;
                    muscleManager.legMuscles[2].restRotation = direction * animation.calfBack;
                    muscleManager.legMuscles[3].restRotation = direction * animation.calfForward;
                    leftStep = false;
                    rightStep = true;
                    timeStepped = 0.0f;
                    return;
                }
            }
        }

        timeStepped += Time.deltaTime;
    }

    //-----------JUMPING----------------
    public float jumpStrength;
    float jumpCooldown = 0.2f, timeSinceJump;

    public void Jump()
    {
        if (jumpCooldown < Time.time - timeSinceJump && (leftFoot.isGrounded || rightFoot.isGrounded))
        {
            Debug.Log("jumped");
            timeSinceJump = Time.time;
            muscleManager.bodyMuscles[2].bone.velocity =
                new Vector2(muscleManager.bodyMuscles[2].bone.velocity.x, jumpStrength);
        }
    }
}