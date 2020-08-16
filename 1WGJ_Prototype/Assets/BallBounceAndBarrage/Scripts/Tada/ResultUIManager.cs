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

    public void OnRankingButtonClick()
    {
        rankingButton.interactable = false;
        // スコア登録 by tada
        if (naichilab.RankingLoader.Instance != null) naichilab.RankingLoader.Instance.SendScoreAndShowRanking(Score.score);
    }

    public void OnRetryButtonClick()
    {
        retryButton.interactable = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }

    public void OnTitleButtonClick()
    {
        titleButton.interactable = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
    }
}