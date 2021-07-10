using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform target;
    private Rigidbody2D rigid;
    private int enemyMoveSpeed = 2;
    public void Move(){
        Vector3 temp = Vector2.MoveTowards(transform.position,target.position,enemyMoveSpeed*Time.deltaTime);
        rigid.MovePosition(temp);
    }
    private void RotateTowardsTarget()
    {
        float rotationSpeed = 10f; 
        float offset = 90f;    
        Vector3 direction = target.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle + offset, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
    private void Start() {
        rigid = this.GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        RotateTowardsTarget();
    }
}
