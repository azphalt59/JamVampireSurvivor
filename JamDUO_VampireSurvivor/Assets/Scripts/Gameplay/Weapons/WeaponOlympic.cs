using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponOlympic : WeaponBase
{
    [SerializeField] GameObject _prefab;
    [SerializeField] float _speed;
    

    public WeaponOlympic()
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
        GameObject go = GameObject.Instantiate(_prefab, playerPosition + Vector3.forward, Quaternion.identity);
        
        Vector3 direction = enemy.transform.position - playerPosition;
        if (direction.sqrMagnitude > 0)
        {
            direction.Normalize();
            go.GetComponent<BulletOlympic>().Initialize(direction, GetDamage(), _speed);
        }
        if (GetProjectileCount() == 2)
        {
            GameObject go2 = GameObject.Instantiate(_prefab, playerPosition - Vector3.forward, Quaternion.identity);
            Vector3 dir2 = -direction;
            go.GetComponent<BulletOlympic>().Initialize(dir2, GetDamage(), _speed);
        }
    }

    
}
