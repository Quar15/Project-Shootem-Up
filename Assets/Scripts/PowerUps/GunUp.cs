using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunUp : PowerUp
{
    public GameObject[] gunArrayChoices;

    public override void ApplyPowerUp(Player player)
    {
        GunArray gunArray = player.gunArray;

        int newArrayIndex = Random.Range(0, gunArrayChoices.Length);
        while (gunArrayChoices[newArrayIndex].name == gunArray.name)
        {
            newArrayIndex = Random.Range(0, gunArrayChoices.Length);
        }

        gunArray.ChangeArrayTo(gunArrayChoices[newArrayIndex]);
        foreach(Follower option in player.followers)
        {
            option.followerArray.ChangeArrayTo(gunArrayChoices[newArrayIndex]);
        }
    }
}
