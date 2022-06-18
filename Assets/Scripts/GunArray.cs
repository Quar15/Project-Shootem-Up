using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunArray : MonoBehaviour
{
    public GameObject defaultGunArray;
    public Gun[] guns { get; set; }
    public GameObject currentGunArray { get; private set; }
    public Bullet arrayBullet
    {
        get { return guns[0].bullet; }
    }

    private bool _switched = false;

    void Start()
    {
        currentGunArray = Instantiate(defaultGunArray, transform);
        guns = GetComponentsInChildren<Gun>();
    }

    private void FixedUpdate()
    {
        _switched = false;
    }

    public void ChangeArrayBullet(Bullet newBullet)
    {
        foreach (Gun gun in guns)
        {
            gun.bullet = newBullet;
        }
    }

    public void ChangeArrayTo(GameObject newArray)
    {
        Bullet currentBulletType = guns[0].bullet;
        Destroy(currentGunArray);

        currentGunArray = Instantiate(newArray, transform);
        guns = currentGunArray.GetComponentsInChildren<Gun>();
        foreach (Gun g in guns)
        {
            g.bullet = currentBulletType;
        }

        _switched = true;
    }

    public void ChangeArrayTo(GameObject newArray, Bullet arrayBullet)
    {
        Destroy(currentGunArray);

        currentGunArray = Instantiate(newArray, transform);
        guns = currentGunArray.GetComponentsInChildren<Gun>();
        foreach (Gun g in guns)
        {
            g.bullet = arrayBullet;
        }

        _switched = true;
    }

    public void Shoot()
    {
        if (!_switched)
        {
            foreach (Gun gun in guns)
            {
                gun.Shoot();
            }
        }
    }
}
