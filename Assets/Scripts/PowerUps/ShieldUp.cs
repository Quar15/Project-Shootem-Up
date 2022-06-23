using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldUp : PowerUp
{
    public int shieldUpAmount = 3;

    public override void ApplyPowerUp(Player player)
    {
        player.hpSystem.shield = shieldUpAmount;
        player.hpSystem.shieldObject.SetActive(true);
    }
}
