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
    [SerializeField]
    int left;
    int bowScore = 0;
    [SerializeField]
    TextMeshProUGUI unlockedText;
    [SerializeField]
    int[] targetScore;
    int targetIndex = 0;
    [SerializeField]
    TextMeshProUGUI leftTargetScore;
    [SerializeField]
    TextMeshProUGUI rightTargetScore;
    float maxBowSize = 400f;
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
    [SerializeField]
    Image LeftLock;
    [SerializeField]
    Image RightLock;

    Sequence scoreSeq;

    // Start is called before the first frame update
    void Start()
    {
        scoreSeq = DOTween.Sequence();
        //初期化
        unlockedText.gameObject.SetActive(false);
        UpdateColorUi();
        left = Score.score;
        stackScore = PlayerPrefs.GetInt("Score", 0);
        themeIndex = CustomColorTheme.GetThemeNum();
        PlayerPrefs.SetInt("Score", stackScore + left);
        //ターゲットスコアの計算
        for (int i = 0; i < targetScore.Length; i++)
        {
            if (targetScore[i] > stackScore)
            {
                targetIndex = i;
                leftTargetScore.text = targetScore[i - 1].ToString();
                rightTargetScore.text = targetScore[i].ToString();
                break;
            }
        }
        if (stackScore < targetScore[targetScore.Length - 1])
        {
            //text表示    
            BowMove();
        }
        else
        {
            targetIndex = targetScore.Length;
            leftTargetScore.text = targetScore[targetIndex - 2].ToString();
            rightTargetScore.text = targetScore[targetIndex - 1].ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //text更新
        scoreText.text = Score.score + Environment.NewLine + stackScore;
        //バー更新  
        Vector2 tmp = bow.rectTransform.sizeDelta;
        if (targetIndex < targetScore.Length)
        {
            tmp.x = maxBowSize * (stackScore - targetScore[targetIndex - 1]) / (targetScore[targetIndex] - targetScore[targetIndex - 1]);
        }
        else
        {
            tmp.x = maxBowSize;
        }
        bow.rectTransform.sizeDelta = tmp;
        //Lock
        if (targetIndex < targetScore.Length)
        {
            if (themeIndex == targetIndex - 1)
            {
                RightLock.gameObject.SetActive(true);
            }
            else
            {
                RightLock.gameObject.SetActive(false);
            }

            if (themeIndex == 0)
            {
                LeftLock.gameObject.SetActive(true);
            }
            else
            {
                LeftLock.gameObject.SetActive(false);
            }
        }
        else
        {
            RightLock.gameObject.SetActive(false);
            LeftLock.gameObject.SetActive(false);
        }
    }

    private void AppendScore(int target, float duration = 1f)
    {
        scoreSeq.Append(
             DOTween.To
            (
            () => stackScore,　     //何に
            (n) => stackScore = n,　//何を
            target,　              //どこまで(最終的な値)
            duration               //どれくらいの時間
            )
            .SetEase(Ease.InExpo));
    }

    private void AppendScorePunch(int target, float duration = 1f)
    {
        scoreSeq.Append(
             DOTween.To
            (
            () => stackScore,　     //何に
            (n) => stackScore = n,　//何を
            target,　              //どこまで(最終的な値)
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
                targetIndex++;
                BowMove();
            });
    }

    void BowMove()
    {
        leftTargetScore.text = targetScore[targetIndex - 1].ToString();
        rightTargetScore.text = targetScore[targetIndex].ToString();
        if (targetScore[targetIndex] <= stackScore + left) //レベルアップ
        {
            left -= targetScore[targetIndex] - stackScore;
            AppendScorePunch(targetScore[targetIndex]);
            Debug.Log("target:" + targetScore[targetIndex] + ", left:" + left);
        }
        else
        {
            AppendScore(stackScore + left);
            left = 0;
            Debug.Log("target:" + stackScore + left + ", left:" + left);
        }
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
                themeIndex = (themeIndex + 1) % targetIndex;
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
                themeIndex = (themeIndex - 1 + targetIndex) % targetIndex;
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
        colorText.text = theme.ThemeName;
        leftText.color = theme.TextColor;
        scoreText.color = theme.TextColor;
        unlockedText.color = theme.TextColor;
        bow.color = theme.BallColorBlue;
        leftTargetScore.color = theme.TextColor;
        rightTargetScore.color = theme.TextColor;
        Color tmp = theme.BallColorBlue;
        tmp.a = 0.25f;
        backBow.color = tmp;
    }

    [ContextMenu("ScoreReset")]
    public void ResetScore()
    {
        PlayerPrefs.DeleteKey("Score");
    }
}
