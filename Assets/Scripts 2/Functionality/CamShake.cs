using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour
{
    public Animator animator;
    private void Start() {
        animator = GetComponent<Animator>();    
    }
    public void BeginKick(){
        animator.SetBool("KickActive",true);
        StartCoroutine(KickCo());
    }
    public IEnumerator KickCo(){
        yield return new WaitForSeconds(0.06f);
        animator.SetBool("KickActive",false);
    }
}
