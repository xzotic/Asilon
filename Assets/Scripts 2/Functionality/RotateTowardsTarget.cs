using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsTarget : MonoBehaviour
{
    public Transform target;
    private void Rotate()
    {
        float rotationSpeed = 10f; 
        float offset = 0f;    
        Vector3 direction = target.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle + offset, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    // Update is called once per frame
    private void Awake() {
        target=GameObject.FindGameObjectWithTag("Player").transform;
        Rotate();
    }
}
