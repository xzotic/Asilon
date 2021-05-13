using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
 
public class guiscript : MonoBehaviour
{
    public PixelPerfectCamera cam;
 
    void Start()
    {
        AdjustScalingFactor();
    }
 
    void LateUpdate()
    {
        AdjustScalingFactor();
    }
 
    void AdjustScalingFactor()
    {
        GetComponent<CanvasScaler>().scaleFactor = cam.pixelRatio;
    }
 
}