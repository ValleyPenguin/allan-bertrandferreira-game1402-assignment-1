using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInputActions _playerInputActions;

    public System.Action<bool> OnJump;

    public System.Action<bool> OnPause;
    
    public System.Action<float> OnMove;

    private bool _isPaused = false;
    
    void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        //_playerInputActions.Enable();
    }

    void OnEnable()
    {
        _playerInputActions.Player.Jump.performed += OnJumpPressed;
        _playerInputActions.Player.Jump.canceled += OnJumpPressed;
        _playerInputActions.Player.Pause.performed += OnPausePressed;
        _playerInputActions.Enable();
        //_playerInputActions.Player.Horizontal.performed += OnMovement;
    }

    void OnDisable()
    {
        _playerInputActions.Player.Jump.performed -= OnJumpPressed;
        _playerInputActions.Player.Jump.canceled -= OnJumpPressed;
        _playerInputActions.Player.Pause.performed -= OnPausePressed;
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

    void OnPausePressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if(_isPaused == true) _isPaused = false;
            else if(_isPaused == false) _isPaused = true;
        }
        else
        {
            _isPaused = false;
        }
        
        OnPause?.Invoke(_isPaused);
    }

    public void ResumeInput()
    {
        _isPaused = false;
        OnPause?.Invoke(_isPaused);
    }

    void Update()
    {
        OnMovement();
    }
}
