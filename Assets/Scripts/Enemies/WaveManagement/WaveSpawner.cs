using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    private List<Wave> waves;
    [SerializeField] private EnemiesManager _enemiesManager;

    private int waveIndex;
    private float _timer;

    private void Awake() 
    {
        waveIndex = 0;
        _timer = 5; // Initial wave delay

        waves = new List<Wave>();
        foreach (Transform child in transform.GetChild(0))
        {
            Wave w = child.GetComponent<Wave>();
            if(w != null)
                waves.Add(w);
        }
    }

    private void Update()
    {
        if(_timer <= 0)
            SpawnWave();

        _timer -= Time.deltaTime;
    }

    public void SpawnWave()
    {
        if(waveIndex >= waves.Count)
        {
            Debug.Log("@INFO: All waves spawned");
            return;
        }

        waves[waveIndex].SpawnWave(_enemiesManager);
        Debug.Log("@INFO: Full Wave spawned");

        waveIndex++;
        if(waveIndex < waves.Count)
            _timer = waves[waveIndex].delay;
    } 
}
