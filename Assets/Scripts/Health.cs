using UnityEngine;

public class Health : MonoBehaviour, ICollectable
{
    public void OnCollect()
    {
        Debug.Log("Health collected");
        Destroy(gameObject);
    }
}
