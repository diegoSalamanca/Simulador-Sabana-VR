

using UnityEngine;

public class PlayerSelector : MonoBehaviour
{
    public bool selected = false;

    public GameObject maleButton, femaleButton;

    public UserData UserData;

    public void SelectMale()
    {
        UserData.gender = 0;
        selected = true;
        iTween.ScaleTo(maleButton, iTween.Hash("x", 1.2f, "y", 1.2f, "time", 0.3f, "easeType", iTween.EaseType.easeInOutBack));
        femaleButton.transform.localScale = Vector3.one;
        FindObjectOfType<DatosRolBUttonValidator>().ValidatorPlayer();
    }

    public void SelectFemale()
    {
        UserData.gender = 1;
        selected = true;
        iTween.ScaleTo(femaleButton, iTween.Hash("x", 1.2f, "y", 1.2f, "time", 0.3f, "easeType", iTween.EaseType.easeInOutBack));
        maleButton.transform.localScale = Vector3.one;
        FindObjectOfType<DatosRolBUttonValidator>().ValidatorPlayer();
    }
}
