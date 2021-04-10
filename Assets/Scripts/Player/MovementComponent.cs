using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.AI;

namespace Character
{
    public class MovementComponent : MonoBehaviour
    {


        [SerializeField] private float walkSpeed;
        [SerializeField] private float runSpeed;
        [SerializeField] private float JumpForce;


        //
        private Animator playerAnimator;
        private PlayerController playerController;
        private NavMeshAgent playerNavMesh;




        Transform playerTransform;
        private Vector2 inputVector = Vector2.zero;
        private Vector3 moveDirection = Vector3.zero;
        private Rigidbody playerRigidbody;

        private void Awake()
        {
            playerTransform = transform;
            playerController = GetComponent<PlayerController>();
            playerAnimator = GetComponent<Animator>();
            playerRigidbody = GetComponent<Rigidbody>();
            playerNavMesh = GetComponent<NavMeshAgent>();
        }

        public void OnMovement(InputValue value)
        {
            inputVector = value.Get<Vector2>();

            playerAnimator.SetFloat("MovementX", inputVector.x);
            playerAnimator.SetFloat("MovementY", inputVector.y);
        }

        public void OnRun(InputValue value)
        {
            playerController.IsRunning = value.isPressed;
            playerAnimator.SetBool("IsRunning", value.isPressed);
        }

        public void OnJump(InputValue value)
        {
            if (playerController.IsJumping) return;

            playerController.IsJumping = value.isPressed;
            playerAnimator.SetBool("IsJumping", value.isPressed);

            playerNavMesh.enabled = false;

            Invoke(nameof(Jump), 0.1f);

        }

        public void Jump()
        {
            playerRigidbody.AddForce((playerTransform.up + moveDirection) * JumpForce, ForceMode.Impulse);
        }

        // Update is called once per frame
        void Update()
        {
            if(playerController.IsJumping) return;
            if (!(inputVector.magnitude > 0)) moveDirection = Vector3.zero;

            moveDirection = playerTransform.forward * inputVector.y + playerTransform.right * inputVector.x;

            float currentSpeed = playerController.IsRunning ? runSpeed : walkSpeed;

            Vector3 movementDirection = moveDirection * (currentSpeed * Time.deltaTime);

            playerNavMesh.Move(movementDirection);

            //playerTransform.position += movementDirection;


        }


        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Ground") && !playerController.IsJumping) return;

            playerNavMesh.enabled = true;
            playerController.IsJumping = false;
            playerAnimator.SetBool("IsJumping", false);
        }
    }
}

