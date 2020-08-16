using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TadaLib;

/// <summary>
/// エフェクトを生成するクラス
/// </summary>

public enum eEffectType
{
    Bubble = 0,
    Burst = 1,
}

[RequireComponent(typeof(ObjectPool))]
public class EffectFactory : MonoBehaviour
{
    // インスタンスを生成するクラス
    private static ObjectPool poolBubble;
    private static ObjectPool poolBurst;


    private void Awake()
    {
        var pools = GetComponents<ObjectPool>();
        poolBubble = pools[0];
        poolBurst = pools[1];
    }

    public static void Play(eEffectType type, Vector3 pos, int colorIndex)
    {
        // 強引に...
        GameObject obj;
        if (type == eEffectType.Bubble) obj = poolBubble.GetInstance();
        else obj = poolBurst.GetInstance();

        if (obj == null) return;

        ParticleSystem eff = obj.GetComponent<ParticleSystem>();
        eff.transform.position = pos;
        eff.startColor = CustomColorTheme.GetColors()[colorIndex];
        eff.Play();
    }
}
