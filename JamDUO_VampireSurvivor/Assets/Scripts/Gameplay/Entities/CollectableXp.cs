using UnityEngine;

/// <summary>
/// Represents the xp points the player has to collect to level up
/// </summary>
public class CollectableXp : MonoBehaviour
{
    public int Value { get; private set; }

    public void Initialize(int value)
    {
        Value = value;
    }
    
    void OnTriggerEnter(Collider col)
    {
        var other = HitWithParent.GetComponent<PlayerController>(col);
        
        if (other != null)
        {
            other.CollectXP(Value);
            GameObject.Destroy(gameObject);
        }
    }
    
    
    
}
