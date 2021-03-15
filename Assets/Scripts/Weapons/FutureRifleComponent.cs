﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class FutureRifleComponent : WeaponComponent
    {
        private Camera ViewCamera;

        private RaycastHit HitLocation;

        private void Awake()
        {
            ViewCamera = Camera.main;
        }

        protected override void FireWeapon()
        {
            if (WeaponStats.BulletsInClip > 0 && !Reloading && !WeaponHolder.Controller.IsJumping)
            {
                Debug.Log("Firing Weapon");
                base.FireWeapon();

                Ray screenRay = ViewCamera.ScreenPointToRay(new Vector3(Crosshair.CurrentMousePosition.x,
                    Crosshair.CurrentMousePosition.y, 0));

                if (Physics.Raycast(screenRay, out RaycastHit hit, WeaponStats.FireDistance,
                    WeaponStats.WeaponHitLayer))
                {
                    Vector3 RayDirection = HitLocation.point - ViewCamera.transform.position;

                    Debug.DrawRay(ViewCamera.transform.position, RayDirection * WeaponStats.FireDistance, Color.red);

                    HitLocation = hit;
                }
              
            }
            else if (WeaponStats.BulletsInClip <= 0)
            {
                WeaponHolder.StartReloading();
            }


        }


        private void OnDrawGizmos()
        {
            if (HitLocation.transform)
            {
                Gizmos.DrawSphere(HitLocation.point, 0.2f);
            }
        }
    }
}
