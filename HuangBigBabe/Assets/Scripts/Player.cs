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
    private float acclration = 60f;
    private float declration = 80f;
    private float Maxspeed = 5f;
    public Vector2 movedir;
    private float curentspeed;





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
        stateMachine.SwitchState<IdleState>();


        animator = GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        movedir = GameInput.Instance.GetMovedir();

        stateMachine.Update();


    }
    private void Start()
    {
        GameInput.Instance.OnJumpWasClick += Instance_OnJumpWasClick;
    }

    private void Instance_OnJumpWasClick(object sender, System.EventArgs e)
    {
        if (GetIsGround())
        {
            stateMachine.SwitchState<JumpState>();
        }
    }


    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();

    }
    public void HandleMove()
    {
        if (movedir.x >= 0)
            transform.localScale = new Vector3(0.06f, 0.06f, 0.06f);   // 面朝右
        else
            transform.localScale = new Vector3(-0.06f, 0.06f, 0.06f);  // 面朝左
        curentspeed = rb.velocity.x;
        float accl = Mathf.Abs(curentspeed) > 0.1 ? acclration : declration;
        float targetspeed = movedir.x * Maxspeed;
        float a = Mathf.MoveTowards(curentspeed, targetspeed, accl * Time.fixedDeltaTime);
        rb.velocity = new Vector2(a, rb.velocity.y);

    }
    public bool GetIsGround()
    {

        float raycastHistDistance = 0.5f;
        if (Physics2D.Raycast(transform.position, Vector2.down, raycastHistDistance, GroundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
