using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text.RegularExpressions;

public class PatientSpeakerController : MonoBehaviour
{
    public AudioSource AudioSource;
    Animator Animator;

    public bool ActivatePatientDialog { get; set; }

    public UserData UserData;

    public DialogsScriptable DialogsScriptableNurseMale, DialogsScriptableNurseFemale;

    DialogsScriptable SelectDialogObject;

    SoundManager SoundManager;

    public DonAlbertoCanvasController DonAlbertoCanvasController;

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


    }

    public void SelectDialogScriptable()
    {
        if (UserData.gender == 0)
        {
            SelectDialogObject = DialogsScriptableNurseMale;
        }
        else
        {
            SelectDialogObject = DialogsScriptableNurseFemale;
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
                        SoundManager.PlaySoundByIndex(2);
                        StartCoroutine(PlayAudio(SelectDialogObject.Options[i].PatienteResponse));                        
                        Animator.SetTrigger(SelectDialogObject.Options[i].AnimationName);
                        DonAlbertoCanvasController.EnlabeMessage(1);
                        return;
                    }                   
                }
            }

            var coincidentPhrases = new List<string>();

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
                                        //print("Match =" + keyword + " in " + phrase);
                                        coincidentPhrases.Add(phrase);                                        
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (coincidentPhrases.Count > 0)
            {
                foreach (var item in coincidentPhrases)
                {
                    print("Match =" + item);
                }



                return;
            }

            DontMatch();
        }
       
    } 
    public void DontMatch()
    {
        SoundManager.PlaySoundByIndex(3);
        DonAlbertoCanvasController.EnlabeMessage(2);
    }
}
