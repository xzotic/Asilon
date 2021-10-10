using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueBaseClass : MonoBehaviour
    {
        public bool finished {get;protected set;}
        public bool ItemTaken;
        //public static bool ItemFound;
        protected IEnumerator WriteText(string input, Text TextHolder, float delay, Text NameHolder, string NpcName, bool IsItemFind, Image ImageHolder, Sprite CharSprite, Item2 item)//, Inventory2 inventory) 
        { 
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().CurrentState = PlayerMovement.PlayerState.Interacting;  
            if ((GameObject.FindWithTag("DialogueBox").activeInHierarchy)==true) {
                if (!IsItemFind) FindObjectOfType<AudioManager>().Play("NPC");
                else {
                    FindObjectOfType<AudioManager>().Play("ItemFind");
                }
            }

            NameHolder.text = NpcName;
            ImageHolder.sprite = CharSprite;
            ImageHolder.SetNativeSize();
            ImageHolder.preserveAspect = true;

            for (int i = 0; i <input.Length;i++)
            {
                TextHolder.text += input[i];
                if (i==(input.Length-1)) FindObjectOfType<AudioManager>().Stop("NPC");
                yield return new WaitForSeconds(delay);
            }
            yield return new WaitUntil(()=> Input.GetKeyDown(KeyCode.Space));
            finished = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().CurrentState = PlayerMovement.PlayerState.Walking;
        }
    }
}