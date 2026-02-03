using UnityEngine;

public class Coin : MonoBehaviour, ICollectable
{
    void Awake()
    {

    }
    
    public void OnCollect()
    {
        GameManager.Instance.AddCoin();
        Destroy(gameObject);
    }
}
