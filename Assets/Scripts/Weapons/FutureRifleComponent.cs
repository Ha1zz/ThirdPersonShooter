using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Health_System;


namespace Weapons
{
    public class FutureRifleComponent : WeaponComponent
    {
        private Camera ViewCamera;

        private RaycastHit HitLocation;

        public AudioSource firing;

        private void Awake()
        {
            ViewCamera = Camera.main;
        }

        protected override void FireWeapon()
        {
            if (WeaponStats.BulletsInClip > 0 && !Reloading && !WeaponHolder.Controller.IsJumping)
            {
                firing.Play(0);

                //Debug.Log("Firing Weapon");
                base.FireWeapon();

                Ray screenRay = ViewCamera.ScreenPointToRay(new Vector3(Crosshair.CurrentMousePosition.x,
                    Crosshair.CurrentMousePosition.y, 0));

                if (!Physics.Raycast(screenRay, out RaycastHit hit, WeaponStats.FireDistance,
                    WeaponStats.WeaponHitLayer)) return;
                
                    Vector3 RayDirection = HitLocation.point - ViewCamera.transform.position;

                    //Debug.DrawRay(ViewCamera.transform.position, RayDirection * WeaponStats.FireDistance, Color.red);

                Debug.DrawRay(ViewCamera.transform.position, transform.forward * 1000f, Color.yellow);

                HitLocation = hit;

                DamageTarget(hit);
 
              
            }
            else if (WeaponStats.BulletsInClip <= 0)
            {
                WeaponHolder.StartReloading();
            }


        }

        private void DamageTarget(RaycastHit hit)
        {
            if (hit.transform.tag == "Cube")
            {
                IDamagable damagable = hit.collider.GetComponent<IDamagable>();
                damagable?.TakeDamage(WeaponStats.Damage);
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
