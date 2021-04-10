using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Character;
using Character.UI;

namespace Weapons
{

    public enum WeaponType
    {
        None,
        MachineGun,
        Pistol
    }



    [Serializable]
    public struct WeaponStats
    {
        public WeaponType WeaponType;
        public string Name;
        public float Damage;
        public int BulletsInClip;
        public int ClipSize;
        public int TotalBullestAvailable;

        public float FireStartDelay;
        public float FireRate;
        public float FireDistance;
        public bool Repeating;

        public LayerMask WeaponHitLayer;
    }

    public class WeaponComponent : MonoBehaviour
    {
        public Transform handPosition => GripIKLocation;
        [SerializeField] private Transform GripIKLocation;

        public bool Firing { get; private set; }
        public bool Reloading { get; private set; }

        public WeaponStats WeaponStats;

        protected WeaponHolder WeaponHolder;
        protected CrossHairScript Crosshair;

        public void Initialize(WeaponHolder weaponHolder, CrossHairScript crossHair)
        {
            WeaponHolder = weaponHolder;
            Crosshair = crossHair;
        }

        public void Start()
        {

        }

        public virtual void StartFiring()
        {
            Firing = true;
            if(WeaponStats.Repeating)
            {
                //InvokeRepeating(nameof(FireWeapon),WeaponStats.FireStartDelay,WeaponStats.FireRate);
                InvokeRepeating(nameof(FireWeapon), 0.0f, WeaponStats.FireRate);
            }
            else
            {
                FireWeapon();
            }
        }

        public virtual void StopFiring()
        {
            Firing = false;
            CancelInvoke(nameof(FireWeapon));

        }

        protected virtual void FireWeapon()
        {
            WeaponStats.BulletsInClip--;
        }

        public virtual void StartReloading()
        {
            Reloading = true;
            ReloadWeapon();
        }

        public virtual void StopReloading()
        {
            Reloading = false;
        }

        protected virtual void ReloadWeapon()
        {
            int BulletToReload = WeaponStats.ClipSize - WeaponStats.TotalBullestAvailable;
            if (BulletToReload < 0)
            {
                Debug.Log("Reload");
                WeaponStats.BulletsInClip = WeaponStats.ClipSize;
                WeaponStats.TotalBullestAvailable -= WeaponStats.ClipSize;
            }
            else
            {
                Debug.Log("Out Of Ammo");
                WeaponStats.BulletsInClip = WeaponStats.TotalBullestAvailable;
                WeaponStats.TotalBullestAvailable = 0;
            }
        }
    }
}

