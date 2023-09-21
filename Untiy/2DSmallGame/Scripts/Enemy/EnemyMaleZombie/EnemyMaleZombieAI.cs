using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyMaleZombieAI : MonoBehaviour
{
    public float speed;
    public int enemyLife;

    public Vector3 targetPosition, originPosition, terminalPosition;
    [SerializeField]
    protected AudioClip[] audioClips;

    protected bool isAfterBattleCheck, isAlive, canHurt;

    protected BoxCollider2D box;
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;
    protected AudioSource audioSource;
    public GameObject attackCollider;

    protected GameObject player;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        player = GameObject.Find("Player");

        originPosition = transform.position;
        targetPosition = terminalPosition;
        isAfterBattleCheck = false;

        isAlive = true;
        canHurt = true;

        if (transform.position.x > terminalPosition.x)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (isAlive)
        {
            Move();
            Attack();
        }
    }

    protected virtual void Move()
    {
        if (transform.position == targetPosition)
        {
            animator.SetTrigger("Idle");

            StartCoroutine(TurnScale());
            // 交换origin和target位置的值
            if (targetPosition == originPosition)
            {
                targetPosition = terminalPosition;
            }
            else
            {
                targetPosition = originPosition;
            }
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }

    IEnumerator TurnScale()
    {
        yield return new WaitForSeconds(3.0f);
        if (transform.position.x > targetPosition.x)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        else
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

    }

    protected virtual void Attack()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 1.3f)
        {
            if (player.transform.position.x <= transform.position.x)
            {
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            }
            else
            {
                transform.localScale = new Vector3(1f, 1.0f, 1.0f);
            }

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") || animator.GetCurrentAnimatorStateInfo(0).IsName("AttackIdle"))
            {
                return;
            }

            audioSource.PlayOneShot(audioClips[1]);
            animator.SetTrigger("Attack");
            isAfterBattleCheck = true;
        }
        else
        {
            if (isAfterBattleCheck)
            {
                if (targetPosition == originPosition)
                {
                    transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                }
                else
                {
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                }
                isAfterBattleCheck = false;
            }
        }
    }

    public void SetAttackColliderOn()
    {
        attackCollider.SetActive(true);
    }
    public void SetAttackColliderOff()
    {
        attackCollider.SetActive(false);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerAttack")
        {
            AttackedEvent(collision);
        }
    }
    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        {
            if (collision.tag == "PlayerAttack")
            {
                AttackedEvent(collision);
            }
        }
    }


    private void AttackedEvent(Collider2D collision)
    {
        if (canHurt == true)
        {
            audioSource.PlayOneShot(audioClips[0]);
            enemyLife--;
            if (enemyLife > 0)
            {
                animator.SetTrigger("Hurt");
                canHurt = false;
                StartCoroutine(BeCanHurt());
            }
            else
            {
                animator.SetTrigger("Die");
                isAlive = false;
                box.enabled = false;
                StartCoroutine(AfterDie());
            }
        }
    }

    IEnumerator BeCanHurt()
    {
        yield return new WaitForSeconds(0.4f);
        canHurt = true;
    }

    IEnumerator AfterDie()
    {
        yield return new WaitForSeconds(1.0f);
        spriteRenderer.material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        yield return new WaitForSeconds(1.0f);
        spriteRenderer.material.color = new Color(1.0f, 1.0f, 1.0f, 0.2f);
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);
    }
}
