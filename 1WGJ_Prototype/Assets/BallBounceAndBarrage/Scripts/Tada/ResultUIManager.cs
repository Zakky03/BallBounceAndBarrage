using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using NCMB;
using NCMB.Extensions;

public class ResultUIManager : MonoBehaviour
{
    [SerializeField]
    private Button rankingButton;
    [SerializeField] 
    private Button retryButton;
    [SerializeField]
    private Button titleButton;

    [SerializeField]
    private BannerViewer banner_;

    public void OnRankingButtonClick()
    {
        rankingButton.interactable = false;
        // スコア登録 by tada
        if (naichilab.RankingLoader.Instance != null) naichilab.RankingLoader.Instance.SendScoreAndShowRanking(Score.score);
    }

    public void OnRetryButtonClick()
    {
        banner_.Destroy();
        retryButton.interactable = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }

    public void OnTitleButtonClick()
    {
        banner_.Destroy();
        titleButton.interactable = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
    }
}