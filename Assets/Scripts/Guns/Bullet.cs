using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType { Normal, Enemy, Laser, Missile }

public class Bullet : MonoBehaviour
{
    public AudioClip bulletSound;

    public float delayMultiplier = 1;
    public float speed = 20;
    public bool pierce = false;

    [SerializeField] private BulletType _type;
    public BulletType GetBulletType() { return _type; }

    public BulletsManager bulletsManager;
    private Vector3 _velocity;
    private EdgeLimiter _limiter;


    // Start is called before the first frame update
    void Start()
    {
        _limiter = GetComponent<EdgeLimiter>() ?? PlayAreaManager.Instance.screenEdgeLimiter;
    }

    private void Update()
    {
        Vector3 pos = transform.localPosition;

        pos += _velocity * Time.deltaTime;

        if (!_limiter.IsInside(pos))
        {
            DisableBullet();
        }

        transform.localPosition = pos;
    }

    public void InitBulletMovement(Vector3 direction, Vector3 startingPosition, Quaternion bulletRotation, float speedMultiplier)
    {
        _velocity = direction * speed * speedMultiplier;
        transform.position = startingPosition;
        transform.rotation = bulletRotation;
        gameObject.SetActive(true);
    }

    public bool IsOnScreen()
    {
        return _limiter.IsInside(transform.localPosition);
    }

    public void DisableBullet()
    {
        Vector3 pos = transform.localPosition;
        pos.z = -30f;

        transform.localPosition = pos;
        bulletsManager.AddToOffScreenBullets(this);
        gameObject.SetActive(false);
    }

}
