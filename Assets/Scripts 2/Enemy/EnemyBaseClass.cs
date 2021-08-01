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
    public LootTable thisloot;
    //public int baseAttack;

    private void Awake() {
        health = maxHealth;
    }

    IEnumerator FadeTo(float aValue, float aTime)
    {
        float alpha = transform.GetComponent<SpriteRenderer>().material.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
            transform.GetComponent<SpriteRenderer>().material.color = newColor;
            yield return null;
        }
    }
    private void MakeLoot(){
        if (thisloot != null){
            GameObject current = thisloot.LootLoot();
            if (current != null) {
                Instantiate(current.gameObject, transform.position, Quaternion.identity);
            }
        }
    }


    public IEnumerator TakeDamage(int damage){
        health -= damage;
        if (health<=0){
            SpriteRenderer a = this.GetComponent<SpriteRenderer>();
            this.transform.GetChild(0).GetComponent<Collider2D>().enabled=false;
            for (float i = 1f; i > 0.5f; i-=0.05f)
                {
                    a.color = new Color(i,i,i,1);
                    yield return new WaitForSeconds(0.04f);                                 //Apply Color change
                }
            MakeLoot();
            StartCoroutine(FadeTo(0.0f,0.6f));
            Destroy(this.gameObject);
            //this.gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
        }
    }
}
