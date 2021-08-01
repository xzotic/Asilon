using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float MoveSpeed;
    public Vector2 direction;
    public float LifeTime;
    private float LifeTimeSeconds;
    public Rigidbody2D rigid;
    // Start is called before the first frame update
    void Start()
    {
        LifeTimeSeconds = LifeTime;
        rigid = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        LifeTimeSeconds -= Time.deltaTime;
        if (LifeTimeSeconds<=0) Destroy(this.gameObject);

    }
    public void Launch(Vector2 InitialVelo){
        rigid.velocity= InitialVelo * MoveSpeed;
    }
    private IEnumerator OnTriggerEnter2D(Collider2D other) {
        /*PlayerMovement pmrb = other.GetComponent<PlayerMovement>();
        SpriteRenderer pmsr = other.GetComponent<SpriteRenderer>();
        pmsr.color = new Color(1,1,1,1);
        pmrb.CurrentState = PlayerMovement.PlayerState.Walking;  
        Debug.Log("hello");*/
        this.gameObject.GetComponent<SpriteRenderer>().enabled=false;
        this.transform.GetChild(0).GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
