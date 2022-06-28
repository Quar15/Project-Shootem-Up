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

    private void OnTriggerEnter(Collider other)
    {
        PowerUp powerUp = other.GetComponent<PowerUp>();

        if (powerUp != null)
        {
            _player.playerAudioSource.PlayOneShot(powerUp.powerupSound, 0.8f);
            powerUp.ApplyPowerUp(_player);
            Destroy(other.gameObject);
        }

    }
}
