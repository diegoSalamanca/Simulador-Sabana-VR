using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RolDatosButtonsSelector : MonoBehaviour
{

    public GameObject activeButtonRol , activeButtonDatos, rolContainer, datosContainer, playerContainer;
    // Start is called before the first frame update
    void Start()
    {
        SwitchToDatos();
    }

    

    public void SwitchToRol()
    {
        activeButtonRol.SetActive(true);
        activeButtonDatos.SetActive(false);
        rolContainer.SetActive(true);
        datosContainer.SetActive(false);
        playerContainer.SetActive(false);
    }

    public void SwitchToDatos()
    {
        activeButtonRol.SetActive(false);
        activeButtonDatos.SetActive(true);
        rolContainer.SetActive(false);
        datosContainer.SetActive(true);
        playerContainer.SetActive(false);
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
