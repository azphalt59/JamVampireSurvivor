using Common.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class UpgradeData : ScriptableObject
{
    [SerializeField] [TextArea] string _description;
    [SerializeField] Sprite _sprite;
    [SerializeField] [SerializeReference] [Instantiable(type: typeof(BaseUpgrade))] BaseUpgrade _upgrade;
    [SerializeField] UpgradeData[] _nextUpgrades;

    public BaseUpgrade Upgrade => _upgrade;
    public string Description => _description;
    public Sprite Sprite => _sprite;
    public UpgradeData[] NextUpgrades => _nextUpgrades;

}
