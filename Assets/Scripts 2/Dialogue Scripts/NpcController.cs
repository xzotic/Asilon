using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : ContextClues
{
    [SerializeField] private GameObject Dialogue;
    [SerializeField] private GameObject NpcName;
    [SerializeField] private GameObject Image;

    public void ActivateDialogue(){
        Dialogue.SetActive(true);
        NpcName.SetActive(true);
        Image.SetActive(true);
    }
    public bool DialogueActive(){
        return Dialogue.activeInHierarchy;
    }
}