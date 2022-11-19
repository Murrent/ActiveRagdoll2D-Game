using System.Collections;
using UnityEngine;

public class PlayerArmManager : MonoBehaviour
{
    public MuscleManager muscleManager;

    float armStartStrength;
    public float armRestStrength;

    public PlayerHoldHand leftHand, rightHand;

    Vector2 aimPosition;
    Vector2 walkDirection;

    public void SetWalkDirection(Vector2 direction)
    {
        walkDirection = direction;
    }

    public void SetAimPosition(Vector2 pos)
    {
        aimPosition = pos;
    }

    public void Aim(bool state)
    {
        isAiming = state;
    }

    bool shootDown;

    public void Shoot(bool state)
    {
        shootDown = state;
        if (wepBody != null && holding)
        {
            wepBody.GetComponent<MilitaryGunManager>().triggerDown = state;
        }
    }

    private void Start()
    {
        armStartStrength = muscleManager.armMuscles[0].force;
    }

    void FixedUpdate()
    {
        UpdateArms();
        if (shootDown)
        {
            if (leftHand.touched && !lHold)
            {
                lHold = true;
                leftHand.AttachToTouching();
                holding = true;
            }

            if (rightHand.touched && !rHold)
            {
                rHold = true;
                rightHand.AttachToTouching();
                holding = true;
            }
        }
        else if (holding)
        {
            if (lHold)
            {
                leftHand.DetachFromTouching();
                lHold = false;
                holding = false;
                shootDown = false;
            }

            if (rHold)
            {
                rightHand.DetachFromTouching();
                rHold = false;
                holding = false;
                shootDown = false;
            }
        }
    }

    //-------------ARMS-----------------
    public PlayerWeapon noWeapon;
    public PlayerWeapon weapon;
    public Rigidbody2D wepBody;
    public bool isAiming;
    public bool holding, lHold, rHold;
    public bool dropWep;

    public void HoldWeapon()
    {
        if (wepBody != null)
        {
            holding = true;
            weapon = wepBody.GetComponent<WeaponPickUpProperties>().weaponProperties;
            weapon.AttachToBodies(wepBody, muscleManager.armMuscles[2].bone, muscleManager.armMuscles[3].bone);
        }
    }

    public bool DropWeapon()
    {
        holding = false;
        HingeJoint2D[] hj = wepBody.gameObject.GetComponents<HingeJoint2D>();
        FixedJoint2D[] fj = wepBody.gameObject.GetComponents<FixedJoint2D>();
        if (hj.Length != 0 || fj.Length != 0)
        {
            foreach (HingeJoint2D toDel in hj)
                Destroy(toDel);
            foreach (FixedJoint2D toDel in fj)
                Destroy(toDel);
            StartCoroutine(DropWepAfterTime(wepBody.GetComponent<WeaponPickUpProperties>(), 1.0f));
            weapon = noWeapon;
            wepBody = null;
            return true;
        }

        return false;
    }

    IEnumerator DropWepAfterTime(WeaponPickUpProperties wpup, float time)
    {
        yield return new WaitForSeconds(time);

        wpup.ChangeLayer(9);
    }

    public void UpdateArms()
    {
        Vector3 dir = Camera.main.ScreenToWorldPoint(aimPosition) - (Vector3)muscleManager.armMuscles[0].bone.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (isAiming)
        {
            muscleManager.armMuscles[0].force = armStartStrength;
            muscleManager.armMuscles[1].force = armStartStrength;
            muscleManager.armMuscles[2].force = armStartStrength;
            muscleManager.armMuscles[3].force = armStartStrength;

            if (muscleManager.armMuscles[0].bone.position.x > Camera.main.ScreenToWorldPoint(aimPosition).x)
            {
                muscleManager.armMuscles[0].restRotation = angle - weapon.leftArm.armRotation;
                muscleManager.armMuscles[2].restRotation = angle - weapon.leftArm.forearmRotation;
                muscleManager.armMuscles[1].restRotation = angle - weapon.rightArm.armRotation;
                muscleManager.armMuscles[3].restRotation = angle - weapon.rightArm.forearmRotation;
                if (walkDirection.y < 0)
                {
                    muscleManager.bodyMuscles[0].restRotation = 0;
                    muscleManager.bodyMuscles[1].restRotation = 45;
                    muscleManager.bodyMuscles[2].restRotation = 0;
                }
                else
                {
                    muscleManager.bodyMuscles[1].restRotation = 0;
                }
            }
            else
            {
                muscleManager.armMuscles[0].restRotation = angle + weapon.leftArm.armRotation;
                muscleManager.armMuscles[2].restRotation = angle + weapon.leftArm.forearmRotation;
                muscleManager.armMuscles[1].restRotation = angle + weapon.rightArm.armRotation;
                muscleManager.armMuscles[3].restRotation = angle + weapon.rightArm.forearmRotation;
                if (walkDirection.y < 0)
                {
                    muscleManager.bodyMuscles[0].restRotation = 0;
                    muscleManager.bodyMuscles[1].restRotation = -45;
                    muscleManager.bodyMuscles[2].restRotation = 0;
                }
                else
                {
                    muscleManager.bodyMuscles[1].restRotation = 0;
                }
            }
        }
        else
        {
            muscleManager.armMuscles[0].force = armRestStrength;
            muscleManager.armMuscles[1].force = armRestStrength;
            muscleManager.armMuscles[2].force = armRestStrength;
            muscleManager.armMuscles[3].force = armRestStrength;
            if (muscleManager.armMuscles[0].bone.position.x > Camera.main.ScreenToWorldPoint(aimPosition).x)
            {
                muscleManager.armMuscles[0].restRotation = 180 - weapon.leftArm.restArmRotation;
                muscleManager.armMuscles[1].restRotation = 180 - weapon.rightArm.restArmRotation;
                muscleManager.armMuscles[2].restRotation = 180 - weapon.leftArm.restForearmRotation;
                muscleManager.armMuscles[3].restRotation = 180 - weapon.rightArm.restForearmRotation;
                if (walkDirection.y < 0)
                {
                    muscleManager.bodyMuscles[0].restRotation = 0;
                    muscleManager.bodyMuscles[1].restRotation = 45;
                    muscleManager.bodyMuscles[2].restRotation = 0;
                }
                else
                {
                    muscleManager.bodyMuscles[1].restRotation = 0;
                }
            }
            else
            {
                muscleManager.armMuscles[0].restRotation = weapon.leftArm.restArmRotation;
                muscleManager.armMuscles[1].restRotation = weapon.rightArm.restArmRotation;
                muscleManager.armMuscles[2].restRotation = weapon.leftArm.restForearmRotation;
                muscleManager.armMuscles[3].restRotation = weapon.rightArm.restForearmRotation;
                if (walkDirection.y < 0)
                {
                    muscleManager.bodyMuscles[0].restRotation = 0;
                    muscleManager.bodyMuscles[1].restRotation = -45;
                    muscleManager.bodyMuscles[2].restRotation = 0;
                }
                else
                {
                    muscleManager.bodyMuscles[1].restRotation = 0;
                }
            }
        }
    }
}