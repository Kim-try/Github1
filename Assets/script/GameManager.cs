using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;
    public int[] ints = { -15, 15 };
    public int Money;
    public int CitizonGrade;
    public float MazeTimer;
    public bool Lighton = false;
    public SoundManager SoundManager;
    public int sound = 1;
    public GameObject Arrow;
    public GameObject MainScene;
    public GameObject GameOver;
    public bool isField = false;
    private void Awake()
    {
        SoundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        var objs = FindObjectsOfType<GameManager>();
        if (objs.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
        instance = this;
        PlayerPrefs.SetInt("Money",0);
        PlayerPrefs.SetInt("CitizonGrade",4);
        Money = PlayerPrefs.GetInt("Money");
        CitizonGrade = PlayerPrefs.GetInt("CitizonGrade");
    }
    private void Start()
    {
        MazeTimer = 600.0f;
        if (SceneManager.GetActiveScene().name == "MazeScene")
        {
            player.transform.position = new Vector3(ints[Random.Range(0, 2)], ints[Random.Range(0, 2)], 0);
        }
    }
    private void Update()
    {
        CitizonGrade = PlayerPrefs.GetInt("CitizonGrade");
        Money = PlayerPrefs.GetInt("Money");
        Start_End();
        if(SceneManager.GetActiveScene().name == "MazeScene")
        {
            StartCoroutine(LightOnOff());
        }
        GameESC();
    }
    public void Start_End()
    {
        if (Input.anyKeyDown && sound == 1)
        {
            SoundManager.SceneCheck();
            sound = 0;
        }
        if (SceneManager.GetActiveScene().name != "TownScene")
        {
            MazeTimer -= Time.deltaTime;
            if (SceneManager.GetActiveScene().name == "MazeScene")
            {
                int Maze = Mathf.FloorToInt(MazeTimer);
                if (Maze % 10 == 0)
                {
                    Lighton = true;
                }
                else Lighton = false;
            }
        }
        else MazeTimer = 600.0f;
    }
    public void GradeCheck()
    {
        if (Money < 0)
        {
            if(CitizonGrade<=4)
            {
                PlayerPrefs.SetInt("CitizonGrade", CitizonGrade - 1);
                PlayerPrefs.SetInt("Money", 0);
            }
            else if (CitizonGrade == 5)
            {
                Time.timeScale = 0f;
                GameOver.SetActive(true);
                SoundManager.audioSource.Stop();
            }
        }
    }
    IEnumerator LightOnOff()
    {
        if (SceneManager.GetActiveScene().name == "MazeScene")
        {
            if (Lighton)
            {
                GameObject.Find("Global Light 2D").GetComponent<Light2D>().intensity = 4.0f;
                if(MazeTimer < 300.0f)
                {
                    Arrow.gameObject.SetActive(true);
                }
                yield return new WaitForSeconds(3.0f);
                Lighton = false;
            }
            else 
            {
                GameObject.Find("Global Light 2D").GetComponent<Light2D>().intensity = 0.1f;
                Arrow.gameObject.SetActive(false);
            }
            yield return null;
        }
        else Lighton = false;
    }
    public void GameESC()
    {
        if (SceneManager.GetActiveScene().name == "TownScene")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 0f;
                MainScene.SetActive(true);
                MainScene.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Countinue";
            }
        }
    }
}
