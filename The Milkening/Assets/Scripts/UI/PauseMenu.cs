using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool paused;
    private GameObject pauseMenu;
    private GameObject[] buttons;

    private void Awake()
    {
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        buttons = GameObject.FindGameObjectsWithTag("Button");

        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Pause();
    }

    public void Pause()
    {
        paused = !paused;
        pauseMenu.SetActive(paused);

        Cursor.visible = paused;
        if (paused)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;
        
        if (paused)
        {
            Time.timeScale = 0;
            foreach(GameObject button in buttons)
                button.GetComponent<HoverSelect>().Reset();
        }
        else
            Time.timeScale = 1;
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
