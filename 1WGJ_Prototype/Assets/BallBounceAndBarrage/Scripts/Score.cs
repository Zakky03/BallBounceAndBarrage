using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    //テキストを書き換える
    TextMeshProUGUI text;
    //スコア保存
    static private int score, highScore = 0;
    //シーン内のボールの数保存
    public static int ballNum;

    private void Awake()
    {
        //Awakeでボールの数初期化
        ballNum = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        //スコア初期化
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //スコア表記
        text.text = "Score: " + score.ToString()
            + "\nHighScore: " + highScore.ToString();
    }

    static public void scoreAdder()
    {
        score++;
    }
    static public void scoreAdder(int s)
    {
        score += s;
    }

    //ゲームオーバーになる
    static public void GameOver()
    {
        //Debug.Log("GameOver");
        SceneManager.LoadScene("Main");
        if (highScore < score) highScore = score;
    }
}
