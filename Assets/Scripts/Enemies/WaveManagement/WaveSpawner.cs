using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private bool _endlessWaves;
    private List<Wave> waves;
    [SerializeField] private EnemiesManager _enemiesManager;

    private int waveIndex;
    private float _timer;

    private static System.Random rng = new System.Random();

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

        if(_endlessWaves)
            RandomizeWaves();
    }

    private void Update()
    {
        if(_timer <= 0)
            SpawnWave();

        _timer -= Time.deltaTime;
    }

    private void RandomizeWaves()
    {
        int n = waves.Count;
        while (n > 1) {  
            n--;  
            int k = rng.Next(n + 1);  
            Wave w = waves[k];  
            waves[k] = waves[n];  
            waves[n] = w;
        }  
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
        {
            _timer = waves[waveIndex].delay;
        }
        else
        {
            waveIndex = 0;
            RandomizeWaves();
            _timer = waves[waveIndex].delay;
        }
            
    } 
}
