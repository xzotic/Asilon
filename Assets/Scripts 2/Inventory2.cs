using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory2 
{
    //public event EventHandler OnItemListChanged;
    public List<Item2> itemList;
    public Inventory2(){
        itemList = new List<Item2>();
        //Debug.Log(itemList);
    }

    public void AddItem(Item2 item){
        itemList.Add(item);
        //OnItemListChanged?.Invoke(this,EventArgs.Empty);
    }

    public void RemoveItem(Item2 item){
        itemList.Remove(item);
    }

    public List<Item2> GetItemList(){
        return itemList;
    }

    
}
