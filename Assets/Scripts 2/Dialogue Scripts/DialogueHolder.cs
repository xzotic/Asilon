using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    public class DialogueHolder : MonoBehaviour
    {
        
        private IEnumerator DialogueSeq;

        private void OnEnable()
        {
            DialogueSeq=DialogueSequence();
            StartCoroutine(DialogueSeq);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Deactivate();
                FindObjectOfType<AudioManager>().Stop("NPC");
                gameObject.SetActive(false);
                GameObject.FindWithTag("NameBox").SetActive(false);
            }
        }

        private IEnumerator DialogueSequence()
        {
            for (int i = 0; i <transform.childCount;i++)
            {
                Deactivate();
                transform.GetChild(i).gameObject.SetActive(true);
                yield return new WaitUntil(()=> transform.GetChild(i).GetComponent<DialogueLine>().finished);
            }
            gameObject.SetActive(false);
            (GameObject.FindWithTag("NameBox")).SetActive(false);
        }

        private void Deactivate()
        {
            for (int i=0; i<transform.childCount;i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}