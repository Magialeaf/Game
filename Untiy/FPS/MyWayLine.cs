using UnityEngine;

/// <summary>
/// 路线类
/// </summary>

internal class MyWayLine : MonoBehaviour
{
    /// <summary>
    /// 是否可用
    /// </summary>
    public bool IsUsable { get; set; }

    public MyWayLine(int wayPointCount)
    {
        this.WayPoints = new Vector3[wayPointCount];
        this.IsUsable = true;
    }
    /// <summary>
    /// 当前路线左右路点坐标
    /// </summary>
    public Vector3[] WayPoints { get; set; }


}
