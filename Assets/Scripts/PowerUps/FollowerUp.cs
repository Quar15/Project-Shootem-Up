using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerUp : PowerUp
{
    public Follower followerType;
    public int maxFollowers = 4;

    public override void ApplyPowerUp(Player player)
    {
        if (player.followers.Count < maxFollowers)
        {
            GameObject followerTarget = player.followers.Count == 0 ? player.gameObject : player.followers[player.followers.Count - 1].gameObject;

            Follower newFollower = Follower.SpawnFollower(followerType, followerTarget, player.transform, player.gunArray);

            player.followers.Add(newFollower);
        }
    }
}
