using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dontdes : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject lefttimetext;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        var objs = FindObjectsOfType<Dontdes>();
        if (objs.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "TownScene")
        {
            lefttimetext.GetComponent<TextMeshProUGUI>().text = "남은 시간" + " : " + Mathf.FloorToInt(GameManager.instance.MazeTimer).ToString();
        }
        else lefttimetext.GetComponent<TextMeshProUGUI>().text = "";
    }
    public void PanelSound()
    {
        audioSource.Play();
    }
}
