using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Health_System;

public class PlayerHealthComponent : HealthComponent
{
    protected void Start()
    {
        PlayerEvents.Invoke_OnPlayerHealthSet(this);
    }
}
