
using UnityEngine;
using UnityEngine.UI;

public class OptPlayer : MonoBehaviour
{
    public OptionsScriptable OptionsScriptable;
    public string MatchPhrase;
    private Button Button;

    public void SetOptData(OptionsScriptable opt, string phrase)
    {
        OptionsScriptable = opt;
        MatchPhrase = phrase;
    }

    private void Start()
    {
        Button = GetComponent<Button>();
        Button.onClick.AddListener(PlayOption);
    }

    private void  PlayOption()
    {
        PatientSpeakerController.Instance.PlayDialogObject(OptionsScriptable);
    }
}

