using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    public static Score instance;


    static int pointsLeft;
    static int score;
    static bool increasing;
    Color fontBaseColor;
    [SerializeField]
    float fontIncreaseFactor=1.1f;

    float fontBaseSize;

    float timeSinceIncrease;

    [SerializeField]
    float timeToNormal;

    static int highscore;
    void Awake()
    {
        instance = this;
        fontBaseColor = GetComponent<Text>().color;
        fontBaseSize = GetComponent<Text>().fontSize;
        highscore = PlayerPrefs.GetInt("highscore", 0);
    }
    

    // Update is called once per frame
    void Update()
    {
        if(pointsLeft>0)
        {
            GetComponent<Text>().fontSize = Mathf.FloorToInt(fontBaseSize * fontIncreaseFactor);
            timeSinceIncrease = 0;
        }
        else
        {
            timeSinceIncrease += Time.deltaTime;
        }

        if(timeSinceIncrease>timeToNormal)
        {
            ReturnToNormal();
        }
        GetComponent<Text>().text = "Score: " + score;
    }

    public static void IncreaseScore(int points)
    {
        pointsLeft += points;
        increasing = true;
        for(int i=0;i<points;i++)
        {
            instance.Invoke("Increase", 0.3f*Mathf.Sqrt(i));
        }
    }

    public void Increase()
    {
        score++;
        pointsLeft--;
        if(pointsLeft==0)
        {
            increasing = false;
        }
    }
    public void ReturnToNormal()
    {
        GetComponent<Text>().fontSize = Mathf.FloorToInt(fontBaseSize);
        GetComponent<Text>().color = fontBaseColor;
    }

    public static int Points
    {
        get
        {
            return score;
        }
        private set
        {
            score = value;
        }
    }

    public static int Highscore
    {
        get
        {
            return highscore;
        }
        private set
        {
            highscore = value;
        }
    }

    public static void Init()
    {
        score = 0;
        highscore= PlayerPrefs.GetInt("highscore", 0);
    }
}
