using UnityEngine;
using UnityEngine.InputSystem;

public class InputTest : MonoBehaviour
{
    private PlayerInputActions _testActions;

    void Awake()
    {
        _testActions = new PlayerInputActions(); // we created an object
        _testActions.Enable();
    }

    void OnEnable()
    {
        _testActions.Player.Jump.performed += Jump;
    }

    void OnDisable()
    {
        _testActions.Player.Jump.performed -= Jump;
    }

    void Jump(InputAction.CallbackContext ctx)
    {
        Debug.Log("Jump");
    }

}
