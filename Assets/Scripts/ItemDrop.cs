using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public int dropChancePercent = 5;

    public PowerUp[] dropList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RollItemDrop()
    {
        if (dropChancePercent > Random.Range(0, 100))
        {
            int toSpawn = Random.Range(0, dropList.Length);

            Instantiate(dropList[toSpawn].gameObject, transform.position, Quaternion.identity, PlayAreaManager.Instance.playArea);
        }
    }
}
