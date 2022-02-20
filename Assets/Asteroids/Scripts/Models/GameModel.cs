using System.Collections.Generic;
using UnityEngine;

public static class GameModel
{
    public static Dictionary<Border, float> ScreenBorder = new Dictionary<Border, float>();

    public static void SetScreenBorders()
    {
        Vector3 screenSize = Camera.main.ViewportToWorldPoint(Vector3.one);
        ScreenBorder[Border.Left] = -screenSize.x;
        ScreenBorder[Border.Right] = screenSize.x;
        ScreenBorder[Border.Top] = screenSize.z;
        ScreenBorder[Border.Bottom] = -screenSize.z;
    }
}