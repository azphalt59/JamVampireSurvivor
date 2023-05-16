using System;
using UnityEngine;


[Serializable]
public class SpeedUpgrade : BaseUpgrade
{
    [Tooltip("Speed multiplier")]
    [SerializeField] private float _multiplier = 1.1f;

    public override void Execute(PlayerController player)
    {
        player.IncreaseMvtSpeed(_multiplier);
    }
}

