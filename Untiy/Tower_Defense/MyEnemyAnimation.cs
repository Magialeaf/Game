using UnityEngine;


/// <summary>
/// 敌人动画类，定义需要播放的动画片段名称
/// </summary>
public class MyEnemyAnimation : MonoBehaviour
{
    // 跑步
    public string runAnimName = "run.anim";
    // 攻击动画名称
    public string attackAnimName = "attack.anim";
    // 闲置动画名称
    public string idleAnimName = "idle.anim";
    // 死亡动画名称
    public string deathAnimName = "death.anim";

    /// <summary>
    /// 行为类，先初始化一次
    /// </summary>
    public MyAnimationAction action;
    private void Awake()
    {
        action = new MyAnimationAction(this.GetComponent<Animation>());
    }
}

