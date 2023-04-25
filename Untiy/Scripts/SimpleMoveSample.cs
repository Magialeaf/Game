using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SimpleMoveSample : MonoBehaviour
{
    private int go = 0;
    private void OnGUI()
    {
        if (GUILayout.Button("发射子弹"))
        {
            this.GetComponentInChildren<MeshRenderer>().material.color = Color.black;
            var comp = this.GetComponentsInChildren<MeshRenderer>();
            foreach (var i in comp)
            {
                i.material.color = Color.red;
            }
            this.go = 1;
        }
    }

    private void Update()
    {
        if(this.go == 1 && this.transform.localPosition.y < 5f)
        {
            transform.Translate(0, 0.25f, 0);
        }
    }   
}
