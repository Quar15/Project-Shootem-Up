using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoUp : PowerUp
{
    public Bullet[] bullets;

    public override void ApplyPowerUp(Player player)
    {
        int newBullet = 0;

        Gun[] guns = player._gunArray.arrayGuns;

        while (bullets[newBullet].GetHashCode().Equals(guns[0].bullet.GetHashCode()))
        {
            newBullet =  Random.Range(0, bullets.Length);
        }

        foreach(Gun gun in guns)
        {
            gun.bullet = bullets[newBullet];
        }
    }
}
