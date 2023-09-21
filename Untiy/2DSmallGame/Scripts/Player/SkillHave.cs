using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHave : MonoBehaviour
{
    string skillName;
    // Start is called before the first frame update
    void Start()
    {
        skillName = name;
        int skillHave = PlayerPrefs.GetInt(skillName, 0);
        if (skillHave == 0)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            PlayerPrefs.SetInt(skillName, 1);

            string skillLoad = "Canvas/SafeAreaPanel/PlayerTransport/" + skillName;
            GameObject skill = GameObject.Find(skillLoad);
            skill.SetActive(true);

            gameObject.SetActive(false);
        }
    }

}
