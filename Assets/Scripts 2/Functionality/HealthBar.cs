using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    private Slider slider;
    public TMP_Text HPTextHolder;
    private void Start(){
        slider = this.GetComponent<Slider>();
    }
    
    public void SetMaxHealth(int health){
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health){
        slider.value = health;
        HPTextHolder.text = health.ToString();
    }
}
