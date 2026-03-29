using UnityEngine;

public class E_AtkState : State
{
    private Enemy enemy;

    

    public E_AtkState(StateMachine stateMachine,Enemy enemy) : base(stateMachine)
    {
        this.enemy = enemy;
    }

    public override void On_Enter()
    {
        enemy.rb.velocity = Vector2.zero;
        
       
    }

    public override void Update()
    {
        
        if(enemy.AtkCoolDowmTimer<=0)
        {
            ATK();
        }

        CheckState();
            
          
       
    }
    public override void FixedUpdate()
    {
        enemy.rb.velocity = Vector2.zero;
    }
   

    private void ATK()
    {
        Debug.Log("¹¥»÷¹¥»÷");
        enemy. AtkCoolDowmTimer = enemy. AtkCoolDownTimerMax;
        
    }
    private void CheckState()
    {
        if (enemy.PlayerInRange == false)
        {
            stateMachine.SwitchState<ReturnState>();
        }
        else if (enemy.movedir.magnitude > enemy.atkDistance)
        {
            stateMachine.SwitchState<ChaseState>();
        }

    }
    public override void Exit()
    {
        base.Exit();
    }
   
}
