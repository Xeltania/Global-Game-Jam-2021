using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{

    public Canvas GameUI;
    public Canvas pauseMenuUI;

    public GameController pause;
    bool paused;




    
    // Start is called before the first frame update
    void Start()
    {
        pause = gameObject.GetComponent<GameController>();

    }

    // Update is called once per frame
    void Update()
    {
        //find if game is paused
        paused = pause.gameIsPaused;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                MenuScreen();
            }
            else
            {
                Resume();
            }
        }


    }

    public void MenuScreen()
    {
        pause.PauseGame();

        GameUI.enabled = false;
        pauseMenuUI.enabled = true;
        
    }

    public void Resume()
    {

        pause.ResumeGame();

        GameUI.enabled = true;
        pauseMenuUI.enabled = false;

    }

    public void Restart()
    {

        GameUI.enabled = true;
        pauseMenuUI.enabled = false;

        pause.ResumeGame();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void Controls()
    {

    }

    public void ExitToMainMenu(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
