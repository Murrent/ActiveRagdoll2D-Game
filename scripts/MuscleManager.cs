using UnityEngine;

public class MuscleManager : MonoBehaviour
{
    public _Muscle[] bodyMuscles;
    public _Muscle[] legMuscles;
    public _Muscle[] armMuscles;

    public PlayerWalkManager walkManager;
    public PlayerArmManager armManager;
    public bool useInputs;

    private void FixedUpdate()
    {
        foreach (_Muscle muscle in bodyMuscles)
        {
            if (!muscle.dead)
                muscle.ActivateMuscle();
        }

        foreach (_Muscle muscle in legMuscles)
        {
            if (!muscle.dead)
                muscle.ActivateMuscle();
        }

        foreach (_Muscle muscle in armMuscles)
        {
            if (!muscle.dead)
                muscle.ActivateMuscle();
        }
    }
}

[System.Serializable]
public class _Muscle
{
    public Rigidbody2D bone;
    public float restRotation;
    public float force;
    public bool dead;

    public void ActivateMuscle()
    {
        bone.MoveRotation(Mathf.LerpAngle(bone.rotation, restRotation, force * Time.fixedDeltaTime));
    }
    //Reminder
    //Moverotation simply moves the body into the right rotation
    //BUT the more mass the body has, the stronger it moves
}