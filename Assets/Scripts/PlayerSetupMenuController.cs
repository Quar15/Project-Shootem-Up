using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSetupMenuController : MonoBehaviour
{
    public int playerIndex;
    public PlayerConfigurationManager playerConfigurationManager;

    private PlayerInput _playerInput;

    private void OnReady(InputAction.CallbackContext context)
    {
        playerConfigurationManager.ReadyPlayer(playerIndex);
    }

    private void InitPlayerInput()
    {
        _playerInput.actions["Ready"].performed += OnReady;
    }

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        Invoke("InitPlayerInput", 1f);
    }
}
