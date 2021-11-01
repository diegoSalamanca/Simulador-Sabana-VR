using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPanel : MonoBehaviour
{

    public GameObject CloseConfirm;
    Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(EnableConfirm);
    }

    public void EnableConfirm()
    {
        CloseConfirm.transform.SetAsLastSibling();
        CloseConfirm.SetActive(true);
        iTween.ScaleFrom(CloseConfirm, Vector3.zero, 0.3f);
    }
    
}
