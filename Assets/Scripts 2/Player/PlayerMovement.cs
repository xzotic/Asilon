using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public VectorValue StartingPosition;
    public HealthValue HPValue;

    Vector2 movement;

    //FPS
    private void Awake()
    {
       //Application.targetFrameRate = 75;
    }

    public enum PlayerState{
        Walking, Attacking, Interacting,stagger
    };

    void Start()
    {
        rb.position = StartingPosition.InitialValue;
        CurrentState = PlayerState.Walking;
        animator = GetComponent<Animator>();
        animator.SetFloat("Horizontal",0);
        animator.SetFloat("Vertical",-1);
    }

    void FixedUpdate()
    {
        StartDialogue();
        if (!InDialogue())
        {
            animator.enabled = true;
            movement = Vector2.zero;
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            if (CurrentState==PlayerState.Walking && CurrentState != PlayerState.stagger) 
                UpdateAnimationAndMove();
        } else animator.enabled = false;
    }

    void Update()
    {
        if (!InDialogue())
        {
            if (Input.GetButtonDown("Attack")&&CurrentState!=PlayerState.Attacking) {
                StartCoroutine(AttackCo());
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
            animator.SetFloat("Horizontal",movement.x);
            animator.SetFloat("Vertical",movement.y);
            animator.SetBool("Moving", true);
            movement.Normalize();
            rb.MovePosition(rb.position + movement*moveSpeed*Time.fixedDeltaTime);
        } else animator.SetBool("Moving", false);
    }

    //Check in range of dialogue
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.tag == "Interactable") InRange = true;
        npc = collision.gameObject.GetComponent<NpcController>();
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
        HPValue.InitialHP -= damage;
        UnityEditor.EditorUtility.SetDirty(HPValue);
        healthBar.SetHealth(HPValue.InitialHP);
        if (HPValue.InitialHP<=0) this.gameObject.SetActive(false);
    }
}