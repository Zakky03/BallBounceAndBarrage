using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 画面の色のテーマ色を提供するクラス
/// </summary>

[System.Serializable]
public class ColorTheme
{
    [field: SerializeField]
    public Color BallColor0 { private set; get; } // GREEN
    [field: SerializeField]
    public Color BallColor1 { private set; get; } // BLUE
    [field: SerializeField]
    public Color BallColor2 { private set; get; }  // RED

    [field:SerializeField]
    public Color RodColor { private set; get; } // 棒

    [field:SerializeField]
    public Color FieldColor { private set; get; } // 背景
}

public class CustomColorTheme : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
