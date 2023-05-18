using UnityEngine;

/// <summary>
/// 敌人马达，提供移动、旋转、寻路
/// </summary>
public class MyEnemyMotor : MonoBehaviour
{

    // 移动速度
    public int moveSpeed = 2;
    // 当前路点的索引
    private int currentPointIndex = 0;
    // 一条路线
    internal MyWayLine line;

    private Material material;
    private void Start()
    {
        MeshRenderer meshRenderer = this.GetComponentInChildren<MeshRenderer>();
        this.material = meshRenderer.materials[0];
    }


    private void Update()
    {
        Pathfinding();
    }

    // 向前移动
    public void MovemetnForward()
    {
        // 每帧更新
        this.transform.Translate(0, 0, moveSpeed * Time.deltaTime);
    }

    /// <summary>
    /// 注视旋转
    /// </summary>
    /// <param name="targetPoint">需要注视的目标点</param>
    public void LookRotation(Vector3 targetPoint)
    {
        this.transform.LookAt(targetPoint);
    }

    /// <summary>
    /// 寻路，沿路线(Vector3[])移动
    /// </summary>
    /// <returns></returns>
    public bool Pathfinding()
    {
        // 加上空防止异常
        if (this.line.WayPoints == null || this.currentPointIndex >= this.line.WayPoints.Length)
        {
            this.material.color = Color.red;
            return false;
        }
        else
        {
            this.LookRotation(this.line.WayPoints[this.currentPointIndex]);
            this.MovemetnForward();
            // 当前位置接近领界值（不能写0）
            if (Vector3.Distance(this.transform.position, this.line.WayPoints[this.currentPointIndex]) < 0.2f)
                this.currentPointIndex++;
            return true;
        }
    }
}
