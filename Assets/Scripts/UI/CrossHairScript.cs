using System;
using Parent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character.UI
{ 
    public class CrossHairScript : InputMonoBehaviour
    {
        public Vector2 CurrentMousePosition { get; private set; }



        public bool Inverted = true;

        public Vector2 MouseSensitivity = Vector2.zero;

        [SerializeField, Range(0.0f, 1.0f)]
        private float CrosshairHorizontalPercentage = 0.25f;
        private float HorizontalOffset;
        private float MaxHorizontalDeltaConstrain;
        private float MinHorizontalDeltaConstrain;

        [SerializeField, Range(0.0f, 1.0f)]
        private float CrosshairVerticalPercentage = 0.25f;
        private float VerticalOffset;
        private float MaxVerticalDeltaConstrain;
        private float MinVerticalDeltaConstrain;


        private Vector2 CrosshairStartingPosition;
        private Vector2 CurrentLookDelta;

        //private ThirdPersonShooterInputAction InputActions;

        // Start is called before the first frame update
        void Start()
        {
            if (GameManager.Instance.CursorActive)
            {
                AppEvents.Invoke_OnMouseCursorEnble(false);
            }

            CrosshairStartingPosition = new Vector2(Screen.width / 2f, Screen.height / 2f);

            HorizontalOffset = (Screen.width * CrosshairHorizontalPercentage) / 2f;
            MinHorizontalDeltaConstrain = -(Screen.width / 2f) + HorizontalOffset;
            MaxHorizontalDeltaConstrain = (Screen.width / 2f) - HorizontalOffset;

            VerticalOffset = (Screen.width * CrosshairVerticalPercentage) / 2f;
            MinVerticalDeltaConstrain = -(Screen.height / 2f) + VerticalOffset;
            MaxVerticalDeltaConstrain = (Screen.height / 2f) - VerticalOffset;
        }

        // Update is called once per frame
        void Update()
        {
            float crosshairXPosition = CrosshairStartingPosition.x + CurrentLookDelta.x;
            float crosshairYPosition = Inverted 
                ? CrosshairStartingPosition.y - CurrentLookDelta.y 
                : CrosshairStartingPosition.y + CurrentLookDelta.y;

            CurrentMousePosition = new Vector2(crosshairXPosition, crosshairYPosition);

            transform.position = CurrentMousePosition;
        }

        private new void OnEnable()
        {
            base.OnEnable();
            GameInput.Player.Look.performed += OnLook;
        }

        private new void OnDisable()
        {
            base.OnDisable();
            GameInput.Player.Look.performed -= OnLook;
        }

        private void OnLook(InputAction.CallbackContext delta)
        {
            Vector2 mouseDelta = delta.ReadValue<Vector2>();

            CurrentLookDelta.x += mouseDelta.x * MouseSensitivity.x;
            if (CurrentLookDelta.x >= MaxHorizontalDeltaConstrain || CurrentLookDelta.x <= MinHorizontalDeltaConstrain)
            {
                CurrentLookDelta.x -= mouseDelta.x * MouseSensitivity.x;
            }
            CurrentLookDelta.y += mouseDelta.y * MouseSensitivity.y;
            if (CurrentLookDelta.y >= MaxVerticalDeltaConstrain || CurrentLookDelta.y <= MinVerticalDeltaConstrain)
            {
                CurrentLookDelta.y -= mouseDelta.y * MouseSensitivity.y;
            }
        }

    }
}
