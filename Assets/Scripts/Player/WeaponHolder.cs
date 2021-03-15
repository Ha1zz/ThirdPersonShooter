using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character.UI;
using UnityEngine.InputSystem;
using Parent;
using Weapons;

namespace Character
{
    public class WeaponHolder : MonoBehaviour
    {
        [Header("Weapon To Spawn"), SerializeField]
        private GameObject WeaponToSpawn;

        [SerializeField] private Transform WeaponSocket;

        private Transform GripIKLocation;

        //
        private Animator playerAnimator;
        private PlayerManager playerController;
        public PlayerManager Controller => playerController;

        //
        private bool WasFiring = false;
        private bool FiringPressed = false;

        //
        private CrossHairScript playerCrosshair;

        //
        private Camera viewCamera;
        private WeaponComponent EquippedWeapon;

        private void Awake()
        {
            playerAnimator = GetComponent<Animator>();
            playerController = GetComponent<PlayerManager>();

            if (playerController)
            {
                playerCrosshair = playerController.CrossHair;
            }

            viewCamera = Camera.main;


        }

        // Start is called before the first frame update
        void Start()
        {
            //GameObject spawnedWeapon = Instantiate(WeaponToSpawn, WeaponSocket.position, WeaponSocket.rotation, WeaponSocket);
            //if (!spawnedWeapon) return;

            GameObject spawnedWeapon = WeaponToSpawn;
            if (!spawnedWeapon) return;

            EquippedWeapon = spawnedWeapon.GetComponent<WeaponComponent>();

            EquippedWeapon.Initialize(this, playerController.CrossHair);
            playerAnimator.SetInteger("WeaponType", (int)EquippedWeapon.WeaponStats.WeaponType);


            PlayerEvents.Invoke_OnWeaponEquipped(EquippedWeapon);

            //spawnedWeapon.transform.parent = WeaponSocket;
            //equippedWeapon = spawnedWeapon.GetComponent<WeaponComponent>();
            //if (!equippedWeapon) return;

            //EquippedWeapon.Initialize(this, PlayerCrosshair);

            //GripIKLocation = equippedWeapon.handPosition;
            //PlayerAnimator.SetInteger(WeaponTypeHash, (int)EquippedWeapon.WeaponInformation.WeaponType);
        }

        public void OnLook(InputValue delta)
        {
            Vector3 independentMousePosition = viewCamera.ScreenToViewportPoint(playerCrosshair.CurrentMousePosition);

            //Debug.Log(independentMousePosition);

            playerAnimator.SetFloat("AimHorizontal",independentMousePosition.x);
            //playerAnimator.SetFloat("AimVertical", independentMousePosition.y);
        }

        public void OnFire(InputValue Button)
        {
            FiringPressed = Button.isPressed;
            if (Button.isPressed)
            {
                StartFiring();
            }
            else
            {
                StopFiring();
            }
        }

        private void StartFiring()
        {

            if (EquippedWeapon.WeaponStats.TotalBullestAvailable <= 0
                && EquippedWeapon.WeaponStats.BulletsInClip <= 0) return;


            playerController.IsFiring = true;
            playerAnimator.SetBool("IsFiring", true);
            EquippedWeapon.StartFiring();
        }

        private void StopFiring()
        {
            playerController.IsFiring = false;
            playerAnimator.SetBool("IsFiring", false);
            EquippedWeapon.StopFiring();
        }

        public void OnReload(InputValue Button)
        {
            StartReloading();
        }

        public void StartReloading()
        {
            if (EquippedWeapon.WeaponStats.TotalBullestAvailable <= 0 && playerController.IsFiring)
            {
                StopFiring();
                return;
            }

            playerController.IsReloading = true;
            playerAnimator.SetBool("IsReloading", true);
            EquippedWeapon.StartReloading();

            InvokeRepeating(nameof(StopReloading), 0, 0.1f);
        }

        public void StopReloading()
        {
            if (playerAnimator.GetBool("IsReloading")) return;

            playerController.IsReloading = false;
            EquippedWeapon.StopReloading();
            CancelInvoke(nameof(StopReloading));

            if (!WasFiring && !FiringPressed) return;
           
            StartFiring();
            WasFiring = false;
            
        }







        //private void OnAnimatorIK(int layerIndex)
        //{
        //    playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
        //    playerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, GripIKLocation.position);
        //}


        // Update is called once per frame
        void Update()
        {

        }
    }
}
