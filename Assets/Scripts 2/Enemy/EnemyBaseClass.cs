using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState{
    idle,
    walk,
    attack,
    stagger
}
public class EnemyBaseClass : MonoBehaviour
{
    public EnemyState currentState;
    public int maxHealth;
    public int health;
    public string enemyName;
    //public int baseAttack;

    private void Awake() {
        health = maxHealth;
    }

    public void TakeDamage(int damage){
        health -= damage;
        if (health<=0){
            this.gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
        }
    }
}
