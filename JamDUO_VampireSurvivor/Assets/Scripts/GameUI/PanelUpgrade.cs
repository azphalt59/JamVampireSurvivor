using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelUpgrade : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text TextName;
    [SerializeField] TMPro.TMP_Text TextDescription;
    [SerializeField] Image ImageIcon;

    UpgradeData _data;

    public void Initialize(UpgradeData data)
    {
        _data = data;

        TextName.text = data.name;
        TextDescription.text = data.Description;
        ImageIcon.sprite = data.Sprite;
    }

    
    public void OnClick()
    {
        MainGameplay.Instance.Player.UnlockUpgrade(_data);
        _data.Upgrade.Execute(MainGameplay.Instance.Player);
        MainGameplay.Instance.UnPause();
        MainGameplay.Instance.GameUIManager.ClosePanelUpgrade();
    }
}
