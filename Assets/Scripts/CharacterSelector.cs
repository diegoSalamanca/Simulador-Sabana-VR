using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelector : MonoBehaviour
{
    public Color colorLight, colorDark;
    public Slider mixColors;
    public Image imageColor1, imageResult, imageColor2;
    Color resultColor;
    public Material ColorToneMaterial;

    

    public void ChangeMaterialColor(float value)
    {
        var colorResult = CombineColors(value, colorLight, colorDark);
        imageResult.color = colorResult;
        ColorToneMaterial.color = colorResult;
    }

    public Color CombineColors(params Color[] aColors)
    {
        Color result = new Color(0, 0, 0, 0);
        foreach (Color c in aColors)
        {
            result += c;
        }

        result /= aColors.Length;
        
        return result;
    }

    public Color CombineColors(float value, params Color[] aColors )
    {
        var invertvalue = 1 - value;

        Color result = new Color((aColors[1].r*value) + (aColors[0].r*invertvalue), (aColors[1].g * value) + (aColors[0].g * invertvalue), (aColors[1].b * value) + (aColors[0].b * invertvalue), 1);        

        return result;
    }
}
