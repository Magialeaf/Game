using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomLevel : MonoBehaviour
{

    private Camera CameraField;
    private int nowGear = 0;
    public int[] zoomLevel;
    private void Start()
    {
        this.CameraField = GetComponent<Camera>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1) == true)
        {
            this.nowGear = (this.nowGear + 1) % this.zoomLevel.Length;
        }
        this.CameraField.fieldOfView = Mathf.Lerp(this.CameraField.fieldOfView, this.zoomLevel[this.nowGear], 0.1f);
        if (Mathf.Abs(this.CameraField.fieldOfView - this.zoomLevel[this.nowGear]) < 0.1f)
            this.CameraField.fieldOfView = this.zoomLevel[this.nowGear];
    }
}
