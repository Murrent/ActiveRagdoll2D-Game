using UnityEngine;

[CreateAssetMenu(menuName = "Player Walking Animation")]
public class WalkingAnimation : ScriptableObject
{
    public float thighForward, calfForward, thighBack, calfBack, calfMidAir, stepTime = 0.5f;
}