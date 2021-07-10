using UnityEngine;
using System;
using System.Collections;

public class ContextClues : MonoBehaviour {
    private SpriteRenderer sr;
    public Sprite sprite;
    private void Start(){
        sr= GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).GetComponent<SpriteRenderer>();
        sr.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            sr.gameObject.SetActive(true);
            sr.sprite = sprite;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) sr.gameObject.SetActive(false);
    }
}