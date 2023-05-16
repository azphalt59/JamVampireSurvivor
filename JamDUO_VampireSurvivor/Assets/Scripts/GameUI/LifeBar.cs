using UnityEngine;

/// <summary>
/// Represents the in-game lifebar of the player
/// </summary>
public class LifeBar : MonoBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer;

    public void SetValue(float value)
    {
        value = Mathf.Clamp01(value);
        var localScale = _spriteRenderer.transform.localScale;
        localScale = new Vector3(value, localScale.y, localScale.z);
        _spriteRenderer.transform.localScale = localScale;
    }

    public void SetValue(float current , float maxValue)
    {
        float value = current / maxValue;
        value = Mathf.Clamp01(value);
        var localScale = _spriteRenderer.transform.localScale;
        localScale = new Vector3(value, localScale.y, localScale.z);
        _spriteRenderer.transform.localScale = localScale;
    }

}
