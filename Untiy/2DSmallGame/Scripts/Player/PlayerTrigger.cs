using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    private PlayerControl playerScript;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GetComponentInParent<PlayerControl>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            playerScript.canJump = true;
            playerScript.isThrow = false;
            playerScript.animator.SetBool("Jump", false);
        }
        else if (collision.tag == "AirPlatform")
        {
            playerScript.canJump = true;
            playerScript.isThrow = false;
            playerScript.animator.SetBool("Jump", false);

            playerScript.transform.parent = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "AirPlatform")
        {
            playerScript.transform.parent = null;
        }
    }
}
