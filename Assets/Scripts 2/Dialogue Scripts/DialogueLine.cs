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

        [Header("Text Options")]
        [SerializeField] [TextArea(1, 4)] private string input;
        [SerializeField] private float delay;
        [SerializeField] private string NpcName;
        [SerializeField] private Sprite CharSprite;

        [Header("Chest things")]
        [SerializeField] private Item item;
        [SerializeField] private Inventory inventory;
        [SerializeField] private Collider2D TriggerCol;
        [SerializeField] private bool IsItemFind;

        private IEnumerator LineAppear;
        private void OnEnable() 
        {
            ResetLine();
            LineAppear = WriteText(input,TextHolder,delay,NameHolder, NpcName, IsItemFind, ImageHolder, CharSprite, item, inventory);
            StartCoroutine(LineAppear);
            if (IsItemFind) TriggerCol.enabled=false;
        }

        private void Update()
        {
            if ((Input.GetKeyDown(KeyCode.Space)))
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