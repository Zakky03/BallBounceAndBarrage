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
    public static int score, highScore = 0;
    //シーン内のボールの数保存
    public static int ballNum;

    public static bool Finished { private set; get; }

    void Awake()
    {
        //Awakeでボールの数初期化
        ballNum = 0;
        Finished = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        //スコア初期化
        score = 0;
        highScore = PlayerPrefs.GetInt("HighScore", highScore);
    }

    // Update is called once per frame
    void Update()
    {
        //スコア表記
        text.text = "Score: " + score.ToString()
            + "\nHighScore: " + highScore.ToString();
    }

    //スコアsetter
    static public void ScoreAdder()
    {
        if (Finished) return;
        score++;
    }
    static public void ScoreAdder(int s)
    {
        if (Finished) return;
        score += s;
    }

    //ゲームオーバーになる
    static public void GameOver()
    {
        if (Finished) return;

        Finished = true;

        //if (highScore < score) highScore = score;
        highScore = Mathf.Max(highScore, score);
        PlayerPrefs.SetInt("HighScore", highScore);

        // リザルトシーン読み込み
        SceneManager.LoadScene("ResultScene", LoadSceneMode.Additive);
    }
}
