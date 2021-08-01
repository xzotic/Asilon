using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    private Slider slider;
    public TMP_Text HPTextHolder;
    public HealthValue HPValue;
    private void Start(){
        slider = this.GetComponent<Slider>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        slider.maxValue = playerMovement.maxHealth;
        slider.value = HPValue.InitialHP;
        HPTextHolder.text = HPValue.InitialHP.ToString("00");
    }

    public void SetHealth(int health){
        slider.value = health;
        HPTextHolder.text = health.ToString();
    }
}
