using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    int score;
    float timeLeft;
    Canvas pausePanel;
    Collider scoreZone;

    bool gameIsPaused { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = 120;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        Debug.Log("TimeLeft: " + timeLeft);
        Debug.Log("Timer: " + GetTimeAsString());
        Debug.Log("Score: " + score);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        if(timeLeft <= 0)
        {
            //End the game, show score and option to play again or return to menu
            //Pause the game, Hide Game UI and disallow player control - WASD, Esc, etc...
            //Display end game canvas
            //Play Again - Reload scene
            //Menu - Load Menu Scene
        }


    }

    public float GetTime()
    {
        return timeLeft;
    }

    public int GetScore()
    {
        return score;
    }

    //Might be moved to UIScript Update()
    public string GetTimeAsString()
    {
        string result;
        int timeAsSeconds;
        int timeAsMinutes;

        timeAsSeconds = (int)timeLeft % 60;
        timeAsMinutes = ((int)timeLeft / 60);

        result = timeAsMinutes.ToString() + ":" + timeAsSeconds.ToString();

        return result;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        gameIsPaused = true;
        //Disable scripts that still work while timescale is set to 0

    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        gameIsPaused = false;
        //Enable scripts that still work while timescale is set to 0
    }

    public void AddScore()
    {
        score++;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collider Hit");
        if(other.tag == "Sheep")
        {
            Debug.Log("Is Sheep");
            AddScore();
            other.gameObject.GetComponent<SheepTemp>().StopSheep(); //Function which deactivates
        }
    }
}
