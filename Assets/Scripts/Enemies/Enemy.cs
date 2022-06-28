using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType {Small, Medium, Big};

public class Enemy : MonoBehaviour
{
    [Tooltip("After x[s] deal 9999 DMG to Enemy.\n(Skips values <= 0)")]
    [SerializeField]  private float _forceKillTime = 20f;
    [SerializeField] private EnemyType _type;
    [SerializeField] private int _pointsForKill;
    public EnemyType GetEnemyType() { return _type; }

    private HPSystem _hpSystem;
    private EnemyMovement _enemyMovement;
    private Gun[] _enemyGuns;
    public EnemiesManager enemiesManager;
    public ScoreManager scoreManager;

    private bool _shouldAddPoints = true;

    private void Awake() 
    {
        _hpSystem = GetComponent<HPSystem>();
        _enemyMovement = GetComponent<EnemyMovement>();
        _enemyGuns = GetComponentsInChildren<Gun>();
    }

    private void Update()
    {
        if (PlayAreaManager.Instance.screenEdgeLimiter.IsInside(transform.localPosition))
        {
            foreach (Gun g in _enemyGuns)
            {
                g.InitAutofire();
            }
        }
    }

    public void InitEnemyMovement(MovementType movementType, Transform[] routePoints, float speed)
    {
        _enemyMovement.SetRoutePoints(routePoints);
        _enemyMovement.SetSpeed(speed);
        _enemyMovement.movementType = movementType;
        gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        CancelInvoke();
        if(_forceKillTime > 0)
        {
            Invoke("ForceKill", _forceKillTime);
        }
    }

    public void ResetEnemy()
    {
        _enemyMovement.InitMovementSystem();
        _hpSystem.ResetHP();
        _shouldAddPoints = true;            
    }

    public bool IsAlive()
    {
        return _hpSystem.IsAlive();
    }

    private void ForceKill()
    {
        _shouldAddPoints = false;
        _hpSystem.Damage(9999);
    }

    public void Death()
    {
        if(_shouldAddPoints)
            scoreManager.AddScore(_pointsForKill);
        foreach(Gun g in _enemyGuns)
        {
            g.autoFire = false;
        }
        
        enemiesManager.AddToAvailableEnemies(this);
    }

    public void ChangePosition(Transform newTransform)
    {
        transform.position = newTransform.position;
        transform.rotation = newTransform.rotation;
    }
}
