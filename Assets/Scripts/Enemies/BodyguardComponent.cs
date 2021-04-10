using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(StateMachine))]
public class BodyguardComponent : MonoBehaviour
{
    public float BodyguardDamage => Damage;

    [SerializeField] private float Damage;

    public NavMeshAgent BodyguardNavMesh { get; private set; }
    public Animator BodyguardAnimator { get; private set; }

    public bool IsDead { get; set; } = false;

    public StateMachine StateMachine { get; private set; }

    public GameObject FollowTarget;
    public GameObject BulletPrefab;
    public GameObject FiringPoint;

    [SerializeField] private bool Debug;
    [SerializeField] private bool IsShortRange;

    public AudioSource dead;


    private void Awake()
    {
        //dead = GetComponent<AudioSource>();
        FollowTarget = GameObject.FindGameObjectWithTag("Player");
        BodyguardNavMesh = GetComponent<NavMeshAgent>();
        BodyguardAnimator = GetComponent<Animator>();
        StateMachine = GetComponent<StateMachine>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (Debug)
        {
            Initialize(IsShortRange,FollowTarget,BulletPrefab,FiringPoint);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDead == true)
        {
            dead.Play(0);
            EnemySpawner.Instance.total--;
            Destroy(this.gameObject);
        }
    }







    public void Initialize(bool isShortRange,GameObject followTarget,GameObject bulletPrefab,GameObject firingPoint)
    {
        FollowTarget = followTarget;
        BulletPrefab = bulletPrefab;
        FiringPoint = firingPoint;
        IsShortRange = isShortRange;

        BodyguardIdleState idleState = new BodyguardIdleState(this, StateMachine);
        StateMachine.AddState(BodyguardStateType.Idle, idleState);

        BodyguardFollowState followState = new BodyguardFollowState(IsShortRange,FollowTarget, this, StateMachine);
        StateMachine.AddState(BodyguardStateType.Follow, followState);

        BodyguardAttackState attackState = new BodyguardAttackState(IsShortRange,FiringPoint,BulletPrefab,FollowTarget, this, StateMachine);
        StateMachine.AddState(BodyguardStateType.Attack, attackState);

        BodyguardDeathState deadState = new BodyguardDeathState(this, StateMachine);
        StateMachine.AddState(BodyguardStateType.Dead, deadState);



        StateMachine.Initialize(BodyguardStateType.Follow);

    }


}
