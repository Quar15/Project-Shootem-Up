using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunArray : MonoBehaviour
{
    public GameObject defaultGunArray;
    public Gun[] guns { get; set; }
    public GameObject currentGunArray { get; private set; }
    public BulletType bulletType
    {
        get { return guns[0].bulletType; }
    }
    private BulletsManager _bm;
    public BulletsManager GetBulletsManager() { return _bm; }

    private bool _switched = false;

    void Start()
    {
        _bm = FindObjectOfType<BulletsManager>();
        
        if (currentGunArray == null)
        {
            currentGunArray = Instantiate(defaultGunArray, transform);
        }

        guns = GetComponentsInChildren<Gun>();
    }

    private void FixedUpdate()
    {
        _switched = false;
    }

    public void ChangeBulletType(BulletType bulletType)
    {
        foreach (Gun gun in guns)
        {
            gun.bulletType = bulletType;
        }
    }

    public void ChangeArrayTo(GameObject newArray)
    {
        ChangeArrayTo(newArray, bulletType);
    }

    public void ChangeArrayTo(GameObject newArray, BulletType bulletType)
    {
        Destroy(currentGunArray);

        currentGunArray = Instantiate(newArray, transform);
        guns = currentGunArray.GetComponentsInChildren<Gun>();
        foreach (Gun g in guns)
        {
            g.bulletType = bulletType;
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
