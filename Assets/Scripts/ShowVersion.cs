using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowVersion : MonoBehaviour
{
    private TMPro.TextMeshProUGUI GuiText;

    // Start is called before the first frame update
    void Start()
    {
        GuiText = GetComponent<TMPro.TextMeshProUGUI>();
        GuiText.text = "version "+ Application.version;
    }

    
}
