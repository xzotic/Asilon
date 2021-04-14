using System.Threading;
using System.Collections;
using UnityEngine;

namespace RPGM.Gameplay
{
    /// <summary>
    /// Marks a sprite that should fade away when the player character enters it's trigger.
    /// </summary>
    /// <typeparam name="FadingSprite"></typeparam>
    [RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
    public class FadingSprite : MonoBehaviour
    {
        internal SpriteRenderer SpriteRenderer;
        private SpriteRenderer CSR;
        [SerializeField] public Collider2D col;

        float speed = 0.08f;
        float transparency = 1f; 

        void Awake()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
            Transform child = transform.GetChild(0);
            CSR = child.GetComponent<SpriteRenderer>();
        }

        IEnumerator OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                transparency = 1f;
                SpriteRenderer.color = new Color(1,1,1,transparency); 
                CSR.color = new Color(1,1,1,transparency);   
                while (transparency>0.2f)
                {
                    transparency -= speed;
                    SpriteRenderer.color = new Color(1,1,1,transparency);
                    CSR.color = new Color(1,1,1,transparency);
                    //Debug.Log(transparency);
                    yield return 0;
                }
                SpriteRenderer.color = new Color(1,1,1,0.2f); 
                CSR.color = new Color(1,1,1,0.2f);
            }  
        }

        IEnumerator OnTriggerExit2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                transparency = 0.2f;
                SpriteRenderer.color = new Color(1,1,1,transparency); 
                CSR.color = new Color(1,1,1,transparency);
                while (transparency<1f)
                {
                    transparency += speed;
                    SpriteRenderer.color = new Color(1f,1f,1f,transparency);
                    CSR.color = new Color(1,1,1,transparency);
                    //Debug.Log(transparency);
                    yield return 0;
                }
                SpriteRenderer.color = new Color(1,1,1,1); 
                CSR.color = new Color(1,1,1,1);
            }
        }
    }
}