using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public float delay;
    public Route[] routes;
    private WaveSpawnPoint[] _spawnPoints;

    private void Awake() 
    {
        List<WaveSpawnPoint> spawnPointList = new List<WaveSpawnPoint>();
        foreach (Transform child in transform.GetChild(1))
        {
            WaveSpawnPoint wsp = child.GetComponent<WaveSpawnPoint>();
            if(wsp != null)
                spawnPointList.Add(wsp);
        }
        _spawnPoints = spawnPointList.ToArray();

        List<Route> routeList = new List<Route>();
        foreach (Transform child in transform.GetChild(0))
        {
            Route r = child.GetComponent<Route>();
            if(r != null)
                routeList.Add(r);
        }
        routes = routeList.ToArray();
    }

    public void SpawnWave(EnemiesManager em)
    {
        Debug.Log("@INFO: Spawning " + _spawnPoints.Length + " enemies");
        for (int i=0; i < _spawnPoints.Length; i++)
        {
            WaveSpawnPoint wsp = _spawnPoints[i];
            Enemy e = em.GetEnemy(wsp.enemyType);
            e.ResetEnemy();
            e.ChangePosition(wsp.gameObject.transform);
            e.InitEnemyMovement(routes[i].GetMovementType(), routes[i].GetRoutePoints(), routes[i].GetRouteSpeed());
        }
    }
}
