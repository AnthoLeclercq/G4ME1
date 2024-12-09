using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchState : MovementBaseState
{
    public override void EnterState(MovementsStateManager movement)
    {
        movement.anim.SetBool("Crouching", true);
    }

    public override void UpdateState(MovementsStateManager movement)
    {
        if (Input.GetKey(KeyCode.LeftShift))
            ExitState(movement, movement.Run);
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (movement.dir.magnitude < 0.01f)
                ExitState(movement, movement.Idle);
            else
                ExitState(movement, movement.Walk);
        }

        if (movement.vInput < 0)
            movement.currentMoveSpeed = movement.crouchBackSpeed;
        else
            movement.currentMoveSpeed = movement.crouchSpeed;
        
    }
    
    void ExitState(MovementsStateManager movement, MovementBaseState state)
    {
        movement.anim.SetBool("Crouching", false);
        movement.SwitchState(state);
    }
}
