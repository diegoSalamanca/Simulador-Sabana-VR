using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text.RegularExpressions;

public class PatientSpeakerController : MonoBehaviour
{

    private static PatientSpeakerController _instance;
    public static PatientSpeakerController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<PatientSpeakerController>();
            }

            return _instance;
        }
    }

    public AudioSource AudioSource;
    Animator Animator;

    public bool ActivatePatientDialog { get; set; }

    public UserData UserData;

    public DialogsScriptable DialogsScriptableNurseMale, DialogsScriptableNurseFemale, DialogsScriptableDoctorMale, DialogsScriptableDoctorFemale, DialogsScriptableFisioMale, DialogsScriptableFisioFemale;

    DialogsScriptable SelectDialogObject;

    SoundManager SoundManager;   

    public bool CompareStringsWithOutDiacritics(string value1, string value2)
    {
        string value1Norm = Regex.Replace(value1.Normalize(System.Text.NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "");
        string value2Norm = Regex.Replace(value2.Normalize(System.Text.NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "");
        print(value1Norm+" "+ value2Norm);
        return value1Norm.Equals(value2Norm, System.StringComparison.InvariantCultureIgnoreCase);
    }

    public string NormalizeString(string value)
    {
        string valueNorm = Regex.Replace(value.Normalize(System.Text.NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "");
        valueNorm = Regex.Replace(valueNorm.Normalize(System.Text.NormalizationForm.FormD), @"[\s+]|[,+]", "");
        return valueNorm;
    }

    public int CompareWordsArray(string[] array0, string[] array1)
    {
        return 0;
    }

    void Start()
    {
        
        SoundManager = SoundManager.Instance;
         
        Animator = GetComponent<Animator>();
        ActivatePatientDialog = false;

        SelectDialogScriptable();

        PacienteCanvasManager.Instance.SetNeutralmessage("Presione el botón – A - para hablar. Hable despacio y claro. Cuando termine suelte el botón para que Jairo escuche su frase.");
    }

    public void SelectDialogScriptable()
    {
        if (UserData.gender == 0)
        {
            switch (UserData.especialidad)
            {
                default:
                    SelectDialogObject = DialogsScriptableDoctorMale;
                    break;

                case 0:
                    SelectDialogObject = DialogsScriptableDoctorMale;
                    break;

                case 1:
                    SelectDialogObject = DialogsScriptableNurseMale;
                    break;

                case 2:
                    SelectDialogObject = DialogsScriptableFisioMale;
                    break;
            }
            
        }
        else
        {
            switch (UserData.especialidad)
            {
                default:
                    SelectDialogObject = DialogsScriptableDoctorFemale;
                    break;

                case 0:
                    SelectDialogObject = DialogsScriptableDoctorFemale;
                    break;

                case 1:
                    SelectDialogObject = DialogsScriptableNurseFemale;
                    break;

                case 2:
                    SelectDialogObject = DialogsScriptableFisioFemale;
                    break;
            }
           
        }
    }

    public void SetActivatePatientDialog(bool value)
    {
        ActivatePatientDialog = value;
    }


     IEnumerator PlayAudio(AudioClip clip)
    {
        yield return new WaitForSeconds(0.5f);
        AudioSource.PlayOneShot(clip);
    }

    public void AnalizeString(string value)
    {
        SelectDialogScriptable();

        var speakerSayList = new List<string>(value.Split(" "));

        if (ActivatePatientDialog)
        {
            for (int i = 0; i < SelectDialogObject.Options.Length; i++)
            {
                foreach (var opt in SelectDialogObject.Options[i].Options)
                {
                    if (NormalizeString(value).Equals(NormalizeString(opt), System.StringComparison.InvariantCultureIgnoreCase))
                    {                        
                        StartCoroutine(PlayAudio(SelectDialogObject.Options[i].PatienteResponse));                        
                        Animator.SetTrigger(SelectDialogObject.Options[i].AnimationName);
                        PacienteCanvasManager.Instance.SetCorrectmessage("Muy Bien, Jairo le ha entendido correctamente.");
                        return;
                    }                   
                }
            }

            var coincidentPhrases = new List<Matchitem>();

            for (int i = 0; i < SelectDialogObject.Options.Length; i++)
            {
                foreach (var keyword in SelectDialogObject.Options[i].KeyWords)
                {
                    foreach (var speakerWord in speakerSayList)
                    {
                        if (NormalizeString(speakerWord).Equals(NormalizeString(keyword), System.StringComparison.InvariantCultureIgnoreCase))
                        {
                            var concidenceFlag = false;

                            foreach (var phrase in SelectDialogObject.Options[i].Options)
                            {
                                var optionWords = new List<string>(phrase.Split(" "));                                

                                foreach (var worditem in optionWords)
                                {                                   

                                    if (NormalizeString(worditem).Equals(NormalizeString(keyword), System.StringComparison.InvariantCultureIgnoreCase) && !concidenceFlag)
                                    {
                                        concidenceFlag = true;
                                        var MatchitemCoincidence = new Matchitem();
                                        MatchitemCoincidence.MatchPhrase = phrase;
                                        MatchitemCoincidence.OptionsScriptable = SelectDialogObject.Options[i];
                                        coincidentPhrases.Add(MatchitemCoincidence);                                        
                                    }
                                }
                            }

                            if (!concidenceFlag)
                            {
                                var MatchitemCoincidence = new Matchitem();
                                MatchitemCoincidence.MatchPhrase = SelectDialogObject.Options[i].Options[0];
                                MatchitemCoincidence.OptionsScriptable = SelectDialogObject.Options[i];
                                coincidentPhrases.Add(MatchitemCoincidence);
                            }
                        }
                    }
                }
            }

            if (coincidentPhrases.Count > 0)
            {      
                PacienteCanvasManager.Instance.SetPhrases(coincidentPhrases);
                return;
            }

            DontMatch();
        }
       
    }

    public void PlayDialogObject(OptionsScriptable option)
    {
        StartCoroutine(PlayAudio(option.PatienteResponse));
        Animator.SetTrigger(option.AnimationName);        
    }

    public void DontMatch()
    {
        PacienteCanvasManager.Instance.SetIncorrectmessage("Jairo no le ha entendido, Por favor Intente nuevamente");
    }
}

public class Matchitem
{
    public OptionsScriptable OptionsScriptable;

    public string MatchPhrase;

}
