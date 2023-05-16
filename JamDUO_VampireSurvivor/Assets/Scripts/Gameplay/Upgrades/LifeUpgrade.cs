using System;
using UnityEngine;


[Serializable]
public class LifeUpgrade : BaseUpgrade
{
    [Tooltip("Life max multiplier")]
    [SerializeField] private float _multiplier = 1.1f;
    
    public override void Execute(PlayerController player)
    {
        player.IncreaseLifeMax(_multiplier);
    }
}

