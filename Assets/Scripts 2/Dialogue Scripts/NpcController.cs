using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    [SerializeField] private GameObject dialogue;
    [SerializeField] private GameObject NpcName;

    public void ActivateDialogue(){
        dialogue.SetActive(true);
        NpcName.SetActive(true);
    }
    public bool DialogueActive(){
        return dialogue.activeInHierarchy;
    }
}