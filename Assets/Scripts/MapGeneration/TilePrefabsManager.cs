using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePrefabsManager : MonoBehaviour
{
    public List<Transform> tilePrefabs;

    public void Init()
    {
        int i=1;
        foreach (Transform child in transform)
        {
            tilePrefabs.Add(child);
            child.GetComponent<Tile>().id = i;
            i++;
        }
    }
}
