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
    [SerializeField] private GameObject _shipModel;
    public GunArray gunArray { get; set; }
    public List<Follower> followers { get; set; }

    bool _shouldFire;
    Vector3 _moveInput;
    EdgeLimiter _limiter;
    Animator _shipAnimator;

    private PlayerInput _playerInput;

    public PauseMenu pauseMenu;
    public GameOverMenu gameOverMenu;

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = normalSpeed;
        hpSystem = GetComponent<HPSystem>();
        hpSystem.ResetHP();
        gunArray = GetComponentInChildren<GunArray>();
        followers = new List<Follower>();

        gameOverMenu.AddPlayer(this);

        _limiter = GetComponent<EdgeLimiter>();
        _shipAnimator = GetComponentInChildren<Animator>();

        hpSystem.Damage(0);
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
            _shipModel.SetActive(!_shipModel.activeSelf);
        }
        else
        {
            _shipModel.SetActive(true);
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
        if(playerInput == null)
        {
            Destroy(gameObject);
            return;
        }
            
        
        _playerInput = playerInput;
        playerInput.actions["Move"].performed += OnMove;
        playerInput.actions["Move"].canceled += OnMove;
        playerInput.actions["PreciseMove"].performed += OnPreciseMove;
        playerInput.actions["Shoot"].performed += OnShoot;
        playerInput.actions["Dodge"].performed += OnDodge;
        playerInput.actions["Pause"].performed += OnPaused;
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

    void OnPaused(InputAction.CallbackContext context)
    {
        pauseMenu.PauseSwitch();
    }

    public void Death()
    {
        gameOverMenu.CheckForEnd();
    }

    private void OnDestroy() 
    {
        if(_playerInput == null)
            return;
            
        _playerInput.actions["Move"].performed -= OnMove;
        _playerInput.actions["Move"].canceled -= OnMove;
        _playerInput.actions["PreciseMove"].performed -= OnPreciseMove;
        _playerInput.actions["Shoot"].performed -= OnShoot;
        _playerInput.actions["Dodge"].performed -= OnDodge;
        _playerInput.actions["Pause"].performed -= OnPaused;
    }
}
