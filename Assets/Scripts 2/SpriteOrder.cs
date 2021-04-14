using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteOrder : MonoBehaviour
{

    private Renderer myRenderer;

    private void Start()
    {
        myRenderer = GetComponent<Renderer>();
    }

    void LateUpdate()
    {
        myRenderer.sortingOrder = -(int)(GetComponent<Collider2D>().bounds.min.y *10);
    }
}
