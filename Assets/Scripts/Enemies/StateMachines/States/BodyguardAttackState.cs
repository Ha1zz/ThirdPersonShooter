using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyguardAttackState : BodyguardStates
{
    private GameObject FollowTarget;
    private float AttackRange = 1.5f;

    public BodyguardAttackState(GameObject followTarget, BodyguardComponent Bodyguard, StateMachines stateMachine) : base(Bodyguard, stateMachine)
    {
        FollowTarget = followTarget;
        UpdateInterval = 2.0f;
    }

    public override void Start()
    {
        OwnerBodyguard.BodyguardNavMesh.isStopped = true;
        OwnerBodyguard.BodyguardNavMesh.ResetPath();
        OwnerBodyguard.BodyguardAnimator.SetFloat("MovementZ", 0.0f);
        OwnerBodyguard.BodyguardAnimator.SetBool("IsAttacking", true);

    }

    public override void IntervalUpdate()
    {
        base.IntervalUpdate();
    }

    public override void Update()
    {
        OwnerBodyguard.transform.LookAt(FollowTarget.transform.position, Vector3.up);

        float distanceBetween = Vector3.Distance(OwnerBodyguard.transform.position, FollowTarget.transform.position);

        if (distanceBetween > AttackRange)
        {
            StateMachine.ChangeState(BodyguardStateType.Follow);
        }
    }

    public override void Exit()
    {
        base.Exit();
        OwnerBodyguard.BodyguardAnimator.SetBool("IsAttacking", false);
    }
}
