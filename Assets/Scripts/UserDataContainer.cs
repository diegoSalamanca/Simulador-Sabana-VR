using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDataContainer : MonoBehaviour
{
    // Start is called before the first frame update

    public TMPro.TextMeshProUGUI name, id; 

    public UserData UserData;
   

    // Update is called once per frame
    void Update()
    {
        name.text = UserData.name;
        id.text = UserData.id;
    }
}
