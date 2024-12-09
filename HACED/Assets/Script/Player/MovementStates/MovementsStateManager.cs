using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MovementsStateManager : MonoBehaviour
{
    [Header("Axis Deplacement")]
    public float currentMoveSpeed;
    public float walkSpeed = 4, walkBackSpeed = 3;
    public float runSpeed = 7, runBackSpeed = 5;
    public float crouchSpeed = 3, crouchBackSpeed = 2;
    public float airSpeed = 1.5f;

    [Header("Falling Timer")]
    public float fallingTimer = 2f;
    float currentFallingTimer;

    [HideInInspector] public Vector3 dir;
    public float hzInput, vInput;
    CharacterController controller;

    [Header("Check Ground")]
    [SerializeField] float groundYOffset;
    [SerializeField] LayerMask groundMask;
    Vector3 spherePos;

    [Header("Gravity on Earth")]
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float jumpForce = 5;
    [HideInInspector] public bool jumped;
    Vector3 velocity;

    [Header("State Movement")]
    public MovementBaseState previousState;
    public MovementBaseState currentState;
    public IdleState Idle = new IdleState();
    public WalkState Walk = new WalkState();
    public CrouchState Crouch = new CrouchState();
    public RunState Run = new RunState();
    public JumpState Jump = new JumpState(); 

    [Header("Animations")]
    [HideInInspector] public Animator anim;

    public PlayerHealth playerHealth;

    void Start()
    {
        currentFallingTimer = fallingTimer;
        playerHealth = GetComponent<PlayerHealth>();
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        SwitchState(Idle);
    }
    
    void Update()
    {
        GetDirectionAndMove();
        Gravity();
        Falling();

        anim.SetFloat("hzInput", hzInput);
        anim.SetFloat("vInput", vInput);
        
        currentState.UpdateState(this);
    }

    public void SwitchState(MovementBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    void GetDirectionAndMove()
    {
        hzInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");
        Vector3 airDir = Vector3.zero;
        if (!IsGrounded())
            airDir = transform.forward * vInput + transform.right * hzInput;
        else
            dir = transform.forward * vInput + transform.right * hzInput;
        controller.Move((dir.normalized * currentMoveSpeed + airDir.normalized * airSpeed) * Time.deltaTime);
    }

    public bool IsGrounded()
    {
        spherePos = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);
        if(Physics.CheckSphere(spherePos, controller.radius - 0.05f, groundMask))
        {
            if (currentFallingTimer <= 0)
            {
                playerHealth.TakeDamage(20);
            }
            currentFallingTimer = fallingTimer;

            return true;
        }
        return false;
    }

    void Gravity()
    {
        if (!IsGrounded())
        {
            velocity.y += gravity * Time.deltaTime;
            currentFallingTimer = currentFallingTimer - Time.deltaTime;
        }
        else if (velocity.y < 0) //fall
        {
            velocity.y = -2;
        }
           

        controller.Move(velocity * Time.deltaTime);
    }

    private void OnDrawGuizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spherePos, controller.radius - 0.05f);
    }

    void Falling() => anim.SetBool("Falling", !IsGrounded() && !(previousState == Idle));

    public void JumpForce() => velocity.y += jumpForce;

    public void Jumped() => jumped = true;
}
