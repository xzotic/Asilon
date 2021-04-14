using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyBaseClass
{
    private Rigidbody2D rigid;
    private Transform target;
    public float chaseRadius;
    public float attackRadius;
    //public Transform homePosition;
    public int enemyMoveSpeed;
    private bool touched;
    public Animator anim;
    
    void Start()
    {
        currentState = EnemyState.idle;
        rigid = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        CheckDistance();
        //Debug.Log(touched);
    }

    void CheckDistance()
    {
        if (Vector2.Distance(transform.position,target.position)<chaseRadius&& touched == false &&
            Vector2.Distance(transform.position,target.position)>attackRadius)
        {
            // @ts-ignore
            if (currentState == EnemyState.idle || currentState == EnemyState.walk) {  
                Vector2 temp = Vector2.MoveTowards(transform.position,target.position,enemyMoveSpeed*Time.deltaTime);
                rigid.MovePosition(temp);
                ChangeState(EnemyState.walk);
                anim.SetBool("WakeUp", true);
            }
        } else anim.SetBool("WakeUp",false);
    }


    private void ChangeState(EnemyState NewState) {
        if (currentState!=NewState) currentState = NewState;
    }

    /*void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag=="Player") touched = true;
        //Debug.Log(touched);
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag=="Player") touched = false;
        //Debug.Log(touched);
    }*/
}
