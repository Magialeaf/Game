using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformHelper : MonoBehaviour
{
    /// <summary>
    /// 在未知层级的情况下查找子物体
    /// </summary>
    /// <param name="ParentTF">父物体变换组件</param>
    /// <param name="childName">子物体名称</param>
    /// <returns></returns>
    public static Transform GetChild(Transform parentTF,string childName)
    {
        //在子物体中查找
        Transform childTK = parentTF.Find(childName);
        if (childTK != null)
            return childTK;

        //将问题交由子物体
        int count = parentTF.childCount;
        for (int i = 0; i < count; i++)
        {
            childTK = GetChild(parentTF.GetChild(0), childName);
            if (childTK != null)
                return childTK;
        }
        return null;
    }
}
