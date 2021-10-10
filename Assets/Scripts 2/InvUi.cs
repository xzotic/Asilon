using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;

public class InvUi : MonoBehaviour
{
    private Inventory2 inventory;
    public Transform ItemSlotContainer;
    public Transform ItemSlotTemplate;
    public Item2 SelectedItem;

    [SerializeField] private Image ImageHolder;
    [SerializeField] private TextMeshProUGUI TextHolder;
    [SerializeField] private TextMeshProUGUI NameHolder;
    [SerializeField] private Transform DB;
    [SerializeField] private Transform UB;
    public int SelectedItemIndex;
    public GameObject GlobalDataManager;

    private void Awake() {
        ItemSlotContainer = this.transform.Find("ItemSlotContainer");
        ItemSlotTemplate = this.transform.Find("ItemSlotTemplate");
        DB = this.transform.Find("DeleteButton");
        UB= this.transform.Find("UseButton");
    }

    private void Update(){
        if (Input.GetKeyDown(KeyCode.Escape) ) {
            foreach (Transform child in ItemSlotContainer) {
                    Destroy(child.gameObject);
            }
            this.gameObject.SetActive(false);
            this.ItemSlotTemplate.gameObject.SetActive(true);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().IsInventory = false;
        }
    }

    public void SetInventory(Inventory2 inventory) {
        this.inventory = inventory;  
        RefreshInventoryItems();
    }


    public void RefreshInventoryItems(){   
        int x = 0;
        int y = 0;
        float cellsize = 60f;
        foreach (Item2 item in inventory.GetItemList()) {
            
            RectTransform ItemSlotRectTransform = Instantiate(ItemSlotTemplate,ItemSlotContainer).GetComponent<RectTransform>();
            ItemSlotRectTransform.gameObject.SetActive(true);

            ItemSlotRectTransform.anchoredPosition = new Vector2((-322 + x * cellsize), (180 - y * cellsize));

            Image image = ItemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();

            image.SetNativeSize();

            x++;
            if (x>7) {
                x=0;
                y++;
            }
        }
    }

    public void DiscardItem(){
        ImageHolder.color = new Color(1,1,1,0);
        TextHolder.text = "";
        NameHolder.text = "";
        DB.gameObject.SetActive(false);
        PlayerMovement temp2 = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        temp2.inventory.itemList.RemoveAt(SelectedItemIndex);
        foreach (Transform child in temp2.UIInventory.ItemSlotContainer) {
            Destroy(child.gameObject);
        }
        SetInventory(temp2.inventory);
    }

    public void UseItem(){
        string t = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().inventory.itemList[SelectedItemIndex].ItemName;
        t = t.Replace(" ", "");
        var tempscript = Resources.Load<MonoScript>("ItemScript/" + t);
        gameObject.AddComponent(tempscript.GetClass());
        UB.gameObject.SetActive(false);
        DiscardItem();
    }

    public void SetItemDesc(){
        ImageHolder.sprite = SelectedItem.GetSprite();
        ImageHolder.SetNativeSize();
        ImageHolder.color = new Color (1,1,1,1);
        TextHolder.text = SelectedItem.ItemDesc;
        NameHolder.text = SelectedItem.ItemName;
    }
}
