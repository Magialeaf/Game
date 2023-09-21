using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReItem : MonoBehaviour
{
    private PlayerControl player;
    private Canvas canvas;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerControl>();
        canvas = GameObject.Find("/Canvas").GetComponent<Canvas>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 查找第一个空格的位置
        int spaceIndex = gameObject.name.IndexOf(" ");

        // 提取前面的字母部分
        string objectName = (spaceIndex != -1) ? gameObject.name.Substring(0, spaceIndex) : gameObject.name;

        if (collision.name == "Player" && objectName == "ReLife")
        {
            int life = PlayerPrefs.GetInt("PlayerLife") + 1;
            PlayerPrefs.SetInt("PlayerLife", life);
            player.playerLife = life;
            canvas.LifeUpdate();
            Destroy(this.gameObject);
        }
        else if (collision.name == "Player" && objectName == "ReKunai")
        {
            int kunai = PlayerPrefs.GetInt("PlayerKunai") + 3;
            PlayerPrefs.SetInt("PlayerKunai", kunai);
            player.playerKunai = kunai;
            canvas.KunaiUpdate();
            Destroy(this.gameObject);
        }
        else if (collision.name == "Player" && objectName == "ReStone")
        {
            int stone = PlayerPrefs.GetInt("PlayerStone") + 3;
            PlayerPrefs.SetInt("PlayerStone", stone);
            player.playerStone = stone;
            canvas.StoneUpdate();
            Destroy(this.gameObject);
        }
    }
}
