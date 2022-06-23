using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float preciseSpeed = 7;
    public float normalSpeed = 14;
    public float flinchTime = 1;

    public float currentSpeed { get; set; }
    public float previousSpeed { get; set; }

    public HPSystem hpSystem { get; set; }
    public GunArray gunArray { get; set; }
    public List<Follower> followers { get; set; }

    bool _shouldFire;
    Vector3 _moveInput;
    EdgeLimiter _limiter;
    Animator _shipAnimator;
    SpriteRenderer _shipSprite;

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = normalSpeed;
        hpSystem = GetComponent<HPSystem>();
        gunArray = GetComponentInChildren<GunArray>();
        followers = new List<Follower>();

        _limiter = GetComponent<EdgeLimiter>();
        _shipAnimator = GetComponentInChildren<Animator>();
        _shipSprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_shouldFire)
        {
            gunArray.Shoot();
            foreach(Follower follower in followers)
            {
                follower.FollowerShoot();
            }
        }
        if (hpSystem.IsFlashing())
        {
            _shipSprite.enabled = !_shipSprite.enabled;
        }
        else
        {
            _shipSprite.enabled = true;
        }

        _shipAnimator.SetBool("dodge", hpSystem.IsDodging());
    }

    private void FixedUpdate()
    {
        Vector3 pos = transform.localPosition;

        pos += _moveInput * currentSpeed * Time.fixedDeltaTime;

        transform.localPosition = _limiter.Delimit(pos);
    }

    public void InitInput(PlayerInput playerInput)
    {
        playerInput.actions["Move"].performed += OnMove;
        playerInput.actions["Move"].canceled += OnMove;
        playerInput.actions["PreciseMove"].performed += OnPreciseMove;
        playerInput.actions["Shoot"].performed += OnShoot;
        playerInput.actions["Dodge"].performed += OnDodge;
    }


    void OnMove(InputAction.CallbackContext context)
    {
        Vector2 change = context.ReadValue<Vector2>();
        _moveInput.x = change.x;
        _moveInput.z = change.y;
    }

    void OnPreciseMove(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() > 0.5f)
        {
            previousSpeed = currentSpeed;
            currentSpeed = preciseSpeed;
        }
        else
        {
            currentSpeed = previousSpeed;
        }
    }

    void OnShoot(InputAction.CallbackContext context)
    {
        _shouldFire = context.ReadValue<float>() > 0.5f;
    }

    void OnDodge(InputAction.CallbackContext context)
    {
        if (!hpSystem.IsDodging())
        {
            hpSystem.StartDodge();
        }
    }
}
