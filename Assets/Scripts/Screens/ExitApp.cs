
using UnityEngine;
using UnityEngine.UI;

public class ExitApp : MonoBehaviour
{
    Button button;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ExitAppQuit);
    }

   

    public void ExitAppQuit()
    {
        print("Exit");
        Application.Quit();          
    }
}
