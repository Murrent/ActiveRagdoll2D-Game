using UnityEngine;

public class GameSceneInputManager : MonoBehaviour
{
    public InputManager input;
    public PlayerWalkManager pwm;
    public PlayerArmManager pam;
    void Awake()
    {
        input = new InputManager();
        input.Player.Interact.performed += ctx => Interact();
        input.Player.DropItem.performed += ctx => DropItem();
        input.Player.Jump.performed += ctx => Jump();
        input.Player.Mouse.performed += ctx => Aim(ctx.ReadValue<Vector2>());
        input.Enable();
    }
    private void OnDisable()
    {
        input.Disable();
    }
    void Interact()
    {
        Debug.Log("Interact");
    }
    void DropItem()
    {
        Debug.Log("DropItem");
    }
    void Jump()
    {
        Debug.Log("Jump");
    }
    void Aim(Vector2 pos)
    {
        //Debug.Log(pos);
    }
}
