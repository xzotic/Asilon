using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : ContextClues
{
    public Item contents;
    public bool IsOpen;
    public GameObject dialogWindow;
    public TextMesh DialogText;
    void Start()
    {
        GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !IsOpen && GetComponent<PlayerMovement>().InRange) {

        }
    }

    public void OpenChest() {
        
    }
}
