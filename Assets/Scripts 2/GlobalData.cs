using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GlobalData : MonoBehaviour
{
    private string jsonGlobal;
    private string jsonItem;
    public GlobalVariables global = new GlobalVariables();
    public GlobalManager gm;
    public GameObject pause;
    public List<Item2> itemList;
    public WrappingClass variable;
    private void Update(){
        //Save();
    }

    private void Start() {
        Load();
    }

    private void OnApplicationQuit() {
        Save();
    }
    
    public void Save(){
        PlayerMovement temp =GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        global.HPValue = gm.InitialHP;
        global.money = gm.money;
        global.VectorValue = gm.VectorValue;

        itemList = temp.inventory.itemList;

        jsonGlobal = JsonUtility.ToJson(global);
        variable  = new WrappingClass() { Inventory = itemList};
        jsonItem = JsonUtility.ToJson(variable);

        Debug.Log(jsonItem);

        File.WriteAllText(Application.dataPath + "/Resources/globalsave.json",jsonGlobal);
        File.WriteAllText(Application.dataPath + "/Resources/inventorysave.json",jsonItem); 

        Time.timeScale = 1;
        pause.SetActive(false);
        temp.CurrentState = PlayerMovement.PlayerState.Walking;
    }

    public void Load(){
        PlayerMovement temp = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        jsonGlobal = File.ReadAllText(Application.dataPath + "/Resources/globalsave.json");
        GlobalVariables loaded = JsonUtility.FromJson<GlobalVariables>(jsonGlobal);

        jsonItem = File.ReadAllText(Application.dataPath + "/Resources/inventorysave.json");
        
        variable = JsonUtility.FromJson<WrappingClass>(jsonItem);
        temp.inventory.itemList = variable.Inventory;

        /*WrappingClass variable2  = new WrappingClass() { Inventory = variable.Inventory};
        jsonItem = JsonUtility.ToJson(variable2);
        Debug.Log(jsonItem);*/

        foreach (Transform child in temp.UIInventory.ItemSlotContainer) {
            Destroy(child.gameObject);
        }
        //temp.UIInventory.SetInventory(temp.inventory);
        //temp.inventory.AddItem(null);

        gm.InitialHP = loaded.HPValue;
        gm.VectorValue = loaded.VectorValue;
        gm.money = loaded.money;

        GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().position = gm.VectorValue;
        temp.CurrentState=PlayerMovement.PlayerState.Walking;

        pause.SetActive(false);
        Time.timeScale = 1;
        temp.CurrentState = PlayerMovement.PlayerState.Walking;
    }


    public class GlobalVariables {
        public Vector3 VectorValue;
        public int HPValue;
        public int money;
    }
}

[System.Serializable]
public class WrappingClass {
    public List<Item2> Inventory;
}
