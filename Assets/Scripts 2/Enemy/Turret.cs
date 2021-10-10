using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Enemy
{
    public GameObject projectile;
    public float FireDelay;
    private float FireDelaySeconds=0;
    public bool CanFire =true;
    private void Update() {
        FireDelaySeconds-=Time.deltaTime;
        if(FireDelaySeconds<=0){
            CanFire = true;
            FireDelaySeconds = FireDelay;
        }
    }
    public override void CheckDistance()
    {
        if (Vector2.Distance(transform.position,target.position)<=chaseRadius&& touched == false && CanFire)
        {
            // @ts-ignore
            if ((currentState == EnemyState.idle || currentState == EnemyState.walk) && GameObject.FindGameObjectWithTag("Player").GetComponent<Stun>().IFrame == false) {  
                CanFire=false;
                Vector3 tempVector = target.transform.position-transform.position;
                GameObject current = Instantiate(projectile,transform.position,Quaternion.identity);
                ChangeState(EnemyState.walk);
                if ( GameObject.FindGameObjectWithTag("Player").GetComponent<Stun>().IFrame==false) current.GetComponent<Projectile>().Launch(tempVector);
                anim.SetBool("WakeUp", true);
            }
        } else if (Vector2.Distance(transform.position,target.position)>chaseRadius) {
            anim.SetBool("WakeUp",false);
        }
    }

    //script is real fucked up needs to be fixed
}
