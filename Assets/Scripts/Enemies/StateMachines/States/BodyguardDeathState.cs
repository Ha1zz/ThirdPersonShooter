using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyguardDeathState : BodyguardStates
{
    public BodyguardDeathState(BodyguardComponent Bodyguard, StateMachines stateMachine) : base(Bodyguard, stateMachine)
    {

    }

    public override void Start()
    {
        base.Start();
        OwnerBodyguard.BodyguardNavMesh.isStopped = true;
        OwnerBodyguard.BodyguardNavMesh.ResetPath();

        OwnerBodyguard.BodyguardAnimator.SetFloat("MovementZ", 0.0f);
        OwnerBodyguard.BodyguardAnimator.SetBool("IsDead", true);

    }

    public override void Exit()
    {
        base.Exit();
        OwnerBodyguard.BodyguardNavMesh.isStopped = false;
        OwnerBodyguard.BodyguardAnimator.SetBool("IsDead", false);

    }
}
