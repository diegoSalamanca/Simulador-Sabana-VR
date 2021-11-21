using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text.RegularExpressions;
using BestHTTP;

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

    string Especialidad, Genero;

    System.DateTime InitialDate;


    public void SaveQuizzData()
    {
        var Data = new QuizzDataObject();
        Data.estudiante = int.Parse(UserData.id);        
        Data.rol = Especialidad;
        Data.genero = Genero;
        Data.estado = 80;
        Data.empatia = 80;
        Data.tacto = 80;
        Data.comunicacion = "buena";
        Data.contacto_visual = 80;
        Data.tiempo_de_prueba = (int)(System.DateTime.Now - InitialDate).TotalSeconds;

        Data.historia.titulo = "Listado de preguntas";
        Data.historia.rol = Especialidad;
        Data.historia.nombre = UserData.name;

        var subhistoria = new SubHistoria();
        Data.historia.historia.Add(subhistoria);
        subhistoria.seccion = "Saludo/Presentación";

        for (int i = 0; i < SelectDialogObject.Options.Length; i++)
        {
            var valor = new Valores();
            valor.nombre = SelectDialogObject.Options[i].name;
            valor.valor = SelectDialogObject.Options[i].valor;
            Data.historia.historia[0].valores.Add(valor);
        }      

        ApiManager.Instance.SaveQuizzData(Data);    
    }

    public void EndApplication(HTTPRequest request, HTTPResponse response)
    {        
        print("test Response Code = " + response.StatusCode);
        print("test Response = " + response.DataAsText);
        print("Exit App");
        Application.Quit();
    }

    public void ResetDataScriptables()
    {
        foreach (var item in SelectDialogObject.Options)
        {
            item.valor = false;
        }
    
    }

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
        InitialDate = System.DateTime.Now;

        SoundManager = SoundManager.Instance;
         
        Animator = GetComponent<Animator>();
        ActivatePatientDialog = false;

        SelectDialogScriptable();

        ResetDataScriptables();

        PacienteCanvasManager.Instance.SetNeutralmessage("Presione el botón – A - para hablar. Hable despacio y claro. Cuando termine suelte el botón para que Jairo escuche su frase.");
    }

    public void SelectDialogScriptable()
    {
        if (UserData.gender == 0)
        {
            Genero = "Masculino";
            switch (UserData.especialidad)
            {
                default:
                    SelectDialogObject = DialogsScriptableDoctorMale;
                    Especialidad = "Medicina";
                    break;

                case 0:
                    SelectDialogObject = DialogsScriptableDoctorMale;
                    Especialidad = "Medicina";
                    break;

                case 1:
                    SelectDialogObject = DialogsScriptableNurseMale;
                    Especialidad = "Enfermería";
                    break;

                case 2:
                    SelectDialogObject = DialogsScriptableFisioMale;
                    Especialidad = "Fisioterapia";
                    break;
            }
            
        }
        else
        {
            Genero = "Femenino";
            switch (UserData.especialidad)
            {
                default:
                    SelectDialogObject = DialogsScriptableDoctorFemale;
                    Especialidad = "Medicina";
                    break;

                case 0:
                    SelectDialogObject = DialogsScriptableDoctorFemale;
                    Especialidad = "Medicina";
                    break;

                case 1:
                    SelectDialogObject = DialogsScriptableNurseFemale;
                    Especialidad = "Enfermería";
                    break;

                case 2:
                    SelectDialogObject = DialogsScriptableFisioFemale;
                    Especialidad = "Fisioterapia";
                    break;
            }
           
        }
    }

    public void SetActivatePatientDialog(bool value)
    {
        ActivatePatientDialog = value;
    }


    IEnumerator PlayAudio(OptionsScriptable option, bool camilla)
    {

        option.valor = true;

        yield return new WaitForSeconds(0.5f);
        AudioSource.PlayOneShot(option.PatienteResponse);

        if (camilla)
            yield break;

        yield return new WaitUntil(() => AudioSource.isPlaying == false);
        Animator.SetTrigger("idle_camilla");
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
                        var camilla = false;

                        if (SelectDialogObject.Options[i].name == "acuestese")
                            camilla = true;

                        StartCoroutine(PlayAudio(SelectDialogObject.Options[i], camilla));                        
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
        var camilla = false;

        if(option.name == "acuestese")
            camilla = true; 


        StartCoroutine(PlayAudio(option, camilla));
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
