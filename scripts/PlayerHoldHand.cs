using UnityEngine;

public class PlayerHoldHand : MonoBehaviour
{
    public bool touched;
    public GameObject touching;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        touched = true;
        touching = collision.gameObject;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (GetComponents<FixedJoint2D>().Length != 0) return;
        touched = false;
        touching = null;
    }

    public void AttachToTouching()
    {
        FixedJoint2D fj = gameObject.AddComponent<FixedJoint2D>();
        if (touching.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            fj.connectedBody = rb;
        }
    }

    public void DetachFromTouching()
    {
        touched = false;
        touching = null;
        FixedJoint2D[] joints = GetComponents<FixedJoint2D>();
        foreach (FixedJoint2D toDel in joints)
            Destroy(toDel);
    }
}