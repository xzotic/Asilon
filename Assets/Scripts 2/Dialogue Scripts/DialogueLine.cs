using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueLine : DialogueBaseClass
    {
        [SerializeField] private Text TextHolder;
        [SerializeField] public Text NameHolder;

        [Header("Text Options")]
        [SerializeField] [TextArea(1, 4)] private string input;
        [SerializeField] private float delay;
        [SerializeField] private string NpcName;
        [SerializeField] private bool IsItemFind;
        [SerializeField] private Collider2D col;

        private IEnumerator LineAppear;

        private void OnEnable() 
        {
            ResetLine();
            LineAppear = WriteText(input,TextHolder,delay,NameHolder, NpcName, IsItemFind);
            StartCoroutine(LineAppear);
            if (IsItemFind) col.enabled=false;
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

                else finished = true;
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