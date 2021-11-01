using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoToScreen : MonoBehaviour
{
    public GameObject ScreenDestination;
    public GameObject ScreenOrigin;
    public bool NoBG;

    Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(GoToNewScreen);
    }

    public void GoToNewScreen()
    {
        var screens = FindObjectsOfType<MenuScreen>();

        if (NoBG)
        {            
            foreach (var item in screens)
            {
                item.gameObject.SetActive(false);  
            }

            ScreenDestination.SetActive(true);

        }
        else 
        {
            foreach (var item in screens)
            {
                item.gameObject.SetActive(false);

            }

            ScreenOrigin.transform.SetAsLastSibling();
            ScreenOrigin.SetActive(true);
            ScreenDestination.SetActive(true);
            ScreenDestination.transform.SetAsLastSibling();
        }
        
        iTween.MoveFrom(ScreenDestination, new Vector3(ScreenDestination.transform.position.x-200, ScreenDestination.transform.position.y, ScreenDestination.transform.position.z), 0.5f);
    }
}
