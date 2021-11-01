
using UnityEngine;
using UnityEngine.UI;

public class DisableObject : MonoBehaviour
{
    public GameObject objectOff;
    Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ObjectOff);
    }

    public void ObjectOff()
    {
        objectOff.SetActive(false);
    }
}
