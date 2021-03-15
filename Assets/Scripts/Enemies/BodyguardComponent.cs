using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(StateMachines))]


public class BodyguardComponent : MonoBehaviour
{
    public NavMeshAgent BodyguardNavMesh { get; private set; }
    public Animator BodyguardAnimator { get; private set; }

    public StateMachines StateMachine { get; private set; }

    public GameObject FollowTarget;

    [SerializeField] private bool Debug;


    private void Awake()
    {
        BodyguardNavMesh = GetComponent<NavMeshAgent>();
        BodyguardAnimator = GetComponent<Animator>();
        StateMachine = GetComponent<StateMachines>();
    }

    // Start is called before the first frame update
    void Start()
    {

        if (Debug)
        {
            Initialize(FollowTarget);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Initialize(GameObject followTarget)
    {
        FollowTarget = followTarget;

        BodyguardIdleState idleState = new BodyguardIdleState(this, StateMachine);
        StateMachine.AddState(BodyguardStateType.Idle, idleState);

        BodyguardFollowState followState = new BodyguardFollowState(FollowTarget, this, StateMachine);
        StateMachine.AddState(BodyguardStateType.Follow, followState);

        BodyguardAttackState attackState = new BodyguardAttackState(FollowTarget, this, StateMachine);
        StateMachine.AddState(BodyguardStateType.Attack, attackState);

        BodyguardDeathState deadState = new BodyguardDeathState(this, StateMachine);
        StateMachine.AddState(BodyguardStateType.Dead, deadState);



        StateMachine.Initialize(BodyguardStateType.Follow);

    }


}
