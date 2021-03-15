using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyguardIdleState : BodyguardStates
{
    public BodyguardIdleState(BodyguardComponent Bodyguard, StateMachines stateMachine) : base(Bodyguard, stateMachine)
    {
    }

    public override void Start()
    {
        base.Start();
        OwnerBodyguard.BodyguardNavMesh.isStopped = true;
        OwnerBodyguard.BodyguardNavMesh.ResetPath();
        OwnerBodyguard.BodyguardAnimator.SetFloat("MovementZ", 0.0f);
    }
}
