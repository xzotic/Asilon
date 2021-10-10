using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Stun : MonoBehaviour
{   
    public float knockback;
    public int damage;
    public bool IFrame;
    public Collider2D col;

    private void Update() {
        
    }
    // -----------------Apply enemy stun and player knockback-------------------------
    private IEnumerator OnTriggerEnter2D(Collider2D other) {
        Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
        
        if (other.gameObject.CompareTag("Player")) {
            PlayerMovement pmrb = rb.GetComponent<PlayerMovement>();
            SpriteRenderer sr = other.gameObject.GetComponent<SpriteRenderer>();
            if (rb!=null) {
                
                pmrb.CurrentState = PlayerMovement.PlayerState.Stagger;                     //Start stagger

                Vector2 difference = rb.transform.position - transform.position;
                rb.GetComponent<PlayerMovement>().animator.enabled=false;
                difference = difference.normalized*knockback;                               //Apply knockback
                rb.AddForce(difference,ForceMode2D.Impulse);
                GameObject.FindGameObjectWithTag("CMCam").GetComponent<CamShake>().BeginKick();

                rb.GetComponent<PlayerMovement>().PlayerTakeDamage(damage);

                
                for (float i = 0.4f; i < 0.8f; i+=0.2f)
                {
                    sr.color = new Color(1,i,i,1);
                    yield return new WaitForSeconds(0.08f);                                 //Apply color change  
                }

                

                rb.GetComponent<PlayerMovement>().animator.enabled=true;
                pmrb.CurrentState = PlayerMovement.PlayerState.Walking;                      //End Stagger

                GameObject.FindGameObjectWithTag("Player").GetComponent<Stun>().IFrame = true;
                sr.color = new Color(1,1,1,0.8f);
                col.enabled = false;
                yield return new WaitForSeconds(2f); 
                GameObject.FindGameObjectWithTag("Player").GetComponent<Stun>().IFrame = false;
                sr.color = new Color(1,1,1,1);
                col.enabled = true;
            }
        }

        if (other.gameObject.CompareTag("Enemy")) {

            SpriteRenderer esr = other.gameObject.GetComponent<SpriteRenderer>();
            if (rb!=null ) {
                
                rb.GetComponent<Enemy>().currentState = EnemyState.stagger;                 //Start stagger

                StartCoroutine(rb.GetComponent<Enemy>().TakeDamage(damage));                                //Apply damage

                for (float i = 0.4f; i < 0.8f; i+=0.2f)
                {
                    esr.color = new Color(1,i,i,1);
                    yield return new WaitForSeconds(0.08f);                                 //Apply Color change
                }
                esr.color = new Color(1,1,1,1);

                rb.GetComponent<Enemy>().currentState = EnemyState.idle;                    //End Stagger
            }
        }
    }
}