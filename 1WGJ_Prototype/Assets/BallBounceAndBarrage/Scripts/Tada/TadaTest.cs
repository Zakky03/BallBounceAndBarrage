using NCMB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TadaTest : MonoBehaviour
{
    private IEnumerator Start()
    {
        // Type == Number の場合
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(100);

        //// Type == Time の場合
        //var millsec = 123456;
        //var timeScore = new System.TimeSpan(0, 0, 0, 0, millsec);
        //naichilab.RankingLoader.Instance.SendScoreAndShowRanking(timeScore);

        yield return new WaitForSeconds(1.0f);
    }
}
