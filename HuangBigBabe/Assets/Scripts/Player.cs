using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UIElements;

public class Player :Entity
{  
 
    [Header("移动信息")]
    public  float acclration = 60f;  //最大加速度
    public  float declration = 80f;  //最大减速度
    public  float Maxspeed = 5f;  //最大移动速度
    public Vector2 movedir;    //移动方向
    
    [Header("Dash信息")]
    public float DashcooldownTimer = 0f;
    public bool CanDash() => DashcooldownTimer <= 0;
    public  bool IsJumping = false;

    [Header("攻击信息")]
    public float AtkcooldownTimer;
    [SerializeField] public Player_colliderControl player_ColliderControl;
    public float JumpFprce = 8f;
    public bool CanAtk() => AtkcooldownTimer <= 0;

     private bool IsGrounded;
    public static Player Instance;

    

   override protected void Awake()
    {
        base.Awake();
        AddState();
        if (Instance != null)
        {
            Debug.LogError("player_instance existed");
        }
        else
        {
            Instance = this;
        }
    }




    private void Update()
    {
        
        movedir = GameInput.Instance.GetMovedir();
        IsGrounded=IsGround();
        stateMachine.Update();
        DashCoolDown();
        AtkCoolDown();
    
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
       
        curentspeed = rb.velocity.x;
        float accl = Mathf.Abs(curentspeed) > 0.1 ? acclration : declration;
        float targetspeed = movedir.x * Maxspeed;
        float a = Mathf.MoveTowards(curentspeed, targetspeed, accl * Time.fixedDeltaTime);
        rb.velocity = new Vector2(a, rb.velocity.y);
        FlipController(movedir.x);
        
    }
    public bool GetIsGround()
    {
        
        return IsGrounded;
    }
    private void DashCoolDown()
    {
        if (DashcooldownTimer > 0)
        {
            DashcooldownTimer -= Time.deltaTime;
        }
    }
    private void AtkCoolDown()
    {

        if (AtkcooldownTimer > 0)
        {
            AtkcooldownTimer -= Time.deltaTime;
        }
    }
    private void AddState()
    {
        stateMachine.AddState(new IdleState(stateMachine, this));
        stateMachine.AddState(new MoveState(stateMachine, this));
        stateMachine.AddState(new JumpState(stateMachine, this));
        stateMachine.AddState(new DashState(stateMachine, this));
        stateMachine.AddState(new ATKState(stateMachine, this));
    }
    private void OnDestroy()
    {
        GameInput.Instance.OnDashWasClick -= Instance_OnDashWasClick;
        GameInput.Instance.OnATKWasClick -= Instance_OnATKWasClick;
    }
   
}
