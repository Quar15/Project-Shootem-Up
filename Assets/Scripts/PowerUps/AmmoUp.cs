using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoUp : PowerUp
{
    public Bullet[] bullets;

    public override void ApplyPowerUp(Player player)
    {
        int newBullet = 0;

        while (bullets[newBullet].name == player.gunArray.arrayBullet.name)
        {
            newBullet =  Random.Range(0, bullets.Length);
        }

        foreach(Gun gun in player.gunArray.guns)
        {
            gun.bullet = bullets[newBullet];
        }
        foreach(Follower option in player.followers)
        {
            option.followerArray.ChangeArrayBullet(bullets[newBullet]);
        }
    }
}
