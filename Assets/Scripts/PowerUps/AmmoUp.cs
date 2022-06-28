using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoUp : PowerUp
{
    public Bullet[] bulletChoices;

    public override void ApplyPowerUp(Player player)
    {
        int newBullet = 0;

        while (bulletChoices[newBullet].GetBulletType() == player.gunArray.bulletType)
        {
            newBullet = Random.Range(0, bulletChoices.Length);
        }

        BulletType newType = bulletChoices[newBullet].GetBulletType();
        foreach (Gun gun in player.gunArray.guns)
        {
            gun.bulletType = newType;
        }
        foreach (Follower option in player.followers)
        {
            option.followerArray.ChangeBulletType(newType);
        }
    }
}
