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
    private static int score, highScore = 0;
    //シーン内のボールの数保存
    public static int ballNum;

    private static bool finished;

    void Awake()
    {
        //Awakeでボールの数初期化
        ballNum = 0;
        finished = false;
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

    //スコアsetter
    static public void ScoreAdder()
    {
        score++;
    }
    static public void ScoreAdder(int s)
    {
        score += s;
    }

    //ゲームオーバーになる
    static public void GameOver()
    {
        if (finished) return;

        finished = true;
        
        //とりあえずシーン再読み込み
        if (highScore < score) highScore = score;

        // スコア登録 by tada
        if(naichilab.RankingLoader.Instance != null) naichilab.RankingLoader.Instance.SendScoreAndShowRanking(score);
        // SceneManager.LoadScene("Main");
    }
}
