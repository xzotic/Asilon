using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueLine : DialogueBaseClass
    {
        public static bool ItemFound;
        [SerializeField] private Text TextHolder;
        [SerializeField] public Text NameHolder;
        [SerializeField] private Image ImageHolder;
        [SerializeField] private GlobalManager gm;

        [Header("Text Options")]
        [SerializeField] [TextArea(1, 4)] private string input;
        [SerializeField] private float delay;
        [SerializeField] private string NpcName;
        [SerializeField] private Sprite CharSprite;

        [Header("Chest things")]
        [SerializeField] public Item2 item;
        [SerializeField] private Inventory inventory;
        [SerializeField] private Collider2D TriggerCol;
        [SerializeField] private bool IsItemFind;
        [SerializeField] private string ChestIndex;
        private string tempstring;


        private IEnumerator LineAppear;
        private void Awake() {
            tempstring = input;
        }
        private void OnEnable() 
        {
            ResetLine();
    
            if (IsItemFind) { 
                int listcount = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().inventory.itemList.Count;
                if (listcount < 56) { 
                        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().inventory.AddItem(item);
                        ItemTaken = true;
                    }
                else ItemTaken = false;

                if (ItemTaken) {
                    PlayerPrefs.SetInt(ChestIndex,1);
                    input = tempstring;
                }
                else {
                    tempstring = input;
                    input = "There is no space left in your inventory";
                }
            }

            LineAppear = WriteText(input,TextHolder,delay,NameHolder, NpcName, IsItemFind, ImageHolder, CharSprite, item);//, inventory);
            StartCoroutine(LineAppear);
            
        }
        //private void OnDisable(){
            //TextHolder.text="";
        //}

        private void Update()
        {
            if ((Input.GetKeyDown(KeyCode.Space))&&GameObject.FindGameObjectsWithTag("DialogueChoiceButton").Length==0) //&& gm.IsInteracting == true)
            {
                if (TextHolder.text != input) 
                {
                    StopCoroutine(LineAppear);
                    FindObjectOfType<AudioManager>().Stop("NPC");
                    TextHolder.text = input;
                }

                else {
                    finished = true; 
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().CurrentState = PlayerMovement.PlayerState.Walking;
                }
            }
        }

        private void ResetLine()
        {
            TextHolder.text = "";
            NameHolder.text = "";
            finished = false;
        } 
    }
}