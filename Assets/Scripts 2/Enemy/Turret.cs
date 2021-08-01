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
        if (Vector2.Distance(transform.position,target.position)<=chaseRadius&& touched == false &&
            Vector2.Distance(transform.position,target.position)>attackRadius&&CanFire)
        {
            // @ts-ignore
            if (currentState == EnemyState.idle || currentState == EnemyState.walk) {  
                Vector3 tempVector = target.transform.position-transform.position;
                GameObject current = Instantiate(projectile,transform.position,Quaternion.identity);
                ChangeState(EnemyState.walk);
                current.GetComponent<Projectile>().Launch(tempVector);
                CanFire=false;
                anim.SetBool("WakeUp", true);
            }
        } else if (Vector2.Distance(transform.position,target.position)>chaseRadius) {
            anim.SetBool("WakeUp",false);
        }
    }
}
