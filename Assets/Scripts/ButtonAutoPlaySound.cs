
using UnityEngine;
using UnityEngine.UI;

public class ButtonAutoPlaySound : MonoBehaviour
{
    Button ActiveButton;
    public int SoundIndex;

    // Start is called before the first frame update
    void Start()
    {
        ActiveButton = GetComponent<Button>();
        ActiveButton.onClick.AddListener(PlayUiSoundButton);
    }

    private void PlayUiSoundButton()
    {
        SoundManager.Instance.PlaySoundByIndex(0);
    }
}
