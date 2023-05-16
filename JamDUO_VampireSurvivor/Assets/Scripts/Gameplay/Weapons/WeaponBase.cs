using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the base class of all the player's weapons
/// </summary>
public abstract class WeaponBase
{
    [SerializeField] protected float _damageMin;
    [SerializeField] protected float _damageMax;
    [SerializeField] protected float _coolDown;
    [SerializeField] protected int _projectileCount;
    
    public int Slot { get; private set; }
    
    protected float _timerCoolDown;
    
    

    public void Initialize(int slot)
    {
        Slot = slot;
    }

    protected float GetDamage()
    {
        return Random.Range(_damageMin, _damageMax);
    }
    protected int GetProjectileCount()
    {
        return _projectileCount;
    }
    
    public abstract void Update(PlayerController player);
}
