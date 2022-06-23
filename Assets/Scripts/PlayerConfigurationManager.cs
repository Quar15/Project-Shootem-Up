using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerConfiguration
{
    public PlayerConfiguration(PlayerInput pi)
    {
        playerIndex = pi.playerIndex;
        input = pi;
    }

    public PlayerInput input { get; private set; }
    public int playerIndex { get; private set; }
    public bool isReady { get; set; }
    public Material playerMaterial {get; set;}
}

public class PlayerConfigurationManager : MonoBehaviour
{
    private List<PlayerConfiguration> _playerConfigs;
    private PlayerJoinUIManager _playerJoinUIManager;

    // [SerializeField] private int maxPlayers = 2;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        _playerConfigs = new List<PlayerConfiguration>();
        _playerJoinUIManager =  GetComponent<PlayerJoinUIManager>();
    }

    public void SetPlayerColor(int playerIndex, Material mat)
    {
        _playerConfigs[playerIndex].playerMaterial = mat;
    }

    public void ReadyPlayer(int playerIndex)
    {
        _playerConfigs[playerIndex].isReady = true;
        _playerJoinUIManager.HandlePlayerReady(playerIndex);
        if(_playerConfigs.All(p => p.isReady == true))
        {
            SceneManager.LoadScene("GameScene"); // @TODO: Change scene name
        }
    }

    public void HandlePlayerJoin(PlayerInput pi)
    {
        Debug.Log("Player " + pi.playerIndex + " clicked something");
        
        if(!_playerConfigs.Any(p => p.playerIndex == pi.playerIndex))
        {
            pi.transform.SetParent(transform);
            _playerConfigs.Add(new PlayerConfiguration(pi));

            _playerJoinUIManager.HandlePlayerJoin(pi.playerIndex);

            PlayerSetupMenuController pc = pi.transform.GetComponent<PlayerSetupMenuController>();
            pc.playerIndex = pi.playerIndex;
            pc.playerConfigurationManager = this;
        }
    }
}
