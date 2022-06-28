using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Bullet bullet;

    [SerializeField] private AudioSource _gunAudioSource;

    public bool autoFire = false;
    public float initialDelay = 0;
    public float fireDelay = 0.3f;
    public float bulletSpeedMultiplier = 1;


    float _cooldown;
    Vector3 _targetDirection;

    // Update is called once per frame
    void Update()
    {
        _targetDirection = (transform.localRotation * Vector3.forward).normalized;

        if (_cooldown > 0)
        {
            _cooldown -= Time.deltaTime;
        }
        if (initialDelay > 0)
        {
            initialDelay -= Time.deltaTime;
        }
        if (autoFire)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        if (_cooldown <= 0 && initialDelay <= 0)
        {
            GameObject go = Instantiate(
                bullet.gameObject,
                new Vector3(transform.position.x, transform.parent.position.y, transform.position.z),
                new Quaternion(0,transform.rotation.y,0,transform.rotation.w),
                //transform.rotation,
                PlayAreaManager.Instance.playArea
            );

            Bullet goBullet = go.GetComponent<Bullet>();
            goBullet.direction = _targetDirection;
            goBullet.speed *= bulletSpeedMultiplier;

            _gunAudioSource.pitch = Random.Range(0.8f, 1.2f);
            _gunAudioSource.Play();

            _cooldown = fireDelay;
        }
    }

}
