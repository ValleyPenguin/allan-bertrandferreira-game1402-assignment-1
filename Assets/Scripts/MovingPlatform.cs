using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    [SerializeField] private float cycleTime = 5f;
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    
    private float _currentTime = 0f;
    private float _speed = 1f;
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _currentTime += _speed * Time.deltaTime;
        
        if(_currentTime > cycleTime) _speed = -1f;
        if (_currentTime < 0f) _speed = 1f;
        
        float t = _currentTime / cycleTime;
        transform.position = Vector3.Lerp(pointA.position, pointB.position, t);
    }
}
