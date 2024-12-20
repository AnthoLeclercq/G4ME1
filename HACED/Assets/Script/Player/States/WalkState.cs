using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : MovementBaseState
{
    public override void EnterState(MovementsStateManager movement)
    {
        movement.anim.SetBool("Walking", true);
    }

    public override void UpdateState(MovementsStateManager movement)
    {
        if (Input.GetKey(KeyCode.LeftShift))
            ExitState(movement, movement.Run);
        else if (Input.GetKeyDown(KeyCode.LeftControl))
            ExitState(movement, movement.Crouch);
        else if (movement.dir.magnitude < 0.01f)
            ExitState(movement, movement.Idle);

        if (movement.vInput < 0)
            movement.currentMoveSpeed = movement.walkBackSpeed;
        else
            movement.currentMoveSpeed = movement.walkSpeed;
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            movement.previousState = this;
            ExitState(movement, movement.Jump);
        }
    }

    void ExitState(MovementsStateManager movement, MovementBaseState state)
    {
        movement.anim.SetBool("Walking", false);
        movement.SwitchState(state);
    }
}
