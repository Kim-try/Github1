using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
public class StairController : MonoBehaviour
{
    public float StairTimer=0;
    public SpriteRenderer stairsprite;
    public Sprite sprite1;
    public Sprite sprite2;
    public bool IsPlayer;
    public GameObject npcpanel;
    private void OnEnable()
    {
        stairsprite = GetComponent<SpriteRenderer>();
        npcpanel = GameObject.Find("Canvas").transform.GetChild(2).gameObject;
    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "MazeScene")
        {
            transform.GetChild(0).gameObject.GetComponent<Light2D>().intensity = 0.2f;
        }
        else return;
        stairsprite.sprite = sprite1;
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "MazeScene")
        {
            StairAlarm();
            if (stairsprite.sprite == sprite2)
            {
                PlayerPrefs.SetInt("Money", GameManager.instance.Money + Mathf.FloorToInt(Player.instance.Crystal) - 50 * (10 - GameManager.instance.CitizonGrade) * (5 - GameManager.instance.CitizonGrade));
                SceneManager.LoadScene("TownScene");
                GameManager.instance.sound = 1;
                Player.instance.transform.position = Vector3.zero;
            }
        }
        else if(SceneManager.GetActiveScene().name == "FieldScene" && GameManager.instance.isField==true)
        {
            npcpanel.SetActive(true);
            npcpanel.transform.GetChild(0).gameObject.SetActive(true);
            npcpanel.GetComponent<NPCPanel>().textms.text = "±§∏∆¿ª ±˙º≠ ºˆ¡§¿ª »πµÊ«œººø‰";
            GameManager.instance.isField = false;
        }
    }
    public void StairAlarm()
    {
        if(StairTimer >=20.0f)
        {
            stairsprite.sprite = sprite2;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (SceneManager.GetActiveScene().name == "FieldScene")
            {
                if (Inventory.Instance.inven.Count == 0)
                {
                    SceneManager.LoadScene("MazeScene");
                    GameManager.instance.sound = 1;
                    npcpanel.SetActive(true);
                    npcpanel.transform.GetChild(0).gameObject.SetActive(true);
                    npcpanel.GetComponent<NPCPanel>().textms.text = "∞Ë¥‹¿ª √£æ∆ ≈ª√‚«œººø‰";
                }
                else
                {
                    npcpanel.SetActive(true);
                    npcpanel.transform.GetChild(0).gameObject.SetActive(true);
                    npcpanel.GetComponent<NPCPanel>().textms.text = "π∞∞«¿∫ µŒ∞Ì∞°";
                }
            }
            else return;
        }        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (SceneManager.GetActiveScene().name == "MazeScene")
            {
                transform.GetChild(0).gameObject.GetComponent<Light2D>().intensity = 10;
                transform.GetChild(0).gameObject.GetComponent<Light2D>().pointLightOuterRadius = 2;
                GameManager.instance.Lighton = true;
                StairTimer += Time.deltaTime;
            }
            else return;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(SceneManager.GetActiveScene().name == "MazeScene")
            {
                transform.GetChild(0).gameObject.GetComponent<Light2D>().intensity = 0.2f;
                GameManager.instance.Lighton = false;
                transform.GetChild(0).gameObject.GetComponent<Light2D>().pointLightOuterRadius = 1;
            }
        }
        else return;
    }
}
