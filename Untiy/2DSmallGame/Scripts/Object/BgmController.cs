using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BgmController : MonoBehaviour
{
    public AudioClip[] bgmClip;
    public AudioClip[] buttonClip;
    [HideInInspector] public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        string levelName = SceneManager.GetActiveScene().name;

        if (levelName == "MainMenu")
        {
            // …Ë÷√“Ù∆µ
            audioSource.clip = bgmClip[0];
            audioSource.loop = true;  // —≠ª∑
            audioSource.volume = 0.3f;
            // ≤•∑≈–¬“Ù∆µ
            audioSource.Play();
        }
        else if (levelName == "Level1" || levelName == "Level2")
        {
            // …Ë÷√“Ù∆µ
            audioSource.clip = bgmClip[1];
            audioSource.loop = true;  // —≠ª∑
            audioSource.volume = 0.3f;
            // ≤•∑≈–¬“Ù∆µ
            audioSource.Play();
        }
        else if (levelName == "Level3")
        {
            // …Ë÷√“Ù∆µ
            audioSource.clip = bgmClip[2];
            audioSource.loop = true;  // —≠ª∑
            audioSource.volume = 0.3f;
            // ≤•∑≈–¬“Ù∆µ
            audioSource.Play();
        }





    }
}
