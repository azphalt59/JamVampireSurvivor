using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDumbbell : WeaponBase
{
    [SerializeField] GameObject _prefab;
    [SerializeField] float _speed;

    public WeaponDumbbell()
    {
    }
    public override void Update(PlayerController player)
    {
        _timerCoolDown += Time.deltaTime;

        if (_timerCoolDown < _coolDown)
            return;

        _timerCoolDown -= _coolDown;

        EnemyController enemy = MainGameplay.Instance.GetClosestEnemy(MainGameplay.Instance.Player.transform.position);
        if (enemy == null)
            return;

        var playerPosition = MainGameplay.Instance.Player.transform.position;
        GameObject go = GameObject.Instantiate(_prefab, playerPosition, Quaternion.identity);
        Vector3 direction = enemy.transform.position - playerPosition;
        if (direction.sqrMagnitude > 0)
        {
           
            direction.Normalize();
            go.GetComponent<Bullet>().Initialize(direction, GetDamage(), _speed);
        }
    }

}
