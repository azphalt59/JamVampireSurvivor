using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponExplosifPill : WeaponBase
{
    [SerializeField] GameObject _prefab;
    [SerializeField] float _speed;
    
    public WeaponExplosifPill()
    {

    }

    public override void Update(PlayerController player)
    {
        _timerCoolDown += Time.deltaTime;

        if (_timerCoolDown < _coolDown)
            return;

        _timerCoolDown -= _coolDown;

        EnemyController enemy = MainGameplay.Instance.GetRandomEnemyOnScreen();
        if (enemy == null)
            return;

        var playerPosition = MainGameplay.Instance.Player.transform.position;
        GameObject go = GameObject.Instantiate(_prefab, playerPosition, Quaternion.identity);
        Vector3 direction = enemy.transform.position - playerPosition;
        if (direction.sqrMagnitude > 0)
        {

            direction.Normalize();
            go.GetComponent<ExplosifBullet>().Initialize(direction, GetDamage(), _speed);
        }
    }
}
