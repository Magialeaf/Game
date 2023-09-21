using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyPumpkinManAI : MonoBehaviour
{
    public float jumpSpeed, slideSpeed, enemyLife, attackDistance;
    private Vector3 jumpDownTargetPosition, slideTargetPositon;

    private bool isAlive, isIdle, jumpAttack, isJumpUp, slideAttack, isHurt, canBeHurt;

    private Animator animator;
    private BoxCollider2D box;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;

    private GameObject player;

    private void Awake()
    {
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        isAlive = true;
        isIdle = true;
        jumpAttack = false;
        isJumpUp = true;
        slideAttack = false;
        isHurt = false;
        canBeHurt = true;
    }

    private void Update()
    {
        if (isAlive)
        {
            State();
        }
        else
        {
            Vector3 targetPosition = new Vector3(transform.position.x, -2f, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 10f * Time.deltaTime);
        }
    }
    private void LookAtPlayer()
    {
        if (player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        else
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
    }

    private void State()
    {
        if (isIdle)
        {
            LookAtPlayer();
            if (Vector3.Distance(player.transform.position, transform.position) <= attackDistance)
            {
                isIdle = false;
                slideAttack = true;
                StartCoroutine(IdleToSlideAttack());
            }
            else
            {
                isIdle = false;
                StartCoroutine(IdleToJumpAttack());
            }
        }
        else if (jumpAttack)
        {
            LookAtPlayer();
            if (isJumpUp)
            {
                Vector3 targetY = Camera.main.ScreenToWorldPoint(new Vector3(0f, Screen.height * 0.85f, 0f));
                Vector3 target = new Vector3(player.transform.position.x, targetY.y, 0);

                transform.position = Vector3.MoveTowards(transform.position, target, jumpSpeed * Time.deltaTime);

                animator.SetBool("JumpUp", true);

                if (transform.position.y == targetY.y)
                {
                    isJumpUp = false;
                    jumpDownTargetPosition = new Vector3(player.transform.position.x, player.transform.position.y, 0);
                }
            }
            else
            {

                transform.position = Vector3.MoveTowards(transform.position, jumpDownTargetPosition, jumpSpeed * Time.deltaTime);

                animator.SetBool("JumpUp", false);
                animator.SetBool("JumpDown", true);

                if (transform.position.y == jumpDownTargetPosition.y)
                {
                    jumpAttack = false;
                    StartCoroutine(JumpDownToIdle());
                }
            }
        }
        else if (slideAttack)
        {
            animator.SetBool("Slide", true);

            transform.position = Vector3.MoveTowards(transform.position, slideTargetPositon, slideSpeed * Time.deltaTime);

            if (transform.position == slideTargetPositon)
            {
                box.offset = new Vector2(-0.1709125f, -0.2050955f);
                box.size = new Vector2(0.9728885f, 1.905259f);
                animator.SetBool("Slide", false);
                slideAttack = false;
                isIdle = true;
            }
        }
        else if (isHurt)
        {
            Vector3 targetPosition = new Vector3(transform.position.x, -2f, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 10f * Time.deltaTime);
        }
    }

    IEnumerator IdleToJumpAttack()
    {
        yield return new WaitForSeconds(1.0f);
        jumpAttack = true;
    }

    IEnumerator JumpDownToIdle()
    {
        yield return new WaitForSeconds(0.5f);
        isIdle = true;
        isJumpUp = true;
        animator.SetBool("JumpUp", false);
        animator.SetBool("JumpDown", false);
    }
    IEnumerator IdleToSlideAttack()
    {
        yield return new WaitForSeconds(1.0f);
        box.offset = new Vector2(-0.1709125f, -0.490155f);
        box.size = new Vector2(0.9728885f, 1.33514f);
        LookAtPlayer();
        slideTargetPositon = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        slideAttack = true;
    }

    IEnumerator SetAnimHurtToFalse()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Hurt", false);
        animator.SetBool("JumpUp", false);
        animator.SetBool("JumpDown", false);
        animator.SetBool("Slide", false);
        spriteRenderer.material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        isHurt = false;
        isIdle = true;

        yield return new WaitForSeconds(2.0f);
        spriteRenderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        canBeHurt = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerAttack(collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerAttack(collision);
    }

    private void PlayerAttack(Collider2D collision)
    {
        if (collision.tag == "PlayerAttack")
        {
            if (canBeHurt)
            {
                audioSource.PlayOneShot(audioSource.clip);
                enemyLife--;

                if (enemyLife > 0)
                {
                    isIdle = false;
                    jumpAttack = false;
                    slideAttack = false;
                    canBeHurt = false;
                    isHurt = true;

                    StopCoroutine(JumpDownToIdle());
                    StopCoroutine(IdleToSlideAttack());
                    StopCoroutine(IdleToJumpAttack());

                    animator.SetBool("Hurt", true);
                    StartCoroutine(SetAnimHurtToFalse());
                }
                else
                {
                    isAlive = false;
                    box.enabled = false;
                    StopAllCoroutines();
                    animator.SetBool("Die", true);

                    Time.timeScale = 0.5f;
                    StartCoroutine(AfterDie());
                }
            }
        }
    }

    IEnumerator AfterDie()
    {
        yield return new WaitForSecondsRealtime(3.0f);
        FadeInOut.instance.SceneFadeInOut("LevelSelect");
    }
}
