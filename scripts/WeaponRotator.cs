using UnityEngine;

public class WeaponRotator : MonoBehaviour
{
    void Update()
    {
        Vector3 dir = Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2) - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        if (Input.mousePosition.x < Screen.width / 2)
            transform.localScale = new Vector3(1, -1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);
    }
}