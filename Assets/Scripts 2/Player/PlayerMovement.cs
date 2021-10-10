using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
    using UnityEditor;
#endif

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    [Header("General")]
    public PlayerState CurrentState;
    public Rigidbody2D rb;
    public Animator animator;
    private NpcController npc;
    public Inventory2 inventory;
    public HealthBar healthBar;
    public GlobalManager global;
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] public InvUi UIInventory;

    [Header("Variables")]
    public bool InRange; 
    public int currentHealth;
    public int maxHealth;

    public bool IsPause;
    public bool IsInventory;

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

        inventory = new Inventory2();
        UIInventory.gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        
        if (CurrentState!=PlayerState.Interacting)
        {
            animator.enabled = true;
            movement = Vector2.zero;
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            if (CurrentState==PlayerState.Walking && CurrentState != PlayerState.Stagger) 
                UpdateAnimationAndMove();
        } else {
            animator.enabled = false;
        }
    }

    void Update()
    {
        global.VectorValue = rb.position;
        StartDialogue();
        
        if (CurrentState!=PlayerState.Interacting)
        {
            if (Input.GetButtonDown("Attack")&&CurrentState==PlayerState.Walking) {
                StartCoroutine(AttackCo());
            }
        }

        PauseAndInvUI();
    }

    private void PauseAndInvUI() {
        if (Input.GetKeyDown(KeyCode.Escape)&&CurrentState!=PlayerState.Interacting && IsInventory==false){
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

        /**if (Input.GetKeyDown(KeyCode.BackQuote)&&CurrentState!=PlayerState.Interacting && IsPause == false){
            if (IsInventory ==true){
                IsInventory = false;
                Time.timeScale = 1;
                CurrentState = PlayerState.Walking;
                foreach (Transform child in UIInventory.ItemSlotContainer) {
                    Destroy(child.gameObject);
                }

                UIInventory.gameObject.SetActive(false);
                UIInventory.ItemSlotTemplate.gameObject.SetActive(true);
            }
            else {
                IsInventory = true;
                Time.timeScale = 0;
                CurrentState = PlayerState.Pause;
                UIInventory.gameObject.SetActive(true);
                UIInventory.SetInventory(inventory);
                UIInventory.ItemSlotTemplate.gameObject.SetActive(false);
            }
        }*/
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
        if (InRange == true && Input.GetKeyDown(KeyCode.E)) {
            npc.ActivateDialogue();
            Debug.Log("doglog");
        }
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