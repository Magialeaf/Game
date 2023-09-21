using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFemaleZombieAI : EnemyMaleZombieAI
{
    public float runSpeed;
    private bool isBattleMode;

    protected override void Start()
    {
        base.Start();

        isBattleMode = true;
    }
    protected override void Attack()
    {
        if (isBattleMode)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < 4.0f)
            {
                if (player.transform.position.x <= transform.position.x)
                {
                    transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                }
                else
                {
                    transform.localScale = new Vector3(1f, 1.0f, 1.0f);
                }

                Vector3 newTarget = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
                {
                    transform.position = Vector3.MoveTowards(transform.position, newTarget, runSpeed * Time.deltaTime);
                }

                isAfterBattleCheck = true;
            }
            else
            {
                if (isAfterBattleCheck)
                {
                    if (transform.position.x != targetPosition.x)
                    {
                        if (transform.position.x > targetPosition.x)
                        {
                            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                        }
                        else
                        {
                            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                        }

                    }
                    else
                    {
                        if (targetPosition == originPosition)
                        {
                            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                        }
                        else
                        {
                            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                        }

                    }
                    isAfterBattleCheck = false;
                }
            }
        }
        else
        {
            if (transform.position.x > targetPosition.x)
            {
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            }
            else
            {
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }

            if (transform.position == targetPosition)
            {
                isBattleMode = true;
            }
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        IfStopPoint(collision);
    }

    private void IfStopPoint(Collider2D collision)
    {
        if (collision.tag == "StopPoint")
        {
            isBattleMode = false;
        }

        if (collision.tag == "PlayerAttack")
        {
            isBattleMode = true;
        }
    }
}
