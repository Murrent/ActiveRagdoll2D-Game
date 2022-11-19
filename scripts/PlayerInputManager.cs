using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public InputManager input;
    public PlayerWalkManager pwm;
    public PlayerArmManager pam;
    public CameraTransformManager ctm;

    void Awake()
    {
        input = new InputManager();
        input.Player.Interact.performed += ctx => Interact();
        input.Player.DropItem.performed += ctx => DropItem();
        input.Player.Jump.performed += ctx => Jump();
        input.Player.LeftMouse.performed += ctx => LeftMouse(true);
        input.Player.LeftMouse.canceled += ctx => LeftMouse(false);
        input.Player.RightMouse.performed += ctx => RightMouse(true);
        input.Player.RightMouse.canceled += ctx => RightMouse(false);
        input.Player.Mouse.performed += ctx => Aim(ctx.ReadValue<Vector2>());
        input.Player.Walking.performed += ctx => Walk(ctx.ReadValue<Vector2>());
        input.Player.Walking.canceled += ctx => Walk(ctx.ReadValue<Vector2>());
        input.Player.Enable();
    }

    private void OnDisable()
    {
        input.Player.Disable();
    }

    void Interact()
    {
        Debug.Log("Interact");
    }

    void DropItem()
    {
        pam.DropWeapon();
    }

    void Jump()
    {
        pwm.Jump();
    }

    void LeftMouse(bool down)
    {
        pam.Shoot(down);
    }

    void RightMouse(bool down)
    {
        pam.Aim(down);
    }

    void Aim(Vector2 pos)
    {
        pam.SetAimPosition(pos);
        ctm.SetMousePos(pos);
        pwm.SetLookPosition(pos);
    }

    void Walk(Vector2 direction)
    {
        pwm.SetWalkDirection(direction);
        pam.SetWalkDirection(direction);
    }
}