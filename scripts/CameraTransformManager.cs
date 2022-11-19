using UnityEngine;

public class CameraTransformManager : MonoBehaviour
{
    public Transform anchor;
    public float speed;
    public float lookMultiplier;
    private Vector3 inputPos;
    private Vector2 mousePos;
    public void SetMousePos(Vector2 pos)
    {
        mousePos = pos;
    }
    void Update()
    {
        inputPos = lookMultiplier * (mousePos - new Vector2(Screen.width / 2, Screen.height / 2)) / Screen.width;
        transform.position = Vector3.Lerp(
            transform.position,
            anchor.position + inputPos,
            speed * Time.deltaTime);
    }
}
