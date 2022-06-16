using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunUp : PowerUp
{
    public int currentIndex = -1;
    public GameObject[] gunArrayChoices;

    public override void ApplyPowerUp(Player player)
    {
        GunArray gunArray = player._gunArray;

        int newArrayIndex = Random.Range(0, gunArrayChoices.Length);
        while (newArrayIndex == currentIndex)
        {
            newArrayIndex = Random.Range(0, gunArrayChoices.Length);
        }

        gunArray.ChangeArrayTo(gunArrayChoices[newArrayIndex]);
        currentIndex = newArrayIndex;
    }
}
