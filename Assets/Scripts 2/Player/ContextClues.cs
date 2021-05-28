using UnityEngine;
using System;
using System.Collections;

public class ContextClues : MonoBehaviour {
    public SpriteRenderer sr;
    public Sprite sprite;
    private void Start(){
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