using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteFollow : MonoBehaviour
{
    [SerializeField] private Transform[] Routes;
    private int RouteToGo;
    private float tParam;
    private Vector2 ImagePosition;
    private float speedModifier;
    private bool CoroutineAllowed;

    void Start()
    {
        RouteToGo=0;
        tParam=0f;
        speedModifier=0.5f;
        CoroutineAllowed=true;
    }
    void Update()
    {
        if (CoroutineAllowed) StartCoroutine(GoByTheRoute(RouteToGo));
    }
    private IEnumerator GoByTheRoute(int routeNumber)
    {
        CoroutineAllowed = false;
        Vector2 p0 = Routes[routeNumber].GetChild(0).position;
        Vector2 p1 = Routes[routeNumber].GetChild(1).position;
        Vector2 p2 = Routes[routeNumber].GetChild(2).position;
        Vector2 p3 = Routes[routeNumber].GetChild(3).position;
    

        while (tParam<1)
        {
            tParam += Time.deltaTime*speedModifier;
            ImagePosition = Mathf.Pow(1-tParam, 3)*p0+
                3*Mathf.Pow(1-tParam,2) * tParam * p1 +
                3*(1-tParam)*Mathf.Pow(tParam,2) * p2 +
                Mathf.Pow(tParam,3) *p3;

            transform.position = ImagePosition;
            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;
        RouteToGo += 1;
        if (RouteToGo>=Routes.Length -1) RouteToGo=0;
        CoroutineAllowed = true;
    }
}
    
