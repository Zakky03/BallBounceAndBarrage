using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TadaLib;

/// <summary>
/// エフェクトを生成するクラス
/// </summary>

[RequireComponent(typeof(ObjectPool))]
public class EffectFactory : MonoBehaviour
{
    // インスタンスを生成するクラス
    private static ObjectPool pool;

    private void Awake()
    {
        pool = GetComponent<ObjectPool>();
    }

    public static void Play(Vector3 pos, int colorIndex, float duration = 2.0f)
    {
        ParticleSystem eff = pool.GetInstance().GetComponent<ParticleSystem>();
        eff.transform.position = pos;
        eff.startColor = CustomColorTheme.GetColors()[colorIndex];
        eff.Play();
    }
}
