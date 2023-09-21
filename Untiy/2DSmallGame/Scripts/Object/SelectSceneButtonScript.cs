using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectSceneButtonScript : MonoBehaviour
{
    public Sprite sprite;
    public int allLevel = 3;

    private int clearedLevel;

    Image[] imageBtns;

    // Start is called before the first frame update
    void Start()
    {
        imageBtns = new Image[allLevel];

        for (int i = 0; i < allLevel; i++)
        {
            string imageTip = "Canvas/SafeAreaPanel/Level" + (i + 1).ToString() + "Button";
            imageBtns[i] = GameObject.Find(imageTip).GetComponent<Image>();
        }

        clearedLevel = PlayerPrefs.GetInt("clearedLevel", 0);


        for (int i = 0; i < clearedLevel + 1; i++)
        {
            imageBtns[i].sprite = sprite;
        }

    }

    // Update is called once per frame
    public void GotoLevel(string level)
    {
        string levelTip = "level";
        levelTip += level;

        if (clearedLevel + 2 > int.Parse(level))
        {
            BgmController bgm = GameObject.Find("BgmController").GetComponent<BgmController>();
            bgm.audioSource.PlayOneShot(bgm.buttonClip[0]);
            FadeInOut.instance.SceneFadeInOut(levelTip);
        }
        else
        {
            BgmController bgm = GameObject.Find("BgmController").GetComponent<BgmController>();
            bgm.audioSource.PlayOneShot(bgm.buttonClip[1]);
        }
    }

    public void GoToMainMenu()
    {
        BgmController bgm = GameObject.Find("BgmController").GetComponent<BgmController>();
        bgm.audioSource.PlayOneShot(bgm.buttonClip[0]);
        FadeInOut.instance.SceneFadeInOut("MainMenu");

    }
}
