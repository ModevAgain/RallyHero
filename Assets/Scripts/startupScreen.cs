using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class startupScreen : MonoBehaviour {

    private float highscore = 0;
    private float yourScore = 0;


    // hacky singleton-like
    public void Awake()
    {
        DontDestroyOnLoad(this);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }


    void Start () {

        DontDestroyOnLoad(gameObject);
        GameObject.FindGameObjectWithTag("yourScore").GetComponent<Text>().text = "Score: " + yourScore;
        GameObject.FindGameObjectWithTag("highScore").GetComponent<Text>().text = "Highscore: " + highscore;

    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += SceneFinishedLoading;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneFinishedLoading;
    }


    void Update () {
		
	}

    public void loadStartupLevel()
    {

        SceneManager.LoadScene("start");
       
    }

    public void loadMainLevel()
    {
        SceneManager.LoadScene("mainTest");
    }

    public void quitApp()
    {
        Application.Quit();
    }

    public void addScore(float score)
    {
        if(score > highscore)
        {
            highscore = score;
        }

        yourScore = score;
    }

    void SceneFinishedLoading(Scene s, LoadSceneMode m)
    {

        if(s == SceneManager.GetSceneByName("start"))
        {
            GameObject.FindGameObjectWithTag("yourScore").GetComponent<Text>().text = "Score: " + yourScore;
            GameObject.FindGameObjectWithTag("highScore").GetComponent<Text>().text = "Highscore: " + highscore;
            Debug.Log("higscore: " + highscore);
            GameObject.FindGameObjectWithTag("startButton").GetComponent<Button>().onClick.AddListener(loadMainLevel);
            GameObject.FindGameObjectWithTag("quitButton").GetComponent<Button>().onClick.AddListener(quitApp);
        }

        

        Debug.Log("Level Loaded, " + s.name + " , " + m);
    }
}
