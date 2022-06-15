using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    Player _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        PowerUp powerUp = other.GetComponent<PowerUp>();

        if (powerUp != null)
        {
            powerUp.ApplyPowerUp(_player);
            Destroy(other.gameObject);
        }

    }
}
