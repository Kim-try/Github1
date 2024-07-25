using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MainButton : MonoBehaviour
{
    public Button StartButton;
    public Button SettingButton;
    public Button QuitButton;

    private void Awake()
    {
        StartButton.onClick.AddListener(GameStart);
        SettingButton.onClick.AddListener(SettingControll);
        QuitButton.onClick.AddListener(QuitGame);
    }

    public void GameStart()
    {
        if(StartButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text == "Countinue")
        {
            StartButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Game Start";
        }
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
        gameObject.SetActive(false);
    }
    public void SettingControll()
    {
        //¼³Á¤ panel¶ç¿ì±â
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
    public void Restart()
    {
        PlayerPrefs.SetInt("Money", 0);
        PlayerPrefs.SetInt("CitizonGrade", 4);
        Player.instance.Crystal = 10;
        Player.instance.health = Player.instance.Maxhealth;
        Player.instance.isHide = false;
        Inventory.Instance.inven.RemoveRange(0, Inventory.Instance.inven.Count);
        for (int i = 0; i < Inventory.Instance.slots.Length; i++)
        {
            Inventory.Instance.slots[i].slotitemcount = 0;
            Inventory.Instance.SlotCheck();
        }
        Time.timeScale = 1f;
        gameObject.SetActive (false);
    }

}
