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
    private int initialColorIndex = 0;

    private void Start()
    {
        CustomColorTheme.ChangeTheme(3);// initialColorIndex);
        SetColors(CustomColorTheme.GetColors());
    }

    public void SetColors(ColorTheme theme)
    {
        foreach (var sr in rods) sr.color = theme.RodColor;
        foreach (var sr in walls) sr.color = theme.WallColor;
        foreach (var sr in fields) sr.color = theme.FieldColor;
        foreach (var txt in texts) txt.color = theme.TextColor;
    }
}
