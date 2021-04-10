using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;

[CreateAssetMenu(fileName = "Consumable", menuName = "Items/Consumable", order = 2)]
public class ConsumableScriptable : ItemScriptables
{
    public int Effect = 0;



    public override void UseItem(PlayerController controller)
    {

        if (ItemCategory == ItemCategory.Consumable)
        {
            if (controller.Health.Health >= controller.Health.MaxHealth) return;
            controller.Health.HealPlayer(Effect);
        }
        if (ItemCategory == ItemCategory.Ammo)
        {
            //if (controller.Health.Health >= controller.Health.MaxHealth) return;
            controller.WeaponHolder.EquippedWeapon.WeaponStats.TotalBullestAvailable += Effect;
        }



        SetAmount(Amount - 1);
        if (Amount <= 0)
        {
            DeleteItem(controller);
        }
    }


    //public override void UseAmmo(PlayerController controller)
    //{
    //    if (controller.Health.Health >= controller.Health.MaxHealth) return;
    //    controller.Health.HealPlayer(Effect);

    //    SetAmount(Amount - 1);
    //    if (Amount <= 0)
    //    {
    //        DeleteItem(controller);
    //    }
    //}
}
