
using UnityEngine;
using UnityEngine.UI;

public class SkinSelector : MonoBehaviour
{
    public UserData userData;

    public PjTexturesScribtable texturesScribtable;

    public Image maleImage, femaleImage;

    public GameObject skin1Button, skin2Button, skin3Button;
    void Start()
    {
        SelectSkin1();
    }

    private void OnEnable()
    {
        UpdateSkin();
    }


    public void SelectSkin1()
    {
        maleImage.sprite = texturesScribtable.GetSpritesMaleEspecialidad(userData.especialidad)[0];
        femaleImage.sprite = texturesScribtable.GetSpritesFemaleEspecialidad(userData.especialidad)[0];
        iTween.ScaleTo(skin1Button, iTween.Hash("x", 1.2f, "y", 1.2f, "time", 0.3f, "easeType", iTween.EaseType.easeInOutBack));
        skin2Button.transform.localScale = Vector3.one;
        skin3Button.transform.localScale = Vector3.one;
        userData.skin = 0;
    }

    public void SelectSkin2()
    {
        maleImage.sprite = texturesScribtable.GetSpritesMaleEspecialidad(userData.especialidad)[1];
        femaleImage.sprite = texturesScribtable.GetSpritesFemaleEspecialidad(userData.especialidad)[1];
        iTween.ScaleTo(skin2Button, iTween.Hash("x", 1.2f, "y", 1.2f, "time", 0.3f, "easeType", iTween.EaseType.easeInOutBack));
        skin1Button.transform.localScale = Vector3.one;
        skin3Button.transform.localScale = Vector3.one;
        userData.skin = 1;
    }

    public void SelectSkin3()
    {
        maleImage.sprite = texturesScribtable.GetSpritesMaleEspecialidad(userData.especialidad)[2];
        femaleImage.sprite = texturesScribtable.GetSpritesFemaleEspecialidad(userData.especialidad)[2];
        iTween.ScaleTo(skin3Button, iTween.Hash("x", 1.2f, "y", 1.2f, "time", 0.3f, "easeType", iTween.EaseType.easeInOutBack));
        skin2Button.transform.localScale = Vector3.one;
        skin1Button.transform.localScale = Vector3.one;
        userData.skin = 2;
    }

    public void UpdateSkin()
    {
        switch (userData.skin)
        {
            default:
                SelectSkin1();
                break;

            case 0:
                SelectSkin1();
                break;

            case 1:
                SelectSkin2();
                break;

            case 2:
                SelectSkin3();
                break;
        }
    }
}
