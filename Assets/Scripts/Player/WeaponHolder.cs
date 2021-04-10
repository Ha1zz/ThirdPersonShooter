using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character.UI;
using UnityEngine.InputSystem;
using Weapons;
using Character;

 public class WeaponHolder : InputMonoBehaviour
 {
     [Header("Weapon To Spawn"), SerializeField]
     private GameObject WeaponToSpawn;

     [SerializeField] private Transform WeaponSocket;

     private Transform GripIKLocation;

     //
     private Animator playerAnimator;
     private PlayerController playerController;
     public PlayerController Controller => playerController;

     //
     private bool WasFiring = false;
     private bool FiringPressed = false;

     //
     private CrossHairScript playerCrosshair;

     //
     private Camera viewCamera;

    //
    public WeaponComponent EquippedWeapon => WeaponComponent;
    private WeaponComponent WeaponComponent;

    private new void Awake()
     {
        base.Awake();
         playerAnimator = GetComponent<Animator>();
         playerController = GetComponent<PlayerController>();

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

         WeaponComponent = spawnedWeapon.GetComponent<WeaponComponent>();

         WeaponComponent.Initialize(this, playerController.CrossHair);
         playerAnimator.SetInteger("WeaponType", (int)WeaponComponent.WeaponStats.WeaponType);


         PlayerEvents.Invoke_OnWeaponEquipped(WeaponComponent);

         //spawnedWeapon.transform.parent = WeaponSocket;
         //WeaponComponent = spawnedWeapon.GetComponent<WeaponComponent>();
         //if (!WeaponComponent) return;

         //WeaponComponent.Initialize(this, PlayerCrosshair);

         //GripIKLocation = WeaponComponent.handPosition;
         //PlayerAnimator.SetInteger(WeaponTypeHash, (int)WeaponComponent.WeaponInformation.WeaponType);
     }

    //public void OnLook(InputValue delta)
    //{
    //    Vector3 independentMousePosition = viewCamera.ScreenToViewportPoint(playerCrosshair.CurrentMousePosition);

    //    //Debug.Log(independentMousePosition);

    //    playerAnimator.SetFloat("AimHorizontal",independentMousePosition.x);
    //    //playerAnimator.SetFloat("AimVertical", independentMousePosition.y);
    //}

    public void OnLookFix(InputAction.CallbackContext obj)
    {
        Vector3 independentMousePosition = viewCamera.ScreenToViewportPoint(playerCrosshair.CurrentMousePosition);

        //Debug.Log(independentMousePosition);

        playerAnimator.SetFloat("AimHorizontal", independentMousePosition.x);
        //playerAnimator.SetFloat("AimVertical", independentMousePosition.y);
    }


    private new void OnEnable()
     {
         base.OnEnable();
         GameInput.PlayerActionMap.Look.performed += OnLookFix;
     }

     private new void OnDisable()
     {
         base.OnDisable();
         GameInput.PlayerActionMap.Look.performed -= OnLookFix;
     }

 public void OnReload(InputValue button)
 {
     StartReloading();
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

         if (WeaponComponent.WeaponStats.TotalBullestAvailable <= 0
             && WeaponComponent.WeaponStats.BulletsInClip <= 0) return;


         playerController.IsFiring = true;
         playerAnimator.SetBool("IsFiring", true);
         WeaponComponent.StartFiring();
     }

     private void StopFiring()
     {
         playerController.IsFiring = false;
         playerAnimator.SetBool("IsFiring", false);
         WeaponComponent.StopFiring();
     }

     public void StartReloading()
     {
         if (WeaponComponent.WeaponStats.TotalBullestAvailable <= 0 && playerController.IsFiring)
         {
             StopFiring();
             return;
         }

         playerController.IsReloading = true;
         playerAnimator.SetBool("IsReloading", true);
         WeaponComponent.StartReloading();

         InvokeRepeating(nameof(StopReloading), 0, 0.1f);
     }

     public void StopReloading()
     {
         if (playerAnimator.GetBool("IsReloading")) return;

         playerController.IsReloading = false;
         WeaponComponent.StopReloading();
         CancelInvoke(nameof(StopReloading));

         if (!WasFiring && !FiringPressed) return;
        
         StartFiring();
         WasFiring = false;
         
     }

    public void EquipWeapon(WeaponScriptable weaponScripable)
    {
        //GameObject spawnedWeapon = Instantiate(weaponScripable.ItemPrefab, WeaponSocketLocation.position, WeaponSocketLocation.rotation, WeaponSocketLocation);

        GameObject spawnedWeapon = WeaponToSpawn;
        if (!spawnedWeapon) return;

        WeaponComponent = spawnedWeapon.GetComponent<WeaponComponent>();

        WeaponComponent.Initialize(this, playerController.CrossHair);



        if (!spawnedWeapon) return;
        if (spawnedWeapon)
        {
            WeaponComponent = spawnedWeapon.GetComponent<WeaponComponent>();
            //if (WeaponComponent)
            //{
            //    GripIKLocation = WeaponComponent.GripLocation;
            //}
        }

        WeaponComponent.Initialize(this, playerController.CrossHair);
        playerAnimator.SetInteger("WeaponType", (int)WeaponComponent.WeaponStats.WeaponType);
        PlayerEvents.Invoke_OnWeaponEquipped(WeaponComponent);
    }

    public void UnEquipWeapon()
    {
        Destroy(WeaponComponent.gameObject);
        WeaponComponent = null;
    }








    //private void OnAnimatorIK(int layerIndex)
    //{
    //    playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
    //    playerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, GripIKLocation.position);
    //}

}

