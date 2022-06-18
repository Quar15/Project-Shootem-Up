using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUp : PowerUp
{
    public int upAmount = 1;

    public override void ApplyPowerUp(Player player)
    {
        player.hpSystem.health += upAmount;
    }
}
