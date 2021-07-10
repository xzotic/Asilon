using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Item contents;
    public static bool IsOpen;
    public Inventory PlayerInventory;
    [SerializeField] private Collider2D col;
    /*private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")&&!IsOpen) {
            PlayerInventory.AddItem(contents);
            PlayerInventory.CurrentItem=contents;
        }
    }*/

    void Update()
    {
        if (col.enabled == false) IsOpen = true;
        if (IsOpen) col.enabled=false;
    }
}
