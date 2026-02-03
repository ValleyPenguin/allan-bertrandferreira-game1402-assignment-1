using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInputActions _playerInputActions;

    public System.Action<bool> OnJump;

    public System.Action<float> OnMove;
    
    void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();
    }

    void OnEnable()
    {
        _playerInputActions.Player.Jump.performed += OnJumpPressed;
        _playerInputActions.Player.Jump.canceled += OnJumpPressed;
        //_playerInputActions.Player.Horizontal.performed += OnMovement;
    }

    void OnDisable()
    {
        _playerInputActions.Player.Jump.performed -= OnJumpPressed;
        _playerInputActions.Player.Jump.canceled -= OnJumpPressed;
        _playerInputActions.Disable();
        //_playerInputActions.Player.Horizontal.performed -= OnMovement;
    }

    void OnJumpPressed(InputAction.CallbackContext context)
    {
        OnJump?.Invoke(context.performed);
    }

    void OnMovement()
    {
        //Debug.Log("Move Value = " + context.ReadValue<float>());
        OnMove?.Invoke(_playerInputActions.Player.Horizontal.ReadValue<float>());
    }

    void Update()
    {
        OnMovement();
    }
}
