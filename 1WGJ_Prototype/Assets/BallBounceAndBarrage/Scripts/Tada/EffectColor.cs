using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectColor : MonoBehaviour
{
    private ParticleSystem eff;

    private void Awake()
    {
        eff = GetComponent<ParticleSystem>();
    }

    // 色を変更
    public void Play(Ball.BALL_STATE state)
    {
        eff.startColor = CustomColorTheme.GetColors()[(int)state];
        eff.Play();
    }
}
