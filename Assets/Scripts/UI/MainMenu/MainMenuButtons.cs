using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject _optionsPanel;
    [SerializeField] private GameObject _creditsPanel;
    [SerializeField] private ScreenFade screenFade;

    private void Awake() 
    {
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(gameObject.transform.GetChild(0).GetChild(0).gameObject);
    }

    public void LoadJoinScene()
    {
        StartCoroutine(screenFade.LoadLevel("JoinScene"));
    }

    public void OpenOptionsPanel()
    {
        _optionsPanel.SetActive(true);
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(_optionsPanel.transform.GetChild(0).gameObject);
    }

    public void CloseOptionsPanel()
    {
        _optionsPanel.SetActive(false);
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(gameObject.transform.GetChild(0).GetChild(0).gameObject);
    }

    public void OpenCreditsPanel()
    {
        _creditsPanel.SetActive(true);
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(_creditsPanel.transform.GetChild(0).gameObject);
    }

    public void CloseCreditsPanel()
    {
        _creditsPanel.SetActive(false);
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(gameObject.transform.GetChild(0).GetChild(0).gameObject);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
