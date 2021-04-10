using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;
using Character;

[CreateAssetMenu(fileName = "Item", menuName = "Items/Weapon", order = 1)]
public class WeaponScriptable : EquippableScriptable
{
    public WeaponStats WeaponStats;

    public override void UseItem(PlayerController controller)
    {
        base.UseItem(controller);
        if (Equipped)
        {
            controller.WeaponHolder.EquipWeapon(this);
        }
        else
        {
            controller.WeaponHolder.UnEquipWeapon();
        }
    }
}
