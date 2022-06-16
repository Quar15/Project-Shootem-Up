using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 direction;
    public float speed = 20;
    public int damage = 1;
    public bool pierce = false;

    Vector3 _velocity;
    EdgeLimiter _limiter;

    // Start is called before the first frame update
    void Start()
    {
        _velocity = direction * speed;
        _limiter = GetComponent<EdgeLimiter>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        Vector3 pos = transform.localPosition;

        pos += _velocity * Time.fixedDeltaTime;

        if (!_limiter.IsInside(pos))
        {
            Destroy(gameObject);
        }

        transform.localPosition = pos;
    }

}
