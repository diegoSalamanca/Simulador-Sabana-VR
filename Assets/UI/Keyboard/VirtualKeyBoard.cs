using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualKeyBoard : MonoBehaviour
{

    public InputField InputFieldSelected;
    bool Minus = false;

    public GameObject[] NumericLines;
    public GameObject[] AlphabeticLines;

    public float ScaleAnim;

    void Start()
    {
        transform.localScale = Vector3.zero;
    }

    public void EnableAlphabetic()
    {
        foreach (var item in NumericLines)
        {
            item.SetActive(false);
        }

        foreach (var item in AlphabeticLines)
        {
            item.SetActive(true);
        }
    }

    public void EnableNumeric()
    {
        foreach (var item in NumericLines)
        {
            item.SetActive(true);
        }

        foreach (var item in AlphabeticLines)
        {
            item.SetActive(false);
        }
    }
    public void ShowKeyBoard(GameObject Inputobject)
    {
        InputFieldSelected = Inputobject.GetComponent<InputField>();
        transform.localScale = Vector3.one * ScaleAnim;
    }

    public void HideKeyBoard()
    {
        InputFieldSelected = null;
        transform.localScale = Vector3.zero;
    }

    public void AddCharToInput(char valueChar)
    {
        InputFieldSelected.text += valueChar;        
    }

    public void RemoveChar()
    {
        var text = InputFieldSelected.text;
        if (text.Length > 0)
        {
            text = text.Remove(InputFieldSelected.text.Length-1);
        }

        InputFieldSelected.text = text;
    }

    public void ToogleMayusMinusAllKeys()
    {
        var keys = GetComponentsInChildren<KeyboardButtonKey>();

        if (!Minus)
        {
            foreach (var item in keys)
            {

                item.UpdateChar(char.Parse(item.Char.ToString().ToUpper()));

            }
            Minus = true;
        }

        else 
        {
            foreach (var item in keys)
            {

                item.UpdateChar(char.Parse(item.Char.ToString().ToLower()));

            }
            Minus = false;
        }
        
    }

   

    
}
