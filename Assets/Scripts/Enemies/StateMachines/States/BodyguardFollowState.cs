using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyguardFollowState : BodyguardStates
{
    private readonly GameObject FollowTarget;
    private const float StopDistance = 2.0f;

    public BodyguardFollowState(GameObject followTarget, BodyguardComponent Bodyguard, StateMachines stateMachine) : base(Bodyguard, stateMachine)
    {
        FollowTarget = followTarget;
        UpdateInterval = 2.0f;

    }

    public override void Start()
    {
        base.Start();
        OwnerBodyguard.BodyguardNavMesh.SetDestination(FollowTarget.transform.position);
    }

    public override void IntervalUpdate()
    {
        base.IntervalUpdate();
        OwnerBodyguard.BodyguardNavMesh.SetDestination(FollowTarget.transform.position);
    }


    public override void Update()
    {
        base.Update();
        OwnerBodyguard.BodyguardAnimator.SetFloat("MovementZ", OwnerBodyguard.BodyguardNavMesh.velocity.normalized.z);

        if (Vector3.Distance(OwnerBodyguard.transform.position, FollowTarget.transform.position) < StopDistance)
        {
            StateMachine.ChangeState(BodyguardStateType.Attack);
        }


    }
}
