using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Health_System;

public class BodyguardAttackState : BodyguardStates
{
    private GameObject FollowTarget;
    private IDamagable DamagableObject;
    private float AttackRange = 20.0f;
    private GameObject BulletPrefab;
    private GameObject FiringPoint;
    private bool IsShortRange;

    public BodyguardAttackState(bool isShortRange,GameObject firingPoint,GameObject bulletPrefab,GameObject followTarget, BodyguardComponent Bodyguard, StateMachine stateMachine) : base(Bodyguard, stateMachine)
    {
        IsShortRange = isShortRange;
        FiringPoint = firingPoint;
        BulletPrefab = bulletPrefab;
        FollowTarget = followTarget;
        UpdateInterval = 2.0f;
    }

    public override void Start()
    {
        if (IsShortRange)
        {
            AttackRange = 3.0f;
        }
        else
        {
            AttackRange = 20.0f;
        }

        OwnerBodyguard.BodyguardNavMesh.isStopped = true;
        OwnerBodyguard.BodyguardNavMesh.ResetPath();
        //OwnerBodyguard.BodyguardAnimator.SetFloat("MovementZ", 0.0f);
        //OwnerBodyguard.BodyguardAnimator.SetBool("IsAttacking", true);

        DamagableObject = FollowTarget.GetComponent<IDamagable>();
    }

    public override void IntervalUpdate()
    {
        base.IntervalUpdate();

        if (FollowTarget)
        {
            OwnerBodyguard.transform.LookAt(FollowTarget.transform.position, Vector3.up);

            float distanceBetween = Vector3.Distance(OwnerBodyguard.transform.position, FollowTarget.transform.position);

            if (distanceBetween < AttackRange)
            {
                if (IsShortRange)
                {
                    DamagableObject?.TakeDamage(OwnerBodyguard.BodyguardDamage);
                }
                else
                {
                    Instantiate(BulletPrefab, FiringPoint.transform.position, FiringPoint.transform.rotation);
                }
            }
        }
    }

    public override void Update()
    {
        if (FollowTarget)
        {
            OwnerBodyguard.transform.LookAt(FollowTarget.transform.position, Vector3.up);

            float distanceBetween = Vector3.Distance(OwnerBodyguard.transform.position, FollowTarget.transform.position);

            if (distanceBetween > AttackRange)
            {
                StateMachine.ChangeState(BodyguardStateType.Follow);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        OwnerBodyguard.BodyguardAnimator.SetBool("IsAttacking", false);
    }
}
