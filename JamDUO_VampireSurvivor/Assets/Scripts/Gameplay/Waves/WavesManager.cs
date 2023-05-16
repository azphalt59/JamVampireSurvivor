using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the waves : stores the data, updates and plays them 
/// </summary>
public class WavesManager : MonoBehaviour
{
    [SerializeField] WavesLevelData _wavesLevel;
    [SerializeField] Transform _topRight;
    [SerializeField] Transform _bottomLeft;

    readonly List<WaveInstance> _wavesToPlay = new List<WaveInstance>();
    float _timer;

    void Awake()
    {
        foreach (var data in _wavesLevel.Waves)
        {
            WaveInstance instance = new WaveInstance(data);
            _wavesToPlay.Add(instance);
        }
    }

    void Update()
    {
        if (MainGameplay.Instance.State != MainGameplay.GameState.Gameplay)
            return;
        
        _timer += Time.deltaTime;

        for (int i = _wavesToPlay.Count - 1; i >= 0; i--)
        {
            _wavesToPlay[i].Update(this,_timer);
            
            if ( _wavesToPlay[i].IsDone)
                _wavesToPlay.RemoveAt(i);
        }
    }

    public void Spawn(WaveData data)
    {
        for (int i = 0; i < data.EnemyCount; i++)
        {
            GameObject go = GameObject.Instantiate(data.Enemy.Prefab);

            float rand1 = Random.Range(_bottomLeft.transform.position.x,_topRight.transform.position.x);
            float rand2 = Random.Range(_bottomLeft.transform.position.z, _topRight.transform.position.z);

            Vector3 spawnPosition = new Vector3(rand1, 0, rand2);
            spawnPosition.y = data.Enemy.Prefab.GetComponent<Collider>().bounds.size.y * 0.5f - 0.6f;
            spawnPosition.Normalize();

            Vector3 tempPosition = MainGameplay.Instance.Player.transform.position + spawnPosition * data.SpawnDistance;
            if (tempPosition.x > _topRight.transform.position.x ||
                tempPosition.x < _bottomLeft.transform.position.x)
            {
                spawnPosition.x = -spawnPosition.x;
            }
            if (tempPosition.z > _topRight.transform.position.z ||
                tempPosition.z < _bottomLeft.transform.position.z)
            {
                spawnPosition.z = -spawnPosition.z;
            }
            
            go.transform.position = MainGameplay.Instance.Player.transform.position + spawnPosition * data.SpawnDistance;
            
            var enemy = go.GetComponent<EnemyController>();
            enemy.Initialize(MainGameplay.Instance.Player.gameObject , data.Enemy);
            MainGameplay.Instance.Enemies.Add(enemy);
        }
    }
}