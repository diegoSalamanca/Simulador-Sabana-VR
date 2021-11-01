using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update

    private static SoundManager _instance;
    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<SoundManager>();
            }

            return _instance;
        }
    }

   

    public AudioClip UserInterfaceClick, AuidoRecognicerStart;
    private AudioSource AudioSource;

    public void PlaybuttonUiSound()
    {
        //AudioSource.clip = buttonUiSound;
       // AudioSource.time = 0.2f;
        //AudioSource.Play();
        AudioSource.PlayOneShot(UserInterfaceClick);
    }
    public void PlayAuidoRecognicerStartSound()
    {        
        AudioSource.PlayOneShot(AuidoRecognicerStart);
    }

    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }
   

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
