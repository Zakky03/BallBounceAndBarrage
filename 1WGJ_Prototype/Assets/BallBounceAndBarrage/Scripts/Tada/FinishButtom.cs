using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishButtom : MonoBehaviour
{
    public void Retry()
    {
        SceneManager.LoadScene("Main");
    }

    public void TransitionTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
