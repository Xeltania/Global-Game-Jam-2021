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

    public bool gameIsPaused { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        timeLeft = 120;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        Debug.Log("TimeLeft: " + timeLeft);
        Debug.Log("Timer: " + GetTimeAsString());

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
        int timeAsSeconds = (int)timeLeft % 60;
        int Digit1;
        int Digit2;
        int timeAsMinutes;

        Digit1 = (int)timeAsSeconds / 10;
        Digit2 = (int)timeAsSeconds % 10;

        timeAsMinutes = ((int)timeLeft / 60);

        result = timeAsMinutes.ToString() + ":" + Digit1.ToString() + Digit2.ToString();

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

    void OnTriggerEnter(Collider otherActor)
    {
        if(otherActor.gameObject.tag == "Sheep")
        {
            score += 1;
            //otherActor.gameObject.GetComponent<SheepScript>().Scored(); //Function which deactivates
        }
    }
}
