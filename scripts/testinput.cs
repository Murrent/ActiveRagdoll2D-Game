using UnityEngine;

public class testinput : MonoBehaviour
{
    public InputManager input;

    void Awake()
    {
        input = new InputManager();
        input.Player.Interact.performed += ctx => InputTest();
    }

    void InputTest()
    {
        Debug.Log("int");
    }
}