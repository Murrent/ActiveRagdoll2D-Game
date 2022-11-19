using UnityEngine;

[CreateAssetMenu(menuName = "Player Weapon Asset")]
public class PlayerWeapon : ScriptableObject
{
    public WeaponGrip leftArm;
    public WeaponGrip rightArm;

    [System.Serializable]
    public class WeaponGrip
    {
        public bool enabled;
        public bool fixedRotation, fixedGrip;
        public float weaponRotation, armRotation, forearmRotation, restArmRotation, restForearmRotation;
        public Vector2 anchor, connectedAnchor;

        public void AttachToBody(Rigidbody2D thisBody, Rigidbody2D otherBody)
        {
            if (!enabled) return;
            if (fixedGrip)
            {
                FixedJoint2D fj = thisBody.gameObject.AddComponent<FixedJoint2D>();
                fj.connectedBody = otherBody;
                fj.autoConfigureConnectedAnchor = false;
                fj.anchor = anchor;
                fj.connectedAnchor = connectedAnchor;
            }
            else
            {
                HingeJoint2D hj = thisBody.gameObject.AddComponent<HingeJoint2D>();
                hj.connectedBody = otherBody;
                hj.autoConfigureConnectedAnchor = false;
                hj.anchor = anchor;
                hj.connectedAnchor = connectedAnchor;
            }
        }
    }

    public void AttachToBodies(Rigidbody2D itemToAttach, Rigidbody2D leftHand, Rigidbody2D rightHand)
    {
        leftArm.AttachToBody(itemToAttach, leftHand);
        rightArm.AttachToBody(itemToAttach, rightHand);
    }
}