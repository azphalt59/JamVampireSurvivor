using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDumbbellWheel : WeaponBase
{
    [SerializeField] GameObject _prefab;
    [SerializeField] float _speed;
    public WeaponDumbbellWheel()
    { }

    public override void Update(PlayerController player)
    {
        _timerCoolDown += Time.deltaTime;

        if (_timerCoolDown < _coolDown)
            return;

        _timerCoolDown -= _coolDown;

        var playerPosition = MainGameplay.Instance.Player.transform.position;
        
        float angleY = 0;
        for (int i = 0; i < 9; i++)
        {
            GameObject dumbbell = GameObject.Instantiate(_prefab, playerPosition, Quaternion.identity);
            dumbbell.transform.eulerAngles = new Vector3(0, angleY, 0);
            Vector3 direction = dumbbell.transform.forward;
            dumbbell.GetComponent<Bullet>().Initialize(direction, GetDamage(), _speed);
            angleY += 360 / 9;
        }
    }

   
}
