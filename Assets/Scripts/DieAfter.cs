using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAfter : MonoBehaviour
{
    public float s;

    private void Awake()
    {
        Destroy(gameObject, s);
    }
}
