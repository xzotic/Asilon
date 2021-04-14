using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueBaseClass : MonoBehaviour
    {
        public bool finished {get;protected set;}
        protected IEnumerator WriteText(string input, Text TextHolder, float delay, AudioClip sound, Text NameHolder, string NpcName) 
        {   
            if ((GameObject.FindWithTag("DialogueBox").activeInHierarchy)==true) SoundManager.instance.PlaySound();

            NameHolder.text = NpcName;

            for (int i = 0; i <input.Length;i++)
            {
                TextHolder.text += input[i];
                //SoundManager.instance.PlaySound(sound);
                if (i==(input.Length-1)) SoundManager.instance.StopSound();
                yield return new WaitForSeconds(delay);
            }
            
            yield return new WaitUntil(()=> Input.GetKeyDown(KeyCode.E));
            finished = true;
        }
    }
}