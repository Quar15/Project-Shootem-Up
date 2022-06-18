using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    [Header("Common")]
    public float moveSpeed = 3;

    EdgeLimiter _limiter;

    // Start is called before the first frame update
    void Start()
    {
        _limiter = GetComponent<EdgeLimiter>();
    }

    void Update()
    {
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        if (!_limiter.IsInside(transform.localPosition))
        {
            Destroy(gameObject);
        }
    }

    public abstract void ApplyPowerUp(Player player);
}
