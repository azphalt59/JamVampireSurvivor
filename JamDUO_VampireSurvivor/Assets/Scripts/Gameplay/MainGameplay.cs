using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Entry point of the main gameplay
/// Main singleton to get access to everything :
/// Player, Enemies, Data, UI etc...
/// </summary>
public class MainGameplay : MonoBehaviour
{
    #region Singleton

    public static MainGameplay Instance;

    #endregion

    /// <summary>
    /// Represents the current state of gameplay
    /// </summary>
    public enum GameState
    {
        Gameplay,
        GameOver
    }



    #region Inspector

    [SerializeField] PlayerController _player;
    [SerializeField] GameplayData _data;
    [SerializeField] GameUIManager _gameUIManager;

    [SerializeField] GameObject _prefabXp;
    [SerializeField] GameObject _hitFx;
    [SerializeField] List<GameObject> _onKillFx;

    [SerializeField] GameObject lose;
    [SerializeField] GameObject win;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip levelupClip;


    #endregion

    #region Properties

    public PlayerController Player => _player;
 
    public GameObject PrefabXP => _prefabXp;
    public GameState State { get; private set; }
    public List<EnemyController> Enemies => _enemies;
    public GameUIManager GameUIManager => _gameUIManager;
    public GameObject HitFx => _hitFx;
    public List<GameObject> OnKillFx => _onKillFx;

    #endregion

    #region Fields

    readonly List<EnemyController> _enemies = new List<EnemyController>();
    float _timerIncrement;
    int _timerSeconds;

    #endregion

    #region Initialize

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("an instance of MainGameplay already exists");
        }

        Instance = this;
    }

    void Start()
    {
        _gameUIManager.RefreshTimer(_timerSeconds);

        _gameUIManager.Initialize(_player);
        _player.OnDeath += OnPlayerDeath;
        _player.OnLevelUp += OnLevelUp;
    }



    #endregion

    #region Update

    void Update()
    {
        UpdateTimer();
    }

    void UpdateTimer()
    {
        _timerIncrement += Time.deltaTime;

        if (_timerIncrement >= 1)
        {
            _timerIncrement -= 1;
            _timerSeconds++;
            _gameUIManager.RefreshTimer(_timerSeconds);

            if (_timerSeconds >= _data.TimerToWin)
            {
                SetVictory();
            }
        }
    }

    #endregion

    #region Game Events

    internal void UnPause()
    {
        Time.timeScale = 1;
    }

    internal void Pause()
    {
        Time.timeScale = 0;
    }

    private void OnLevelUp(int level)
    {
        audioSource.clip = levelupClip;
        audioSource.Play();
        Pause();
        
        
        List<UpgradeData> upgrades = new List<UpgradeData>();
        upgrades.AddRange(_player.UpgradesAvailable);

        List<UpgradeData> randomUpgrades = new List<UpgradeData>();
        const int nbUpgrades = 3;
        for (int i = 0; i < nbUpgrades; i++)
        {
            if (upgrades.Count == 0)
                break;

            int rnd = Random.Range(0, upgrades.Count);
            UpgradeData upgrade = upgrades[rnd];
            upgrades.RemoveAt(rnd);
            randomUpgrades.Add(upgrade);
        }

        _gameUIManager.DisplayUpgrades(randomUpgrades.ToArray());
    }

    void OnPlayerDeath()
    {
        State = GameState.GameOver;
        lose.GetComponent<SceneLoader>().LoadScene();
        _gameUIManager.DisplayGameOver();
    }

    void SetVictory()
    {
        State = GameState.GameOver;
        win.GetComponent<SceneLoader>().LoadScene();
        _gameUIManager.DisplayVictory();
    }
    
    public void OnClickQuit()
    {
        SceneManager.LoadScene("MainMenu");
    }

    #endregion
    
    #region Tools

    public EnemyController GetClosestEnemy(Vector3 position)
    {
        float bestDistance = float.MaxValue;
        EnemyController bestEnemy = null;

        foreach (var enemy in _enemies)
        {
            float dist = Vector3.Distance(enemy.transform.position, position);

            if (dist < bestDistance)
            {
                bestDistance = dist;
                bestEnemy = enemy;
            }
        }
        return bestEnemy;
    }

    List<EnemyController> _enemiesOnScreen = new List<EnemyController>();

    public EnemyController GetRandomEnemyOnScreen()
    {
        _enemiesOnScreen.Clear();

        foreach (var enemy in _enemies)
        {
            Vector3 viewport = Camera.main.WorldToViewportPoint(enemy.transform.position);
            if (viewport.x >= 0 && viewport.x <= 1 && viewport.y >= 0 && viewport.y <= 1)
                _enemiesOnScreen.Add(enemy);
        }

        if (_enemiesOnScreen.Count == 0)
            return null;

        int rnd = Random.Range(0, _enemiesOnScreen.Count);

        return _enemiesOnScreen[rnd];
    }

    #endregion
}