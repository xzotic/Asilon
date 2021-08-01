using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LocationName : MonoBehaviour
{   
    public TMP_Text text;
    public string namae;
    public GameObject go;
    public static bool activated;
    
    private IEnumerator OnTriggerEnter2D(Collider2D other) {
        if (!go.activeInHierarchy && !activated && other.CompareTag("Player"))
        {
            activated = true;
            go.SetActive(true);
            text.text = namae;
            yield return new WaitForSecondsRealtime(2.5f);
            text.CrossFadeAlpha(0,2f,false);
            yield return new WaitForSeconds(2);
            go.SetActive(false);
        }
    }
}
