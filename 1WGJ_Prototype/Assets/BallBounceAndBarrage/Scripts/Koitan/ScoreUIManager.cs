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
    Image bow;
    [SerializeField]
    Image backBow;
    [SerializeField]
    TextMeshProUGUI scoreText;
    [SerializeField]
    int debugScore;
    [SerializeField]
    int stackScore;
    int bowScore = 0;
    [SerializeField]
    TextMeshProUGUI unlockedText;
    [SerializeField]
    int[] targetScore;
    [SerializeField]
    CanvasGroup colorThemeUi;
    bool isChanging;
    float fadeTime = 0.1f;
    ColorTheme theme;
    int themeIndex = 0;
    [SerializeField]
    Image redIm;
    [SerializeField]
    Image blueIm;
    [SerializeField]
    Image greenIm;
    [SerializeField]
    Image fieldIm;
    [SerializeField]
    TextMeshProUGUI colorText;
    [SerializeField]
    TextMeshProUGUI leftText;
    [SerializeField]
    Image RightButton;
    [SerializeField]
    Image LeftButton;

    Sequence scoreSeq;

    // Start is called before the first frame update
    void Start()
    {
        scoreSeq = DOTween.Sequence();
        //初期化
        unlockedText.gameObject.SetActive(false);
        UpdateColorUi();
        //text表示    
        scoreText.text = debugScore + Environment.NewLine + stackScore;
        AppendScore(stackScore, stackScore + debugScore);
    }

    // Update is called once per frame
    void Update()
    {
        //text更新
        scoreText.text = debugScore + Environment.NewLine + stackScore;
        //バー更新  
        Vector2 tmp = bow.rectTransform.sizeDelta;
        tmp.x = stackScore;
        bow.rectTransform.sizeDelta = tmp;
    }

    private void AppendScore(int startBow,int endBow,float duration = 1f)
    {
        scoreSeq.Append(
             DOTween.To
            (
            () => stackScore = startBow,　     //何に
            (n) => stackScore = n,　//何を
            endBow,　              //どこまで(最終的な値)
            duration               //どれくらいの時間
            )
            .SetEase(Ease.InExpo))
            .AppendCallback(() =>
            {
                backBow.rectTransform.DOPunchPosition(Vector3.right * 10f, 0.5f);
                unlockedText.gameObject.SetActive(true);
            }
            )
            .AppendInterval(0.5f)
            .AppendCallback(() =>
            {
                AppendScore(stackScore, stackScore + debugScore);
            });
    }

    public void ChangeGroupNext()
    {
        if (!isChanging)
        {
            Sequence seq = DOTween.Sequence()
            .OnStart(() =>
            {
                isChanging = true;
                RightButton.rectTransform.DOPunchScale(Vector3.one * 0.1f, 0.2f);
            })
            .AppendCallback(() =>
            {
                colorThemeUi.DOFade(0, fadeTime).SetEase(Ease.InSine);
                colorThemeUi.transform.DOLocalMoveX(-20, fadeTime).SetRelative().SetEase(Ease.InSine);
            })
            .AppendInterval(fadeTime)
            .AppendCallback(() =>
            {
                themeIndex = (themeIndex + 1) % 5;
                CustomColorTheme.ChangeTheme(themeIndex);
                UpdateColorUi();
                colorThemeUi.transform.DOLocalMoveX(40f, 0).SetRelative();
                colorThemeUi.DOFade(1, fadeTime).SetEase(Ease.OutSine);
                colorThemeUi.transform.DOLocalMoveX(-20, fadeTime).SetRelative().SetEase(Ease.OutSine);
            })
            .AppendInterval(fadeTime)
            .OnComplete(() =>
            {
                isChanging = false;
            });
        }
    }

    public void ChangeGroupPrev()
    {
        if (!isChanging)
        {
            Sequence seq = DOTween.Sequence()
            .OnStart(() =>
            {
                isChanging = true;
                LeftButton.rectTransform.DOPunchScale(Vector3.one * 0.1f, 0.2f);
            })
            .AppendCallback(() =>
            {
                colorThemeUi.DOFade(0, fadeTime).SetEase(Ease.InSine);
                colorThemeUi.transform.DOLocalMoveX(20, fadeTime).SetRelative().SetEase(Ease.InSine);
            })
            .AppendInterval(fadeTime)
            .AppendCallback(() =>
            {
                themeIndex = (themeIndex - 1 + 5) % 5;
                CustomColorTheme.ChangeTheme(themeIndex);
                UpdateColorUi();
                colorThemeUi.transform.DOLocalMoveX(-40f, 0).SetRelative();
                colorThemeUi.DOFade(1, fadeTime).SetEase(Ease.OutSine);
                colorThemeUi.transform.DOLocalMoveX(20, fadeTime).SetRelative().SetEase(Ease.OutSine);
            })
            .AppendInterval(fadeTime)
            .OnComplete(() =>
            {
                isChanging = false;
            });
        }
    }

    public void UpdateColorUi()
    {
        theme = CustomColorTheme.GetColors();
        redIm.color = theme.BallColorRed;
        blueIm.color = theme.BallColorBlue;
        greenIm.color = theme.BallColorGreen;
        fieldIm.color = theme.FieldColor;
        colorText.color = theme.TextColor;
        colorText.text = "Color " + themeIndex;
        leftText.color = theme.TextColor;
        scoreText.color = theme.TextColor;
        unlockedText.color = theme.TextColor;
        bow.color = theme.BallColorBlue;
        Color tmp = theme.BallColorBlue;
        tmp.a = 0.25f;
        backBow.color = tmp;
    }
}
