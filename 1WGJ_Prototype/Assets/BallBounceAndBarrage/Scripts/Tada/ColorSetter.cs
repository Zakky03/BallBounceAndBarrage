using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ColorSetter : MonoBehaviour
{
    [SerializeField]
    private List<SpriteRenderer> rods;
    [SerializeField]
    private List<SpriteRenderer> walls;
    [SerializeField]
    private List<SpriteRenderer> fields;
    [SerializeField]
    private List<TextMeshProUGUI> texts;
    [SerializeField]
    private List<UnityEngine.UI.Image> buttons;

    private void Start()
    {
        // 色を変更
        SetColors(CustomColorTheme.GetColors());

        // 色変更の登録
        CustomColorTheme.RegisterMethod(SetColors);
    }

    private void OnDestroy()
    {
        // 色変更の登録を破棄
        if (CustomColorTheme.Instance != null) 
           CustomColorTheme.UnRegisterMethod(SetColors);
    }

    public void SetColors(ColorTheme theme)
    {
        foreach (var sr in rods) sr.color = theme.RodColor;
        foreach (var sr in walls) sr.color = theme.WallColor;
        foreach (var sr in fields) sr.color = theme.FieldColor;
        foreach (var txt in texts) txt.color = theme.TextColor;
        foreach (var img in buttons) img.color = theme.TextColor;
    }
}
