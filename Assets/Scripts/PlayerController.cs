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

    public MovingPlatformGrabber movingPlatformGrabber;
    public NewMovingPlatform newMovingPlatform;

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

    void HandleJumpInput()
    {
        //apply the jump force
        if (_playerRb == null) return;
        
        if(_isOnGround)
            _playerRb.AddForceY(jumpForce, ForceMode2D.Impulse);
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
        if (_playerRb == null) return;

        // if player is on platform, add linearVelocity of the moving platform to the player
        if (movingPlatformGrabber.playerOnPlatform)
        { 
            _playerRb.linearVelocityX = (moveSpeed * _horizontalInput) + newMovingPlatform.rb.linearVelocityX;
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
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDrawGizmos()
    {
        Debug.DrawLine((Vector2)transform.position + startPointOffset, (Vector2)transform.position + startPointOffset + Vector2.down, _isOnGround ? Color.green : Color.red);
    }
    
}
