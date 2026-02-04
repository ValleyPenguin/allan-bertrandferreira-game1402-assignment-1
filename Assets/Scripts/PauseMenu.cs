using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameObject pauseMenu;
    
    
    public void ResumeGame()
    {
        //pauseMenu.SetActive(false);
        //Time.timeScale = 1f;
        inputManager.ResumeInput();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
