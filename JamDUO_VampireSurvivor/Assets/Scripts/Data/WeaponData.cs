using Common.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponData : ScriptableObject
{
    [SerializeReference] [Instantiable(  type: typeof(WeaponBase))] WeaponBase _weapon;
    [SerializeField] private int _slotIndex;

    public WeaponBase Weapon => _weapon;
    public int SlotIndex => _slotIndex;
}
