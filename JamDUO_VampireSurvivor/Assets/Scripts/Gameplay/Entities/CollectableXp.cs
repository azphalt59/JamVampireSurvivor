using UnityEngine;

/// <summary>
/// Represents the xp points the player has to collect to level up
/// </summary>
public class CollectableXp : MonoBehaviour
{
    public int Value { get; private set; }
    public GameObject mesh;
    [SerializeField] private ParticleSystem PickFx;

    public void Initialize(int value)
    {
        Value = value;
    }
    private void Start()
    {
        Destroy(gameObject, 15f);
    }
    void OnTriggerEnter(Collider col)
    {
        var other = HitWithParent.GetComponent<PlayerController>(col);
        
        if (other != null)
        {
            mesh.SetActive(false);
            other.CollectXP(Value);
            PickFx.Play();
            GameObject.Destroy(gameObject, 1.5f);
        }
    }
    
    
    
}
