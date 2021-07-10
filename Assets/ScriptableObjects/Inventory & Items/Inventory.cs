using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject
{
    public Item CurrentItem;
    public List<Item> items = new List<Item>();
    public void AddItem(Item ItemToAdd){
        items.Add(ItemToAdd);
    }

}
