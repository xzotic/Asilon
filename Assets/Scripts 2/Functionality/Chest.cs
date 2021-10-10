using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Item contents;
    public int IsOpen;
    [SerializeField] private string ChestIndex;
    public Inventory PlayerInventory;
    [SerializeField] private Collider2D col;

    void Update()
    {
        if (PlayerPrefs.HasKey(ChestIndex)) IsOpen = PlayerPrefs.GetInt(ChestIndex);
        if (IsOpen==1) col.enabled=false;
        if (Input.GetKeyDown(KeyCode.Backspace)) {
            PlayerPrefs.SetInt(ChestIndex,0);
            Debug.Log("heya");
        }
    }
}
