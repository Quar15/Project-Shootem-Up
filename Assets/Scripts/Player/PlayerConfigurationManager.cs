using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

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

    [SerializeField] private GameObject[] playerPrefabs;
    [SerializeField] private Vector3[] spawnPositions;
    [SerializeField] private ScreenFade screenFade;

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

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 1f;
        if(scene.name == "GameplayScene")
        {
            GetComponent<PlayerInputManager>().DisableJoining();
            Debug.Log("@INFO: Spawning " + _playerConfigs.Count + " players");
            for(int i=0; i < _playerConfigs.Count; i++)
            {
                var player = Instantiate(playerPrefabs[i], spawnPositions[i], Quaternion.identity, PlayAreaManager.Instance.playArea.transform);
                _playerConfigs[i].input.SwitchCurrentActionMap("Gameplay");
                
                Player tempPlayer = player.GetComponent<Player>();
                tempPlayer.InitInput(_playerConfigs[i].input);
                GameObject canvasGO = GameObject.FindWithTag("Canvas");
                tempPlayer.pauseMenu = canvasGO.GetComponent<PauseMenu>();
                tempPlayer.gameOverMenu = canvasGO.GetComponent<GameOverMenu>();
                tempPlayer.pauseMenu.playersLives[i].SetActive(true);
                player.GetComponent<HPSystem>().hpText = tempPlayer.pauseMenu.playersLives[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            }
        }

        else if(scene.name == "MainMenuScene")
        {
            Destroy(gameObject);
        }
    }

    public void ReadyPlayer(int playerIndex)
    {
        _playerConfigs[playerIndex].isReady = true;
        _playerJoinUIManager.HandlePlayerReady(playerIndex);
        if(_playerConfigs.All(p => p.isReady == true))
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            StartCoroutine(screenFade.LoadLevel("GameplayScene"));
            // SceneManager.LoadScene("GameplayScene", LoadSceneMode.Single);
        }
    }

    public void HandlePlayerJoin(PlayerInput pi)
    {
        Debug.Log("Player " + (pi.playerIndex + 1) + " clicked something");
        
        if(!_playerConfigs.Any(p => p.playerIndex == pi.playerIndex))
        {
            pi.transform.SetParent(transform);
            PlayerConfiguration newPC = new PlayerConfiguration(pi);
            newPC.input.SwitchCurrentActionMap("ReadyUp");
            _playerConfigs.Add(newPC);

            _playerJoinUIManager.HandlePlayerJoin(pi.playerIndex);

            PlayerSetupMenuController pc = pi.transform.GetComponent<PlayerSetupMenuController>();
            pc.playerIndex = pi.playerIndex;
            pc.playerConfigurationManager = this;
            pc.Init();
        }
    }

    public void HandlePlayerLeft(PlayerInput pi)
    {
        for (int i=0; i < _playerConfigs.Count; i++)
        {
            if(_playerConfigs[i].input == pi)
            {
                _playerConfigs.RemoveAt(i);
                break;
            }
        }
    }
}
