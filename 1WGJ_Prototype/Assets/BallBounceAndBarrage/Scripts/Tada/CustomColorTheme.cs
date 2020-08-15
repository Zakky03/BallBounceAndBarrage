using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 画面の色のテーマ色を提供するクラス
/// </summary>

//ボールの状態
public enum eColorState
{
    GREEN = 0,
    BLUE = 1,
    RED = 2,
    ROD = 3,
    FIELD = 4,
    WALL = 5,
}

[System.Serializable]
public class ColorTheme
{
    [SerializeField]
    private bool isRandomColor = false;

    [field: SerializeField]
    public Color BallColorGreen { private set; get; } // GREEN
    [field: SerializeField]
    public Color BallColorBlue { private set; get; } // BLUE
    [field: SerializeField]
    public Color BallColorRed { private set; get; }  // RED

    [field: SerializeField]
    public Color RodColor { private set; get; } // 棒
    [field: SerializeField]
    public Color FieldColor { private set; get; } // 背景
    [field: SerializeField]
    public Color WallColor { private set; get; } // 壁

    [field: SerializeField]
    public Color TextColor { private set; get; } // テキスト

    public Color this[int index]
    {
        get
        {
            UnityEngine.Assertions.Assert.IsTrue(index >= 0 && index <= 6, "範囲外");
            switch (index)
            {
                case 0: return BallColorGreen;
                case 1: return BallColorBlue;
                case 2: return BallColorRed;
                case 3: return RodColor;
                case 4: return FieldColor;
                case 5: return WallColor;
                case 6: return TextColor;
            }
            return new Color(1.0f, 0.0f, 1.0f);
        }
    }

    public void Init()
    {
        if (isRandomColor)
        {
            BallColorGreen = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
            BallColorBlue = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
            BallColorRed = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
            RodColor = BallColorBlue;// new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
            FieldColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
            WallColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
            TextColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
        }
    }
}

public class CustomColorTheme : TadaLib.SingletonMonoBehaviour<CustomColorTheme>
{
    // 色のテーマ
    [field: SerializeField]
    public List<ColorTheme> themes { private set; get; }

    // 現在選択中のテーマ
    public ColorTheme curTheme { private set; get; }

    protected override void Awake()
    {
        base.Awake();

        UnityEngine.Assertions.Assert.IsFalse(themes.Count == 0);
        foreach (var theme in themes) theme.Init();
        curTheme = themes[0];
    }

    public static ColorTheme GetColors()
    {
        return Instance.curTheme;
    }

    public static int GetThemeNum()
    {
        return Instance.themes.Count;
    }

    public static void ChangeTheme(int index)
    {
        UnityEngine.Assertions.Assert.IsFalse(Instance.themes.Count <= index);
        Instance.curTheme = Instance.themes[index];
        Instance.curTheme.Init();
    }
}
