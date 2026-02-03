using System;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7;

    [SerializeField] private float jumpForce = 700f;
    
    private float coyoteTime;
     
    [SerializeField] private float coyoteTimeDuration = 1f;

    private bool alreadyJumped = false;
    private bool justJumped = false;
    private bool isDead = false;
    
    
    [SerializeField] private InputManager inputManager;

    
    [Header("Ground Check")] 
    
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private Vector2 startPointOffset;

    //[SerializeField] private float groundCheckDistance;
    
    [SerializeField] private float groundCheckRadius;
    
    private float _horizontalInput = 0;
    
    private Rigidbody2D _playerRb;
    private bool _isOnGround;

    private MovingPlatformGrabber _currentGrabber;
    private NewMovingPlatform _currentPlatform;
    
    [Header("Death")] 
    
    public PlayerDeathEffect deathEffect;
    [SerializeField] private float howLongToWaitAfterDeathToRespawn;

    void Start()
    {
        isDead = false;
    }
    
    void Awake()
    {
        _playerRb = GetComponent<Rigidbody2D>();
        coyoteTime = coyoteTimeDuration;
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

        /*if (((_isOnGround || coyoteTime > 0f) && !alreadyJumped) && isJumping && !isDead)
        {
            coyoteTime = 0f;
            //alreadyJumped = true;
            _playerRb.AddForceY(jumpForce, ForceMode2D.Force);
        }*/
        
        //if (_isOnGround || (coyoteTime > 0 && !alreadyJumped) && isJumping && !isDead)
        if((_isOnGround || coyoteTime > 0) && isJumping && !justJumped && !isDead)
        {
            coyoteTime = 0f;
            justJumped = true;
            //alreadyJumped = true;
            _playerRb.AddForceY(jumpForce, ForceMode2D.Force);
        }

        if (!isJumping && _playerRb.linearVelocityY >= 0f)
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
        if (!isDead)
        {
            HandleMovement();
        }
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
        //_isOnGround = Physics2D.Raycast((Vector2)transform.position + startPointOffset, Vector2.down, groundCheckDistance, groundLayer);
        _isOnGround = Physics2D.CircleCast((Vector2)transform.position + startPointOffset, groundCheckRadius, Vector2.down, 0.1f, groundLayer);

    }
    
    void OnDrawGizmos()
    {
        //Debug.DrawLine((Vector2)transform.position + startPointOffset, Vector2.down, _isOnGround ? Color.green : Color.red);
        Gizmos.DrawWireSphere((Vector2)transform.position + startPointOffset, groundCheckRadius);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlatformGrabber"))
        {
            _currentGrabber = other.gameObject.GetComponent<MovingPlatformGrabber>();
            _currentPlatform = other.transform.parent.GetComponent<NewMovingPlatform>();
        }

        if (other.gameObject.CompareTag("Spike"))
        {
            isDead = true;
            deathEffect.PlayDeathEffect();
            StartCoroutine(WaitBeforeReloadingScene(1.5f));
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

    void Update()
    {
        /*if (!_isOnGround && coyoteTime >= 0f)
        {
            coyoteTime -= Time.deltaTime;
            alreadyJumped = false;
        }
        
        if (!_isOnGround && coyoteTime < 0f)
        {
            alreadyJumped = true;
        }

        if (_isOnGround && _playerRb.linearVelocityY <= 0f)
        {
            coyoteTime = coyoteTimeDuration;
            alreadyJumped = false;
        }*/

        /*if (_isOnGround)
        {
            coyoteTime = coyoteTimeDuration;
            if (_playerRb.linearVelocityY <= 0f)
            {
                alreadyJumped = false;
            }
            
        }

        if (!_isOnGround)
        {
            if (coyoteTime > 0f && justJumped)
            {
                coyoteTime = 0f;
                alreadyJumped = true;
            }

            if (coyoteTime > 0f && !justJumped)
            {
                coyoteTime -= Time.deltaTime;
                alreadyJumped = false;
            }

            if (alreadyJumped) coyoteTime = 0f;
        }

        if (coyoteTime <= 0f)
        {
            if (_isOnGround)
            {
                alreadyJumped = false;
                coyoteTime = coyoteTimeDuration;
            }
        }*/

        if (_isOnGround)
        {
            coyoteTime = coyoteTimeDuration;
            justJumped = false;
        }
        else if (_playerRb.linearVelocityY > 0f)
        {
            coyoteTime = 0f;
            justJumped = true;
        }
        else
        {
            coyoteTime -=  Time.deltaTime;
        }
        
        Debug.Log(alreadyJumped);
    }

    IEnumerator WaitBeforeReloadingScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        GameManager.Instance.ResetScene();
    }
    
}
