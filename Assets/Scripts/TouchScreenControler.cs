using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchScreenControler : MonoBehaviour
{
    private TouchScreenKeyboard overlayKeyboard;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void EnableKeyboard()
    {
        overlayKeyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
        print("Enable Keyboard");
    }

    
}
