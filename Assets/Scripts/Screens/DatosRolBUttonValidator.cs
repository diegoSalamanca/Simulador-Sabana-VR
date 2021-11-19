
using UnityEngine;
using UnityEngine.UI;


public class DatosRolBUttonValidator : MonoBehaviour
{

    public InputField dataName, dataID;

    public RolSwitch rolSwitch;

    public PlayerSelector PlayerSelector;

    public GameObject nextButon, nextButonRol, nextButonPlayer;


  
    public void ValidatorDatos()
    {
        if (/*dataName.text.Length > 1 &&*/ dataID.text.Length > 1)
        {
            iTween.ScaleTo(nextButonRol, iTween.Hash("x", 1, "y", 1, "z", 1, "time", 0.5f, "easeType", iTween.EaseType.easeInOutBack));
        }

        else
        {
            nextButonRol.transform.localScale = Vector3.zero;
        }
    }

    public void ValidatorRol()
    {
        if (rolSwitch.selected)
        {
            iTween.ScaleTo(nextButonPlayer, iTween.Hash("x", 1, "y", 1, "z", 1, "time", 0.5f, "easeType", iTween.EaseType.easeInOutBack));
        }

        else
        {
            nextButonPlayer.transform.localScale = Vector3.zero;
        }
    }

    public void ValidatorPlayer()
    {
        if (PlayerSelector.selected)
        {
            iTween.ScaleTo(nextButon, iTween.Hash("x", 1, "y", 1, "z", 1, "time", 0.5f, "easeType", iTween.EaseType.easeInOutBack));
        }

        else
        {
            nextButon.transform.localScale = Vector3.zero;
        }
    }



}
