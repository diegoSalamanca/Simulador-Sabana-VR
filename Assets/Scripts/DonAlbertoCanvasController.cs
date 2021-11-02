
using UnityEngine;

public class DonAlbertoCanvasController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] Messages;

    public void EnlabeMessage(int value)
    {
        foreach (var item in Messages)
        {
            item.SetActive(false);
        }
        Messages[value].SetActive(true);

    }

    private void Start()
    {
        EnlabeMessage(0);
    }

}
