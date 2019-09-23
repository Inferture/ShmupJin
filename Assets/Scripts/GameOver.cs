using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{

    [SerializeField]
    bool victory = false;
    [SerializeField]
    int boardSize;
    [SerializeField]
    Text scoreName;


    // Start is called before the first frame update

    void Start()
    {
        OnEnable();
    }
    void OnEnable()
    {
        scoreName.text = "???";
        scoreName.GetComponent<InputField>().Select();
        scoreName.GetComponent<InputField>().ActivateInputField();
        
        if (Score.Points <= PlayerPrefs.GetInt("highscore" + boardSize, -1))
        {
            scoreName.gameObject.SetActive(false);
            Results();
        }

    }

    public void Results()
    {
        string scoreTag = scoreName.text;
        scoreName.gameObject.SetActive(false);
        Text results = GetComponentInChildren<Text>();
        results.text = "Game Over \nScore: " + Score.Points;
        if (Score.Points > Score.Highscore)
        {
            results.text += "\n<color=yellow>New highscore !!!</color>\n";
            results.text += "<color=yellow>Highscore: " + Score.Points + "</color>\n";
            results.text += "(Last highscore: " + Score.Highscore + ")";
        }
        else
        {
            results.text += "\nHighscore: " + Score.Highscore;
        }

        results.text += "\n\nScoreboard: \n\n";

        int highscore = Mathf.Max(PlayerPrefs.GetInt("highscore", 0), Score.Points);
        PlayerPrefs.SetInt("highscore", highscore);

        int place = boardSize;

        int[] boardScores = new int[boardSize];
        string[] boardTags = new string[boardSize];

        for (int i = 0; i < boardSize; i++)
        {
            boardScores[i] = PlayerPrefs.GetInt("highscore" + (i + 1),-1);
            boardTags[i] = PlayerPrefs.GetString("scoreTag" + (i + 1), "???");
            Debug.Log("Score " + i + ":" + boardScores[i]);
        }
        while (place > 0 && Score.Points > boardScores[place - 1])
        {
            place--;
            Debug.Log("aah");

        }
        for (int i = boardSize - 1; i > place; i--)
        {
            boardScores[i] = boardScores[i - 1];
            boardTags[i] = boardTags[i - 1];
        }
        if (place < boardSize)
        {
            boardScores[place] = Score.Points;
            boardTags[place] = scoreTag;
        }
        for (int i = 0; i < boardSize; i++)
        {
            if (boardScores[i] >= 0)
            {
                PlayerPrefs.SetInt("highscore" + (i + 1), boardScores[i]);
                PlayerPrefs.SetString("scoreTag" + (i + 1), boardTags[i]);
                results.text += boardTags[i] + " : " + boardScores[i] + "\n";
            }

        }

        results.text += "\n\nThanks for playing !\n\nRestart (Shoot)";

        Score.Init();
    }

    // Update is called once per frame
    void Update()
    {
        if(scoreName.isActiveAndEnabled)
        {
            OnEnable();
        }
        if (Controls.GetActionDown(KeyStrings.key_shoot))
        {
            SceneManager.LoadScene("Intro");
        }
    }
}
