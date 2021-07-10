using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : Enemy
{
    public Transform[] path;
    public int CurrentPoint;
    public Transform CurrentGoal;
    private float RoundingDistance = 0.5f;
    public override void CheckDistance()
    {
        if (Vector2.Distance(transform.position,target.position)<=chaseRadius&& touched == false &&
            Vector2.Distance(transform.position,target.position)>attackRadius)
        {
            // @ts-ignore
            if (currentState == EnemyState.idle || currentState == EnemyState.walk) {  
                Vector3 temp = Vector2.MoveTowards(transform.position,target.position,enemyMoveSpeed*Time.deltaTime);
                anim.SetBool("WakeUp", true);
                ChangeAnim(temp-transform.position);
                rigid.MovePosition(temp);
            }
        } else if (Vector2.Distance(transform.position,target.position)>chaseRadius) {
                if (Vector2.Distance(transform.position,path[CurrentPoint].position)>RoundingDistance){
                    Vector3 temp = Vector2.MoveTowards(transform.position,path[CurrentPoint].position,enemyMoveSpeed*Time.deltaTime);
                    anim.SetBool("WakeUp",true);
                    ChangeAnim(temp-transform.position);
                    rigid.MovePosition(temp);
                } else ChangeGoal();
        }
    }
    private void ChangeGoal(){
        if (CurrentPoint==path.Length-1){
            CurrentPoint = 0;
            CurrentGoal = path[0];
        }
        else{
            CurrentPoint++;
            CurrentGoal=path[CurrentPoint];
        }
    }
}
