using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RotateTowardsTarget : MonoBehaviour
{
    public Transform target;
    private void Rotate()
    {
        Vector2 TargetVector = new Vector2(target.position.x,target.position.y);
        Vector2 ThisVector = new Vector2(this.transform.position.x,this.transform.position.y);
        float angle = Mathf.Atan2(TargetVector.y-ThisVector.y,TargetVector.x-ThisVector.x)* 180 / Mathf.PI;
        Quaternion rotateangle = Quaternion.Euler(0,0,angle);
        this.transform.rotation = rotateangle;
    }

    // Update is called once per frame
    private void Awake() {
        target=GameObject.FindGameObjectWithTag("Player").transform;
        Rotate();
    }
}
