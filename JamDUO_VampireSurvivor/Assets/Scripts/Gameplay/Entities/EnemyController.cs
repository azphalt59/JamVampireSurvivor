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
    [SerializeField] private ParticleSystem HitFx;
    [SerializeField] private ParticleSystem DeathFx;

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
        float rand = UnityEngine.Random.Range(0.7f, 1.4f);
        if (GetComponent<AudioSource>() == null)
        {
            AudioSource a = gameObject.AddComponent<AudioSource>();
            a.playOnAwake = false;
            a.clip = MainGameplay.Instance.EnemyHit;
            a.pitch = rand;
            a.volume = 0.5f;
            a.Play();
            Destroy(a, 3f);
        }
        else
        {
            AudioSource a = gameObject.GetComponent<AudioSource>();
            a.playOnAwake = false;
            a.clip = MainGameplay.Instance.EnemyHit;
            a.pitch = rand;
            a.volume = 0.5f;
            a.Play();
            Destroy(a, 3f);
        }
        
       
        HitFx.gameObject.transform.position = transform.position;
        if(Life>0)
            HitFx.Play();
       
        if (Life <= 0)
        {
            DeathFx.gameObject.transform.position = transform.position;
            DeathFx.Play();
            Die();
        }
    }

    void Die()
    {
        MainGameplay.Instance.Enemies.Remove(this);
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        gameObject.GetComponent<Collider>().enabled = false;
        GameObject.Destroy(gameObject, 1.5f);

        int random = UnityEngine.Random.Range(0, 101);
        if (random <= 50)
        {
            var xp = GameObject.Instantiate(MainGameplay.Instance.PrefabXP, transform.position, Quaternion.identity);
            xp.GetComponent<CollectableXp>().Initialize(_xpValue);
        }
        if(random <= 75)
        {
            int random2 = UnityEngine.Random.Range(0, MainGameplay.Instance.OnKillFx.Count);
            var fx = GameObject.Instantiate(MainGameplay.Instance.OnKillFx[random2], transform.position, Quaternion.identity);
            Destroy(fx, 1f);
        }

       
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