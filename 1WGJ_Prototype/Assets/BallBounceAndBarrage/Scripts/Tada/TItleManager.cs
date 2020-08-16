using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TItleManager : MonoBehaviour
{
    [SerializeField]
    private Button startButton;


    public void OnStartButtonClicked()
    {
        startButton.interactable = false;
        startButton.gameObject.SetActive(false);
        StartCoroutine(StartFlow());
    }

    private IEnumerator StartFlow()
    {

        yield return new WaitForSeconds(1.0f);
    
    }
}
