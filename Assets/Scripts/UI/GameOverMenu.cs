using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    private List<Player> _players;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private ScoreManager _scoreManager;

    private void Awake()
    {
        _players = new List<Player>();
    }

    public void AddPlayer(Player p)
    {
        _players.Add(p);
    }

    public void CheckForEnd()
    {
        foreach (Player p in _players)
        {
            if(p.hpSystem.IsAlive())
                return;
        }

        Time.timeScale = 0f;
        Cursor.visible = true;
        _scoreManager.SaveScore();
        _gameOverPanel.SetActive(true);
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(_gameOverPanel.gameObject.transform.GetChild(0).GetChild(0).gameObject);
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
    }
}
