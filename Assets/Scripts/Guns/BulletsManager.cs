using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsManager : MonoBehaviour
{

    [SerializeField] private Transform bulletsContainer;
    [SerializeField] private Bullet[] bulletPrefabs;
    private List<Bullet> spawnedBullets; // All bullets that are currently spawned
    private List<Bullet> offScreenBullets; // All bullets that went off screen

    private void Awake()
    {
        spawnedBullets = new List<Bullet>();
        offScreenBullets = new List<Bullet>();
    }

    public Bullet GetBullet(BulletType bulletType)
    {
        foreach (Bullet b in offScreenBullets)
        {
            if (b.GetBulletType() == bulletType)
            {
                offScreenBullets.Remove(b);
                return b;
            }

        }

        foreach (Bullet b in bulletPrefabs)
        {
            Debug.Log(string.Format("@INFO: Spawning bullet of type: {0}", bulletType));
            if (b.GetBulletType() == bulletType)
                return SpawnNewBullet(b);
        }

        return null;
    }

    private Bullet SpawnNewBullet(Bullet bulletPrefab)
    {
        Bullet newBullet = Instantiate(
                bulletPrefab,
                new Vector3(0f, 0f, -30f),
                Quaternion.identity,
                bulletsContainer
            )
            .GetComponent<Bullet>();

        newBullet.bulletsManager = this;
        newBullet.gameObject.SetActive(false);
        spawnedBullets.Add(newBullet);
        return newBullet;
    }

    public void AddToOffScreenBullets(Bullet b)
    {
        offScreenBullets.Add(b);
    }
}
