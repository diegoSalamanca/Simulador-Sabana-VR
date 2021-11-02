using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UserData", menuName = "ScriptableObjects/Options", order = 1)]
public class OptionsScriptable : ScriptableObject
{
    public string[] Options;

    public string AnimationName;
}
