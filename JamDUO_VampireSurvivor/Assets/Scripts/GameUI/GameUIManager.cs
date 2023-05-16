using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Manages all the UI of the main Gameplay
/// </summary>
public class GameUIManager : MonoBehaviour
{
    [SerializeField] GameObject _panelGameOver;
    [SerializeField] GameObject _panelVictory;

    [SerializeField] TMP_Text _textTimer;
    [SerializeField] XPBar _xpBar;
    [SerializeField] GameObject _panelUpgradesParent;
    [SerializeField] PanelUpgrade[] _panelUpgrades;

    internal void ClosePanelUpgrade()
    {
        _panelUpgradesParent.SetActive(false);
    }

    public void Initialize(PlayerController player)
    {
        player.OnXP += OnXP;
        player.OnLevelUp += OnLevelUp;
    }
    
    void OnLevelUp(int level)
    {
        _xpBar.SetLevel(level);
    }

    void OnXP(int currentXP, int levelXPMin, int levelXPMax)
    {
        _xpBar.SetValue(currentXP, levelXPMin, levelXPMax);
    }


    public void DisplayUpgrades(UpgradeData[] upgrades)
    {
        _panelUpgradesParent.SetActive(true);

        for (int i = 0; i < _panelUpgrades.Length; i++)
        {
            _panelUpgrades[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < upgrades.Length; i++)
        {
            _panelUpgrades[i].gameObject.SetActive(true);
            _panelUpgrades[i].Initialize(upgrades[i]);
        }
    }

    public void DisplayGameOver()
    {
        _panelGameOver.SetActive(true);
    }

    public void DisplayVictory()
    {
        _panelVictory.SetActive(true);
    }

    public void RefreshTimer( int timer)
    {
        int seconds = timer % 60;
        int minutes = timer / 60;
        _textTimer.text = $"{minutes:00}:{seconds:00}";
    }


}
