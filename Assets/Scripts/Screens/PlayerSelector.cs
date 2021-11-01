

using UnityEngine;

public class PlayerSelector : MonoBehaviour
{
    public bool selected = false;

    public GameObject maleButton, femaleButton;

    public void SelectMale()
    {
        selected = true;
        iTween.ScaleTo(maleButton, iTween.Hash("x", 1.2f, "y", 1.2f, "time", 0.3f, "easeType", iTween.EaseType.easeInOutBack));
        femaleButton.transform.localScale = Vector3.one;
        FindObjectOfType<DatosRolBUttonValidator>().ValidatorPlayer();
    }

    public void SelectFemale()
    {
        selected = true;
        iTween.ScaleTo(femaleButton, iTween.Hash("x", 1.2f, "y", 1.2f, "time", 0.3f, "easeType", iTween.EaseType.easeInOutBack));
        maleButton.transform.localScale = Vector3.one;
        FindObjectOfType<DatosRolBUttonValidator>().ValidatorPlayer();
    }
}
