using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerJoinUIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] playerPanels;

    public void HandlePlayerJoin(int playerIndex)
    {
        if(playerPanels.ElementAtOrDefault(playerIndex) == null)
        {
            Debug.LogWarning("@WARNING: playerIndex out of range (array: playerPanels)");
            return;
        }

        Debug.Log("Player " + (playerIndex + 1) + " Joined");
        playerPanels[playerIndex].transform.GetChild(1).gameObject.SetActive(true);
        playerPanels[playerIndex].transform.GetChild(2).gameObject.SetActive(false);
    }

    public void HandlePlayerReady(int playerIndex)
    {
        if(playerPanels.ElementAtOrDefault(playerIndex) == null)
        {
            Debug.LogWarning("@WARNING: playerIndex out of range (array: playerPanels)");
            return;
        }

        playerPanels[playerIndex].transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
        playerPanels[playerIndex].transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
    }
}
