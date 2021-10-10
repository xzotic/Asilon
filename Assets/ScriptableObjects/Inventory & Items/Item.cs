using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class Item : ScriptableObject
{
    public Sprite ItemSprite;
    public string ItemDesc;
    public string ItemName;
}
