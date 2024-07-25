using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TextMeshProUGUI text2;

    private void Update()
    {
        text.text = ":" + Mathf.FloorToInt(Player.instance.Crystal).ToString();
        text2.text = ":" + Mathf.FloorToInt(PlayerPrefs.GetInt("Money")).ToString();
    }
}
