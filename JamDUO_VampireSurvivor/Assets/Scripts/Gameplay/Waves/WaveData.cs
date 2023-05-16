using System;
using UnityEngine;
using UnityEngine.Serialization;


/// <summary>
/// Represents a wave a spawning enemies
/// </summary>
[Serializable]
public class WaveData
{
    /// <summary>
    /// Enemies can have different movement patterns
    /// </summary>
    public enum MoveType
    {
        Normal,
    }

    public int TimeToStart => _timeToStart;
    public int TimesToRepeat => _timesToRepeat;
    public float RepeatTimer => _repeatTimer;
    public int EnemyCount => _enemyCount;
    public EnemyData Enemy => _enemy;
    public MoveType MovementType => _movementType;
    public float SpawnDistance => _spawnDistance;

    [SerializeField] int _timeToStart;
    [SerializeField] int _timesToRepeat = 1;
    [SerializeField] float _repeatTimer = 0;
    [SerializeField] int _enemyCount = 20;
    [SerializeField] EnemyData _enemy;
    [SerializeField] MoveType _movementType;
    [SerializeField] float _spawnDistance = 15;
}