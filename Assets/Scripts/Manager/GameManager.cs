using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject UI_Inventory;
    [SerializeField] GameObject UI_Input;
    [SerializeField] GameObject UI_Store;
    [SerializeField] TMP_InputField txt_inputName;
    [SerializeField] TMP_Text txt_playerName;
    [SerializeField] PlayerController playerController;

    public static bool isStartGame = false;
    public static bool isStore = false;

    void Start()
    {
        UI_Inventory.SetActive(false);
        UI_Input.SetActive(true);
        UI_Store.SetActive(false);
        playerController = FindObjectOfType<PlayerController>();
    }
    void Update()
    {
        if (!isStartGame) return;
        if(Input.GetKeyDown(KeyCode.I))
        {
            ShowInvnetory();
        }
        
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            HideUI();
        }

        else if (isStore)
        {
            UI_Store.SetActive(true);
            UI_Inventory.SetActive(true);
        }

        else if (!isStore)
        {
            UI_Store.SetActive(false);
        }
    }

    void ShowInvnetory()
    {
        UI_Inventory.SetActive(!UI_Inventory.activeSelf);
    }

    void HideUI()
    {
        UI_Inventory.SetActive(false);
        UI_Store.SetActive(false);
        isStore = false;
    }

    public void SetPlayerName()
    {
        txt_playerName.text = txt_inputName.text;
        UI_Input.SetActive(false);
        isStartGame = true;
    }
}
