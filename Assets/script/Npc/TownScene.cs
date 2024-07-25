using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TownScene : Npc
{
    public static TownScene instance;
    public bool isChage = false;
    public GameManager GameManager;
    public int citizonGrade;
    private void Awake()
    {
        instance = this;
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Player = GameObject.Find("Player").gameObject;
    }
    private void Update()
    {
        if(GameManager.CitizonGrade < citizonGrade)
        {
            isChage = true;
        }
        else if(GameManager.CitizonGrade >= citizonGrade)
        {
            if (Inventory.Instance.inven.Count != 0)
            {
                isChage=false;
            }
        }
        Pcheck();
        Talk(this.npctype);
    }
    public void FieldSceneLoad(bool value)
    {
        if (isPlayerClose)
        {
            if (isChage)
            {
                if (value)
                {
                    SceneManager.LoadScene("FieldScene");
                    GameManager.instance.sound = 1;
                    Player.transform.position = Vector3.zero;
                    GameManager.instance.isField = true;
                }
            }
        }
    }

}
