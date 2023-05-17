using UnityEngine;

/// <summary>
/// AI
/// </summary>
public class MyEnemyAI : MonoBehaviour
{
    /// <summary>
    /// 定义敌人状态的枚举类型
    /// </summary>
    public enum State
    {
        Attack,     //攻击
        PathFinding //寻路
    }
    // 默认状态寻路
    private State currentState = State.PathFinding;
    private MyEnemyAnimation anim;
    private MyEnemyMotor motor;
    // 攻击计时器
    private float atkTimer = 0;
    private float atkInterval = 3;
    private void Start()
    {
        anim = GetComponent<MyEnemyAnimation>();
        motor = GetComponent<MyEnemyMotor>();
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Attack:
                if (!anim.action.IsPlaying(anim.attackAnimName))
                {
                    anim.action.Play(anim.idleAnimName);
                }
                if (this.atkTimer < Time.time)
                {
                    anim.action.Play(anim.attackAnimName);
                    this.atkTimer += this.atkInterval;
                }
                // 闲置动画不能写在else中，update太快
                break;
            case State.PathFinding:
                anim.action.Play(anim.runAnimName);
                if (motor.Pathfinding() == false)
                    currentState = State.Attack;
                break;
        }
    }
}
