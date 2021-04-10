using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyguardFollowState : BodyguardStates
{
    private readonly GameObject FollowTarget;
    private float StopDistance = 15.0f;
    private bool IsShortRange;

    public BodyguardFollowState(bool isShortRange,GameObject followTarget, BodyguardComponent Bodyguard, StateMachine stateMachine) : base(Bodyguard, stateMachine)
    {
        IsShortRange = isShortRange;
        FollowTarget = followTarget;
        UpdateInterval = 2.0f;

    }

    public override void Start()
    {
        base.Start();
        OwnerBodyguard.BodyguardNavMesh.SetDestination(FollowTarget.transform.position);
        if (IsShortRange)
        {
            StopDistance = 2.0f;
        }
        else
        {
            StopDistance = 15.0f;
        }
    }

    public override void IntervalUpdate()
    {
        base.IntervalUpdate();
        if (FollowTarget) OwnerBodyguard.BodyguardNavMesh.SetDestination(FollowTarget.transform.position);
    }


    public override void Update()
    {
        base.Update();
        //OwnerBodyguard.BodyguardAnimator.SetFloat("MovementZ", OwnerBodyguard.BodyguardNavMesh.velocity.normalized.z);

        if (FollowTarget)
        {
            if (Vector3.Distance(OwnerBodyguard.transform.position, FollowTarget.transform.position) < StopDistance)
            {
                StateMachine.ChangeState(BodyguardStateType.Attack);
            }
        }
    }
}
