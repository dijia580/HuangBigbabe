using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Enemy : Entity
{
    [Header("组件信息")]
    public Rigidbody2D checkPlayerCollider;

    [Header("移动信息")]
    public Vector2 movedir; //面向Player       
    public  Vector2 IniPosition;//初始地址

    [Header("攻击信息")]
    public float atkDistance=1f;

    [Header("攻击冷却信息")]
    public  float AtkCoolDowmTimer;
    public  float AtkCoolDownTimerMax = 3f;

    public  bool PlayerInRange;

    [SerializeField] private  ColiderControl coliderControl;

    private void Start()
    {
       
        IniPosition = transform.position;
    }

   
    override protected  void Awake()
    {
        base.Awake();
        PlayerInRange = false;
        AddState();
        atkDistance = 1f;
        AtkCoolDowmTimer = 0;
        AtkCoolDownTimerMax = 3f;

        stateMachine.SwitchState<enemy_Patrol>();
    }

    private void Update()
    {
        stateMachine.Update();
        AtkTimerHandle();
        movedir= Player.Instance.transform.position - transform.position;

    }
    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

   private void AtkTimerHandle()
    {
       AtkCoolDowmTimer -= Time.deltaTime;
    }
   

    private void  AddState()
    {
        stateMachine.AddState(new enemy_Patrol(stateMachine, this));
        stateMachine.AddState(new ChaseState(stateMachine, this));
        stateMachine.AddState(new ReturnState(stateMachine, this));
        stateMachine.AddState(new E_AtkState(stateMachine, this));
    }
 
    private void OnDestroy()
    {
       
    }
}
