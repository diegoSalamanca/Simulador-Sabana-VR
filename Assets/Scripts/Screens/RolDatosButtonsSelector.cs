using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BestHTTP;
using UnityEngine.UI;

public class RolDatosButtonsSelector : MonoBehaviour
{

    public GameObject activeButtonRol , activeButtonDatos, rolContainer, datosContainer, playerContainer, errorMessage, buttonNextRol, userDataContainer;
    public InputField  dataID;
    public UserData UserData;



    // Start is called before the first frame update
    void Start()
    {
        SwitchToDatos();
        buttonNextRol.SetActive(true);
        errorMessage.SetActive(false);
        userDataContainer.SetActive(false);
    }


    public void TryLogin()
    {
        ApiManager.Instance.TryLogin(dataID.text);
        buttonNextRol.SetActive(false);
        errorMessage.SetActive(false);
    }

    public void LoginResponse(HTTPRequest request, HTTPResponse response)
    {
        if (response.StatusCode > 399)
        {
            errorMessage.SetActive(true);
            buttonNextRol.SetActive(true);
            return;
        }
        
        var data = response.DataAsText;
        print(data);

        var dto = JsonUtility.FromJson<DataObjectSerializable>(data);
        UserData.id = dto.id_siiga;
        UserData.name = dto.name;
        SwitchToRol();
    }

    

    public void SwitchToRol()
    {
        activeButtonRol.SetActive(true);
        activeButtonDatos.SetActive(false);
        rolContainer.SetActive(true);
        datosContainer.SetActive(false);
        playerContainer.SetActive(false);
        userDataContainer.SetActive(true);
    }

    public void SwitchToDatos()
    {
        activeButtonRol.SetActive(false);
        activeButtonDatos.SetActive(true);
        rolContainer.SetActive(false);
        datosContainer.SetActive(true);
        playerContainer.SetActive(false);
        errorMessage.SetActive(false);
        buttonNextRol.SetActive(true);
        userDataContainer.SetActive(false);
    }

    public void SwitchToPlayer()
    {
        activeButtonRol.SetActive(true);
        activeButtonDatos.SetActive(false);
        rolContainer.SetActive(false);
        datosContainer.SetActive(false);
        playerContainer.SetActive(true);
    }
}
