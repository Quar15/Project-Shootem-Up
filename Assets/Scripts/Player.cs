using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float preciseSpeed = 7;
    public float normalSpeed = 14;
    public float flinchTime = 1;

    float _currentSpeed;
    float _previousSpeed;
    bool _shouldFire;
    Vector3 _moveInput;
    EdgeLimiter _limiter;
    HPSystem _hpSystem;
    Animator _shipAnimator;
    Gun[] _guns;
    SpriteRenderer _shipSprite;

    // Start is called before the first frame update
    void Start()
    {
        _currentSpeed = normalSpeed;
        _limiter = GetComponent<EdgeLimiter>();
        _hpSystem = GetComponent<HPSystem>();
        _shipAnimator = GetComponent<Animator>();
        _guns = GetComponentsInChildren<Gun>();
        _shipSprite = GetComponentInChildren<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (_shouldFire)
        {
            foreach (Gun gun in _guns)
            {
                gun.Shoot();
            }
        }
        if (_hpSystem.IsFlashing())
        {
            _shipSprite.enabled = !_shipSprite.enabled;
        }
        else
        {
            _shipSprite.enabled = true;
        }

        _shipAnimator.SetBool("dodge", _hpSystem.IsDodging());
    }

    private void FixedUpdate()
    {
        Vector3 pos = transform.localPosition;

        pos += _moveInput * _currentSpeed * Time.fixedDeltaTime;

        transform.localPosition = _limiter.Delimit(pos);
    }


    void OnMove(InputValue value)
    {
        Vector2 change = value.Get<Vector2>();
        _moveInput.x = change.x;
        _moveInput.z = change.y;
    }

    void OnPreciseMove(InputValue value)
    {
        if (value.Get<float>() > 0.5f)
        {
            _previousSpeed = _currentSpeed;
            _currentSpeed = preciseSpeed;
        }
        else
        {
            _currentSpeed = _previousSpeed;
        }
    }

    void OnShoot(InputValue value)
    {
        _shouldFire = value.Get<float>() > 0.5f;
    }

    void OnDodge(InputValue value)
    {
        if (!_hpSystem.IsDodging())
        {
            _hpSystem.StartDodge();
        }
    }
}
