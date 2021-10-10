using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    [SerializeField] private float AnimLength;
    public override void CheckDistance()
    {
        if (Vector2.Distance(transform.position,target.position)<=chaseRadius&& touched == false &&
            Vector2.Distance(transform.position,target.position)>attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk) {  
                anim.SetBool("WakeUp", true);
                Vector3 temp = Vector2.MoveTowards(transform.position,target.position,enemyMoveSpeed*Time.deltaTime);
                ChangeAnim(temp-transform.position);
                rigid.MovePosition(temp);
                ChangeState(EnemyState.walk);
            }
       
        } else if (Vector2.Distance(transform.position,target.position)>chaseRadius) {
            anim.SetBool("WakeUp",false);
            ChangeState(EnemyState.idle);
        } else if (Vector2.Distance(transform.position,target.position)<=chaseRadius&& touched == false && Vector2.Distance(transform.position,target.position)<=attackRadius){
            if ((currentState == EnemyState.idle || currentState == EnemyState.walk) && GameObject.FindGameObjectWithTag("Player").GetComponent<Stun>().IFrame == false) StartCoroutine(AttackCo());
        }
    }

    private IEnumerator AttackCo(){
        currentState = EnemyState.attack;
        anim.SetBool("IsAttack", true);
        yield return new WaitForSeconds(AnimLength);
        currentState = EnemyState.walk;
        anim.SetBool("IsAttack",false); 
    }
}