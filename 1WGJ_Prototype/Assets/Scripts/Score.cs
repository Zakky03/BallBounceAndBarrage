﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    TextMeshProUGUI text;
    static private int score, highScore = 0;
    public static int ballNum;

    private void Awake()
    {
        ballNum = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Score: " + score.ToString()
            + "\nHighScore: " + highScore.ToString();
    }

    static public void scoreAdder(int s)
    {
        score += s;
    }

    static public void GameOver()
    {
        Debug.Log("GameOver");
        SceneManager.LoadScene("Main");
        if (highScore < score) highScore = score;
    }
}
