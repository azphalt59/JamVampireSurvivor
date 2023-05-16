using UnityEngine;

/// <summary>
/// Represents the xp required to level up
/// CAUTION : First level of the player is level 1
/// So the 2 first entries should be 0
/// </summary>
[CreateAssetMenu]
public class LevelUpData : ScriptableObject
{
    [SerializeField] int[] _xpForLevels;

    public bool IsLevelMax(int level) => level >= _xpForLevels.Length -1;
    
    public int GetXpForLevel(int level)
    {
        if (IsLevelMax(level))
            level = _xpForLevels.Length - 1;
        
        return _xpForLevels[level];
    }
}
