using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SkillManage : MonoBehaviour
{
    public int skillNum;
    public AudioClip[] audioClips;

    public GameObject swordLight;
    public GameObject kumiPrefab;

    private AudioSource audioSource;
    private Image[] skillImages;
    private Image[] coolingImages;
    private float[] coolingTime;
    private float[] currentTime;
    private InputAction[] playerSkill;

    private GameObject player;
    private PlayerControl playerControl;
    private Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        skillNum = 3;
        skillImages = new Image[skillNum];
        coolingImages = new Image[skillNum];
        playerSkill = new InputAction[skillNum];
        audioSource = GetComponent<AudioSource>();
        player = GameObject.Find("Player");
        playerControl = player.GetComponent<PlayerControl>();
        canvas = GameObject.Find("/Canvas").GetComponent<Canvas>();

        coolingTime = new float[3] { 8f, 30f, 15f };
        currentTime = new float[3];

        Image[] allImages = GetComponentsInChildren<Image>();

        for (int i = 0; i < skillNum; i++)
        {
            skillImages[i] = allImages[i * 2];
            coolingImages[i] = allImages[i * 2 + 1];

            string skillName = "Skill" + (i + 1).ToString();
            int skillHave = PlayerPrefs.GetInt(skillName, 0);
            if (skillHave == 0)
            {
                skillImages[i].gameObject.SetActive(false);
            }
            currentTime[i] = 0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        IfPressSkill();
        IfIsCooling();
    }

    private void IfPressSkill()
    {
        for (int i = 0; i < skillNum; i++)
        {
            string skillName = "Skill" + (i + 1).ToString();
            float value = Input.GetAxisRaw(skillName);
            if (value > 0 && currentTime[i] <= 0)
            {
                Invoke(skillName, 0);
            }
        }
    }

    private void Skill1()
    {
        if (playerControl.playerKunai >= 2)
        {
            coolingImages[0].fillAmount = 1.0f;
            currentTime[0] = coolingTime[0];
            audioSource.PlayOneShot(audioClips[0]);

            playerControl.playerKunai -= 2;
            PlayerPrefs.SetInt("PlayerKunai", playerControl.playerKunai);
            canvas.KunaiUpdate();

            float positionY = 2f;

            Vector3 position = player.transform.position;

            for (int i = 0; i < 5; i++)
            {
                GameObject kumiInstance = Instantiate(kumiPrefab);

                // 设置生成的实例的位置
                Vector3 newPosition = new Vector3(position.x, position.y + positionY, position.z);
                kumiInstance.transform.position = newPosition;

                // 更新下一个预制件的 X 位置
                positionY--;
            }
        }
    }

    private void Skill2()
    {
        if (playerControl.playerStone >= 3)
        {
            coolingImages[1].fillAmount = 1.0f;
            currentTime[1] = coolingTime[1];
            audioSource.PlayOneShot(audioClips[1]);

            playerControl.playerStone -= 3;
            PlayerPrefs.SetInt("PlayerStone", playerControl.playerStone);
            canvas.StoneUpdate();


            Time.timeScale = 0f;
            StartCoroutine(Skill2Close());
        }


    }

    IEnumerator Skill2Close()
    {
        Vector3 scale = player.transform.localScale;

        for (int i = 1; i < 10; i++)
        {
            for (int j = 1; j < 10; j++)
            {
                GameObject kumiInstance = Instantiate(kumiPrefab);

                // 设置生成的实例的位置
                Vector3 screenPosition = new Vector3(0.1f * i * Screen.width, 0.1f * j * Screen.height, 0f);
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
                kumiInstance.transform.position = new Vector3(worldPosition.x, worldPosition.y, 0f);

                Kunai kunai = kumiInstance.GetComponent<Kunai>();
                kunai.speed = 2f;

                kumiInstance.transform.localScale = new Vector3(-1f, 1f, 1f);

                if (i < 5)
                {
                    player.transform.localScale = new Vector3(-1f, 1f, 1f);
                }
                else
                {
                    player.transform.localScale = new Vector3(1f, 1f, 1f);
                }

                yield return new WaitForSecondsRealtime(0.02f);
            }
        }

        yield return new WaitForSecondsRealtime(0.5f);

        player.transform.localScale = scale;

        Time.timeScale = 1.0f;
    }

    private void Skill3()
    {
        if (playerControl.playerStone >= 2)
        {
            coolingImages[2].fillAmount = 1.0f;
            currentTime[2] = coolingTime[2];
            audioSource.PlayOneShot(audioClips[2]);

            playerControl.playerStone -= 2;
            PlayerPrefs.SetInt("PlayerStone", playerControl.playerStone);
            canvas.StoneUpdate();

            swordLight.SetActive(true);
            StartCoroutine(Skill3Close());
        }
    }

    IEnumerator Skill3Close()
    {
        yield return new WaitForSeconds(1.5f);
        swordLight.SetActive(false);
    }

    private void IfIsCooling()
    {
        for (int i = 0; i < skillNum; i++)
        {
            if (currentTime[i] > 0)
            {
                //按时间比例计算出Fill Amount值
                currentTime[i] -= Time.deltaTime;
                coolingImages[i].fillAmount = currentTime[i] / coolingTime[i];
            }
        }
    }
}
