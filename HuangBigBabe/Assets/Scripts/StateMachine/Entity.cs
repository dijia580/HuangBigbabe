using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Entity : MonoBehaviour
{

    [Header("组件信息")]
    public Rigidbody2D rb;
    public Animator animator;

    [Header("状态机信息")]
    protected StateMachine stateMachine;

    [Header("碰撞器信息")]
    [SerializeField] protected  LayerMask GroundLayer;
    [SerializeField] protected Transform GroundCheckTransform;
    protected float GroundCheckDis = .2f;

    [Header("翻转信息")]
    public  int facingdir = 1; //1为向左，-1为向右
    protected bool FacingRight = true;

    public System.Action onFlipped;

    protected virtual  void Awake()
    {
        stateMachine = new StateMachine();
        rb =GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    public  bool IsGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(GroundCheckTransform.position, Vector2.down, GroundCheckDis, GroundLayer);
      
        Debug.DrawRay(GroundCheckTransform.position , Vector2.down * GroundCheckDis, hit.collider != null ? Color.green : Color.red);
        return hit.collider != null;
    }

    protected void Flip()
    {
        facingdir *= -1;
        FacingRight = !FacingRight;
        transform.Rotate(0, 180, 0);
        onFlipped?.Invoke();
    }
    protected virtual void FlipController(float x)
    {
        if (x > 0 && !FacingRight) Flip();
        if (x < 0 && FacingRight) Flip();

    }
}
