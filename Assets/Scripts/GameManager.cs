using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TMP_Text coinText;
    
    [SerializeField] private int coinCount = 0;

    // Singleton since there will only be one of this script in the game, and I want it easily accessible by the coin prefabs
    public static GameManager Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }

        else
        {
            Instance = this;
        }
    }
    public void AddCoin()
    {
        coinCount++;
        Debug.Log(coinCount);
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Update()
    {
        coinText.text = coinCount.ToString();
    }
}
