using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public int dropChancePercent = 5;

    public PowerUp[] dropList;

    static PowerUp previousDrop = null;

    public void RollItemDrop()
    {
        if (dropChancePercent > Random.Range(0, 100))
        {
            int toSpawn = Random.Range(0, dropList.Length);
            while (previousDrop != null && dropList[toSpawn].GetType().Equals(previousDrop.GetType()))
            {
                toSpawn = Random.Range(0, dropList.Length);
            }

            Instantiate(dropList[toSpawn].gameObject, transform.position, Quaternion.identity, PlayAreaManager.Instance.playArea);
            previousDrop = dropList[toSpawn];
        }
    }
}
