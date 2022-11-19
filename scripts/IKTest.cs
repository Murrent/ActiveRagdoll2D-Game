using UnityEngine;

public class IKTest : MonoBehaviour
{
    public Transform shoulder, elbow, end;
    float fullExtension;
    void Start()
    {
        fullExtension = end.position.x - shoulder.position.x;
        Debug.Log("FE: " + fullExtension);
    }

    void Update()
    {
        float currExtension = (end.position.x - shoulder.position.x) / fullExtension;
        shoulder.localRotation = Quaternion.Euler(0, 0, (currExtension * 90) - 90 );
        elbow.localRotation = Quaternion.Euler(0, 0, 180 - (currExtension * 180));
    }
}
