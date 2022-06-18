using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : PowerUp
{
    public float speedMultiplier = 1.33f;

    override public void ApplyPowerUp(Player player)
    {
        player.normalSpeed *= speedMultiplier;

        if (player.currentSpeed == player.preciseSpeed)
        {
            player.previousSpeed = player.normalSpeed;
        } else
        {
            player.currentSpeed = player.normalSpeed;
        }
    }
}
