using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "UserData", menuName = "ScriptableObjects/Dialogs", order = 1)]
public class DialogsScriptable : ScriptableObject
{
    public AudioClip[] PatienteResponse;

    public OptionsScriptable[] Options;
    
}
