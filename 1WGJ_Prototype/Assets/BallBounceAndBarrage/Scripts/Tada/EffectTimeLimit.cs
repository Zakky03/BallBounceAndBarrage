using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectTimeLimit : MonoBehaviour
{
    [SerializeField]
    private float lifeTime = 2.0f;

    private float timer = 0.0f;

    private void OnEnable()
    {
        timer = 0.0f;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > lifeTime)
        {
            timer = 0.0f;
            gameObject.SetActive(false);
        }
    }
}
