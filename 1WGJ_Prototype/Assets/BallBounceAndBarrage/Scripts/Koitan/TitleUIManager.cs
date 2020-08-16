using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUIManager : MonoBehaviour
{
    [SerializeField]
    Animator startAnimator;

    [SerializeField]
    private UnityEngine.UI.Image titleFrame;

    // Start is called before the first frame update
    void Start()
    {
        titleFrame.color = CustomColorTheme.GetColors().FieldColor + new Color(1.0f, 1.0f, 1.0f) * 0.2f;
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
