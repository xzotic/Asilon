using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemObject{
    public string ItemName;
    [TextArea(3,10)]
    public string ItemDesc;
    public int ItemNum; 
}

