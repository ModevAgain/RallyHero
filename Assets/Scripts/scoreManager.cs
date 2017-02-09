using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreManager : MonoBehaviour {

    [SerializeField]
    Text scoreText;
    [SerializeField]
    Text multiText;
    [SerializeField]
    Text debugger;
    [SerializeField]
    int multiplyThreshold = 10;
    [SerializeField]
    int failureThreshold = 10;

    int failureCounter = 0;

    int multiplier = 1;
    int multiplyCounter = 0;
    float score = 100;

    // Use this for initialization
    void Start () {
        scoreText.text = "Score: " + score;
	}
	
    public void alterScore(float x)
    {
       if(x>0) multiplyCounter++;

        if(multiplyCounter > multiplyThreshold)
        {
            
            multiplier++;
            GameObject.FindGameObjectWithTag("Player").GetComponent<carScript>().alterSpeed(multiplier + 1);
            multiText.text = "x" + multiplier;
            multiplyCounter = 0;
        }
        score += x * multiplier;
        scoreText.text = "Score: " + score;
    }

    public void resetMultiplier()
    {
        debugger.text = "failures: " + failureCounter;

        failureCounter++;

        if(failureCounter > failureThreshold)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<carScript>().alterSpeed(1);
            multiplier = 1;
            multiText.text = "x" + multiplier;
            multiplyCounter = 0;
            failureCounter = 0;
        }

        
    }

	// Update is called once per frame
	void Update () {
		
	}

    public void runEnded()
    {
        GameObject.FindGameObjectWithTag("serialized").GetComponent<startupScreen>().addScore(score);
        GameObject.FindGameObjectWithTag("serialized").GetComponent<startupScreen>().loadStartupLevel();
    }
}
