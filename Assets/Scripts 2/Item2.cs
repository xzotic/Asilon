using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item2 
{
    public enum ItemType{
        Melee, Firearm, HealthPotion, AmmoClip, Item
    }

    public ItemType itemType;
    public int ItemID;
    public string ItemName;
    [TextArea(3,10)]
    public string ItemDesc;
    public int amount;

    public Sprite GetSprite(){
        return Resources.Load<Sprite>("ItemSprite/"+ItemID);
    }
}
