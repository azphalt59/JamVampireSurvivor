using UnityEngine;

/// <summary>
/// Represents the data of an enemy's type 
/// </summary>
[CreateAssetMenu]
public class EnemyData : ScriptableObject
{
    public float Life => _life;
    public float DamagePerSeconds => _damagePerSeconds;
    public float MoveSpeed => _moveSpeed;
    public int Xp => _xp;
    public GameObject Prefab => _prefab;
    


    [SerializeField] float _life;
    [SerializeField] float _damagePerSeconds;
    [SerializeField] float _moveSpeed;
    [SerializeField] int _xp;
    [SerializeField] GameObject _prefab;

    

}