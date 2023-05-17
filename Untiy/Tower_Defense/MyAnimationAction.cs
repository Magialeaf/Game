using UnityEngine;

/// <summary>
/// 动画行为类，提供动画有关行为
/// </summary>
public class MyAnimationAction : MonoBehaviour
{
    // 附加在敌人模型上的动画组件引用
    private Animation anim;

    /// <summary>
    /// 创建动画行为类
    /// </summary>
    /// <param name="anim"></param>
    public MyAnimationAction(Animation anim)
    {
        this.anim = anim;
    }

    /// <summary>
    /// 播放动画
    /// </summary>
    /// <param name="animName">动画片段名称</param>

    public void Play(string animName)
    {
        anim.CrossFade(animName);
    }
    /// <summary>
    /// 判断指定动画是否正在播放
    /// </summary>
    /// <param name="animName">动画片段名称</param>
    /// <returns></returns>
    public bool IsPlaying(string animName)
    {
        return anim.IsPlaying(animName);
    }
}
