using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Weapons;
using System;

public class WeaponInfoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI CurrentClipText;
    [SerializeField] private TextMeshProUGUI WeaponNameText;
    [SerializeField] private TextMeshProUGUI TotalAmmoText;

    private WeaponComponent EquippedWeapon;

    // Start is called before the first frame update
    void OnEnable()
    {
        PlayerEvents.OnWeaponEquipped += OnWeaponEuipped;
    }

    void OnDisable()
    {
        PlayerEvents.OnWeaponEquipped -= OnWeaponEuipped;
    }

    private void OnWeaponEuipped(WeaponComponent weapon)
    {
        Debug.Log("Weapon Equipped");
        EquippedWeapon = weapon;
        WeaponNameText.text = weapon.WeaponStats.Name;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentClipText.text = EquippedWeapon.WeaponStats.BulletsInClip.ToString();
        TotalAmmoText.text = EquippedWeapon.WeaponStats.TotalBullestAvailable.ToString();
    }
}
