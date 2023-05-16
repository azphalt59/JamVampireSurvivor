using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// Represents an enemy who's moving toward the player
/// and damage him on collision
/// data bout the enemy are stored in the EnemyData class
/// CAUTION : don't forget to call Initialize when you create an enemy
/// </summary>
public class EnemyController : Unit
{
    GameObject _player;
    Rigidbody _rb;
    EnemyData _data;
    private List<PlayerController> _playersInTrigger = new List<PlayerController>();
    int _xpValue;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _player = MainGameplay.Instance.Player.gameObject;
    }

    private void Start()
    {
        UnScale();
    }
    public void Initialize(GameObject player, EnemyData data)
    {
        _player = player;
        _player = MainGameplay.Instance.Player.gameObject;
        _data = data;
        _life = data.Life;
        _team = 1;
        _xpValue = data.Xp;
    }

    private void Update()
    {
        

        if (_life <= 0)
            return;


        foreach (var player in _playersInTrigger)
        {
            player.Hit(Time.deltaTime * _data.DamagePerSeconds);
        }
    }
    
    void FixedUpdate()
    {
        MoveToPlayer();
    }
    
    private void UpScale()
    {
       
        transform.DOScale(1.2f, 0.4f).OnComplete(UnScale);
    }
    private void UnScale()
    {
        transform.DOScale(0.8f, 0.4f).OnComplete(UpScale);
    }
    private void MoveToPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, Time.deltaTime * _data.MoveSpeed);
        transform.LookAt(_player.transform);
    }

    public override void Hit(float damage)
    {
        _life -= damage;

        if (Life <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        MainGameplay.Instance.Enemies.Remove(this);
        GameObject.Destroy(gameObject);
        var xp = GameObject.Instantiate(MainGameplay.Instance.PrefabXP, transform.position, Quaternion.identity);
        xp.GetComponent<CollectableXp>().Initialize(_xpValue);
    }

  
    private void OnTriggerEnter(Collider col)
    {
        var other = HitWithParent.GetComponent<PlayerController>(col);
        if (other != null)
        {
            if (_playersInTrigger.Contains(other) == false)
                _playersInTrigger.Add(other);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        var other = HitWithParent.GetComponent<PlayerController>(col);

        if (other != null)
        {
            if (_playersInTrigger.Contains(other))
                _playersInTrigger.Remove(other);
        }
    }
}