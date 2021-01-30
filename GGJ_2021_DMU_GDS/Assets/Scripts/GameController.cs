﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    float score;
    float timeLeft;
    Canvas pausePanel;
    Collider scoreZone;

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

    }

    public float GetTime()
    {
        return timeLeft;
    }

    public float GetScore()
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

        //Disable scripts that still work while timescale is set to 0

    }

    public void ResumeGame()
    {
        Time.timeScale = 1;

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