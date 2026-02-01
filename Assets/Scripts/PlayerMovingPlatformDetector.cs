using UnityEngine;

public class PlayerMovingPlatform : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlatformGrabber"))
        {
            transform.SetParent(other.transform);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlatformGrabber"))
        {
            transform.SetParent(null);
        }
    }
}
