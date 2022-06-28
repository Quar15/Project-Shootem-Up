using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAreaManager : MonoBehaviour
{
    public static PlayAreaManager Instance { get; private set; }

    public Transform playArea;
    public EdgeLimiter screenEdgeLimiter;

    private void Awake()
    {
        screenEdgeLimiter = GetComponent<EdgeLimiter>();

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
