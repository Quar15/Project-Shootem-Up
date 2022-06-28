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

    public void InitPlayerInput()
    {
        _playerInput.actions["Ready"].performed += OnReady;
        Debug.Log("@INFO: PlayerInput initialized");
    }

    public void Init()
    {
        _playerInput = GetComponent<PlayerInput>();
        Invoke("InitPlayerInput", .1f); // Instant ready prevention
    }

    private void OnDestroy() 
    {
        _playerInput.actions["Ready"].performed -= OnReady;
    }
}
