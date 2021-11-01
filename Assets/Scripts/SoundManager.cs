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



    public AudioClip[] Clips;
    public AudioClip[] Backgrounds;
    public  AudioSource AudioSourceShorts, AudioSourcebackgrounds;

    public void PlaybuttonUiSound()
    {
        //AudioSource.clip = buttonUiSound;
        // AudioSource.time = 0.2f;
        //AudioSource.Play();
        AudioSourceShorts.PlayOneShot(Clips[0]);
    }
    public void PlayAuidoRecognicerStartSound()
    {
        AudioSourceShorts.PlayOneShot(Clips[1]);
    }

    void Start()
    {
        AudioSourceShorts = GetComponent<AudioSource>();
    }  

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySoundByIndex(int index)
    {
        AudioSourceShorts.PlayOneShot(Clips[index]);
    }

    public void PlayBackgrounnds(int index)
    {
        AudioSourcebackgrounds.clip = Clips[index];
        AudioSourcebackgrounds.Play();
    }
}
