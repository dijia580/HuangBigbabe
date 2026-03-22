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
    public event EventHandler OnDashWasClick;
    public event EventHandler OnATKWasClick;
    private void Start()
    {
        inputActions.PlayerMap.Jump.performed += Jump_performed;
        inputActions.PlayerMap.Dash.performed += Dash_performed;
        inputActions.PlayerMap.ATK.performed += ATK_performed;
    }

    private void ATK_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnATKWasClick?.Invoke(this, EventArgs.Empty);
    }

    private void Dash_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnDashWasClick?.Invoke(this, EventArgs.Empty);
    }

    private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnJumpWasClick?.Invoke(this, EventArgs.Empty);
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

        return Movedir;

    }
    
}
