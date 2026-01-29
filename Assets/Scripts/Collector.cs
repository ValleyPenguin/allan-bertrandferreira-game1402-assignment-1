using UnityEngine;

public class Collector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
       ICollectable otherCollectable = other.GetComponent<ICollectable>();

       if (otherCollectable != null)
       {
           otherCollectable.OnCollect();
       }
    }
}
