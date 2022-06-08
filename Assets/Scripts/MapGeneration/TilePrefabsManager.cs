using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePrefabsManager : MonoBehaviour
{
    public List<Transform> tilePrefabs;
    public bool debugText;

    public void Init()
    {
        int i=1;
        foreach (Transform child in transform)
        {
            tilePrefabs.Add(child);
            Tile t = child.GetComponent<Tile>();
            t.id = i;
            t.SetActiveDebugText(debugText);
            i++;
        }
    }
}
