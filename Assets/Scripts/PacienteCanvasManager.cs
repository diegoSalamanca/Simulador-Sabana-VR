using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PacienteCanvasManager : MonoBehaviour
{
    private static PacienteCanvasManager _instance;
    public static PacienteCanvasManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<PacienteCanvasManager>();
            }

            return _instance;
        }
    }

    public GameObject BG, Option, Message, OptionBox, OptionPrefab;
    SoundManager SoundManager;
    public TMPro.TextMeshProUGUI textMessage;
    public Color[] Colors;

    

    // Start is called before the first frame update
    void Start()
    {
        SoundManager = SoundManager.Instance;
        Option.SetActive(false);
        Message.SetActive(false);
        HideCanvas();
    }   

    public void ShowCanvas()
    {        
        iTween.ScaleTo(BG, Vector3.one, 0.5f);
    }

    public void HideCanvas()
    {
        iTween.ScaleTo(BG, Vector3.zero, 0.5f);

        foreach (Transform child in OptionBox.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void SetCorrectmessage(string message)
    {
        Option.SetActive(false);
        Message.SetActive(true);
        SoundManager.PlaySoundByIndex(2);
        textMessage.color = Colors[1];
        textMessage.text = message;
        ShowCanvas();
    }

    public void SetIncorrectmessage(string message)
    {
        Option.SetActive(false);
        Message.SetActive(true);
        SoundManager.PlaySoundByIndex(3);
        textMessage.color = Colors[2];
        textMessage.text = message;
        ShowCanvas();
    }

    public void SetNeutralmessage(string message)
    {
        Option.SetActive(false);
        Message.SetActive(true);
        textMessage.color = Colors[0];
        textMessage.text = message;
        ShowCanvas();
    }

    public void SetPhrases(List<Matchitem> matchs)
    {
        Option.SetActive(true);
        Message.SetActive(false);
        SoundManager.PlaySoundByIndex(2);
        foreach (var item in matchs)
        {
            var opt = Instantiate(OptionPrefab, OptionBox.transform);
            opt.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = item.MatchPhrase;
            opt.GetComponent<Button>().onClick.AddListener(() => { PatientSpeakerController.Instance.PlayDialogObject(item.OptionsScriptable); HideCanvas(); });
        }
        ShowCanvas();
    }

    


}
