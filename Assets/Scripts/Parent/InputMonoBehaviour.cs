﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Parent
{ 
    public class InputMonoBehaviour : MonoBehaviour
    {
        protected ThirdPersonShooterInputAction GameInput;

        protected void Awake()
        {
            GameInput = new ThirdPersonShooterInputAction();
        }

        protected void OnEnable()
        {
            GameInput.Enable();
        }

        protected void OnDisable()
        {
            GameInput.Disable();
        }
    }
}
