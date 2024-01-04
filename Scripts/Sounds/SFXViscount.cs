using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXViscount : MonoBehaviour
{
    AudioSource sfx;
    [SerializeField] AudioClip correctSound;
    [SerializeField] AudioClip incorrectSound;
    [SerializeField] AudioClip winSound;
    [SerializeField] AudioClip christmas;
    [SerializeField] AudioClip cheering;
    [SerializeField] AudioClip nextQuestion;

    void Start()
    {
       sfx = GetComponent<AudioSource>();
    }

    public void PlayCorrectOrIncorrectSound(bool good)
    {
        if (good)
        {
            sfx.clip = correctSound;
            sfx.Play();
        } else
        {
            sfx.clip = incorrectSound;
            sfx.Play();
        }
    }

    public void PlaySFX(string sound)
    {
        switch (sound)
        {
            case "win":
                sfx.clip = cheering;
                break;
            case "start":
                sfx.clip = christmas;
                sfx.volume = .7f;
                break;
            case  "question":
                sfx.clip = nextQuestion;
                break;
        }
        Debug.Log(sound);
        sfx.Play();
        sfx.volume = 1f;
    }
    public void PlayWinSound()
    {
        sfx.clip = winSound;
        sfx.Play();
    }
}
