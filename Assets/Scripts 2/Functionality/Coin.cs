using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{  
    public int value;
    private IEnumerator OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")){
            yield return new WaitForSeconds(0.05f);
            //this.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }       
    }
}
