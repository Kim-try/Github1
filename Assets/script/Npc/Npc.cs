using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    public NPCType npctype;
    public GameObject Player;
    public bool isPlayerClose = false;

    public enum NPCType
    {
        ShopNPC, TownScene, AdministrationNPC, PristNPC, FieldNPC, None
    }
    protected void Talk(NPCType type)
    {
        if (isPlayerClose)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GameObject.Find("Canvas").transform.GetChild(2).gameObject.SetActive(true);
                NPCPanel.instance.NPCcheck(type);
            }
        }
    }
    protected void Pcheck()
    {
        if ((Player.transform.position - transform.position).magnitude >= 1.8)
        {
            isPlayerClose = false;
        }
        else
            isPlayerClose = true;
        if (!isPlayerClose)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else if (isPlayerClose)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
