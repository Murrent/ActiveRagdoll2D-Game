using UnityEngine;

public class SpriteRotationFlip : MonoBehaviour
{
    void Update()
    {
        if (transform.rotation.eulerAngles.z < 270 && 90 < transform.rotation.eulerAngles.z)
            transform.localScale = new Vector3(1, -1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);
    }
}