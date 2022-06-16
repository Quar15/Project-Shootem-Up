using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float preciseSpeed = 7;
    public float normalSpeed = 14;
    public float flinchTime = 1;

    public float _currentSpeed { get; set; }
    public float _previousSpeed { get; set; }

    public HPSystem _hpSystem { get; set; }
    public GunArray _gunArray { get; set; }

    bool _shouldFire;
    Vector3 _moveInput;
    EdgeLimiter _limiter;
    Animator _shipAnimator;
    SpriteRenderer _shipSprite;

    // Start is called before the first frame update
    void Start()
    {
        _currentSpeed = normalSpeed;
        _limiter = GetComponent<EdgeLimiter>();
        _hpSystem = GetComponent<HPSystem>();
        _shipAnimator = GetComponent<Animator>();
        _gunArray = GetComponentInChildren<GunArray>();
        _shipSprite = GetComponentInChildren<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (_shouldFire)
        {
            _gunArray.Shoot();
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
