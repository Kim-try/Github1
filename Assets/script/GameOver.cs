using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameOver : MonoBehaviour
{
    public Button totownbutton;
    private void Awake()
    {
        totownbutton.onClick.AddListener(ToTown);
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void ToTown()
    {
        if (Player.instance.isDead)
        {
            Player.instance.health = Player.instance.Maxhealth;
            Inventory.Instance.inven.RemoveRange(0, Inventory.Instance.inven.Count);
            for (int i = 0; i < Inventory.Instance.slots.Length; i++)
            {
                Inventory.Instance.slots[i].slotitemcount = 0;
                Inventory.Instance.SlotCheck();
            }
            PlayerPrefs.SetInt("Money", GameManager.instance.Money- (5-GameManager.instance.CitizonGrade) * 500);
            SceneManager.LoadScene("TownScene");
            GameManager.instance.sound = 1;
            Player.instance.transform.position = Vector3.zero;
            Time.timeScale = 1;
        }
        else return;
    }
}
