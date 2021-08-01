using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyBaseClass
{
    public Rigidbody2D rigid;
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    //public Transform homePosition;
    public int enemyMoveSpeed;
    public bool touched;
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

    public virtual void CheckDistance()
    {
        if (Vector2.Distance(transform.position,target.position)<=chaseRadius&& touched == false &&
            Vector2.Distance(transform.position,target.position)>attackRadius)
        {
            // @ts-ignore
            if (currentState == EnemyState.idle || currentState == EnemyState.walk) {  
                Vector3 temp = Vector2.MoveTowards(transform.position,target.position,enemyMoveSpeed*Time.deltaTime);
                ChangeAnim(temp-transform.position);
                rigid.MovePosition(temp);
                ChangeState(EnemyState.walk);
                anim.SetBool("WakeUp", true);
            }
        } else if (Vector2.Distance(transform.position,target.position)>chaseRadius) {
            anim.SetBool("WakeUp",false);
        }
    }


    public void ChangeState(EnemyState NewState) {
        if (currentState!=NewState) currentState = NewState;
    }

    private void SetAnimFloat(Vector2 SetVector){
        anim.SetFloat("moveX",SetVector.x);
        anim.SetFloat("moveY", SetVector.y);
    }

    public void ChangeAnim(Vector2 direction){
        if(Mathf.Abs(direction.x)>(Mathf.Abs(direction.y))){
            if (direction.x>0){
                SetAnimFloat(Vector2.right);
            } else if (direction.x<0){
                SetAnimFloat(Vector2.left);
            }
        } else if (Mathf.Abs(direction.x)<(Mathf.Abs(direction.y))){
            if (direction.y>0){
                SetAnimFloat(Vector2.up);
            } else if (direction.y<0){
                SetAnimFloat(Vector2.down);
            }
        }
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
