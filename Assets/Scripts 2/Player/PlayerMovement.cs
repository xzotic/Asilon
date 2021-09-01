using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
    using UnityEditor;
#endif

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    public PlayerState CurrentState;
    public Rigidbody2D rb;
    public Animator animator;
    private NpcController npc;
    public bool InRange; 
    public int currentHealth;
    public int maxHealth;
    public HealthBar healthBar;
    public GlobalManager global;
    private bool IsPause;
    [SerializeField] private GameObject PauseMenu;

    Vector2 movement;

    //FPS
    private void Awake()
    {
       //Application.targetFrameRate = 75;
    }

    public enum PlayerState{
        Walking, Attacking, Interacting,Stagger,Pause
    };

    void Start()
    {
        Time.timeScale = 1;
        rb.position = global.InitialValue;
        CurrentState = PlayerState.Walking;
        animator = GetComponent<Animator>();
        animator.SetFloat("Horizontal",0);
        animator.SetFloat("Vertical",-1);
    }

    void FixedUpdate()
    {
        StartDialogue();
        if (CurrentState!=PlayerState.Interacting)
        {
            animator.enabled = true;
            movement = Vector2.zero;
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            if (CurrentState==PlayerState.Walking && CurrentState != PlayerState.Stagger) 
                UpdateAnimationAndMove();
        } else animator.enabled = false;
    }

    void Update()
    {
        if (CurrentState!=PlayerState.Interacting)
        {
            if (Input.GetButtonDown("Attack")&&CurrentState==PlayerState.Walking) {
                StartCoroutine(AttackCo());
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab)){
            if (IsPause ==true){
                IsPause = false;
                Time.timeScale = 1;
                CurrentState = PlayerState.Walking;
                PauseMenu.SetActive(false);

            }
            else {
                IsPause = true;
                Time.timeScale = 0;
                CurrentState = PlayerState.Pause;
                PauseMenu.SetActive(true);
            }
        }
    }

    private IEnumerator AttackCo()
    {
        animator.SetBool("Attacking",true);
        CurrentState = PlayerState.Attacking;
        yield return null;
        animator.SetBool("Attacking",false);
        yield return new WaitForSeconds(.3f);
        CurrentState = PlayerState.Walking;
    }

    void UpdateAnimationAndMove()
    {
        if (movement != Vector2.zero)
        {
            movement.x = Mathf.Round(movement.x);
            movement.y = Mathf.Round(movement.y);
            animator.SetFloat("Horizontal",movement.x);
            animator.SetFloat("Vertical",movement.y);
            animator.SetBool("Moving", true);
            movement.Normalize();
            rb.MovePosition(rb.position + movement*moveSpeed*Time.fixedDeltaTime);
        } else animator.SetBool("Moving", false);
    }

    //Check in range of dialogue
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.tag == "Interactable") {InRange = true;
            npc = collision.gameObject.GetComponent<NpcController>();}
        if (collision.CompareTag("Coin")){
                int amount = collision.GetComponent<Coin>().value;
                global.money+=amount;
                #if UNITY_EDITOR
                EditorUtility.SetDirty(global);
                #endif
            }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        npc = null;
        if (collision.gameObject.tag == "Interactable") InRange = false;
    }


    private bool InDialogue()
    {
        if (npc != null) return npc.DialogueActive(); 
        else return false;
    }
    private void StartDialogue()
    {
        if (InRange == true && Input.GetKeyDown(KeyCode.E)) npc.ActivateDialogue();
    }

    public void PlayerTakeDamage(int damage){
        global.InitialHP -= damage;
        #if UNITY_EDITOR
            EditorUtility.SetDirty(global);
        #endif
        healthBar.SetHealth(global.InitialHP);
        if (global.InitialHP<=0) { 
            healthBar.SetHealth(0);
            this.gameObject.SetActive(false);
        }
    }
}