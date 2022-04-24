using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorHelper
{
    public const string BLUE = "#297bffff";
    public const string RED = "#ff4c53ff";
    public const string GREEN = "#00be53ff";
    public const string YELLOW = "#ffbe53ff";
    private static Dictionary<string, Color> colors = new Dictionary<string, Color>();

    public static Color GetColor(string colorHex)
    {
        if (!colors.ContainsKey(colorHex)) {
            var color = new Color();
            ColorUtility.TryParseHtmlString(colorHex, out color);
            colors.Add(colorHex, color);
        }

        return colors[colorHex];
    }
}
