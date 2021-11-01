using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiCloseConfirm : MonoBehaviour
{    


    public void CloseWindow()
    {
        gameObject.SetActive(false);
    }

    public void ExitApp()
    {
        Application.Quit();
    }
}
