using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponProteinFart : WeaponBase
{
    [SerializeField] private float duration;
    [SerializeField] GameObject fartArea;
    [SerializeField] float _speed;
    [SerializeField] float _radius;

    public WeaponProteinFart()
    {
    }

    public override void Update(PlayerController player)
    {
        _timerCoolDown += Time.deltaTime;

        if (_timerCoolDown < _coolDown)
        {
            return;
        }
        _timerCoolDown -= _coolDown;
        var playerPosition = MainGameplay.Instance.Player.transform.position;
        GameObject go = GameObject.Instantiate(fartArea, playerPosition, Quaternion.identity, MainGameplay.Instance.Player.transform);
        go.transform.localScale = new Vector3(_radius/3, _radius/3, _radius / 3);
        go.GetComponent<ProteinFart>().SetDamage(GetDamage());
        
        go.GetComponent<CapsuleCollider>().radius = _radius;
     
    }
}
