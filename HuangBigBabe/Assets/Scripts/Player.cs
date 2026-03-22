using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{   //组件
    public Rigidbody2D rb;
    public Animator animator;

    private float movespeed = 5f;
    private StateMachine stateMachine;

    //for controling move.x
    private float acclration = 60f;  //最大加速度
    private float declration = 80f;  //最大减速度
    private float Maxspeed = 5f;  //最大移动速度
    public Vector2 movedir;    //移动方向
    public float FacingDirection { get; private set; } = 1f;

    public float DashcooldownTimer = 0f;

    public float AtkcooldownTimer;
    public bool CanDash() => DashcooldownTimer <= 0;
    public bool CanAtk() => AtkcooldownTimer <= 0;
    public bool IsGround { get; private set; }




    //for controling jump

    [SerializeField] private LayerMask GroundLayer;
    private RaycastHit GroundHit;
    public float JumpFprce = 3f;
    private void Awake()
    {

        stateMachine = new StateMachine();
        stateMachine.AddState(new IdleState(stateMachine, this));
        stateMachine.AddState(new MoveState(stateMachine, this));
        stateMachine.AddState(new JumpState(stateMachine, this));
        stateMachine.AddState(new DashState(stateMachine, this));
        stateMachine.AddState(new ATKState(stateMachine, this));
        


        animator = GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        movedir = GameInput.Instance.GetMovedir();
        Checkisground();
        stateMachine.Update();


        if (DashcooldownTimer > 0)
        {
            DashcooldownTimer -= Time.deltaTime;
        }
        if(AtkcooldownTimer>0)
        {
            AtkcooldownTimer -= Time.deltaTime;
        }

       

    }
    private void Start()
    {
        GameInput.Instance.OnDashWasClick += Instance_OnDashWasClick;
        GameInput.Instance.OnATKWasClick += Instance_OnATKWasClick;
        stateMachine.SwitchState<IdleState>();
    }

    private void Instance_OnATKWasClick(object sender, System.EventArgs e)
    {
        
        if (!(stateMachine.currentState is ATKState)&&CanAtk())
        {
            stateMachine.SwitchState<ATKState>();
        }
    }

    private void Instance_OnDashWasClick(object sender, System.EventArgs e)
    {
        if(!(stateMachine.currentState is DashState)&&CanDash())
        {
            stateMachine.SwitchState<DashState>();
        }
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();

    }
    //移动相关逻辑供movestate和jumpstate调用
    public void HandleMove()
    {
        float curentspeed;
        if (movedir.x != 0)
        {
            if (movedir.x >= 0)
            {
                transform.localScale = new Vector3(0.06f, 0.06f, 0.06f);
                FacingDirection = 1f;
            }
            else
            {
                transform.localScale = new Vector3(-0.06f, 0.06f, 0.06f);
                FacingDirection = -1f;
            }
        }
        curentspeed = rb.velocity.x;
        float accl = Mathf.Abs(curentspeed) > 0.1 ? acclration : declration;
        float targetspeed = movedir.x * Maxspeed;
        float a = Mathf.MoveTowards(curentspeed, targetspeed, accl * Time.fixedDeltaTime);
        rb.velocity = new Vector2(a, rb.velocity.y);

    }
    public void Checkisground()
    {

        float raycastHistDistance = 0.05f;
       IsGround= Physics2D.Raycast(transform.position, Vector2.down, raycastHistDistance, GroundLayer);
        Debug.DrawRay(transform.position, Vector2.down * raycastHistDistance, IsGround ? Color.green : Color.red);


    }

}
