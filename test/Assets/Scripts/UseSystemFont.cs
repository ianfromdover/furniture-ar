using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseSystemFont : MonoBehaviour
{
    public Text label;
    void Start()
    {
        // Get available fonts on device
        string[] systemFontNames = Font.GetOSInstalledFontNames();
        string fontName = systemFontNames[0];
        int fontSize = 33;
        Font systemFont = Font.CreateDynamicFontFromOSFont(fontName, fontSize);
 
        label.font = systemFont;
    }
}
