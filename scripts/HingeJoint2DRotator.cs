using UnityEngine;

public class HingeJoint2DRotator : MonoBehaviour
{
    public float desiredRotation;
    public float strength;
    private HingeJoint2D hj;
    private Rigidbody2D rb;
    void Start()
    {
        hj = GetComponent<HingeJoint2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Quaternion desired = new Quaternion();
        desired.eulerAngles = new Vector3(0, 0, desiredRotation);
        Quaternion rotDiff = Quaternion.Lerp(transform.localRotation, desired, strength);
        Debug.Log(rotDiff.eulerAngles);
        //rb.AddTorque(strength * rotDiff * Time.deltaTime);
    }
}
