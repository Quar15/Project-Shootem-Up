using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject _optionsPanel;


    public void LoadJoinScene()
    {
        SceneManager.LoadScene("JoinScene");
    }

    public void OpenOptionsPanel()
    {
        _optionsPanel.SetActive(true);
    }

    public void CloseOptionsPanel()
    {
        _optionsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
