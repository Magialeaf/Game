
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
/// <summary>
/// 敌人生成器
/// </summary>

internal class MyEnemySpawn : MonoBehaviour
{
    // 需要创建的敌人预制件数组
    public GameObject[] enemyType;

    // 关卡敌人数
    public int maxCount = 5;
    // 初始敌人数
    public int startCount = 2;
    // 已创建敌人数
    public int spawnedCount;
    // 最大延迟生成敌人
    public int maxDelay = 10;

    private void Start()
    {
        this.CalculateWayLines();
        this.GenerateEnemy();
        this.GenerateEnemy();
    }
    /// <summary>
    /// 创建敌人
    /// </summary>
    public void GenerateEnemy()
    {
        this.spawnedCount++;
        // 生成数量达到上限
        if (this.spawnedCount >= maxCount)
        {
            return;
        }
        else
        {
            // 延迟时间敌人
            float delay = Random.Range(0, maxDelay);
            Invoke("CreateEnemy", delay);
        }
    }

    private void CreateEnemy()
    {
        // 随机选一条路线
        MyWayLine[] usableWayLines = this.SelectUsableWayLine();
        MyWayLine line = usableWayLines[Random.Range(0, usableWayLines.Length)];

        // 随机创建敌人
        int randomIndex = Random.Range(0, enemyType.Length);
        GameObject go = Instantiate(enemyType[randomIndex], line.WayPoints[0], Quaternion.identity) as GameObject;

        // 配置信息
        MyEnemyMotor motor = go.GetComponent<MyEnemyMotor>();
        motor.line = line;
        line.IsUsable = false;

        // 传递生成器对象[后续可以用委托代替]
        go.GetComponent<MyEnemyStatusInfo>().spawn = this;
    }

    // 所有路线
    private MyWayLine[] lines;
    // 计算所有路线和点
    private void CalculateWayLines()
    {
        lines = new MyWayLine[this.transform.childCount];
        for (int i = 0; i < lines.Length; i++)
        {
            // 路线
            Transform wayLineTF = this.transform.GetChild(i);
            int count = wayLineTF.childCount;
            lines[i] = new MyWayLine(count);
            for (int pointIndex = 0; pointIndex < count; pointIndex++)
            {
                lines[i].WayPoints[pointIndex] = wayLineTF.GetChild(pointIndex).position;
            }
        }
    }
    // 选择所有可以用的路线
    private MyWayLine[] SelectUsableWayLine()
    {
        List<MyWayLine> result = new List<MyWayLine>(lines.Length);
        foreach (var item in lines)
        {
            if (item.IsUsable)
            {
                result.Add(item);
            }
        }
        return result.ToArray();
    }
}

