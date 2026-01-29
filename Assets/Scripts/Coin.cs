using UnityEngine;

public class Coin : MonoBehaviour, ICollectable
{
    public void OnCollect()
    {
        Debug.Log("Coin collected");
        Destroy(gameObject);
    }
}
