using System;
using UnityEngine;


[Serializable]
public class NewWeaponUpgrade : BaseUpgrade
{
    [Tooltip("Nouveau weapon")]
    [SerializeField] WeaponBase weapon;

    public override void Execute(PlayerController player)
    {
        player.AddWeapon(weapon, weapon.Slot);
    }
}

