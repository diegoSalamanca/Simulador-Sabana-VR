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

    public DialogsScriptable DialogsScriptableNurseMale;

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

        SelectDialogObject = DialogsScriptableNurseMale;
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

    public void PlayAnimation()
    {

    }

    public void AnalizeString(string value)
    {
        if (ActivatePatientDialog)
        {
            for (int i = 0; i < SelectDialogObject.Options.Length; i++)
            {
                foreach (var opt in SelectDialogObject.Options[i].Options)
                {
                    if (NormalizeString(value).Equals(NormalizeString(opt), System.StringComparison.InvariantCultureIgnoreCase))
                    {
                        SoundManager.PlaySoundByIndex(2);
                        StartCoroutine(PlayAudio(SelectDialogObject.PatienteResponse[i]));                        
                        Animator.SetTrigger(SelectDialogObject.Options[i].AnimationName);
                        DonAlbertoCanvasController.EnlabeMessage(1);
                        return;
                    }                   
                }
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
