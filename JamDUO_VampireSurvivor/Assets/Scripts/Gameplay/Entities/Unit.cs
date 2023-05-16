using UnityEngine;

/// <summary>
/// Represents the base class for entities that can be hit and have life
/// </summary>
public class Unit : MonoBehaviour
{
    public int Team => _team;
    public float Life => _life;
    public float LifeMax => _lifeMax;

    protected int _team;
    protected float _life;
    protected float _lifeMax;

    public virtual void Hit(float damage)
    {
   
    }
}
