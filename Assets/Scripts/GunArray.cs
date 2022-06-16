using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunArray : MonoBehaviour
{
    public GameObject defaultGunArray;
    public Gun[] arrayGuns { get; set; }

    public GameObject _currentGunArray { get; private set; }

    private bool _switched = false;
    // Start is called before the first frame update
    void Start()
    {
        _currentGunArray = Instantiate(defaultGunArray, transform);
        arrayGuns = GetComponentsInChildren<Gun>();
    }

    private void FixedUpdate()
    {
        _switched = false;
    }

    public void ChangeArrayTo(GameObject newArray)
    {
        Bullet currentBulletType = arrayGuns[0].bullet;
        Destroy(_currentGunArray);

        _currentGunArray = Instantiate(newArray, transform);
        arrayGuns = _currentGunArray.GetComponentsInChildren<Gun>();
        foreach (Gun g in arrayGuns)
        {
            g.bullet = currentBulletType;
        }

        _switched = true;
    }

    public void Shoot()
    {
        if (!_switched)
        {
            foreach (Gun gun in arrayGuns)
            {
                gun.Shoot();
            }
        }
    }
}
