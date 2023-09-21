using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelButtonScript : MonoBehaviour
{
    public GameObject selectPanel, stopButton, level;

    public void SetSelectPanelOn()
    {
        selectPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void SetSelectPanelOff()
    {
        selectPanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void SetStopButtonOn()
    {
        ButtonPressAudio();
        stopButton.SetActive(true);
    }

    public void SetStopButtonOff()
    {
        ButtonPressAudio();
        stopButton.SetActive(false);
    }


    public void ButtonPressAudio()
    {
        BgmController bgm = GameObject.Find("BgmController").GetComponent<BgmController>();
        bgm.audioSource.PlayOneShot(bgm.buttonClip[0]);
    }

    public void MainMenuPlayButton()
    {
        GameObject mainMenuPlayer = GameObject.Find("MainMenuPlayer");
        Animator animation = mainMenuPlayer.GetComponent<Animator>();
        animation.SetBool("Start", true);

        GameObject startGame = GameObject.Find("Canvas/SafeAreaPanel/PlayButton");
        Button playButton = startGame.GetComponent<Button>();
        playButton.interactable = false;

        ButtonPressAudio();

        FadeInOut.instance.SceneFadeInOut("LevelSelect");
    }


    public void DataDeleteButton()
    {
        RectTransform dataDeleteImage = GameObject.Find("Canvas/SafeAreaPanel/DataDeleteImage").GetComponent<RectTransform>();
        dataDeleteImage.anchoredPosition = Vector2.zero;

        ButtonPressAudio();

    }

    public void YesDelete()
    {
        PlayerPrefs.DeleteAll();
        IsFirstTimePlayCheck checkScript = GameObject.Find("IsFirstTimePlayCheck").GetComponent<IsFirstTimePlayCheck>();
        checkScript.FirstTimePlayerState();

        RectTransform dataDeleteImage = GameObject.Find("Canvas/SafeAreaPanel/DataDeleteImage").GetComponent<RectTransform>();
        dataDeleteImage.anchoredPosition = new Vector2(0f, 1500f);

        ButtonPressAudio();
    }

    public void NoDelete()
    {
        RectTransform dataDeleteImage = GameObject.Find("Canvas/SafeAreaPanel/DataDeleteImage").GetComponent<RectTransform>();
        dataDeleteImage.anchoredPosition = new Vector2(0f, 1500f);

        ButtonPressAudio();
    }

    public void LevelSelectButton()
    {
        ButtonPressAudio();
        FadeInOut.instance.SceneFadeInOut("LevelSelect");
    }

    public void MainMenuButton()
    {
        ButtonPressAudio();
        FadeInOut.instance.SceneFadeInOut("MainMenu");
    }

    public void ReplayButton()
    {
        ButtonPressAudio();
        string sceneName = SceneManager.GetActiveScene().name;
        FadeInOut.instance.SceneFadeInOut(sceneName);
    }

}

