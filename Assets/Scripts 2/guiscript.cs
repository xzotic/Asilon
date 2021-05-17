using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
 
public class guiscript : MonoBehaviour
{
    public PixelPerfectCamera cam;
    public CanvasScaler scaler;
 
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
        scaler.scaleFactor = cam.pixelRatio;
    }
 
}