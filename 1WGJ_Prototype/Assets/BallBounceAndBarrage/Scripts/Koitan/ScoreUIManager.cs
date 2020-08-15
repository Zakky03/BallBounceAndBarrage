using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using System;

public class ScoreUIManager : MonoBehaviour
{
    [SerializeField]
    RectTransform rect;
    [SerializeField]
    Transform tfm;
    [SerializeField]
    TextMeshProUGUI scoreText;
    [SerializeField]
    int debugScore;
    [SerializeField]
    int stackScore;
    [SerializeField]
    GameObject unlockedObj;
    // Start is called before the first frame update
    void Start()
    {
        //初期化
        unlockedObj.SetActive(false);
        //text表示    
        scoreText.text = debugScore + Environment.NewLine + stackScore;
        //バー増やす
        DOTween.To
            (
            () => stackScore,　     //何に
            (n) => stackScore = n,　//何を
            stackScore + debugScore,　              //どこまで(最終的な値)
            1f               //どれくらいの時間
            )
            .SetEase(Ease.InExpo)
            .OnComplete(() =>
            {
                tfm.DOPunchPosition(Vector3.right * 10f, 0.5f);
                unlockedObj.SetActive(true);
            }
            );
    }

    // Update is called once per frame
    void Update()
    {
        //text更新
        scoreText.text = debugScore + Environment.NewLine + stackScore;
        //バー更新  
        Vector2 tmp = rect.sizeDelta;
        tmp.x = stackScore;
        rect.sizeDelta = tmp;
    }
}
