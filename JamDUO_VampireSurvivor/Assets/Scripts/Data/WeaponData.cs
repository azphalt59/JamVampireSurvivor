using Common.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponData : ScriptableObject
{
    [SerializeReference] [Instantiable(  type: typeof(WeaponBase))] WeaponBase _weapon;
    [SerializeField] private int _slotIndex;
    [SerializeField] private int _projectileCount;

    public WeaponBase Weapon => _weapon;
    public int SlotIndex => _slotIndex;
    public int ProjectileCount => _projectileCount;
}
