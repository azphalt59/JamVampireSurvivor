using System;
using System.Collections.Generic;
using System.Data;
using Gameplay.Weapons;
using UnityEngine;

/// <summary>
/// Represents the player
/// manages the controller, the weapons, the in game lifebar and the level up
/// </summary>
public class PlayerController : Unit
{
    [SerializeField] PlayerData _playerData;
    [SerializeField] LevelUpData _levelUpData;

    [SerializeField] LifeBar _lifeBar;

    public Action OnDeath { get; set; }
    public Action<int, int, int> OnXP { get; set; }
    public Action<int> OnLevelUp { get; set; }
    public List<UpgradeData> UpgradesAvailable { get; private set; }

    public PlayerData PlayerData => _playerData;

    public List<WeaponBase> Weapons => _weapons;

    int _level = 1;
    int _xp = 0;
    float moveSpeed;


    bool _isDead = false;
    Rigidbody _rb;
    
    List<WeaponBase> _weapons = new List<WeaponBase>();

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        UpgradesAvailable = new List<UpgradeData>();
        UpgradesAvailable.AddRange(_playerData.Upgrades);
    }

    void Start()
    {
        _lifeMax = _playerData.Life;
        _life = LifeMax;
        moveSpeed = _playerData.MoveSpeed;

        foreach (var weapon in _playerData.Weapons)
        {
            AddWeapon(weapon.Weapon,weapon.SlotIndex);
        }
    }

    void Update()
    {
        if (_isDead)
            return;

        Move();
        Shoot();

        if ( Input.GetKeyDown(KeyCode.F5))
        {
            CollectXP(20);
        }
    }

    private void Shoot()
    {
        
        foreach (var weapon in Weapons)
        {
            weapon.Update(this);
        }
    }

    private void Move()
    {
        float horizontalMvt = Input.GetAxisRaw("Horizontal");
        float verticalMvt = Input.GetAxisRaw("Vertical");
        Vector3 movementVector = new Vector3(horizontalMvt, 0, verticalMvt).normalized;
        transform.position += moveSpeed * Time.deltaTime * movementVector;
        
        Vector3 lookDir = (transform.position + movementVector) - transform.position;
        if (lookDir == Vector3.zero)
            return;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDir.normalized), _playerData.RotationSpeed * Time.deltaTime);
    }
    

    public override void Hit(float damage)
    {
        if (_isDead)
            return;

        _life -= damage;

        _lifeBar.SetValue(Life, LifeMax);

        if (Life <= 0)
        {
            _isDead = true;
            OnDeath?.Invoke();
        }
    }

    internal void UnlockUpgrade(UpgradeData data)
    {
        UpgradesAvailable.Remove(data);

        UpgradesAvailable.AddRange(data.NextUpgrades);
    }


    internal void AddWeapon(WeaponBase weapon , int slot)
    {
        weapon.Initialize(slot);
        Weapons.Add(weapon);
    }


    public void CollectXP(int value)
    {
        if (_levelUpData.IsLevelMax(_level))
            return;

        _xp += value;

        int nextLevel = _level + 1;
        int currentLevelMaxXP = _levelUpData.GetXpForLevel(nextLevel);
        if (_xp >= currentLevelMaxXP)
        {
            _level++;
            OnLevelUp?.Invoke(_level);
            currentLevelMaxXP = _levelUpData.GetXpForLevel(nextLevel);
        }

        int currentLevelMinXP = _levelUpData.GetXpForLevel(_level);

        if (_levelUpData.IsLevelMax(_level))
        {
            OnXP?.Invoke(currentLevelMaxXP + 1, currentLevelMinXP, currentLevelMaxXP + 1);
        }
        else
        {
            OnXP?.Invoke(_xp, currentLevelMinXP, currentLevelMaxXP);
        }
    }


    void OnDestroy()
    {
        OnDeath = null;
        OnXP = null;
        OnLevelUp = null;
    }


    public void IncreaseLifeMax(float multiplier)
    {
        float valueToAdd = _lifeMax * (multiplier - 1.0f);
        
        _life += valueToAdd;
        _lifeMax += valueToAdd;
    }
    public void IncreaseMvtSpeed(float multiplier)
    {
        float valueToAdd = moveSpeed * (multiplier - 1.0f);
        moveSpeed += valueToAdd;
    }
}