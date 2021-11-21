using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardButtonKey : MonoBehaviour
{

    public char Char;

    public bool IsChar, EnterKey, MayusKey, BackspaceKey, NumbersKey, AlphabethKey, SpaceKey, HideKey;

    private VirtualKeyBoard VirtualKeyBoard;
    // Start is called before the first frame update
    private Button ButtonInteract;  
    
    private TMPro.TextMeshProUGUI ButtonText;
    
    void Start()
    {
        VirtualKeyBoard = GetComponentInParent<VirtualKeyBoard>();
        ButtonInteract = GetComponent<Button>();
        ButtonText = GetComponentInChildren<TMPro.TextMeshProUGUI>();

        ButtonInteract.onClick.AddListener(SoundManager.Instance.PlaybuttonUiSound);

        if (IsChar)
        {
            ButtonInteract.onClick.AddListener(SetChar);
            ButtonText.text = Char.ToString();
        }
        else  if (BackspaceKey)
        {
            ButtonInteract.onClick.AddListener(SetBack);
        }

        else if (EnterKey)
        {
            ButtonInteract.onClick.AddListener(HideKeyboardEvent);
        }
        else if (MayusKey)
        {
            ButtonInteract.onClick.AddListener(ToogleMinusMayus);
        }
        else if (SpaceKey)
        {
            Char = ' ';
            ButtonInteract.onClick.AddListener(SetChar);
        }
        else if (NumbersKey)
        {
            ButtonText.text = "123";
            ButtonInteract.onClick.AddListener(ToogleCharsNumbers);
        }
        else if (AlphabethKey)
        {
            ButtonText.text = "ABC";
            ButtonInteract.onClick.AddListener(ToogleCharsAlphabeth);
        }
        else if (HideKey)
        {
            
            ButtonInteract.onClick.AddListener(HideKeyboardEvent);
        }
        else 
        {
            Char = '\0';
        }
       
    }   

    public void SetChar()
    {
        VirtualKeyBoard.AddCharToInput(Char);

    }

    public void SetBack()
    {
        VirtualKeyBoard.RemoveChar(); ;
    }

    public void HideKeyboardEvent()
    {
        VirtualKeyBoard.HideKeyBoard();
    }

    public void ToogleMinusMayus()
    {
        VirtualKeyBoard.ToogleMayusMinusAllKeys();
    }

    public void ToogleCharsNumbers()
    {
        VirtualKeyBoard.EnableNumeric();
    }

    public void ToogleCharsAlphabeth()
    {
        VirtualKeyBoard.EnableAlphabetic();
    }

    public void UpdateChar(char value)
    {
        if (IsChar)
        {
            Char = value;
            ButtonText.text = Char.ToString();
        }
       
    }
}
