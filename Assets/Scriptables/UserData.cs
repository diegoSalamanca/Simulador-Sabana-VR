

using UnityEngine;

[CreateAssetMenu(fileName = "UserData", menuName = "ScriptableObjects/UserData", order = 1)]

public class UserData : ScriptableObject
{
    public string name;
    public int id;
    public int especialidad;
    public int skin;

}