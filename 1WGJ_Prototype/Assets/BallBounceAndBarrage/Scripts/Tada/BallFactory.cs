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

    private void Start()
    {
        Ball ball = pool.GetInstance().GetComponent<Ball>();
        ball.Init();
        ball.transform.position = new Vector3(0f, -3f, 0f);
    }

    public static Ball GetInstance()
    {
        GameObject obj = pool.GetInstance();
        if (obj == null) return null;

        Ball ret = obj.GetComponent<Ball>();
        ret.Init();
        return ret;
    }
}
