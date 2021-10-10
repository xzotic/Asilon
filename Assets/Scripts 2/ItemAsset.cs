using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAsset : MonoBehaviour
{
    private Sprite ItemSprite;
    private int temp;

    public void ReturnItemIndex() {
        temp = this.transform.GetSiblingIndex();
        PlayerMovement temp2 = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        temp2.UIInventory.SelectedItemIndex = temp;
        temp2.UIInventory.SelectedItem = temp2.inventory.itemList[temp];
        if (temp2.UIInventory.SelectedItem.itemType == Item2.ItemType.HealthPotion || temp2.UIInventory.SelectedItem.itemType == Item2.ItemType.Item){
            temp2.UIInventory.transform.Find("UseButton").gameObject.SetActive(true);
        }
        temp2.UIInventory.transform.Find("DeleteButton").gameObject.SetActive(true);
        temp2.UIInventory.SetItemDesc();
    }
}
