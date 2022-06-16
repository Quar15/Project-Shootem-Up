using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : PowerUp
{
    public float speedMultiplier = 1.33f;

    override public void ApplyPowerUp(Player player)
    {
        player.normalSpeed *= speedMultiplier;

        if (player._currentSpeed == player.preciseSpeed)
        {
            player._previousSpeed = player.normalSpeed;
        } else
        {
            player._currentSpeed = player.normalSpeed;
        }
    }
}
