using UnityEngine;


/// <summary>
/// Represents some global gameplay values
/// </summary>
[CreateAssetMenu]
public class GameplayData : ScriptableObject
{
    [Tooltip("Player wins after surviving this time in seconds")]
    public int TimerToWin = 10;
}