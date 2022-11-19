using UnityEngine;

public class TargetJointManager : MonoBehaviour
{
    public Transform target;
    TargetJoint2D tj;

    void Start()
    {
        tj = GetComponent<TargetJoint2D>();
    }

    void Update()
    {
        tj.target = target.position;
    }
}