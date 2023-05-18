using UnityEngine;

/// <summary>
/// 敌人状态信息类，定义敌人信息，提供受伤死亡功能
/// </summary>
public class MyEnemyStatusInfo : MonoBehaviour
{
    // 血量最大值
    public float HP = 200;
    // 最大血量
    public float maxHP = 200;
    // 死亡延迟时间
    public float deathDelay = 3;
    // 敌人生成器引用，用于找到敌人生成器
    internal MyEnemySpawn spawn;

    /// <summary>
    /// 受伤扣血
    /// </summary>
    /// <param name="damage">扣血</param>
    public void Damage(float damage)
    {
        this.HP -= damage;
        if (this.HP <= 0)
        {
            Death();
        }

    }
    /// <summary>
    /// 死亡
    /// </summary>
    public void Death()
    {
        // 播放死亡动画
        var anim = GetComponent<MyEnemyAnimation>();
        anim.action.Play(anim.deathAnimName);
        // 摧毁当前物体
        Destroy(this.gameObject, this.deathDelay);

        // 还原路线
        GetComponent<MyEnemyMotor>().line.IsUsable = true;
        // 产生下一个敌人
        spawn.GenerateEnemy();
    }
}
