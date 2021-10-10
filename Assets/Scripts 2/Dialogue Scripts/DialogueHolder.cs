using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueHolder : MonoBehaviour
    {
        
        private IEnumerator DialogueSeq;
        public string reply;
        public GameObject OptionHolder;
        public bool ButtonPressed;
        //public GlobalManager gm;
        //public bool IsReply;

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
                (GameObject.FindWithTag("SpriteBox")).SetActive(false);
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().CurrentState = PlayerMovement.PlayerState.Walking;
            }
        }

        private IEnumerator DialogueSequence()
        {
            
            for (int i = 0; i <transform.childCount;i++)
            {
                Deactivate();
                GameObject thingy = transform.GetChild(i).gameObject; 
                thingy.SetActive(true);
                yield return new WaitUntil(()=> transform.GetChild(i).GetComponent<DialogueLine>().finished);

                if (IsOptionYes(thingy)){ //

                    for (int a=0;a<thingy.transform.childCount;a++){//
                        thingy.GetComponent<Text>().text="";//
                        OptionHolder.transform.GetChild(a).gameObject.SetActive(true);//
                        OptionHolder.transform.GetChild(a).GetComponentInChildren<Text>().text=thingy.transform.GetChild(a).GetComponent<DialogueHolder>().reply; //
                    }//

                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().CurrentState = PlayerMovement.PlayerState.Interacting;//

                    yield return new WaitUntil(()=>CheckIfButtonPress()==true);//

                    thingy.GetComponent<Text>().text = "";
                    transform.gameObject.GetComponent<Image>().enabled=false;
                    GameObject.FindGameObjectWithTag("UIButtonCheck").GetComponent<ButtonCheck>().ispressed = false;//
                    ButtonPressed = false;
                    for (int a=0;a<thingy.transform.childCount;a++){//
                        OptionHolder.transform.GetChild(a).gameObject.SetActive(false);//
                    }

                    thingy.transform.GetChild(GameObject.FindGameObjectWithTag("UIButtonCheck").GetComponent<ButtonCheck>().AnswerNo).gameObject.SetActive(true);//
                    yield return new WaitUntil(()=> thingy.transform.GetChild(GameObject.FindGameObjectWithTag("UIButtonCheck").GetComponent<ButtonCheck>().AnswerNo).gameObject.activeInHierarchy == false);
                    transform.gameObject.GetComponent<Image>().enabled=true;
                } //
            }


            gameObject.SetActive(false);
            (GameObject.FindWithTag("NameBox")).SetActive(false);
            (GameObject.FindWithTag("SpriteBox")).SetActive(false);
            
        }

        private void Deactivate()
        {
            for (int i=0; i<transform.childCount;i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        private bool IsOptionYes(GameObject go){//
            if (go.transform.childCount>0){//
                return true;//
            }else return false;//
        }

        private bool CheckIfButtonPress(){//
            return GameObject.FindGameObjectWithTag("UIButtonCheck").GetComponent<ButtonCheck>().ispressed;//
        }//
        private void ButtonPress(){//
            ButtonPressed = true;//
        }//
    }
}