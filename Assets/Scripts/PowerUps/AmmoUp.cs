using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoUp : PowerUp
{
    public Bullet[] bullets;

    public override void ApplyPowerUp(Player player)
    {
        int newBullet = 0;

        while (bullets[newBullet].Equals(player._guns[0].bullet))
        {
            newBullet =  Random.Range(0, bullets.Length);
        }

        foreach(Gun gun in player._guns)
        {
            gun.bullet = bullets[newBullet];
        }
    }
}
