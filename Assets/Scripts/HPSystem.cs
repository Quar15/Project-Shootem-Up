using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPSystem : MonoBehaviour
{
    public string damageTag;

    public int health = 1;
    public int shield = 0;
    public float invisTime = 0;

    float _flashingTimeout = 0;
    bool _isDodging = false;

    ItemDrop _itemDrop;

    // Start is called before the first frame update
    void Start()
    {
        _itemDrop = GetComponent<ItemDrop>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_flashingTimeout > 0)
        {
            _flashingTimeout -= Time.deltaTime;
        }
    }

    public bool IsFlashing()
    {
        return _flashingTimeout > 0;
    }
    public bool IsDodging()
    {
        return _isDodging;
    }

    public bool IsShieldUp()
    {
        return shield > 0;
    }

    public bool Damage(int damageAmount = 1)
    {
        if (_flashingTimeout <= 0 && !_isDodging)
        {
            if (shield > 0)
            {
                shield = Mathf.Max(shield - damageAmount, 0);
                return false;
            }

            health -= damageAmount;

            if (health <= 0)
            {
                if (_itemDrop != null)
                {
                    _itemDrop.RollItemDrop();
                }

                Destroy(gameObject);
                return true;
            }

            _flashingTimeout = invisTime;
        }
        return false;
    }

    public void SetFlashing(float time = -1)
    {
        _flashingTimeout = time == -1 ? invisTime : time;
    }

    public void StartDodge()
    {
        _isDodging = true;
    }

    public void EndDodge()
    {
        _isDodging = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        DamageDealer dDealer = other.GetComponent<DamageDealer>();
        if (dDealer != null && dDealer.enemyTag == gameObject.tag)
        {
            bool destroyed = Damage(dDealer.damageDealt);

            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet != null)
            {
                // Bullet is pirecing and destroyed target - should continue 
                if (bullet.pierce && destroyed) return;

                // Bullet hit when object is flashing / dodging - should pass through
                if (_isDodging || (_flashingTimeout > 0 && _flashingTimeout < invisTime)) return;

                Destroy(other.gameObject);
            }
        }
    }
}
