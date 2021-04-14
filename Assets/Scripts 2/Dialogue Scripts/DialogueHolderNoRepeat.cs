using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DialogueSystem
{
    public class DialogueHolderNoRepeat : MonoBehaviour
    {
        private IEnumerator DialogueSeq;
        private bool DialogueFinished;

        [SerializeField] public int AfterLines;

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
                SoundManager.instance.StopSound();
                gameObject.SetActive(false);
                GameObject.FindWithTag("NameBox").SetActive(false);
            }
        }

        private IEnumerator DialogueSequence()
        {
            if (!DialogueFinished)
            {
                for (int i = 0; i <(transform.childCount-AfterLines);i++)
                {
                    Deactivate();
                    transform.GetChild(i).gameObject.SetActive(true);
                    yield return new WaitUntil(()=> transform.GetChild(i).GetComponent<DialogueLine>().finished);
                }
            } else {
                int index = transform.childCount-AfterLines;
                for (int i=index;i<(transform.childCount);i++) {
                    Deactivate();
                    transform.GetChild(i).gameObject.SetActive(true);
                    yield return new WaitUntil(()=> transform.GetChild(i).GetComponent<DialogueLine>().finished);
                }
            }
            DialogueFinished = true;
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