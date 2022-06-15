using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : PowerUp
{
    public float speedBoost = 22;

    override public void ApplyPowerUp(Player player)
    {
        player.normalSpeed = speedBoost;

        if (player._currentSpeed == player.preciseSpeed)
        {
            player._previousSpeed = speedBoost;
        } else
        {
            player._currentSpeed = speedBoost;
        }
    }
}
