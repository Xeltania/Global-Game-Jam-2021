using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class countDown : MonoBehaviour
{

    [SerializeField] Text countdownText;

    public GameController time;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        countdownText.text = time.GetTimeAsString();
    }

    private void Awake()
    {
        time = gameObject.GetComponent<GameController>();
        countdownText = gameObject.GetComponent<Text>();
    }

}

