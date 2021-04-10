using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyguardStates : State
{
    protected BodyguardComponent OwnerBodyguard;
    public BodyguardStates(BodyguardComponent Bodyguard, StateMachine stateMachine) : base(stateMachine)
    {
        OwnerBodyguard = Bodyguard;
    }
}

public enum BodyguardStateType
{
    Idle,
    Attack,
    Follow,
    Dead
}