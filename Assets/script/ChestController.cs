using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public ChestType chest_type;
    private GameObject Player;
    bool isPlayerClose = false;
    int Cost;
    public GameObject[] Prefabs;
    public SpriteRenderer sprites;
    public Sprite images0;
    public Sprite images1;
    public AudioSource chestaudio;

    public enum ChestType
    {
        Gold, Silver, Bronze
    }
    private void Awake()
    {
        chestaudio = GetComponent<AudioSource>();
        sprites = GetComponent<SpriteRenderer>();
        images0 = GetComponent<Sprite>();
        images1 = GetComponent<Sprite>();
        Player = GameObject.Find("Player").gameObject;
    }
    private void Update()
    {
        PlayerClose();
        ChestOpen();
        ChestTypecheck();
    }
    public void PlayerClose()
    {
        if(((Player.transform.position - transform.position).magnitude <= 1.0f)&& (Player.GetComponent<Player>().Crystal >= Cost))
        {
            isPlayerClose = true;
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            isPlayerClose = false;
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }
    public void ChestTypecheck()
    {
        switch (chest_type)
        {
            case ChestType.Gold:
                Cost = 3;
                break;
            case ChestType.Silver:
                Cost = 6;
                break;
            case ChestType.Bronze:
                Cost = 10;
                break;
        }
    }
    public void ChestOpen()
    {
        if (isPlayerClose)
        {
            if ((Player.GetComponent<Player>().Crystal >= Cost))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    chestaudio.Play();
                    sprites.sprite = images1;
                    Player.GetComponent<Player>().Crystal -= Cost;
                    transform.GetChild(1).gameObject.SetActive(false);
                    for (int i = 0; i < 6 - GameManager.instance.CitizonGrade; i++)
                    {
                        Instantiate(Prefabs[Random.Range(0, Prefabs.Length)], new Vector3(transform.position.x + Random.Range(-1.0f, 2.0f), transform.position.y + Random.Range(-1.0f, 2.0f), transform.position.z), Quaternion.identity);
                    }
                    Destroy(gameObject);
                }
            }
        }
    }
}
