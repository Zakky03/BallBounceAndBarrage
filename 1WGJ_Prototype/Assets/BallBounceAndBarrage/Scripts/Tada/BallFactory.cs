using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TadaLib;

/// <summary>
/// ボールを生成するクラス
/// </summary>

[RequireComponent(typeof(ObjectPool))]
public class BallFactory : MonoBehaviour
{
    // インスタンスを生成するクラス
    private static ObjectPool pool;

    private void Awake()
    {
        pool = GetComponent<ObjectPool>();
    }

    public static Ball GetInstance()
    {
        return pool.GetInstance().GetComponent<Ball>();
    }
}
