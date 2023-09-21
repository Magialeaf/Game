using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeInOut : MonoBehaviour
{
    public static FadeInOut instance;
    public GameObject fadeInOutImage;
    public Animator animator;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void SceneFadeInOut(string levelName)
    {
        StartCoroutine(SceneFadeInOutInterface(levelName));
    }

    IEnumerator SceneFadeInOutInterface(string levelName)
    {
        fadeInOutImage.SetActive(true);
        yield return new WaitForSecondsRealtime(1.5f);
        SceneManager.LoadScene(levelName);
        animator.Play("FadeOut");

        yield return new WaitForSecondsRealtime(1.5f);
        fadeInOutImage.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
