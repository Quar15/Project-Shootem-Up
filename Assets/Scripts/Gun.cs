using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Bullet bullet;
    public GameObject bulletParent;
    public string bulletTag;

    public bool autoFire = false;
    public bool piercing = false;
    public float initialDelay = 0;
    public float fireDelay = 0.3f;
    public float bulletSpeed = 30;


    float _cooldown;
    Vector3 _targetDirection;

    // Start is called before the first frame update
    void Start()
    {

    }

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
            GameObject go = Instantiate(bullet.gameObject, new Vector3(transform.position.x, transform.parent.position.y, transform.position.z), Quaternion.identity, bulletParent.transform);
            go.tag = bulletTag;

            Bullet goBullet = go.GetComponent<Bullet>();
            goBullet.direction = _targetDirection;
            goBullet.speed = bulletSpeed;
            goBullet.pierce = piercing;

            _cooldown = fireDelay;
        }
    }
}
