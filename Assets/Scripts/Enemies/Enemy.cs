using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType {Small, Medium, Big};

public class Enemy : MonoBehaviour
{
    [Tooltip("After x[s] deal 9999 DMG to Enemy.\n(Skips values <= 0)")]
    [SerializeField]  private float _forceKillTime = 20f;
    [SerializeField] private EnemyType _type;
    public EnemyType GetEnemyType() { return _type; }

    private HPSystem _hpSystem;
    private EnemyMovement _enemyMovement;
    public EnemiesManager enemiesManager;

    private void Awake() 
    {
        _hpSystem = GetComponent<HPSystem>();
        _enemyMovement = GetComponent<EnemyMovement>();
    }

    public void InitEnemyMovement(MovementType movementType, Transform[] routePoints, float speed)
    {
        _enemyMovement.SetRoutePoints(routePoints);
        _enemyMovement.SetSpeed(speed);
        _enemyMovement.movementType = movementType;
        gameObject.SetActive(true);
    }

    public void ResetEnemy()
    {
        _enemyMovement.InitMovementSystem();
        _hpSystem.ResetHP();
        if(_forceKillTime > 0)
            Invoke("ForceKill", _forceKillTime);
    }

    public bool IsAlive()
    {
        return _hpSystem.IsAlive();
    }

    private void ForceKill()
    {
        _hpSystem.Damage(9999);
    }

    public void Death()
    {
        enemiesManager.AddToAvailableEnemies(this);
    }

    public void ChangePosition(Transform newTransform)
    {
        transform.position = newTransform.position;
        transform.rotation = newTransform.rotation;
    }
}
