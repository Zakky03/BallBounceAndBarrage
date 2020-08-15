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
    [field:SerializeField]
    public Color WallColor { private set; get; } // 壁

    public Color this[int index]
    {
        get
        {
            UnityEngine.Assertions.Assert.IsTrue(index >= 0 && index <= 5, "範囲外");
            switch (index)
            {
                case 0: return BallColor0;
                case 1: return BallColor1;
                case 2: return BallColor2;
                case 3: return RodColor;
                case 4: return FieldColor;
                case 5: return WallColor;
            }
            return new Color(1.0f, 0.0f, 1.0f);
        }
    }
}

public class CustomColorTheme : TadaLib.SingletonMonoBehaviour<CustomColorTheme>
{
    // 色のテーマ
    [field:SerializeField]
    public List<ColorTheme> themes { private set; get; }

    // 現在選択中のテーマ
    public ColorTheme curTheme { private set; get; }

    protected override void Awake()
    {
        UnityEngine.Assertions.Assert.IsFalse(themes.Count == 0);

        base.Awake();

        // 初期のテーマをデフォルトで選択
        curTheme = themes[0];
    }

    public void ChangeTheme(int index)
    {
        UnityEngine.Assertions.Assert.IsFalse(themes.Count >= index);
        curTheme = themes[index];
    }
}
