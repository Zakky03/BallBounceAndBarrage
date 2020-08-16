using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUIManager : MonoBehaviour
{
    [SerializeField]
    Animator startAnimator;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayAnim()
    {
        startAnimator.enabled = true;
        StartCoroutine(LoadCoroutine());
    }

    IEnumerator LoadCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Main");
    }
}
