using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCheck : MonoBehaviour
{
    public bool ispressed;//
    public int AnswerNo;
    public void ButtonPress(){//
        ispressed = true;//
    }//
    public void isbutton1() {
        AnswerNo =0;
    }
    public void isbutton2() {
        AnswerNo =1;
    }
    public void isbutton3() {
        AnswerNo =2;
    }
    public void isbutton4() {
        AnswerNo =3;
    }
}
