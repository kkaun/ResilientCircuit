using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitApplication();
        }
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void GoToDeathMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
    }

    public void GoToEndLevelMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
