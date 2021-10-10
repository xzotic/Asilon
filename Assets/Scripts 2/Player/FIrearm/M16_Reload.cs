using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M16_Reload : MonoBehaviour
{
    private bool IsSlidedDown;
    private bool MagazinePresent;
    public Animator animator;
    private void Start() {
        animator = this.GetComponent<Animator>();
        MagazinePresent = true;
    }
    private IEnumerator OnMouseDrag() {
        if (Input.GetAxis("Mouse Y")<0 && IsSlidedDown ==false && MagazinePresent == true) {
            Debug.Log("Slide down");
            animator.SetBool("IsReload",true);
            yield return new WaitForSeconds(0.35f);
            IsSlidedDown = true;
            MagazinePresent = false;
        }
        else if (Input.GetAxis("Mouse Y")>0 && IsSlidedDown == true && MagazinePresent == false){
            Debug.Log("Slide up");
            animator.SetBool("IsReload",false);
            yield return new WaitForSeconds(0.35f);
            IsSlidedDown = false;
            MagazinePresent = true;
        }
    }
}
