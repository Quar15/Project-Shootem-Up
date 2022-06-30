using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : PowerUp
{
    public float speedMultiplier = 1.33f;
    public float speedMax = 25f;

    override public void ApplyPowerUp(Player player)
    {
        player.normalSpeed = Mathf.Clamp(player.normalSpeed * speedMultiplier, 0f, speedMax);

        if (player.currentSpeed == player.preciseSpeed)
        {
            player.previousSpeed = player.normalSpeed;
        } else
        {
            player.currentSpeed = player.normalSpeed;
        }
    }
}
