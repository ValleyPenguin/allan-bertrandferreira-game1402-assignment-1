using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;

    [SerializeField] private float jumpForce = 15f;
    
    
    [SerializeField] private InputManager inputManager;

    
    [Header("Ground Check")] 
    
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private Vector2 startPointOffset;

    [SerializeField] private float groundCheckDistance;
    
    private float _horizontalInput = 0;
    
    private Rigidbody2D _playerRb;
    private bool _isOnGround;

    private MovingPlatformGrabber _currentGrabber;
    private NewMovingPlatform _currentPlatform;

    void Awake()
    {
        _playerRb = GetComponent<Rigidbody2D>();
    }
    
    void OnEnable()
    {
        inputManager.OnJump += HandleJumpInput;
        inputManager.OnMove += HandleMoveInput;
    }

    void OnDisable()
    {
        inputManager.OnJump -= HandleJumpInput;
        inputManager.OnMove -= HandleMoveInput;
    }

    void HandleJumpInput(bool isJumping)
    {
        //apply the jump force
        if (_playerRb == null) return;

        if (_isOnGround && isJumping)
        {
            _playerRb.AddForceY(jumpForce, ForceMode2D.Impulse); 
        }

        if (!isJumping)
        {
            // multiplying linear velocity by .5 rather than setting it to 0, so player doesn't instantly stop going up, so its smoother
            _playerRb.linearVelocityY *= .5f;
        }
        
    }

    void HandleMoveInput(float value)
    {
        _horizontalInput = value;
    }

    void FixedUpdate()
    {
        HandleMovement();
        GroundCheck();
    }

    void HandleMovement()
    {
        // if player is on platform, add linearVelocity of the moving platform to the player
        //if (_currentGrabber.playerOnPlatform)
        
        if (_playerRb == null) return;
        
        if (_currentGrabber != null && _currentPlatform != null)
        { 
            _playerRb.linearVelocityX = (moveSpeed * _horizontalInput) + _currentPlatform.rb.linearVelocityX;
        }

        else
        {
            _playerRb.linearVelocityX = moveSpeed * _horizontalInput;
        }
    }

    void GroundCheck()
    {
        _isOnGround = Physics2D.Raycast((Vector2)transform.position + startPointOffset, Vector2.down, groundCheckDistance, groundLayer);
    }
    
    void OnDrawGizmos()
    {
        Debug.DrawLine((Vector2)transform.position + startPointOffset, (Vector2)transform.position + startPointOffset + Vector2.down, _isOnGround ? Color.green : Color.red);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlatformGrabber"))
        {
            _currentGrabber = other.gameObject.GetComponent<MovingPlatformGrabber>();
            _currentPlatform = other.transform.parent.GetComponent<NewMovingPlatform>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlatformGrabber"))
        {
            _currentGrabber = null;
            _currentPlatform  = null; 
        }
    }
    
}
