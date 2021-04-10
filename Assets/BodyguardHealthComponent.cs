using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Health_System;

[RequireComponent(typeof(BodyguardComponent))]
public class BodyguardHealthComponent : HealthComponent
{
    private StateMachine BodyguardStateMachine;

    // Start is called before the first frame update
    void Awake()
    {
        BodyguardStateMachine = GetComponent<StateMachine>();
    }

    public override void Destroy()
    {
        BodyguardStateMachine.ChangeState(BodyguardStateType.Dead);
    }
}
