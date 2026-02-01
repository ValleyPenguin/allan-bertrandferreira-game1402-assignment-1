using UnityEngine;

public class NewMovingPlatform : MonoBehaviour
{
    public Rigidbody2D rb; 
    
    [SerializeField] private float platformSpeed;
    
    //[SerializeField] private float cycleTime = 5f;
    
    [SerializeField] private Transform positionOne;
    
    [SerializeField] private Transform positionTwo;

    private float _currentTime;

    //private Vector2 _distanceBetweenPositions;

    //private float _percentageOfTheDistance;

    //private Vector2 targetPosition;

    //private Transform targetPoint;
    
    private Vector3 direction;
    
    // Old code that moved the platform using rigidbody2D MovePosition, problem is it is glitchy if you try to move while on the platform
    /*void Start()
    {
        targetPoint = positionTwo;
        _distanceBetweenPositions = positionOne.position - positionTwo.position;
    }
    
    // FixedUpdate() because I'm dealing with physics, don't want different FPS to affect the speed
    void FixedUpdate()
    {
        rb.MovePosition(FindTargetPosition(_percentageOfTheDistance));
    }
    
    void Update()
    {
        _currentTime += Time.deltaTime;
        _percentageOfTheDistance = _currentTime / cycleTime;

        if (_currentTime >= cycleTime)
        {
            _currentTime = 0;

            if (targetPoint == positionOne)
            {
                targetPoint = positionTwo;
            }

            if (targetPoint == positionTwo)
            {
                targetPoint = positionOne;
            }
        }
    }

    Vector2 FindTargetPosition(float percentage)
    {
        targetPosition = new Vector2(_percentageOfTheDistance * targetPoint.position.x, _percentageOfTheDistance * targetPoint.position.y);
        
        return targetPosition;
    }*/

    void Start()
    {
        // by default, I set the direction to go towards position two
        direction = (positionTwo.position - transform.position).normalized;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlatformPointOne"))
        {
            direction = (positionTwo.position - transform.position).normalized;
        }
        
        else if (other.gameObject.CompareTag("PlatformPointTwo"))
        {
            direction = (positionOne.position - transform.position).normalized;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = direction * platformSpeed;
    }
}
