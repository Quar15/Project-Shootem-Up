using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private Enemy[] enemyPrefabs;
    private List<Enemy> spawnedEnemies; // All enemies that are alive or dead
    private List<Enemy> avEnemies; // Dead enemies that can be reused

    private void Awake() 
    {
        spawnedEnemies = new List<Enemy>();
        avEnemies = new List<Enemy>();
    }

    public Enemy GetEnemy(EnemyType enemyType)
    {
        Enemy chosenEnemy = null;
        foreach (Enemy e in avEnemies)
        {
            if(!e.IsAlive() && e.GetEnemyType() == enemyType)
            {
                chosenEnemy = e;
                break;
            }    
        }

        if(chosenEnemy != null)
        {
            avEnemies.Remove(chosenEnemy);
            return chosenEnemy;
        }      

        foreach (Enemy e in enemyPrefabs)
        {
            if(e.GetEnemyType() == enemyType)
                return SpawnNewEnemy(e);
        }

        return null;
    }

    private Enemy SpawnNewEnemy(Enemy enemyPrefab)
    {
        Enemy newEnemy = Instantiate(enemyPrefab, new Vector3(0f, 0f, -30f), Quaternion.Euler(0f, 180f, 0f), transform).GetComponent<Enemy>();
        newEnemy.enemiesManager = this;
        newEnemy.scoreManager = _scoreManager;
        spawnedEnemies.Add(newEnemy);
        newEnemy.gameObject.SetActive(false);
        return newEnemy;
    }

    public void AddToAvailableEnemies(Enemy e)
    {
        avEnemies.Add(e);
    }
}
