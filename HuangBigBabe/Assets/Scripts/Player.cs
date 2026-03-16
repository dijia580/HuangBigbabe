using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;

    //for controling move.x
    private float acclration = 60f;
    private float declration = 80f;
    private float Maxspeed = 5f;
    private Vector2 movedir;
    private float curentspeed;

    //for controling jump
    private bool isGround;
    [SerializeField] private LayerMask GroundLayer;
    private RaycastHit GroundHit;
    private float JumpFprce = 3f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        movedir = GameInput.Instance.GetMovedir();
        
        
       
        
        
    }
    private void Start()
    {
        GameInput.Instance.OnJumpWasClick += Instance_OnJumpWasClick;
    }

    private void Instance_OnJumpWasClick(object sender, System.EventArgs e)
    {
        float raycastHistDistance = 0.5f;
        isGround = Physics2D.Raycast(transform.position, Vector2.down, raycastHistDistance, GroundLayer);

        if (isGround)
        {
           
            rb.AddForce(JumpFprce*Vector2.up, ForceMode2D.Impulse);
        }
    }
    

    private void FixedUpdate()
    {
        HandleMove();
       
    }
    private void HandleMove()
    {

        curentspeed = rb.velocity.x;
        float accl = Mathf.Abs(curentspeed) > 0.1 ? acclration : declration;
        float targetspeed = movedir.x * Maxspeed;
        float a = Mathf.MoveTowards(curentspeed, targetspeed, accl * Time.fixedDeltaTime);
        rb.velocity = new Vector2(a, rb.velocity.y);
    }
    
}
