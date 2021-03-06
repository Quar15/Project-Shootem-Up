using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _optionsPanel;
    public GameObject[] playersLives;

    public void Continue()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
        _optionsPanel.SetActive(false);
        _pausePanel.SetActive(false);
    }

    public void PauseSwitch()
    {
        if(Time.timeScale == 0)
        {
            Continue();
            return;
        }

        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(_pausePanel.gameObject.transform.GetChild(0).GetChild(0).gameObject);
        Time.timeScale = 0f;
        Cursor.visible = true;
        _pausePanel.SetActive(true);
        _optionsPanel.SetActive(false);
    }

    public void OpenOptions()
    {
        _optionsPanel.SetActive(true);
    }

    public void ResetTextPosition(RectTransform textTransform)
    {
        StartCoroutine(ResetTextPositionCoroutine(textTransform));
    }

    public void OnPointerUpResetText(RectTransform textTransform)
    {
        StopAllCoroutines();
        textTransform.localPosition = new Vector3(0f, 10f, 0f);
    }

    private IEnumerator ResetTextPositionCoroutine(RectTransform textTransform)
    {
        yield return new WaitForSecondsRealtime(.1f);
        textTransform.localPosition = new Vector3(0f, 10f, 0f);
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }

    private void MoveRectTransform(RectTransform rTransform, Vector3 moveVector)
    {
        rTransform.localPosition += moveVector;
    }

    public void OnSelect(RectTransform textTransform)
    {
        MoveRectTransform(textTransform, new Vector3(0f, -15f, 0f));
    }

    public void OnDeselect(RectTransform textTransform)
    {
        MoveRectTransform(textTransform, new Vector3(0f, 15f, 0f));
    }
}
