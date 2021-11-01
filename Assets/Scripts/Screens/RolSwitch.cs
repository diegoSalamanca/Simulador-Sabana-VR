
using UnityEngine;

public class RolSwitch : MonoBehaviour
{
    public GameObject activeEnfermeria, activeFisioterapia, activeMedicina;

    public UserData userData;

    public bool selected = false;
  

    public void SwitchToMedicina()
    {
        userData.especialidad = 0;
        activeEnfermeria.SetActive(false);
        activeFisioterapia.SetActive(false);
        activeMedicina.SetActive(true);
        selected = true;
        Validation();
    }

    public void SwitchToEnfermeria()
    {
        userData.especialidad = 1;
        activeEnfermeria.SetActive(true);
        activeFisioterapia.SetActive(false);
        activeMedicina.SetActive(false);
        selected = true;
        Validation();
    }

    public void SwitchToFisioterapia()
    {
        userData.especialidad = 2;
        activeEnfermeria.SetActive(false);
        activeFisioterapia.SetActive(true);
        activeMedicina.SetActive(false);
        selected = true;
        Validation();
    }

    public void Validation()
    {
        FindObjectOfType<DatosRolBUttonValidator>().ValidatorRol();
    }


}
