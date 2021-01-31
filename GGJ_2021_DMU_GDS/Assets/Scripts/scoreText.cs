using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreText : MonoBehaviour
{
    [SerializeField] Text scoreToText;

    public GameController score;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(score == null)
        {
            scoreToText.text = "No controller attached.";
        }
        else
        {
            scoreToText.text = "Score: " + score.GetScore().ToString();
        }
    }

    void Awake()
    {
        score = gameObject.GetComponent<GameController>();
        scoreToText = gameObject.GetComponent<Text>();


    }

}
