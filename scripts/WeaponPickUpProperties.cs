using UnityEngine;

public class WeaponPickUpProperties : MonoBehaviour
{
    public PlayerWeapon weaponProperties;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            if (other.transform.parent.parent.GetComponent<PlayerArmManager>().holding)
                return;
            other.transform.parent.parent.GetComponent<PlayerArmManager>().wepBody = GetComponent<Rigidbody2D>();
            other.transform.parent.parent.GetComponent<PlayerArmManager>().weapon = weaponProperties;
            other.transform.parent.parent.GetComponent<PlayerArmManager>().HoldWeapon();
            ChangeLayer(8);
        }
    }

    public void ChangeLayer(int layer)
    {
        SetLayerRecursively(gameObject, layer);
    }

    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }
}