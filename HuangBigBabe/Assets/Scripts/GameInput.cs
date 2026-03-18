using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }
    private PlayerInputAction inputActions;
    private Vector2 Movedir;
    public event EventHandler OnJumpWasClick;
    private void Start()
    {
        inputActions.PlayerMap.Jump.performed += Jump_performed;
    }

    private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnJumpWasClick(this, EventArgs.Empty);
    }

    private void Awake()
    {
        if(Instance!=null)
        {
            Destroy(gameObject);
        }
        Instance = this;
        inputActions = new PlayerInputAction();
        inputActions.Enable();
    }
    private void Update()
    {
        Movedir = inputActions.PlayerMap.Move.ReadValue<Vector2>();
        
    }
    public Vector2 GetMovedir()
    {

        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

    }
    
}
