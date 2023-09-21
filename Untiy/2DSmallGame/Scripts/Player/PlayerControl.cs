using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpSpeed = 20f;
    public GameObject swordCollider, kunaiPrefab;

    public int playerLife;
    public int playerKunai;
    public int playerStone;

    [HideInInspector]
    public bool canJump, isAttack, isHurt, canBeHurt, isThrow;

    [HideInInspector]
    public Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    public AudioClip[] audioClips;
    private AudioSource audioSource;
    //private InputAction playerMove, playerJump, playerAttack, playerThrow;

    private Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        //playerMove = GetComponent<PlayerInput>().currentActionMap["Move"];
        //playerJump = GetComponent<PlayerInput>().currentActionMap["Jump"];
        //playerAttack = GetComponent<PlayerInput>().currentActionMap["Attack"];
        //playerThrow = GetComponent<PlayerInput>().currentActionMap["Throw"];
        canvas = GameObject.Find("/Canvas").GetComponent<Canvas>();

        canJump = true;
        isAttack = false;
        isHurt = false;
        canBeHurt = true;
        isThrow = false;

        playerLife = PlayerPrefs.GetInt("PlayerLife");
        playerKunai = PlayerPrefs.GetInt("PlayerKunai");
        playerStone = PlayerPrefs.GetInt("PlayerStone");
    }

    private void Update()
    {
        Attack();
    }

    private void Attack()
    {
        float attack = Input.GetAxisRaw("Sword");
        float throws = Input.GetAxisRaw("Throw");

        if (attack > 0 && isHurt == false)
        {
            animator.SetTrigger("Attack");
            isAttack = true;
            canJump = false;
            isThrow = true;
        }

        if (throws > 0 && isHurt == false && !animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerThrowAttack") && !animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerSwordAttack") && isThrow == false)
        {
            if (playerKunai > 0)
            {
                playerKunai--;
                PlayerPrefs.SetInt("PlayerKunai", playerKunai);
                animator.SetTrigger("Throw");
                canvas.KunaiUpdate();
                isAttack = true;
                canJump = false;
                isThrow = true;
            }
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float leftRight = Input.GetAxisRaw("Horizontal");
        float jump = Input.GetAxisRaw("Jump");

        if (isAttack || isHurt)
        {
            leftRight = 0;
        }

        // 转头
        if (leftRight > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (leftRight < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        if (jump > 0 && canJump == true && isHurt == false)
        {
            rb.AddForce(Vector2.up * jump * jumpSpeed, ForceMode2D.Impulse);
            animator.SetBool("Jump", true);
            canJump = false;
            isThrow = true;
        }
        // 动画
        animator.SetFloat("Run", Mathf.Abs(leftRight));
        // 移动
        if (!isHurt)
        {
            rb.velocity = new Vector2(leftRight * moveSpeed, rb.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerEvent(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        TriggerEvent(collision);
    }

    private void TriggerEvent(Collider2D collision)
    {
        if (collision.tag == "Enemy" && isHurt == false && canBeHurt == true)
        {
            audioSource.PlayOneShot(audioClips[0]);
            playerLife--;
            PlayerPrefs.SetInt("PlayerLife", playerLife);
            canvas.LifeUpdate();
            if (playerLife > 0)
            {
                isHurt = true;
                canBeHurt = false;
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.5f);
                animator.SetBool("Hurt", true);
                rb.velocity = new Vector2(-2.5f * transform.localScale.x, 10.0f);

                StartCoroutine(SetIsHurtFalse());
            }
            else
            {
                audioSource.PlayOneShot(audioClips[4]);
                isHurt = true;
                isAttack = true;
                rb.velocity = new Vector2(0f, 0f);
                animator.SetBool("Die", true);
                PlayerPrefs.SetInt("PlayerLife", 5);
                FadeInOut.instance.SceneFadeInOut("LevelSelect");
            }
        }

        if (collision.tag == "Item")
        {
            audioSource.PlayOneShot(audioClips[1]);
        }
    }

    IEnumerator SetIsHurtFalse()
    {
        yield return new WaitForSeconds(0.4f);
        animator.SetBool("Hurt", false);
        isHurt = false;
        yield return new WaitForSeconds(0.6f);
        canBeHurt = true;
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "BoundBottom")
        {
            playerLife = 0;
            PlayerPrefs.SetInt("PlayerLife", playerLife);
            canvas.LifeUpdate();
            PlayerPrefs.SetInt("PlayerLife", 5);
            audioSource.PlayOneShot(audioClips[4]);
            isHurt = true;
            isAttack = true;
            rb.velocity = new Vector2(0f, 0f);
            animator.SetBool("Die", true);

            FadeInOut.instance.SceneFadeInOut("LevelSelect");
        }
    }


    // 受伤首帧需要触发时间
    public void SetIsAttackFalse()
    {
        isAttack = false;
        canJump = true;
        isThrow = false;
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Throw");
    }

    public void ForIsHurtSetting()
    {
        isAttack = false;
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Throw");
        swordCollider.SetActive(false);
    }

    public void SetAttackColliderOn()
    {
        swordCollider.SetActive(true);
    }

    public void SetAttackColliderOff()
    {
        swordCollider.SetActive(false);
    }

    public void KunaiInstantiate()
    {
        Vector3 temp = transform.position;
        temp.x += transform.localScale.x;

        Instantiate(kunaiPrefab, temp, Quaternion.identity);
    }

    // 音效
    public void PlaySwordEffect()
    {
        audioSource.PlayOneShot(audioClips[3]);
    }
    public void PlayKunaiEffect()
    {
        audioSource.PlayOneShot(audioClips[2]);
    }
}
