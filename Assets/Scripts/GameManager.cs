using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TMP_Text coinText;
    public TMP_Text timerText;
    
    [SerializeField] private InputManager inputManager;
    
    [SerializeField] private int coinCount = 0;
    
    [SerializeField] private GameObject pauseMenu;

    private float _levelTimer = 0f;

    // Singleton since there will only be one of this script in the game, and I want it easily accessible by the coin prefabs
    public static GameManager Instance { get; private set; }

    void OnEnable()
    {
        inputManager.OnPause += Pause;
    }

    void OnDisable()
    {
        inputManager.OnPause -= Pause;
    }
    
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

    void Start()
    {
        Time.timeScale = 1;
    }
    
    void Update()
    {
        _levelTimer += Time.deltaTime;
        
        string minutes = ((int)_levelTimer / 60).ToString("00");
        string seconds = ((int)_levelTimer % 60).ToString("00");
        
        coinText.text = coinCount.ToString();
        timerText.text = minutes + ":" + seconds;
    }

    public void Pause(bool isPaused)
    {
        if (isPaused)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }

        if (!isPaused)
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
    }
}
