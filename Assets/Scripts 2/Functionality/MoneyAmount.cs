using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class MoneyAmount : MonoBehaviour
{
    public TMP_Text MTextHolder;
    public GlobalManager moneyValue;
    public int moneyK;
    private void Update() {
        if (moneyValue.money>=1000 && moneyValue.money <1000000) MTextHolder.text = (moneyValue.money/1000).ToString()+"K";
        else if (moneyValue.money>=1000000) MTextHolder.text = (moneyValue.money/1000000).ToString()+"M";
        else MTextHolder.text = moneyValue.money.ToString();
    }
}
