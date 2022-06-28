using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HPSystem : MonoBehaviour
{
    public string damageTag;

    public int maxHealth = 1;
    private int health = 1;
    public int shield = 0;
    public float invisTime = 0;

    public GameObject shieldObject = null;
    public TextMeshProUGUI hpText;

    float _flashingTimeout = 0;
    bool _isDodging = false;

    ItemDrop _itemDrop;

    // Start is called before the first frame update
    void Start()
    {
        _itemDrop = GetComponent<ItemDrop>();
    }

    public void ResetHP()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (_flashingTimeout > 0)
        {
            _flashingTimeout -= Time.deltaTime;
        }
    }

    public bool IsAlive()
    {
        return health > 0;
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

    public void AddHP(int HPToAdd = 1)
    {
        if(IsAlive())
            health += HPToAdd;

        if(hpText != null)
            hpText.text = health.ToString();
    }

    public bool Damage(int damageAmount = 1)
    {
        if (_flashingTimeout <= 0 && !_isDodging)
        {
            if (shield > 0)
            {
                shield = Mathf.Max(shield - damageAmount, 0);
                if (shield < 1 && shieldObject != null)
                {
                    shieldObject.SetActive(false);
                }
                return false;
            }

            health = Mathf.Clamp(health - damageAmount, 0, 9999);

            if(hpText != null)
            {
                hpText.text = health.ToString();
            }

            if (health <= 0)
            {
                if (_itemDrop != null)
                {
                    _itemDrop.RollItemDrop();
                }

                gameObject.SetActive(false);
                if(gameObject.tag == "Enemy")
                    GetComponent<Enemy>().Death();
                else if(gameObject.tag == "Player")
                    GetComponent<Player>().Death();
                
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
