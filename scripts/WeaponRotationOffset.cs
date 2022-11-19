using UnityEngine;

public class WeaponRotationOffset : MonoBehaviour
{
    void Update()
    {
        Vector3 dir = Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2) - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}