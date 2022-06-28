using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public BulletType bulletType;
    
    [SerializeField] private AudioSource _gunAudioSource;

    public bool autoFire = false;
    public float initialDelay = 0;
    public float fireDelay = 0.3f;
    public float bulletSpeedMultiplier = 1;

    private BulletsManager _bm;

    float _cooldown;
    Vector3 _targetDirection;
    Quaternion _gunRotation;
    private void Start()
    {
        _bm = FindObjectOfType<BulletsManager>();
        _gunRotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);

        _cooldown = initialDelay;
    }

    // Update is called once per frame
    void Update()
    {
        _targetDirection = (transform.rotation * Vector3.forward).normalized;
        _gunRotation.y = transform.rotation.y;

        if (_cooldown > 0)
        {
            _cooldown -= Time.deltaTime;
        }
        if (autoFire)
        {
            Shoot();
        }
    }

    public void InitAutofire()
    {
        if (!autoFire)
        {
            _cooldown = initialDelay;
            autoFire = true;
        }
    }

    public void Shoot()
    {
        if (_cooldown <= 0)
        {
            Bullet newBullet = _bm.GetBullet(bulletType);
            newBullet.InitBulletMovement(
                _targetDirection,
                transform.position,
                _gunRotation,
                bulletSpeedMultiplier
            );

            if (_gunAudioSource != null)
            {
                _gunAudioSource.pitch = Random.Range(0.8f, 1.2f);
                _gunAudioSource.PlayOneShot(newBullet.bulletSound);
            }
            _cooldown = fireDelay * newBullet.delayMultiplier;
        }
    }

}
